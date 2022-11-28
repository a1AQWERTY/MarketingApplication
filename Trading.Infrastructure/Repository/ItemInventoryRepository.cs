using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Data.BusinessEntity;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;

namespace Trading.Infrastructure.Repository
{
    public class ItemInventoryRepository : BaseRepository<ItemInventory>, IItemInventoryRepository
    {
        public ItemInventoryRepository(TradingContext dbContext) : base(dbContext)
        {
           
        }


        /// <summary>
        /// Method to get list of item
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<ItemInventoryList>> GetItemInventoryList()
        {
            return (from itemInventory in _dbContext.ItemInventory
                    join itemMaster in _dbContext.ItemMaster on itemInventory.ItemMasterId equals itemMaster.ItemMasterId
                    join unitMaster in _dbContext.UnitMaster
                    on itemInventory.UnitMasterId equals unitMaster.UnitMasterId
                    where !itemMaster.IsDeleted && !unitMaster.IsDeleted && !itemInventory.IsDeleted
                    select new ItemInventoryList()
                    {
                        ItemCode = itemMaster.ItemCode,
                        ItemDescription = itemMaster.ItemDescription,
                        ItemMasterId = itemMaster.ItemMasterId,
                        ItemName = itemMaster.ItemName,
                        UnitMasterId = itemMaster.UnitMasterId,
                        UnitName = unitMaster.UnitName,
                        Rate = itemInventory.Rate,
                        ItemInventoryId = itemInventory.ItemInventoryId,
                        Quantity = itemInventory.Quantity,
                        BatchNo = itemInventory.BatchNo
                    }).AsQueryable();
        }

        /// <summary>
        /// Method to get detail of Item Inventory
        /// </summary>
        /// <returns></returns>
        public  Task<ItemInventoryList> GetItemInventoryDetail(Guid ItemInventoryId)
        {
            var result = (from itemInventory in _dbContext.ItemInventory
                          join itemMaster in _dbContext.ItemMaster on itemInventory.ItemMasterId equals itemMaster.ItemMasterId
                          join unitMaster in _dbContext.UnitMaster
                          on itemInventory.UnitMasterId equals unitMaster.UnitMasterId
                          where !itemMaster.IsDeleted && !unitMaster.IsDeleted && !itemInventory.IsDeleted
                          && itemInventory.ItemInventoryId == ItemInventoryId
                          select new ItemInventoryList()
                          {
                              ItemCode = itemMaster.ItemCode,
                              ItemDescription = itemMaster.ItemDescription,
                              ItemMasterId = itemMaster.ItemMasterId,
                              ItemName = itemMaster.ItemName,
                              UnitMasterId = itemMaster.UnitMasterId,
                              UnitName = unitMaster.UnitName,
                              Rate = itemInventory.Rate,
                              ItemInventoryId = itemInventory.ItemInventoryId,
                              Quantity = itemInventory.Quantity,
                              BatchNo = itemInventory.BatchNo
                          }).AsQueryable();

            return Task.FromResult(result.FirstOrDefault());
        }


         /// <summary>
        /// Get Item wise item inventory list
        /// </summary>
        /// <returns></returns>
        public  Task<List<ItemInventoryList>> GetItemWiseInventoryList(Guid ItemMasterId)
        {
            var result = (from itemInventory in _dbContext.ItemInventory
                          join itemMaster in _dbContext.ItemMaster on itemInventory.ItemMasterId equals itemMaster.ItemMasterId
                          join unitMaster in _dbContext.UnitMaster
                          on itemInventory.UnitMasterId equals unitMaster.UnitMasterId
                          where !itemMaster.IsDeleted && !unitMaster.IsDeleted && !itemInventory.IsDeleted
                          && itemInventory.ItemMasterId == ItemMasterId
                          select new ItemInventoryList()
                          {
                              ItemCode = itemMaster.ItemCode,
                              ItemDescription = itemMaster.ItemDescription,
                              ItemMasterId = itemMaster.ItemMasterId,
                              ItemName = itemMaster.ItemName,
                              UnitMasterId = itemMaster.UnitMasterId,
                              UnitName = unitMaster.UnitName,
                              Rate = itemInventory.Rate,
                              ItemInventoryId = itemInventory.ItemInventoryId,
                              Quantity = itemInventory.Quantity,
                              BatchNo = itemInventory.BatchNo
                          }).AsQueryable();

            return Task.FromResult(result.ToList());
        }

    }
}
