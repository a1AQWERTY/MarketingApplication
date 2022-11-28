using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using TradingAPI.Common;
using TradingAPI.HandleRequest.Request.Unit;
using TradingAPI.HandleRequest.Response.Unit;

namespace TradingAPI.HandleRequest.Handler.Unit
{
    public class GetUnitMasterListHandler : IRequestHandler<RequestGetUnitMaster, ResponseListClass<List<ResponseGetUnitMaster>>>
    {
        readonly IUnitMasterRepository _unitMasterRepository;
        readonly IMapper _mapper;
        public GetUnitMasterListHandler(IUnitMasterRepository unitMasterRepository, IMapper mapper)
        {
            _unitMasterRepository = unitMasterRepository;
            _mapper = mapper;
        }
        public async Task<ResponseListClass<List<ResponseGetUnitMaster>>> Handle(RequestGetUnitMaster request, CancellationToken cancellationToken)
        {
            List<ResponseGetUnitMaster> lstUnitMaster = new List<ResponseGetUnitMaster>();

            //Get Unit Master Data
            var UnitMasterData = _unitMasterRepository.GetByWhere(x => !x.IsDeleted);
            if (UnitMasterData?.Any() == true)
            {
                lstUnitMaster = _mapper.Map<List<UnitMaster>, List<ResponseGetUnitMaster>>(UnitMasterData.ToList());
            }

            return new ResponseListClass<List<ResponseGetUnitMaster>>() {
                count = lstUnitMaster.Count(),
                Data = lstUnitMaster
            };
        }
    }
}
