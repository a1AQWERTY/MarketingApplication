using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Entities;
using Trading.Exception;
using Trading.Infrastructure.Repository;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Response;

namespace TradingAPI.HandleRequest.Handler
{
    public class ItemAddHandler : IRequestHandler<RequestItemAdd, Guid>
    {
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly IUnitMasterRepository _unitMasterRepository;
        private readonly IMapper _mapper;
        public ItemAddHandler(IItemMasterRepository itemMasterRepository, IMapper mapper, IUnitMasterRepository unitMasterRepository)
        {
            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(RequestItemAdd request, CancellationToken cancellationToken)
        {
            //Call method to add Item
            ItemMaster itemMasterResult = await AddItem(request);

            return itemMasterResult.ItemMasterId;
        }

        /// <summary>
        /// Check basic validations
        /// </summary>
        /// <param name="requestItemAdd"></param>
        /// <returns></returns>
        public async Task BasicValidation(RequestItemAdd requestItemAdd)
        {
            //Check for unit Id is valid or not
            if (_unitMasterRepository.GetByWhere(x => x.UnitMasterId == requestItemAdd.UnitMasterId && !x.IsDeleted)?.Any() == false)
            {
                CommonException.NoDataFound(nameof(requestItemAdd.UnitMasterId));
            }

            //Check wether same item code is exists in DB or not
            if (_itemMasterRepository.GetByWhere(x => x.ItemCode.ToLower() == requestItemAdd.ItemCode.ToLower() && !x.IsDeleted)?.Any() == true)
            {
                CommonException.GeneralException("Item Code already exists");
            }
        }
        public async Task<ItemMaster> AddItem(RequestItemAdd request)
        {
            //Add item to item master table
            ItemMaster itemMasterToAdd = new ItemMaster()
            {
                ItemName = request.ItemName,
                CreatedBy = request.requestUserId,
                CreatedDate = DateTime.Now,
                ItemCode = request.ItemCode,
                ItemDescription = request.ItemDescription,
                UnitMasterId = request.UnitMasterId
            };

            await _itemMasterRepository.AddAsync(itemMasterToAdd);
            await _itemMasterRepository.SaveChangesAsync();

            return itemMasterToAdd;
        }
    }
}
