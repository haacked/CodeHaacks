using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace MiscUtils
{
    public static class AttributeExtensions
    {
        public static Attribute CreateAttribute(this CustomAttributeData data)
        {
            var arguments = data.ConstructorArguments.GetConstructorValues().ToArray();
            var attribute = data.Constructor.Invoke(arguments) as Attribute;

            if (data.NamedArguments == null) return attribute;

            foreach (var namedArgument in data.NamedArguments)
            {
                var propertyInfo = namedArgument.MemberInfo as PropertyInfo;
                var value = namedArgument.TypedValue.GetArgumentValue();

                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(attribute, value, null);
                }
                else
                {
                    var fieldInfo = namedArgument.MemberInfo as FieldInfo;
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(attribute, value);
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

        private static IEnumerable<object> GetConstructorValues(this IEnumerable<CustomAttributeTypedArgument> arguments)
        {
            return from argument in arguments select argument.GetArgumentValue();
        }
 
        private static object GetArgumentValue(this CustomAttributeTypedArgument argument)
        {
            object value;
            
            if (argument.ArgumentType.IsEnum)
            {
                value = Enum.ToObject(argument.ArgumentType, argument.Value);
            }
            else
            {
                value = argument.Value;
            }
            
            var collectionValue = value as ReadOnlyCollection<CustomAttributeTypedArgument>;
            return collectionValue != null
                       ? ConvertCustomAttributeTypedArgumentArray(
                           collectionValue,
                           argument.ArgumentType.GetElementType())
                       : value;
        }

        private static Array ConvertCustomAttributeTypedArgumentArray(
            this IEnumerable<CustomAttributeTypedArgument> arguments,
            Type elementType)
        {
            var valueArray = arguments.Select(x => x.Value).ToArray();
            var newArray = Array.CreateInstance(elementType, valueArray.Length);
            Array.Copy(valueArray, newArray, newArray.Length);
            return newArray;
        }
    }
}