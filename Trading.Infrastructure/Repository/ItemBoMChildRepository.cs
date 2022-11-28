using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;

namespace Trading.Infrastructure.Repository
{
    public class ItemBoMChildRepository : BaseRepository<ItemBoMChild>, IItemBoMChildRepository
    {
        public ItemBoMChildRepository(TradingContext dbContext) : base(dbContext)
        {
        }
    }
}
