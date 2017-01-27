using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Concrete;
using Ledneko.WebUI.Infrastructure.Abstract;
using Ledneko.WebUI.Infrastructure.Concrete;
using Ninject;

namespace Ledneko.WebUI.Infrastructure
{
    public class NinjectHttpControllerFactory : IHttpControllerActivator
    {
         private IKernel ninjectKernel;

        public NinjectHttpControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        void AddBindings()
        {
            #region repositories

            ninjectKernel.Bind<EfSoluionsContext>().To<EfSoluionsContext>().InThreadScope();
            ninjectKernel.Bind<ISolutionRepository>().To<EfSolutionRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<EfCategoryRepository>();
            ninjectKernel.Bind<ICatalogRepository>().To<EfCatalogRepository>();
            ninjectKernel.Bind<IServiceRepository>().To<EFServiceRepository>();
            #endregion

            #region e-mail
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = Boolean.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };


            ninjectKernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            #endregion

            #region Authentication

            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

            #endregion
        }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return controllerType == null ? null : (IHttpController)ninjectKernel.Get(controllerType);
        }
    }
}