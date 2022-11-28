using Autofac;
using FluentValidation;
using Trading.Data.Database;
using Trading.Infrastructure.Repository;
using Trading.Interface.Interface;

namespace TradingAPI.DependencyRegister
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ItemMasterRepository>().As<IItemMasterRepository>().InstancePerDependency();

            builder.RegisterType<UserMasterRepository>().As<IUserMasterRepository>().InstancePerDependency();

            builder.RegisterType<CompanyMasterRepository>().As<ICompanyMasterRepository>().InstancePerDependency();

            builder.RegisterType<UnitMasterRepository>().As<IUnitMasterRepository>().InstancePerDependency();

            builder.RegisterType<ItemInventoryRepository>().As<IItemInventoryRepository>().InstancePerDependency();

            builder.RegisterType<UnitConversionRepository>().As<IUnitConversionRepository>().InstancePerDependency();

            builder.RegisterType<ItemBoMMasterRepository>().As<IItemBoMMasterRepository>().InstancePerDependency();

            builder.RegisterType<ItemBoMChildRepository>().As<IItemBoMChildRepository>().InstancePerDependency();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();


        }
    }
}
