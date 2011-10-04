using System.Web;
using $rootnamespace$.App_Start;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: PreApplicationStartMethod(typeof(FormsAuthenticationConfig), "Register")]
namespace $rootnamespace$.App_Start {
    public static class FormsAuthenticationConfig {
        public static void Register() {
            DynamicModuleUtility.RegisterModule(typeof(SuppressFormsAuthenticationRedirectModule));
        }
    }
}
