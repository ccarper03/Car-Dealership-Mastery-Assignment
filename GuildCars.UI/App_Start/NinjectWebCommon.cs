using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GuildCars.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GuildCars.UI.App_Start.NinjectWebCommon), "Stop")]

namespace GuildCars.UI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Configuration;
    using GuildCars.Data.Interfaces;
    using GuildCars.Data.Repositories.ADO;
    using GuildCustomerContacts.Data.Repositories.ADO;
    using GuildCars.Data.Repositories.Mock;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                string mode = ConfigurationManager.AppSettings["Mode"].ToString();


                if (mode == "Prod")
                {
                    kernel.Bind<ICarsRepository>().To<CarsRepositoryADO>();
                    kernel.Bind<IBodyStyleRepository>().To<BodyStyleRepositoryADO>();
                    kernel.Bind<IColorRepository>().To<ColorRepositoryADO>();
                    kernel.Bind<ICustomerContactRepository>().To<CustomerContactRepositoryADO>();
                    kernel.Bind<IMakeRepository>().To<MakeRepositoryADO>();
                    kernel.Bind<IModelRepository>().To<ModelRepositoryADO>();
                    kernel.Bind<IPurchaseLogRepository>().To<PurchaseLogRepositoryADO>();
                    kernel.Bind<IReportsRepository>().To<ReportsRepositoryADO>();
                    kernel.Bind<ISpecialsRepository>().To<SpecialsRepositoryADO>();
                    kernel.Bind<ITransmissionRepository>().To<TransmissionRepositoryADO>();
                    kernel.Bind<IUserRepository>().To<UserRepositoryADO>();
                }
                else if(mode == "QA")
                {
                    kernel.Bind<ICarsRepository>().To<CarsRepositoryMock>();
                    kernel.Bind<IBodyStyleRepository>().To<BodyStyleRepositoryMock>();
                    kernel.Bind<IColorRepository>().To<ColorRepositoryMock>();
                    kernel.Bind<ICustomerContactRepository>().To<CustomerContactRepositoryMock>();
                    kernel.Bind<IMakeRepository>().To<MakeRepositoryMock>();
                    kernel.Bind<IModelRepository>().To<ModelRepositoryMock>();
                    kernel.Bind<IPurchaseLogRepository>().To<PurchaseLogRepositoryMock>();
                    kernel.Bind<IReportsRepository>().To<ReportsRepositoryMock>();
                    kernel.Bind<ISpecialsRepository>().To<SpecialsRepositoryMock>();
                    kernel.Bind<ITransmissionRepository>().To<TransmissionRepositoryMock>();
                    kernel.Bind<IUserRepository>().To<UserRepositoryMock>();
                }

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
