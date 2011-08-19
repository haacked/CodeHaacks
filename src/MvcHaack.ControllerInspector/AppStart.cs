using System.Web.Mvc;
using MvcHaack.ControllerInspector;
using WebActivator;

[assembly: PostApplicationStartMethod(typeof(AppStart), "Start")]
namespace MvcHaack.ControllerInspector {

    public static class AppStart {
        public static void Start() {
            var factory = ControllerBuilder.Current.GetControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(new InspectorControllerFactory(factory));
        }
    }
}
