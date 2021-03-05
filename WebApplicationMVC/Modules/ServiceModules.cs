using Aplication.Users;
using Aplication.Users.Services;
using Aplication.AplicationServices.Services;
using Aplication.AplicationServices.Interface;
using Autofac;
using Infrastructure.Users;
using Infrastructure.Users.Interface;


namespace PresentationWeb
{
    public class ServiceModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UsersService>().As<IUsersService>();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            builder.RegisterType<ServicesLogin>().As<ILogin>();
        }
    }
}
