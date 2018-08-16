using Autofac;
using Cqs.SampleApp.Console.IoC;
using Cqs.SampleApp.Core.DataAccess;

namespace Cqs.SampleApp.Console.Infrastructure
{
    public static class Bootstrapper
    {
        public static IAutofacContainer Bootstrap()
        {
            var _container = new AutofacContainer(builder =>
            {
                builder.RegisterModule<ApplicationModule>();
                builder.RegisterModule<CqsModule>();
            });

            return _container;
        }

        public class ApplicationModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<ApplicationDbContext>()
                       .AsSelf()
                       .WithParameter("connectionString", "BooksContext")
                       .InstancePerLifetimeScope();
            }
        }

        public class CqsModule : Module
        {

        }

    }
}