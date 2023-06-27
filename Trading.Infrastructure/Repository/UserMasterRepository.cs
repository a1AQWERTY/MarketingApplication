
using System.Threading.Tasks;
using Trading.Data.Database;
using Trading.Data.Entities;
using Trading.Infrastructure.Pagination;
using Trading.Interface.Interface;
using System.Linq;
using System.Collections.Generic;

namespace Trading.Infrastructure.Repository
{
    public class UserMasterRepository : BaseRepository<UserMaster>, IUserMasterRepository
    {
        readonly TradingContext _tradingContext;
        public UserMasterRepository(TradingContext tradingContext) : base(tradingContext)
        {
            _tradingContext = tradingContext;
        }

        public Task<List<UserMaster>> GetUsers(int pageNo, int PageSize, out int Count)
        {
            var userData = _tradingContext.UserMaster.AsQueryable().GetPaged(pageNo, PageSize, out Count).Results;
            return Task.FromResult(userData.ToList());
        }
    }
}
