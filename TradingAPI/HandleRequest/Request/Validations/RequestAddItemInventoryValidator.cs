using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;

namespace TradingAPI.HandleRequest.Request.Validations
{
    public class RequestAddItemInventoryValidator : AbstractValidator<RequestAddItemInventory>
    {
        readonly IItemMasterRepository _itemMasterRepository;

        readonly IUnitMasterRepository _unitMasterRepository;

        public RequestAddItemInventoryValidator(IItemMasterRepository itemMasterRepository, IUnitMasterRepository unitMasterRepository)
        {
            _itemMasterRepository = itemMasterRepository;

            _unitMasterRepository = unitMasterRepository;


            RuleFor(x => x.ItemMasterId).NotNull().NotEmpty();
            RuleFor(x => x.UnitMasterId).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater then 0");
            //RuleFor(x => x.Rate).NotNull().NotEmpty();

            RuleFor(x => x.ItemMasterId).MustAsync(IsItemExists).WithMessage("Please enter valid Item");

            RuleFor(x => x.UnitMasterId).MustAsync(IsUnitExists).WithMessage("Please enter valid Unit");

        }

        public async Task<bool> IsItemExists(Guid ItemMasterId,CancellationToken cancellation)
        {
            return !(_itemMasterRepository.GetByWhere(x => x.ItemMasterId == ItemMasterId && !x.IsDeleted)?.Any() == false);
        }

        public async Task<bool> IsUnitExists(Guid UnitMasterId, CancellationToken cancellation)
        {
            return !(_unitMasterRepository.GetByWhere(x => x.UnitMasterId == UnitMasterId && !x.IsDeleted)?.Any() == false);
        }

       
    }
}
