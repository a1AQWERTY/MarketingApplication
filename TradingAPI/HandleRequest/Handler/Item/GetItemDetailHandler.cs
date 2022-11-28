using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;
using TradingAPI.HandleRequest.Response.Item;

namespace TradingAPI.HandleRequest.Handler.Item
{
    public class GetItemDetailHandler : IRequestHandler<RequestGetItemDetail, ResponseGetItemDetail>
    {
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly IMapper _mapper;


        public GetItemDetailHandler(IItemMasterRepository itemMasterRepository, IMapper mapper)
        {
            _itemMasterRepository = itemMasterRepository ?? throw new ArgumentNullException(nameof(itemMasterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        public async Task<ResponseGetItemDetail> Handle(RequestGetItemDetail request, CancellationToken cancellationToken)
        {
            ResponseGetItemDetail responseGetItemDetail = new ResponseGetItemDetail();
            var ItemDetail = _itemMasterRepository.GetByWhere(x => x.ItemMasterId == request.ItemMasterId && !x.IsDeleted)?.FirstOrDefault();

            if (ItemDetail != null)
            {
                responseGetItemDetail = _mapper.Map<ItemMaster, ResponseGetItemDetail>(ItemDetail);
            }

            return responseGetItemDetail;
        }
    }
}
