using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Trading.Data.BusinessEntity.RequestFilter;
using Trading.Infrastructure.Pagination;

namespace Trading.Infrastructure.Repository
{
    public class ItemMasterRepository : BaseRepository<ItemMaster>, IItemMasterRepository
    {
        public readonly TradingContext _dbContext;
        public ItemMasterRepository(TradingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method to get list of item
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<ItemDetailList>> GetItemDetailList()
        {
            return (from itemMaster in _dbContext.ItemMaster
                    join unitMaster in _dbContext.UnitMaster
                    on itemMaster.UnitMasterId equals unitMaster.UnitMasterId
                    where !itemMaster.IsDeleted && !unitMaster.IsDeleted
                    select new ItemDetailList()
                    {
                        ItemCode = itemMaster.ItemCode,
                        ItemDescription = itemMaster.ItemDescription,
                        ItemMasterId = itemMaster.ItemMasterId,
                        ItemName = itemMaster.ItemName,
                        UnitMasterId = itemMaster.UnitMasterId,
                        UnitName = unitMaster.UnitName
                    }).AsQueryable();
        }

        /// <summary>
        /// Method to test whether Item is valid or not
        /// </summary>
        /// <param name="UnitMasterId"></param>
        /// <returns></returns>
        public async Task<bool> IsValidItem(Guid ItemMasterId)
        {
            return await (from itemMaster in _dbContext.ItemMaster
                          where itemMaster.ItemMasterId == ItemMasterId && !itemMaster.IsDeleted select itemMaster).AsQueryable().AnyAsync();
        }
    }
}
