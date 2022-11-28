using FluentValidation;
using TradingAPI.HandleRequest.Request.Company;

namespace TradingAPI.HandleRequest.Request.Validations
{
    public class CreateCompanyValidator : AbstractValidator<RequestAddUpdateCompany>
    {
        public CreateCompanyValidator()
        {

            RuleFor(m => m.Name).NotNull().NotEmpty();
        }
    }
}
