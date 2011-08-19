using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace MvcHaack.ControllerInspector {
    internal class ControllerDetailer {
        public string GetControllerDetails(ControllerDescriptor controllerDescriptor) {
            var template = new ControllerDetails {
                Model = GetControllerModel(controllerDescriptor)
            };
            return template.TransformText();
        }

        private static object GetControllerModel(ControllerDescriptor controllerDescriptor) {
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
