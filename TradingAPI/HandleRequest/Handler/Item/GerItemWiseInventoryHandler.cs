using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Interface.Interface;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Response.Item;
using System.Linq;
namespace TradingAPI.HandleRequest.Handler.Item
{
    public class GerItemWiseInventoryHandler : IRequestHandler<RequestGetItemWiseInventoryList, ResponseListClass<List<ResponseGetItemWiseInventoryList>>>
    {
        private readonly IItemInventoryRepository _itemInventoryRepository;
        private readonly IMapper _mapper;

        public GerItemWiseInventoryHandler(IItemInventoryRepository itemInventoryRepository, IMapper mapper)
        {
            _itemInventoryRepository = itemInventoryRepository ?? throw new ArgumentNullException(nameof(itemInventoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseListClass<List<ResponseGetItemWiseInventoryList>>> Handle(RequestGetItemWiseInventoryList request, CancellationToken cancellationToken)
        {
            var itemInventoryList = await _itemInventoryRepository.GetItemWiseInventoryList(request.ItemMasterId);
            int Count = 0;

            List<ResponseGetItemWiseInventoryList> responseItemInventoryList = new List<ResponseGetItemWiseInventoryList>();
            
            if (itemInventoryList?.Any() == true)
            {
                responseItemInventoryList = _mapper.Map<List<ItemInventoryList>, List<ResponseGetItemWiseInventoryList>>(itemInventoryList);
            }

            return new ResponseListClass<List<ResponseGetItemWiseInventoryList>>()
            {
                count = Count,
                Data = responseItemInventoryList
            };
        }
    }
}
