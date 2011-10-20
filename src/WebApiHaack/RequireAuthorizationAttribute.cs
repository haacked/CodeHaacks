using System;

namespace WebApiHaack
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequireAuthorizationAttribute : Attribute
    {
        public string Roles { get; set; }
    }
}
