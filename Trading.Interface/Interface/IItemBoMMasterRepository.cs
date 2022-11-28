using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Data.Entities;

namespace Trading.Interface.Interface
{
    public interface IItemBoMMasterRepository : IBaseRepository<ItemBoMMaster>
    {
        Task<ItemBoMDetail> GetItemBoMDetail(Guid ItemBoMMasterId);

        Task<IQueryable<ItemBomList>> GetItemBomList();
    }
}
