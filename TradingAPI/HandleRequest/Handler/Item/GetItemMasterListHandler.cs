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
    public class GetItemMasterListHandler : IRequestHandler<RequestGetItemMasterList, ResponseListClass<List<ResponseGetItemMasterList>>>
    {
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly IUnitMasterRepository _unitMasterRepository;
        private readonly IMapper _mapper;
        public GetItemMasterListHandler(IItemMasterRepository itemMasterRepository, IMapper mapper, IUnitMasterRepository unitMasterRepository)
        {
            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _mapper = mapper;
        }
        public async Task<ResponseListClass<List<ResponseGetItemMasterList>>> Handle(RequestGetItemMasterList request, CancellationToken cancellationToken)
        {
            var itemMasterDataList = await _itemMasterRepository.GetItemDetailList();
            int Count = 0;

            var itemData = itemMasterDataList.GetPaged(request.requestFilter.PageNo, request.requestFilter.PageSize, out Count);

            List<ResponseGetItemMasterList> responseGetItemMasterLists = new List<ResponseGetItemMasterList>();

            if (itemData.Results?.Any() == true)
            {
                responseGetItemMasterLists = _mapper.Map<List<ItemDetailList>, List<ResponseGetItemMasterList>>(itemData.Results.ToList());
            }

            return new ResponseListClass<List<ResponseGetItemMasterList>>()
            {
                count = Count,
                Data = responseGetItemMasterLists
            };
        }


    }
}
