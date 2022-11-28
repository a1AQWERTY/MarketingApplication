using MediatR;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Common;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Request.UploadMaster;

namespace TradingAPI.HandleRequest.Handler.UploadMaster
{
    public class UploadMasterDataHandler : IRequestHandler<RequestUploadMaster, bool>
    {
        readonly IWebHostEnvironment _env;
        readonly IItemMasterRepository _itemMasterRepository;
        readonly IUnitMasterRepository _unitMasterRepository;
        readonly IMediator _mediator;
        RequestUploadMaster requestUploadMaster;

        public UploadMasterDataHandler(IWebHostEnvironment env, IItemMasterRepository itemMasterRepository, IUnitMasterRepository unitMasterRepository, IMediator mediator)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _itemMasterRepository = itemMasterRepository ?? throw new ArgumentNullException(nameof(itemMasterRepository));
            _unitMasterRepository = unitMasterRepository ?? throw new ArgumentNullException(nameof(unitMasterRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(RequestUploadMaster request, CancellationToken cancellationToken)
        {
            requestUploadMaster = request;
            string fileName = $"{_env.ContentRootPath}\\Uploads\\{request.file.FileName}";

            using (FileStream fs = System.IO.File.Create(fileName))
            {
                request.file.CopyTo(fs);
                fs.Flush();
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                var DataSheet = package.Workbook.Worksheets[0];
                int count = DataSheet.Dimension.End.Row;
                if (Convert.ToInt32(request.MasterType) == (int)MastetTypeEnum.ItemMaster)
                {
                    await UploadItemMaster(DataSheet);
                }
            }

            return true;
        }

        public async Task UploadItemMaster(ExcelWorksheet excelWorksheet)
        {
            int startcount = excelWorksheet.Dimension.Start.Row;
            int Endtcount = excelWorksheet.Dimension.End.Row;

            int startColcount = excelWorksheet.Dimension.Start.Column;
            int EndtColcount = excelWorksheet.Dimension.End.Column;

            if (EndtColcount < 4)
            {
                throw new Exception("Please upload valid File");
            }
            else if (excelWorksheet.Cells[startColcount, 1].Text.ToLower() != "itemcode")
            {
                throw new Exception("Please upload valid File");

            }

            for (int i = startcount + 1; i <= Endtcount; i++)
            {
                string ItemCode = excelWorksheet.Cells[i, 1].Text;
                string ItemName = excelWorksheet.Cells[i, 2].Text;
                string ItemDescr = excelWorksheet.Cells[i, 3].Text;
                string UnitName = excelWorksheet.Cells[i, 4].Text;

                if (!string.IsNullOrWhiteSpace(ItemCode) && !string.IsNullOrWhiteSpace(ItemName)
                    && !string.IsNullOrWhiteSpace(UnitName))
                {
                    await ManageItemAdd(new ItemManage()
                    {
                        ItemCode = ItemCode,
                        ItemDescr = ItemDescr,
                        ItemName = ItemName,
                        UnitName = UnitName
                    });
                }
            }


        }

        public async Task ManageItemAdd(ItemManage requestItemAdd)
        {
            //First check for item code exists
            var ItemData = _itemMasterRepository.GetByWhere(x => x.ItemCode.ToLower() == requestItemAdd.ItemCode.ToLower() && !x.IsDeleted)?.FirstOrDefault();
            if (ItemData == null)
            {
                //Check for Unit Exists or not
                UnitMaster unitData = await ManageUnitAdd(requestItemAdd.UnitName);
                if (unitData != null)
                {
                    RequestItemAdd itemMasterAdd = new RequestItemAdd()
                    {
                        ItemCode = requestItemAdd.ItemCode,
                        ItemDescription = requestItemAdd.ItemDescr,
                        ItemName = requestItemAdd.ItemName,
                        UnitMasterId = unitData.UnitMasterId,
                        requestUserId = requestUploadMaster.requestUserId,
                        requestUserEmail = requestUploadMaster.requestUserEmail
                    };

                    await _mediator.Send(itemMasterAdd);

                }
            }
        }

        public async Task<UnitMaster> ManageUnitAdd(string UnitName)
        {
            UnitMaster unitMaster = null;

            unitMaster = _unitMasterRepository.GetByWhere(x => x.UnitCode.ToLower() == UnitName.ToLower() && !x.IsDeleted)?.FirstOrDefault();
            if (unitMaster == null)
            {
                unitMaster = new UnitMaster();

                unitMaster.UnitCode = UnitName;
                unitMaster.UnitName = UnitName;
                unitMaster.CreatedBy = requestUploadMaster.requestUserId;
                unitMaster.CreatedDate = DateTime.Now;
                await _unitMasterRepository.AddAsync(unitMaster);
                await _unitMasterRepository.SaveChangesAsync();
            }

            return unitMaster;
        }

        public class ItemManage
        {
            public string ItemCode { get; set; }

            public string ItemName { get; set; }

            public string ItemDescr { get; set; }

            public string UnitName { get; set; }
        }
    }
}
