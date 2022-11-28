using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;

namespace TradingAPI.HandleRequest.Request.Validations
{
    public class RequestUpdateItemBoMValidator : AbstractValidator<RequestUpdateItemBOM>
    {
        readonly IItemBoMMasterRepository _itemBoMMasterRepository;
        readonly IItemMasterRepository _itemMasterRepository;
        readonly IUnitMasterRepository _unitMasterRepository;
        public RequestUpdateItemBoMValidator(IItemBoMMasterRepository itemBoMMasterRepository, IItemMasterRepository itemMasterRepository,
            IUnitMasterRepository unitMasterRepository)
        {
            _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
            _itemMasterRepository = itemMasterRepository ?? throw new ArgumentNullException(nameof(itemMasterRepository));
            _unitMasterRepository = unitMasterRepository ?? throw new ArgumentNullException(nameof(unitMasterRepository));


            RuleFor(x => x.ItemMasterId).NotEmpty().NotNull();
            RuleFor(x => x.UnitMasterId).NotEmpty().NotNull();
            RuleFor(x => x.Quantity).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.itemBoMChild).NotNull().NotEmpty().WithMessage("Please add atleast one child item");
            RuleFor(x => x.ItemMasterId).MustAsync(IsValidItem).WithMessage("Please select valid item");
            RuleFor(x => x.UnitMasterId).MustAsync(IsValidUnit).WithMessage("Please select valid unit");

            RuleForEach(x => x.itemBoMChild).SetValidator(new ItemBoMUpdateChildValidator(_itemBoMMasterRepository, _itemMasterRepository,_unitMasterRepository));
        }

        public async Task<bool> IsValidItem(Guid ItemMasterId, CancellationToken token)
        {
            return !(await _itemMasterRepository.IsValidItem(ItemMasterId) == false);
        }

        public async Task<bool> IsValidUnit(Guid UnitMasterId, CancellationToken token)
        {
            return !(await _unitMasterRepository.IsValidUnit(UnitMasterId) == false);
        }

        public class ItemBoMUpdateChildValidator : AbstractValidator<RequestUpdateItemBoMChild>
        {
            readonly IItemBoMMasterRepository _itemBoMMasterRepository;
            readonly IItemMasterRepository _itemMasterRepository;
            readonly IUnitMasterRepository _unitMasterRepository;

            public ItemBoMUpdateChildValidator(IItemBoMMasterRepository itemBoMMasterRepository, IItemMasterRepository itemMasterRepository ,
                IUnitMasterRepository unitMasterRepository)
            {
                _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
                _itemMasterRepository = itemMasterRepository ?? throw new ArgumentNullException(nameof(itemMasterRepository));
                _unitMasterRepository = unitMasterRepository ?? throw new ArgumentNullException(nameof(unitMasterRepository));

                

                RuleFor(x => x.ItemMasterId).NotNull().NotEmpty();
                RuleFor(x => x.Quantity).NotNull().NotEmpty().GreaterThan(0);
                RuleFor(x => x.UnitMasterId).NotNull().NotEmpty();
                RuleFor(x => x.UnitMasterId).MustAsync(IsValidUnit).WithMessage("Please select valid unit");
                RuleFor(x => x.ItemMasterId).MustAsync(IsValidItem).WithMessage("Please select valid item");
            }

            public async Task<bool> IsValidItem(Guid ItemMasterId, CancellationToken token)
            {
                return !(await _itemMasterRepository.IsValidItem(ItemMasterId) == false);
            }

            public async Task<bool> IsValidUnit(Guid UnitMasterId, CancellationToken token)
            {
                return !(await _unitMasterRepository.IsValidUnit(UnitMasterId) == false);
            }

        }


    }
}
