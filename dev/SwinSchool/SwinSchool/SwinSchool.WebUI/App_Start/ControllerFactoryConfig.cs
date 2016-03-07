using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SwinSchool.WebUI
{
    public class ControllerFactoryConfig
    {
        public static void RegisterControllerFactory()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var composition = new CompositionContainer(catalog);
            IControllerFactory mefControllerFactory = new MefControllerFactory(composition);
            ControllerBuilder.Current.SetControllerFactory(mefControllerFactory);
        }
    }

    public class MefControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _container;
        public MefControllerFactory(CompositionContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            Lazy<object, object> export = _container.GetExports(controllerType, null, null).FirstOrDefault();

            return null == export
                                ? base.GetControllerInstance(requestContext, controllerType)
                                : (IController)export.Value;
        }
        public override void ReleaseController(IController controller)
        {
            ((IDisposable)controller).Dispose();
        }
    }
}