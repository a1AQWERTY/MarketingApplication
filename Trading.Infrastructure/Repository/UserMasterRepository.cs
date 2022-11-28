using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;

namespace Trading.Infrastructure.Repository
{
    public class UserMasterRepository : BaseRepository<UserMaster>, IUserMasterRepository
    {
        readonly TradingContext _tradingContext;
        public UserMasterRepository(TradingContext tradingContext) : base(tradingContext)
        {
            _tradingContext = tradingContext;
        }
    }
}
