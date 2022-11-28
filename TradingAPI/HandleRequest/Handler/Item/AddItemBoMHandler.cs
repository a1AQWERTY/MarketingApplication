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
    public class AddItemBoMHandler : IRequestHandler<RequestAddItemBOM, Guid>
    {
        private readonly IItemBoMMasterRepository _itemBoMMasterRepository;
        private readonly IItemBoMChildRepository _itemBoMChildRepository;
        private RequestAddItemBOM requestAddItemBOM;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemBoMHandler(IItemBoMMasterRepository itemBoMMasterRepository, IItemBoMChildRepository itemBoMChildRepository,
           IUnitOfWork unitOfWork)
        {
            _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
            _itemBoMChildRepository = itemBoMChildRepository ?? throw new ArgumentNullException(nameof(itemBoMChildRepository));

            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }



        public async Task<Guid> Handle(RequestAddItemBOM request, CancellationToken cancellationToken)
        {
            requestAddItemBOM = request;


            //Save Item BoM Master
            Guid ItemBoMMasterId = await SaveItemBoMMaster();

            foreach (RequestItemBoMChild itemBoMChild in requestAddItemBOM.itemBoMChild)
            {
                SaveItemBoMChild(itemBoMChild, ItemBoMMasterId);
            }

            await _unitOfWork.Commit();

            return ItemBoMMasterId;
        }

        public async Task<Guid> SaveItemBoMMaster()
        {
            try
            {
                ItemBoMMaster itemBoMMasterAdd = new ItemBoMMaster()
                {
                    ItemMasterId = requestAddItemBOM.ItemMasterId,
                    Quantity = requestAddItemBOM.Quantity,
                    UnitMasterId = requestAddItemBOM.UnitMasterId,
                    CreatedBy = requestAddItemBOM.requestUserId,
                    CreatedDate = DateTime.Now

                };

                await _unitOfWork.Repository<ItemBoMMaster>().AddAsync(itemBoMMasterAdd);

                return itemBoMMasterAdd.ItemBoMMasterId;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task SaveItemBoMChild(RequestItemBoMChild requestItemBoMChild, Guid ItemBoMMasterId)
        {
            try
            {
                ItemBoMChild itemBoMChildAdd = new ItemBoMChild()
                {
                    ItemMasterId = requestItemBoMChild.ItemMasterId,
                    Quantity = requestItemBoMChild.Quantity,
                    UnitMasterId = requestItemBoMChild.UnitMasterId,
                    CreatedBy = requestAddItemBOM.requestUserId,
                    CreatedDate = DateTime.Now,
                    ItemBoMMasterId = ItemBoMMasterId
                };

              await _unitOfWork.Repository<ItemBoMChild>().AddAsync(itemBoMChildAdd);
                // _unitOfWork.GetRepository<ItemBoMChild>().Add(itemBoMChildAdd);


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

    }
}
