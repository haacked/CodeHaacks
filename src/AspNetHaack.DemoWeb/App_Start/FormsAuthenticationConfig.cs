using System.Web;
using AspNetHaack.DemoWeb.App_Start;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: PreApplicationStartMethod(typeof(FormsAuthenticationConfig), "Register")]
namespace AspNetHaack.DemoWeb.App_Start {
    public static class FormsAuthenticationConfig {
        public static void Register() {
            DynamicModuleUtility.RegisterModule(typeof(SuppressFormsAuthenticationRedirectModule));
        }
    }
}
