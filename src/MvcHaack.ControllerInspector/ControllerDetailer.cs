using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcHaack.ControllerInspector {
    internal class ControllerDetailer {
        public string GetControllerDetails(ControllerDescriptor controllerDescriptor, RequestContext requestContext) {
            var template = new ControllerDetails {
                Model = GetControllerModel(controllerDescriptor, requestContext)
            };
            return template.TransformText();
        }

        private static object GetControllerModel(ControllerDescriptor controllerDescriptor, RequestContext requestContext) {
            return new {
                ControllerName = controllerDescriptor.ControllerName,
                ControllerType = new {
                    Name = controllerDescriptor.ControllerType.Name,
                    Namespace = controllerDescriptor.ControllerType.Namespace,
                    Attributes = GetAttributesModel(controllerDescriptor.GetCustomAttributes(inherit: true))
                },
                Actions = from action in controllerDescriptor.GetCanonicalActions()
                          let reflectedAction = action as ReflectedActionDescriptor
                          select new {
                              Name = action.ActionName,
                              Id = GetActionId(action),
                              Verbs = GetVerbs(action),
                              Path = GetSamplePath(requestContext, action),
                              MethodInfo = (reflectedAction != null ? reflectedAction.MethodInfo : null),
                              ReturnType = (reflectedAction != null ? reflectedAction.MethodInfo.ReturnType : null),
                              Parameters = from parameter in action.GetParameters()
                                           select new {
                                               Name = parameter.ParameterName,
                                               Type = parameter.ParameterType,
                                               IsComplexType = IsComplexType(parameter.ParameterType),
                                               DefaultValue = parameter.DefaultValue ?? ""
                                           },
                              Attributes = GetAttributesModel(action.GetCustomAttributes(inherit: true))
                          },
                InputModels = GetInputModels(controllerDescriptor),
                GlobalFilters = from filter in GlobalFilters.Filters
                                let filterInstance = filter.Instance
                                let filterType = filterInstance.GetType()
                                select new {
                                    Name = filterType.Name,
                                    Namespace = filterType.Namespace,
                                    Properties = from property in filterType.GetProperties()
                                                 select new {
                                                     Name = property.Name,
                                                     Value = property.GetValue(filterInstance, null)
                                                 }
                                }
            };
        }

        private static IEnumerable<object> GetPropertiesModel(object o, Type t) {
            return from property in t.GetProperties()
                   select new {
                       Name = property.Name,
                       Value = property.GetValue(o, null)
                   };
        }

        private static IEnumerable<object> GetAttributesModel(IEnumerable<object> attributes) {
            return from attribute in attributes
                   let type = attribute.GetType()
                   select new {
                       Name = type.Name,
                       Type = type,
                       Namespace = type.Namespace,
                       Properties = GetPropertiesModel(attribute, type)
                   };
        }

        private static IEnumerable<string> GetVerbs(ActionDescriptor action) {
            var reflectedActionDescriptor = action as ReflectedActionDescriptor;
            if (reflectedActionDescriptor != null) {
                var selectors = (ActionMethodSelectorAttribute[])reflectedActionDescriptor.MethodInfo.GetCustomAttributes(typeof(ActionMethodSelectorAttribute), inherit: true);
                return from selector in selectors
                       where selector is HttpPostAttribute
                           || selector is HttpGetAttribute
                           || selector is HttpPutAttribute
                           || selector is HttpDeleteAttribute
                       select selector.GetType().Name.Replace("Http", "").Replace("Attribute", "");

            }
            return new string[] { };
        }

        internal static string GetActionId(ActionDescriptor actionDescriptor) {
            string actionName = actionDescriptor.ActionName;
            var verbs = GetVerbs(actionDescriptor);
            if (verbs.Any()) {
                actionName = actionName + ":" + String.Join(":", verbs);
            }
            return actionName;
        }

        private static string GetSamplePath(RequestContext requestContext, ActionDescriptor action) {
            var urlHelper = new UrlHelper(requestContext);

            var actionNameAttrib = action.GetCustomAttributes(inherit: true).OfType<ActionNameAttribute>().FirstOrDefault();

            // This is tricky because some of the action parameters may not be meant to come from the route.
            // e.g. they could come from a POST body.
            // In that case, they may end up as bogus query string params on the path, which is a bit buggy
            var routeValues = new RouteValueDictionary();
            foreach (ParameterDescriptor param in action.GetParameters()) {
                routeValues.Add(param.ParameterName, GetDefaultValue(param));
            }

            return urlHelper.Action(
                actionNameAttrib != null ? actionNameAttrib.Name : action.ActionName,
                action.ControllerDescriptor.ControllerName,
                routeValues);
        }

        public static object GetDefaultValue(ParameterDescriptor param) {
            // If it's a string, giving some value based on the param name
            if (param.ParameterType == typeof(string)) {
                return String.Format("Some{0}", param.ParameterName);
            }

            // If it's a number, pick some sample number.
            switch (Type.GetTypeCode(param.ParameterType)) {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return 7;
            }

            // For other value types, go with the default value
            if (param.ParameterType.IsValueType) {
                return Activator.CreateInstance(param.ParameterType);
            }

            return null;
        }

        private static IEnumerable<object> GetInputModels(ControllerDescriptor controller) {
            var models = from action in controller.GetCanonicalActions()
                         let parameters = action.GetParameters()
                         from parameter in parameters
                         let type = parameter.ParameterType
                         // using definition of complex type from ModelMetadata class
                         where IsComplexType(type)
                         select new {
                             Name = type.Name,
                             FullName = type.FullName,
                             Properties = type.GetProperties()
                         };
            return models;
        }

        private static bool IsComplexType(Type type) {
            return type != typeof(object) && !TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
        }
    }
}
