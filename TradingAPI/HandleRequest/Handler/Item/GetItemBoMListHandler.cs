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
    public class GetItemBoMListHandler : IRequestHandler<RequestGetItemBomList, ResponseListClass<List<ResponseGetItemBomList>>>
    {
        private readonly IItemBoMMasterRepository _itemBoMMasterRepository;

        private readonly IMapper _mapper;

        public GetItemBoMListHandler(IItemBoMMasterRepository itemBoMMasterRepository, IMapper mapper)
        {
            _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseListClass<List<ResponseGetItemBomList>>> Handle(RequestGetItemBomList request, CancellationToken cancellationToken)
        {
            var itemBomList = await _itemBoMMasterRepository.GetItemBomList();
            int Count = 0;

            Func<int, int,int> test = (number , number1)=> number * number1;

            Console.WriteLine(test(5, 6));

            var itemBomData = itemBomList.GetPaged(request.requestFilter.PageNo, request.requestFilter.PageSize, out Count);

            List<ResponseGetItemBomList> responseItemInventoryList = new List<ResponseGetItemBomList>();

            if (itemBomData.Results?.Any() == true)
            {
                responseItemInventoryList = _mapper.Map<List<ItemBomList>, List<ResponseGetItemBomList>>(itemBomData.Results.ToList());
            }

            return new ResponseListClass<List<ResponseGetItemBomList>>()
            {
                count = Count,
                Data = responseItemInventoryList
            };
        }
    }
}
