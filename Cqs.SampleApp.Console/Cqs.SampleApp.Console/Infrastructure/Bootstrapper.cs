using Autofac;
using Cqs.SampleApp.Core.Cqs;
using Cqs.SampleApp.Core.DataAccess;
using Cqs.SampleApp.Core.IoC;

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
                //register the EF DbContext
                builder.RegisterType<ApplicationDbContext>()
                       .AsSelf()
                       .WithParameter("connectionString", "BooksContext")
                       .InstancePerLifetimeScope();
            }
        }

        public class CqsModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                var _assembly = typeof(CqsModule).Assembly;

                //register the QueryDispatcher and CommandDispatcher
                builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
                builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

                //Register all QueryHandlers and all CommandHandlers found in this assembly
                builder.RegisterAssemblyTypes(_assembly).AsClosedTypesOf(typeof(IQueryHandler<,>));
                builder.RegisterAssemblyTypes(_assembly).AsClosedTypesOf(typeof(ICommandHandler<,>));
            }
        }

    }
}