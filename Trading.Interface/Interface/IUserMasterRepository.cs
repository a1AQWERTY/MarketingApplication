using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Database;
using Trading.Data.Entities;

namespace Trading.Interface.Interface
{
    public interface IUserMasterRepository : IBaseRepository<UserMaster>
    {
        Task<List<UserMaster>> GetUsers(int pageNo,int PageSize,out int Count);
    }
}
