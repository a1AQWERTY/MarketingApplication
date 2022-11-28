using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Interface.Interface;

namespace Trading.Infrastructure.Repository
{
    public class UnitConversionRepository : BaseRepository<UnitConversion>, IUnitConversionRepository
    {
        public UnitConversionRepository(TradingContext dbContext) : base(dbContext)
        {
        }
    }
}
