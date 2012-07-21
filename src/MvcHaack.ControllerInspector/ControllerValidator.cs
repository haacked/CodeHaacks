using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace MvcHaack.ControllerInspector
{
    public static class ControllerValidator
    {
        public static bool AllControllersMeetConvention(this Assembly assembly)
        {
            return assembly.GetControllersThatViolateConvention().Any();
        }

        public static IEnumerable<Type> GetControllersThatViolateConvention(this Type typeInAssembly)
        {
            return typeInAssembly.Assembly.GetControllersThatViolateConvention();
        }

        public static IEnumerable<Type> GetControllersThatViolateConvention(this Assembly assembly)
        {
            return from t in assembly.GetLoadableTypes()
                   where t.IsControllerType() && !t.MeetsConvention()
                   select t;
        }

        private static bool IsPublicClass(this Type type)
        {
            return (type != null && type.IsPublic && type.IsClass && !type.IsAbstract);
        }


        internal static bool IsControllerType(this Type t)
        {
            return
                t.IsPublicClass() &&
                typeof (IController).IsAssignableFrom(t);
        }

        public static bool MeetsConvention(this Type t)
        {
            return t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase);
        }

        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}