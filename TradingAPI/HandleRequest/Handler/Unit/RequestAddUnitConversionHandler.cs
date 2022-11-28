using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.Unit;

namespace TradingAPI.HandleRequest.Handler.Unit
{
    public class RequestAddUnitConversionHandler : IRequestHandler<RequestAddUnitConversion, bool>
    {
        readonly IUnitConversionRepository _unitConversionRepository;
        readonly IMapper _mapper;

        public RequestAddUnitConversionHandler(IUnitConversionRepository unitConversionRepository, IMapper mapper)
        {
            _unitConversionRepository = unitConversionRepository ?? throw new ArgumentNullException(nameof(unitConversionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(RequestAddUnitConversion request, CancellationToken cancellationToken)
        {
            try
            {
                UnitConversion unitConversionAdd = new UnitConversion()
                {
                    FromUnitMasterId = request.FromUnitMasterId,
                    ToUnitMasterId = request.ToUnitMasterId,
                    ConversionValue = request.ConversionValue,
                    CreatedBy = request.requestUserId,
                    CreatedDate = DateTime.Now
                };

                await _unitConversionRepository.AddAsync(unitConversionAdd);
                await _unitConversionRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;

        }
    }
}
