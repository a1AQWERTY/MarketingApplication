using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;

namespace TradingAPI.HandleRequest.Handler.Item
{
    public class DeleteItemBoMHandler : IRequestHandler<RequestDeleteItemBoM, bool>
    {
        private readonly IItemBoMMasterRepository _itemBoMMasterRepository;
        private readonly IItemBoMChildRepository _itemBoMChildRepository;
        private RequestDeleteItemBoM requestDeleteItemBoM;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteItemBoMHandler(IItemBoMMasterRepository itemBoMMasterRepository, IItemBoMChildRepository itemBoMChildRepository,
           IUnitOfWork unitOfWork)
        {
            _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
            _itemBoMChildRepository = itemBoMChildRepository ?? throw new ArgumentNullException(nameof(itemBoMChildRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }



        public async Task<bool> Handle(RequestDeleteItemBoM request, CancellationToken cancellationToken)
        {
            requestDeleteItemBoM = request;

            if (request.IsParent)
            {
                await DeleteItemBoMMaster();
            }
            else
            {
                await DeleteItemBoMChild();
            }

            await _unitOfWork.Commit();

            return true;
        }

        public async Task DeleteItemBoMMaster()
        {
            var BoMMasterData = _itemBoMMasterRepository.GetByWhere(x => x.ItemBoMMasterId == requestDeleteItemBoM.ItemBomDeleteId && !x.IsDeleted)?.FirstOrDefault();
            if (BoMMasterData != null)
            {
                BoMMasterData.IsDeleted = true;
                BoMMasterData.ModifiedDate = DateTime.Now;
                BoMMasterData.ModifiedBy = requestDeleteItemBoM.requestUserId;

                await _unitOfWork.Repository<ItemBoMMaster>().UpdateAsync(BoMMasterData);
            }

            //Delete all child item 
            await DeleteItemBoMChild();

        }

        public async Task DeleteItemBoMChild()
        {
            var BoMChildData = _itemBoMChildRepository.GetByWhere(x => x.ItemBoMChildId == requestDeleteItemBoM.ItemBomDeleteId && !x.IsDeleted)?.FirstOrDefault();
            if (BoMChildData != null)
            {
                BoMChildData.IsDeleted = true;
                BoMChildData.ModifiedDate = DateTime.Now;
                BoMChildData.ModifiedBy = requestDeleteItemBoM.requestUserId;

                await _unitOfWork.Repository<ItemBoMChild>().UpdateAsync(BoMChildData);
            }
        }
    }
}
