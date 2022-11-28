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
    public class UpdateItemBoMHandler : IRequestHandler<RequestUpdateItemBOM, bool>
    {
        private readonly IItemBoMMasterRepository _itemBoMMasterRepository;
        private readonly IItemBoMChildRepository _itemBoMChildRepository;
        private RequestUpdateItemBOM requestUpdateItemBOM;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateItemBoMHandler(IItemBoMMasterRepository itemBoMMasterRepository, IItemBoMChildRepository itemBoMChildRepository,
           IUnitOfWork unitOfWork)
        {
            _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
            _itemBoMChildRepository = itemBoMChildRepository ?? throw new ArgumentNullException(nameof(itemBoMChildRepository));

            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }



        public async Task<bool> Handle(RequestUpdateItemBOM request, CancellationToken cancellationToken)
        {
            requestUpdateItemBOM = request;

            try
            {


                var ItemBoMOperations = new List<Task> { UpdateMasterBoM(), UpdateChildBoM(), AddNewItemChildItems() };
                #region commented code
                ////here we don't need to execute sequential operations..
                //while (ItemBoMOperations.Count > 0)
                //{
                //    var FinishedTask = await Task.WhenAny(ItemBoMOperations);

                //    //add condition of finished task if any other operation required after that completes..like this if(FinishedTask == UpdateMasterBoM())
                //    ItemBoMOperations.Remove(FinishedTask);
                //} 
                #endregion

                //wait for all task to complete and then call unity of work
                await Task.WhenAll(ItemBoMOperations);

                //Commit the transactions
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                //return false;
                throw ex;
            }
            return true;
        }

        public async Task UpdateMasterBoM()
        {
            var ItemBoMMasterUpdate = _itemBoMMasterRepository.GetByWhere(x => x.ItemBoMMasterId == requestUpdateItemBOM.ItemBomMasterId && !x.IsDeleted)?.FirstOrDefault();
            if (ItemBoMMasterUpdate != null)
            {
                ItemBoMMasterUpdate.ItemMasterId = requestUpdateItemBOM.ItemMasterId;
                ItemBoMMasterUpdate.Quantity = requestUpdateItemBOM.Quantity;
                ItemBoMMasterUpdate.UnitMasterId = requestUpdateItemBOM.UnitMasterId;
                ItemBoMMasterUpdate.ModifiedBy = requestUpdateItemBOM.requestUserId;
                ItemBoMMasterUpdate.ModifiedDate = DateTime.Now;

                await _unitOfWork.Repository<ItemBoMMaster>().UpdateAsync(ItemBoMMasterUpdate);
            }

        }

        public async Task UpdateChildBoM()
        {
            foreach (var itemBomChild in requestUpdateItemBOM.itemBoMChild.Where(x => x.ItemBomChildId.ToString() != ""))
            {
                var ItemBoMChildUpdate = _itemBoMChildRepository.GetByWhere(x => x.ItemBoMChildId == itemBomChild.ItemBomChildId && !x.IsDeleted)?.FirstOrDefault();
                if (ItemBoMChildUpdate != null)
                {
                    ItemBoMChildUpdate.Quantity = itemBomChild.Quantity;
                    ItemBoMChildUpdate.UnitMasterId = itemBomChild.UnitMasterId;
                    ItemBoMChildUpdate.ModifiedBy = requestUpdateItemBOM.requestUserId;
                    ItemBoMChildUpdate.ModifiedDate = DateTime.Now;
                    ItemBoMChildUpdate.ItemMasterId = itemBomChild.ItemMasterId;
                    await _unitOfWork.Repository<ItemBoMChild>().UpdateAsync(ItemBoMChildUpdate);
                }
            }
        }

        public async Task AddNewItemChildItems()
        {
            foreach (var itemBomChild in requestUpdateItemBOM.itemBoMChild.Where(x => x.ItemBomChildId.ToString() == ""))
            {
                ItemBoMChild ItemBoMChildUpdate = new ItemBoMChild();

                ItemBoMChildUpdate.ItemMasterId = itemBomChild.ItemMasterId;
                ItemBoMChildUpdate.Quantity = itemBomChild.Quantity;
                ItemBoMChildUpdate.UnitMasterId = itemBomChild.UnitMasterId;
                ItemBoMChildUpdate.CreatedBy = requestUpdateItemBOM.requestUserId;
                ItemBoMChildUpdate.CreatedDate = DateTime.Now;
                ItemBoMChildUpdate.ItemBoMMasterId = requestUpdateItemBOM.ItemBomMasterId;
                await _unitOfWork.Repository<ItemBoMChild>().AddAsync(ItemBoMChildUpdate);

            }
        }
    }
}
