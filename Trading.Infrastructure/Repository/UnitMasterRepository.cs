using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;

namespace Trading.Infrastructure.Repository
{
    public class UnitMasterRepository : BaseRepository<UnitMaster>, IUnitMasterRepository
    {
        public readonly TradingContext _dbContext;
        public UnitMasterRepository(TradingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method to test whether Unit is valid or not
        /// </summary>
        /// <param name="UnitMasterId"></param>
        /// <returns></returns>
        public async Task<bool> IsValidUnit(Guid UnitMasterId)
        {
            return await (from unitMaster in _dbContext.UnitMaster
                          where unitMaster.UnitMasterId == UnitMasterId && !unitMaster.IsDeleted
                          select unitMaster).AsQueryable().AnyAsync();
        }
    }
}
