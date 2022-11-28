using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;

namespace Trading.Infrastructure.Repository
{
    public class CompanyMasterRepository : BaseRepository<CompanyMaster>, ICompanyMasterRepository
    {
        readonly TradingContext _dbContext;
        public CompanyMasterRepository(TradingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
