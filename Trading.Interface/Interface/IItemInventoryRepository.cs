using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Data.Entities;

namespace Trading.Interface.Interface
{
    public interface IItemInventoryRepository : IBaseRepository<ItemInventory>
    {
        Task<IQueryable<ItemInventoryList>> GetItemInventoryList();

        Task<ItemInventoryList> GetItemInventoryDetail(Guid ItemInventoryId);

        Task<List<ItemInventoryList>> GetItemWiseInventoryList(Guid ItemMasterId);
    }
}
