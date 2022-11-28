using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Interface.Interface
{
	public interface IUnitOfWork
	{
		IBaseRepository<T> Repository<T>() where T : class;

		Task<int> Commit();

		void Rollback();
	}
}
