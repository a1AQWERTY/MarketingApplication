using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Entities;

namespace Trading.Interface.Interface
{
    public interface IUnitMasterRepository : IBaseRepository<UnitMaster>
    {

        Task<bool> IsValidUnit(Guid UnitMasterId);
    }
}
