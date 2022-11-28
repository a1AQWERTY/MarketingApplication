using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Data.BusinessEntity.RequestFilter;
using Trading.Data.Entities;

namespace Trading.Interface.Interface
{
    public interface IItemMasterRepository : IBaseRepository<ItemMaster>
    {
        Task<IQueryable<ItemDetailList>> GetItemDetailList();
        Task<bool> IsValidItem(Guid ItemMasterId);
    }
}
