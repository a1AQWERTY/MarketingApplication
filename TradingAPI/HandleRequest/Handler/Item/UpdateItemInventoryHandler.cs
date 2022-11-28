using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;

namespace TradingAPI.HandleRequest.Handler.Item
{
    public class UpdateItemInventoryHandler : IRequestHandler<RequestUpdateItemInventory, bool>
    {
        private readonly IItemInventoryRepository _itemInventoryRepository;

        public UpdateItemInventoryHandler(IItemInventoryRepository itemInventoryRepository)
        {
            _itemInventoryRepository = itemInventoryRepository ?? throw new ArgumentNullException(nameof(itemInventoryRepository));
        }

       

        public async Task<bool> Handle(RequestUpdateItemInventory request, CancellationToken cancellationToken)
        {
            try
            {
                ItemInventory itemInventoryUpdate = _itemInventoryRepository.GetByWhere(x => x.ItemInventoryId == request.ItemInventoryId && !x.IsDeleted)?.FirstOrDefault();
                if (itemInventoryUpdate != null)
                {
                    itemInventoryUpdate.Quantity = request.Quantity;
                    itemInventoryUpdate.Rate = request.Rate;
                    itemInventoryUpdate.BatchNo = request.BatchNo;
                    itemInventoryUpdate.ModifiedBy = request.requestUserId;
                    itemInventoryUpdate.ModifiedDate = DateTime.UtcNow;

                    await _itemInventoryRepository.UpdateAsync(itemInventoryUpdate);
                    await _itemInventoryRepository.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
