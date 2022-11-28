using FluentValidation;
using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;

namespace TradingAPI.HandleRequest.Request.Validations
{
    public class RequestAddItemBoMValidator : AbstractValidator<RequestAddItemBOM>
    {
        readonly IItemBoMMasterRepository _itemBoMMasterRepository;
        readonly IItemMasterRepository _itemMasterRepository;

        public RequestAddItemBoMValidator(IItemBoMMasterRepository itemBoMMasterRepository, IItemMasterRepository itemMasterRepository)
        {
            _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
            _itemMasterRepository = itemMasterRepository ?? throw new ArgumentNullException(nameof(itemMasterRepository));

            RuleFor(x => x.ItemMasterId).NotEmpty().NotNull();
            RuleFor(x => x.UnitMasterId).NotEmpty().NotNull();
            RuleFor(x => x.Quantity).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.itemBoMChild).NotNull().NotEmpty().WithMessage("Please add atleast one child item");
            RuleFor(x => x.ItemMasterId).MustAsync(IsValidItem).WithMessage("Please select valid item");
            RuleForEach(x => x.itemBoMChild).SetValidator(new ItemBoMChildValidator(_itemBoMMasterRepository, _itemMasterRepository));
        }

        public class ItemBoMChildValidator : AbstractValidator<RequestItemBoMChild>
        {
            readonly IItemBoMMasterRepository _itemBoMMasterRepository;
            readonly IItemMasterRepository _itemMasterRepository;

            public ItemBoMChildValidator(IItemBoMMasterRepository itemBoMMasterRepository, IItemMasterRepository itemMasterRepository)
            {
                _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
                _itemMasterRepository = itemMasterRepository ?? throw new ArgumentNullException(nameof(itemMasterRepository));

                RuleFor(x => x.ItemMasterId).NotNull().NotEmpty();
                RuleFor(x => x.Quantity).NotNull().NotEmpty().GreaterThan(0);
                RuleFor(x => x.UnitMasterId).NotNull().NotEmpty();
                RuleFor(x => x.ItemMasterId).MustAsync(IsValidItem).WithMessage("Please select valid item");
            }

            public async Task<bool> IsValidItem(Guid ItemMasterId, CancellationToken token)
            {
                return !(await _itemMasterRepository.IsValidItem(ItemMasterId) == false);

            }
        }

        public async Task<bool> IsValidItem(Guid ItemMasterId, CancellationToken token)
        {
            return !(await _itemMasterRepository.IsValidItem(ItemMasterId) == false);

        }
    }
}
