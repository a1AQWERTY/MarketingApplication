using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;

using Trading.Interface.Interface;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Response.Item;
using Trading.Infrastructure.Pagination;

namespace TradingAPI.HandleRequest.Handler.Item
{
    public class GetItemInventoryListHandler: IRequestHandler<RequestGetItemInventoryList, ResponseListClass<List<ResponseGetItemInventoryList>>>
    {
        private readonly IItemInventoryRepository _itemInventoryRepository;
        
        private readonly IMapper _mapper;

        public GetItemInventoryListHandler(IItemInventoryRepository itemInventoryRepository,  IMapper mapper)
        {
            _itemInventoryRepository = itemInventoryRepository ?? throw new ArgumentNullException(nameof(itemInventoryRepository));
           
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseListClass<List<ResponseGetItemInventoryList>>> Handle(RequestGetItemInventoryList request, CancellationToken cancellationToken)
        {
            var itemInventoryList = await _itemInventoryRepository.GetItemInventoryList();
            int Count = 0;

            var itemInventoryData = itemInventoryList.GetPaged(request.requestFilter.PageNo, request.requestFilter.PageSize, out Count);

            List<ResponseGetItemInventoryList> responseItemInventoryList = new List<ResponseGetItemInventoryList>();

            if (itemInventoryData.Results?.Any() == true)
            {
                responseItemInventoryList = _mapper.Map<List<ItemInventoryList>, List<ResponseGetItemInventoryList>>(itemInventoryData.Results.ToList());
            }

            return new ResponseListClass<List<ResponseGetItemInventoryList>>()
            {
                count = Count,
                Data = responseItemInventoryList
            };
        }
    }
}
