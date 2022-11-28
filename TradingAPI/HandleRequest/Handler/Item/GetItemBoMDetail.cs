using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Handler.Item
{
    public class GetItemBoMDetail : IRequestHandler<RequestGetItemBoMDetail, ResponseGetItemBoMDetail>
    {
        private readonly IItemBoMMasterRepository _itemBoMMasterRepository;
        private readonly IMapper _mapper;


        public GetItemBoMDetail(IItemBoMMasterRepository itemBoMMasterRepository, IMapper mapper)
        {
            _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<ResponseGetItemBoMDetail> Handle(RequestGetItemBoMDetail request, CancellationToken cancellationToken)
        {
            ResponseGetItemBoMDetail responseGetItemBoMDetail;


            ItemBoMDetail itemBoMDetail = await _itemBoMMasterRepository.GetItemBoMDetail(request.ItemBoMMasterId);

            if (itemBoMDetail != null)
            {
                responseGetItemBoMDetail = _mapper.Map<ItemBoMDetail, ResponseGetItemBoMDetail>(itemBoMDetail);
                return responseGetItemBoMDetail;

            }
            return null;
        }
    }
}
