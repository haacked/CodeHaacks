using System;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Web.Mvc;
using System.Web.Routing;
using MvcHaack.ControllerInspector;
using Xunit;

namespace ControllerInspectorTests
{
    public class ControllerValidatorTests
    {
        public class TheGetControllersThatViolateConventionMethod
        {
            [Fact]
            public void FindsControllersThatDoNotMeetConvention()
            {
                var badControllers = typeof (ControllerValidatorTests).GetControllersThatViolateConvention().ToList();

                foreach (var bad in badControllers)
                    Console.Write(bad.Name);
                Assert.Equal(4, badControllers.Count);

                Assert.True(badControllers.Contains(typeof (Bad)));
                Assert.True(badControllers.Contains(typeof (AnotherBad)));
                Assert.True(badControllers.Contains(typeof (YetAnotherBad)));
                Assert.True(badControllers.Contains(typeof (NotOk)));
            }
        }
    }

    internal static class MediumTrustContext
    {
        public static T Create<T>()
        {
            AppDomain appDomain = CreatePartialTrustDomain();
            var t = (T)appDomain.CreateInstanceAndUnwrap(typeof (T).Assembly.FullName, typeof (T).FullName);
            return t;
        }

        public static AppDomain CreatePartialTrustDomain()
        {
            var setup = new AppDomainSetup {ApplicationBase = AppDomain.CurrentDomain.BaseDirectory};
            var permissions = new PermissionSet(null);
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            permissions.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess));
            return AppDomain.CreateDomain("Partial Trust AppDomain: " + DateTime.Now.Ticks, null, setup, permissions);
        }
    }

    public class AnotherGoodController : ControllerBase
    {
        protected override void ExecuteCore()
        {
            throw new System.NotImplementedException();
        }
    }

    public class YetAnotherGoodController : IController
    {
        public void Execute(RequestContext requestContext)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Bad : Controller
    {
    }

    public class AnotherBad : IController
    {
        public void Execute(RequestContext requestContext)
        {
            throw new System.NotImplementedException();
        }
    }

    public class YetAnotherBad : AnotherBad
    {
    }

    public abstract class NotInstantiable : Controller
    {
    }

    public class OkController : NotInstantiable
    {
    }

    public class NotOk : NotInstantiable
    {
    }
}