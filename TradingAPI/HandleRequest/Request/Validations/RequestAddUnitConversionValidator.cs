using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Trading.Infrastructure.Repository;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.Unit;

namespace TradingAPI.HandleRequest.Request.Validations
{

    public class RequestAddUnitConversionValidator : AbstractValidator<RequestAddUnitConversion>
    {
        readonly IUnitMasterRepository _unitMasterRepository;

        public RequestAddUnitConversionValidator(IUnitMasterRepository unitMasterRepository)
        {
            _unitMasterRepository = unitMasterRepository ?? throw new ArgumentNullException(nameof(unitMasterRepository));

            RuleFor(x => x.FromUnitMasterId).NotNull().NotEmpty();
            RuleFor(x => x.ToUnitMasterId).NotNull().NotEmpty();
            RuleFor(x => x.ToUnitMasterId).NotEqual(x => x.FromUnitMasterId).WithMessage("From unit and to unit must not be same");
            RuleFor(x => x.ConversionValue).NotEqual(0).WithMessage("Conversion value must be greater than 0");

            RuleFor(x => x.FromUnitMasterId).MustAsync(isValidUnit).WithMessage("Please enter valid from unit");
            RuleFor(x => x.ToUnitMasterId).MustAsync(isValidUnit).WithMessage("Please enter valid to unit");
        }

        public async Task<bool> isValidUnit(Guid UnitId, CancellationToken token)
        {
            return !(_unitMasterRepository.GetByWhere(x => x.UnitMasterId == UnitId && !x.IsDeleted)?.Any() == false);
        }
    }
}
