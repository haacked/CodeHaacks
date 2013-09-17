using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiscUtils
{
    public static class AttributeExtensions
    {
        public static Attribute CreateAttribute(this CustomAttributeData data)
        {
            var arguments = from arg in data.ConstructorArguments select arg.Value;

            var attribute = data.Constructor.Invoke(arguments.ToArray()) as Attribute;

            if (data.NamedArguments == null) return attribute;

            foreach (var namedArgument in data.NamedArguments)
            {
                var propertyInfo = namedArgument.MemberInfo as PropertyInfo;
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(attribute, namedArgument.TypedValue.Value, null);
                }
                else
                {
                    var fieldInfo = namedArgument.MemberInfo as FieldInfo;
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(attribute, namedArgument.TypedValue.Value);
                    }
                }
            }

            return attribute;
        }

        public static IEnumerable<Attribute> GetCustomAttributesCopy(this Type type)
        {
            return CustomAttributeData.GetCustomAttributes(type).CreateAttributes();
        }

        public static IEnumerable<Attribute> GetCustomAttributesCopy(this Assembly assembly)
        {
            return CustomAttributeData.GetCustomAttributes(assembly).CreateAttributes();
        }

        public static IEnumerable<Attribute> GetCustomAttributesCopy(this MemberInfo memberInfo)
        {
            return CustomAttributeData.GetCustomAttributes(memberInfo).CreateAttributes();
        }

        public static IEnumerable<Attribute> CreateAttributes(this IEnumerable<CustomAttributeData> attributesData)
        {
            return from attributeData in attributesData
                select attributeData.CreateAttribute();
        }
    }
}