using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Trading.Infrastructure.Repository
{
    public class ItemBoMMasterRepository : BaseRepository<ItemBoMMaster>, IItemBoMMasterRepository
    {
        public ItemBoMMasterRepository(TradingContext dbContext) : base(dbContext)
        {
        }

        public async Task<ItemBoMDetail> GetItemBoMDetail(Guid ItemBoMMasterId)
        {
            var query = (from itemBomMaster in _dbContext.ItemBoMMaster

                         join itemMaster in _dbContext.ItemMaster
                         on itemBomMaster.ItemMasterId equals itemMaster.ItemMasterId
                         join unitMaster in _dbContext.UnitMaster
                        on itemBomMaster.UnitMasterId equals unitMaster.UnitMasterId
                         where itemBomMaster.ItemBoMMasterId == ItemBoMMasterId
                         && !itemBomMaster.IsDeleted
                         select new ItemBoMDetail()
                         {
                             ItemCode = itemMaster.ItemCode,
                             ItemName = itemMaster.ItemName,
                             Quantity = itemBomMaster.Quantity,
                             UnitMasterId = itemBomMaster.UnitMasterId,
                             UnitName = unitMaster.UnitName,
                             ItemMasterId = itemMaster.ItemMasterId,
                             ItemBoMChildDetails = (from itemBomChild in _dbContext.ItemBoMChild
                                                    join itemMaster in _dbContext.ItemMaster
                                                    on itemBomChild.ItemMasterId equals itemMaster.ItemMasterId
                                                    join unitMaster in _dbContext.UnitMaster
                                                   on itemBomChild.UnitMasterId equals unitMaster.UnitMasterId
                                                    where itemBomChild.ItemBoMMasterId == ItemBoMMasterId
                                                    select new ItemBoMChildDetail()
                                                    {
                                                        ItemCode = itemMaster.ItemCode,
                                                        ItemName = itemMaster.ItemName,
                                                        ItemMasterId = itemBomChild.ItemMasterId,
                                                        Quantity = itemBomChild.Quantity,
                                                        UnitMasterId = itemBomChild.UnitMasterId,
                                                        ItemBoMChildId = itemBomChild.ItemBoMChildId,
                                                        UnitName = unitMaster.UnitName,
                                                        ItemDescription = itemMaster.ItemDescription

                                                    }).AsQueryable().ToList()
                         }).AsQueryable()?.FirstOrDefaultAsync();

            return await query;
        }

        /// <summary>
        /// Method to get list of item
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<ItemBomList>> GetItemBomList()
        {
            return (from itemBom in _dbContext.ItemBoMMaster
                    join itemMaster in _dbContext.ItemMaster on itemBom.ItemMasterId equals itemMaster.ItemMasterId
                    join unitMaster in _dbContext.UnitMaster
                    on itemBom.UnitMasterId equals unitMaster.UnitMasterId
                    where !itemMaster.IsDeleted && !unitMaster.IsDeleted && !itemBom.IsDeleted
                    select new ItemBomList()
                    {
                        ItemCode = itemMaster.ItemCode,
                        ItemDescription = itemMaster.ItemDescription,
                        ItemMasterId = itemMaster.ItemMasterId,
                        ItemName = itemMaster.ItemName,
                        UnitMasterId = itemMaster.UnitMasterId,
                        UnitName = unitMaster.UnitName,
                        //Rate = itemBom.Rate,
                        ItemBomMasterId = itemBom.ItemBoMMasterId,
                        Quantity = itemBom.Quantity
                    }).AsQueryable();
        }
    }
}
