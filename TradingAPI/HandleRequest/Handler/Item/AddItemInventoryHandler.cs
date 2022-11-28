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

namespace TradingAPI.HandleRequest.Handler.Item
{
    public class AddItemInventoryHandler : IRequestHandler<RequestAddItemInventory, bool>
    {
        readonly IItemMasterRepository _itemMasterRepository;
        readonly IUnitMasterRepository _unitMasterRepository;
        readonly IItemInventoryRepository _itemInventoryRepository;
        public RequestAddItemInventory requestItemInventory;
        public AddItemInventoryHandler(IItemMasterRepository itemMasterRepository, IUnitMasterRepository unitMasterRepository, IItemInventoryRepository itemInventoryRepository)
        {
            _itemMasterRepository = itemMasterRepository ?? throw new ArgumentNullException(nameof(itemMasterRepository));
            _unitMasterRepository = unitMasterRepository ?? throw new ArgumentNullException(nameof(unitMasterRepository));
            _itemInventoryRepository = itemInventoryRepository ?? throw new ArgumentNullException(nameof(itemInventoryRepository));
        }

        /// <summary>
        /// Handles Item Inventory Add
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(RequestAddItemInventory request, CancellationToken cancellationToken)
        {
            requestItemInventory = request;

            await BasicValidation();

            _ = await AddItemInventory();
            
            return true;
        }

        /// <summary>
        /// basic validations
        /// </summary>
        /// <returns></returns>
        public async Task BasicValidation()
        {
            if (_itemMasterRepository.GetByWhere(x => x.ItemMasterId == requestItemInventory.ItemMasterId && !x.IsDeleted)?.Any() == false)
            {
                CommonException.NoDataFound(nameof(requestItemInventory.ItemMasterId));
            }

            if (_unitMasterRepository.GetByWhere(x => x.UnitMasterId == requestItemInventory.UnitMasterId && !x.IsDeleted)?.Any() == false)
            {
                CommonException.NoDataFound(nameof(requestItemInventory.UnitMasterId));
            }
        }

        public async Task<ItemInventory> AddItemInventory()
        {
            try
            {
                ItemInventory itemInventoryAdd = new ItemInventory()
                {
                    ItemMasterId = requestItemInventory.ItemMasterId,
                    UnitMasterId = requestItemInventory.UnitMasterId,
                    Quantity = requestItemInventory.Quantity,
                    Rate = requestItemInventory.Rate,
                    CreatedBy = requestItemInventory.requestUserId,
                    CreatedDate = DateTime.Now,
                    BatchNo = requestItemInventory.BatchNo
                };

                await _itemInventoryRepository.AddAsync(itemInventoryAdd);
                await _itemInventoryRepository.SaveChangesAsync();

                return itemInventoryAdd;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
