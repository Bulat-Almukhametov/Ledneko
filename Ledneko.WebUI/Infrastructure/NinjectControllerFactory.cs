using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ledneko.Domain.Abstract;
using Ledneko.Domain.Concrete;
using Ledneko.WebUI.Infrastructure.Abstract;
using Ledneko.WebUI.Infrastructure.Concrete;
using Ninject;

namespace Ledneko.WebUI.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
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

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
}