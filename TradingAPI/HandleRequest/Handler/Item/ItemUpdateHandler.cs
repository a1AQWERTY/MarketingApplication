using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Entities;
using Trading.Exception;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Response;

namespace TradingAPI.HandleRequest.Handler.Item
{
    public class ItemUpdateHandler : IRequestHandler<RequestItemUpdate, bool>
    {
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly IUnitMasterRepository _unitMasterRepository;
       
        public ItemUpdateHandler(IItemMasterRepository itemMasterRepository,  IUnitMasterRepository unitMasterRepository)
        {
            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
          
        }
        public async Task<bool> Handle(RequestItemUpdate request, CancellationToken cancellationToken)
        {
            await BasicValidation(request);

            //Call method to add Item
            _ = await UpdateItem(request);

            return true;
        }

        /// <summary>
        /// Check basic validations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task BasicValidation(RequestItemUpdate request)
        {

            //Check data exists or not for item master id
            if (_itemMasterRepository.GetByWhere(x => x.ItemMasterId == request.ItemMasterId)?.Any() == false)
            {
                CommonException.GeneralException("Item data not found");
            }
            //Check for unit Id is valid or not
            if (_unitMasterRepository.GetByWhere(x => x.UnitMasterId == request.UnitMasterId && !x.IsDeleted)?.Any() == false)
            {
                CommonException.NoDataFound(nameof(request.UnitMasterId));
            }

            //Check wether same item code is exists in DB or not
            if (_itemMasterRepository.GetByWhere(x =>x.ItemMasterId != request.ItemMasterId && x.ItemCode.ToLower() == request.ItemCode.ToLower() && !x.IsDeleted)?.Any() == true)
            {
                CommonException.GeneralException("Item Code already exists");
            }
        }
        public async Task<ItemMaster> UpdateItem(RequestItemUpdate request)
        {
            //Update 
            ItemMaster itemMasterToUpdate = _itemMasterRepository.GetByWhere(x => x.ItemMasterId == request.ItemMasterId)?.FirstOrDefault();

            try
            {
                if (itemMasterToUpdate != null)
                {
                    itemMasterToUpdate.ItemName = request.ItemName;
                    itemMasterToUpdate.ItemCode = request.ItemCode;
                    itemMasterToUpdate.UnitMasterId = request.UnitMasterId;
                    itemMasterToUpdate.ItemDescription = request.ItemDescription;
                    itemMasterToUpdate.ModifiedBy = request.requestUserId;
                    itemMasterToUpdate.ModifiedDate = DateTime.Now;

                    await _itemMasterRepository.UpdateAsync(itemMasterToUpdate);
                    await _itemMasterRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
               
            }
            
            

            return itemMasterToUpdate;
        }
    }
}
