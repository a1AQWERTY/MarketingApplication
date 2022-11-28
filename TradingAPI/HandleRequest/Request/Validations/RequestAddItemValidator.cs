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
    public class RequestAddItemValidator : AbstractValidator<RequestItemAdd>
    {
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly IUnitMasterRepository _unitMasterRepository;

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="itemMasterRepository"></param>
        /// <param name="unitMasterRepository"></param>
        public RequestAddItemValidator(IItemMasterRepository itemMasterRepository, IUnitMasterRepository unitMasterRepository)
        {
            _itemMasterRepository = itemMasterRepository ?? throw new ArgumentNullException(nameof(itemMasterRepository));

            _unitMasterRepository = unitMasterRepository ?? throw new ArgumentNullException(nameof(unitMasterRepository));

            RuleFor(m => m.ItemName).NotNull().NotEmpty().WithMessage("Item name can not be null or empty");

            RuleFor(m => m.UnitMasterId).NotNull().NotEmpty().WithMessage("Unit of item can not be null or empty");

            RuleFor(m => m.ItemCode).NotNull().NotEmpty().WithMessage("Item Code can not be null or empty");

            RuleFor(x => x.ItemCode).MustAsync(IsItemExists).WithMessage("Item code already exists");

            //RuleFor(x => x.UnitMasterId).MustAsync(IsUnitExists).WithMessage("Please enter valid Unit");

            RuleFor(x => x.UnitMasterId).MustAsync(async (itemcode, cancellation) => {
                bool exists = await IsUnitExists(itemcode,cancellation);
                return !exists;
            }).WithMessage("Please enter valid Unit");


        }

        /// <summary>
        /// check for item code already exists or not
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<bool> IsItemExists(string ItemCode , CancellationToken cancellation)
        {
            return !(_itemMasterRepository.GetByWhere(x => x.ItemCode.ToLower() == ItemCode.ToLower() && !x.IsDeleted)?.Any() == true);
        }

        /// <summary>
        /// Check Unit Master Id is valid or not
        /// </summary>
        /// <param name="UnitMasterId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<bool> IsUnitExists(Guid UnitMasterId, CancellationToken cancellation)
        {
            return !(_unitMasterRepository.GetByWhere(x => x.UnitMasterId == UnitMasterId && !x.IsDeleted)?.Any() == true);
        }
    }
}
