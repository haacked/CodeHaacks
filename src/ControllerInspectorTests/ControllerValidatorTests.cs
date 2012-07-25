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
        public class TheGetUnconventionalControllersMethod
        {
            [Fact]
            public void FindsControllersThatDoNotMeetConvention()
            {
                var badControllers = typeof (ControllerValidatorTests).GetUnconventionalControllers().ToList();

                Assert.Equal(4, badControllers.Count);

                Assert.True(badControllers.Contains(typeof (Bad)));
                Assert.True(badControllers.Contains(typeof (AnotherBad)));
                Assert.True(badControllers.Contains(typeof (YetAnotherBad)));
                Assert.True(badControllers.Contains(typeof (NotOk)));
            }
        }

        public class TheGetNonPublicControllersMethod
        {
            [Fact]
            public void FindsControllersThatDoNotMeetConvention()
            {
                var badControllers = typeof (ControllerValidatorTests).Assembly.GetNonPublicControllers().ToList();

                Assert.Equal(3, badControllers.Count);

                Assert.True(badControllers.Contains(typeof (NotOk.NestedController)));
                Assert.True(badControllers.Contains(typeof (InternalController)));
                Assert.True(badControllers.Contains(typeof (PrivateController)));
            }
        }

        public class TheGetNonControllersNamedWithControllerSuffixMethod
        {
            [Fact]
            public void FindsClassesWithControllerSuffixButAreNotControllers()
            {
                var badControllers = typeof (ControllerValidatorTests).Assembly.GetNonControllersNamedWithControllerSuffix().ToList();

                Assert.Equal(1, badControllers.Count);

                Assert.True(badControllers.Contains(typeof (FakeController)));
            }
        }
    }

    public class FakeController
    {
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
        public class NestedController : Controller
        {
        }
    }

    internal class PrivateController : Controller
    {
    }

    internal class InternalController : Controller
    {
    }
}