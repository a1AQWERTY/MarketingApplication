using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Handler.Item
{
    public class GerItemInventoryDetailHandler : IRequestHandler<RequestGetItemInventoryDetail, ResponseGetItemInventoryDetail>
    {
        private readonly IItemInventoryRepository _itemInventoryRepository;
        private readonly IMapper _mapper;

        public GerItemInventoryDetailHandler(IItemInventoryRepository itemInventoryRepository, IMapper mapper)
        {
            _itemInventoryRepository = itemInventoryRepository ?? throw new ArgumentNullException(nameof(itemInventoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseGetItemInventoryDetail> Handle(RequestGetItemInventoryDetail request, CancellationToken cancellationToken)
        {
            ResponseGetItemInventoryDetail responseGetItemInventoryDetail = null;

            ItemInventoryList itemInventoryDetail = _itemInventoryRepository.GetItemInventoryDetail(request.ItemInventoryId).Result;

            if (itemInventoryDetail != null)
            {
                responseGetItemInventoryDetail = _mapper.Map<ItemInventoryList, ResponseGetItemInventoryDetail>(itemInventoryDetail);
            }

            return responseGetItemInventoryDetail;
        }
    }
}
