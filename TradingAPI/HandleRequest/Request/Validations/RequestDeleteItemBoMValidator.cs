using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Interface.Interface;
using TradingAPI.HandleRequest.Request.ItemMaster;

namespace TradingAPI.HandleRequest.Request.Validations
{
    public class RequestDeleteItemBoMValidator : AbstractValidator<RequestDeleteItemBoM>
    {
        readonly IItemBoMMasterRepository _itemBoMMasterRepository;
        readonly IItemBoMChildRepository _itemBoMChildRepository;

        public RequestDeleteItemBoMValidator(IItemBoMMasterRepository itemBoMMasterRepository, IItemBoMChildRepository itemBoMChildRepository)
        {
            _itemBoMMasterRepository = itemBoMMasterRepository ?? throw new ArgumentNullException(nameof(itemBoMMasterRepository));
            _itemBoMChildRepository = itemBoMChildRepository ?? throw new ArgumentNullException(nameof(itemBoMChildRepository));

            RuleFor(x => x.ItemBomDeleteId).MustAsync(IsValidItemBomMasterId).When(x => x.IsParent).WithMessage("Please enter valid item bom master id");

            RuleFor(x => x.ItemBomDeleteId).MustAsync(IsValidItemBomMasterId).When(x => !x.IsParent).WithMessage("please etner valid item bom child id");

        }

        public async Task<bool> IsValidItemBomMasterId(Guid ItemBoMMasterId, CancellationToken token)
        {
            return !(await _itemBoMMasterRepository.GetByWhere(x => x.ItemBoMMasterId == ItemBoMMasterId && !x.IsDeleted)?.AnyAsync() == false);
        }

        public async Task<bool> IsValidItemBomChildId(Guid ItemBomChildId, CancellationToken token)
        {
            return !(await _itemBoMChildRepository.GetByWhere(x => x.ItemBoMChildId == ItemBomChildId && !x.IsDeleted)?.AnyAsync() == false);
        }
    }
}
