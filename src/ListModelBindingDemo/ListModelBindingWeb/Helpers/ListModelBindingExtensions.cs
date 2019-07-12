using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace ListModelBindingWeb.Helpers
{
    public static class ListModelBindingExtensions
    {
        static Regex _stripIndexerRegex = new Regex(@"\[(?<index>\d+)\]", RegexOptions.Compiled);

        public static string GetIndexerFieldName(this TemplateInfo templateInfo) {
            string fieldName = templateInfo.GetFullHtmlFieldName("Index");
            fieldName = _stripIndexerRegex.Replace(fieldName, string.Empty);
            if (fieldName.StartsWith(".")) {
                fieldName = fieldName.Substring(1);
            }
            return fieldName;
        }

        public static int GetIndex(this TemplateInfo templateInfo)
        {
            string fieldName = templateInfo.GetFullHtmlFieldName("Index");
            var match = _stripIndexerRegex.Match(fieldName);
            if (match.Success) { 
                return int.Parse(match.Groups["index"].Value);
            }
            return 0;
        }

        public static MvcHtmlString HiddenIndexerInputForModel<TModel>(this HtmlHelper<TModel> html) {
            string name = html.ViewData.TemplateInfo.GetIndexerFieldName();
            object value = html.ViewData.TemplateInfo.GetIndex();
            string markup = String.Format(@"<input type=""hidden"" name=""{0}"" value=""{1}"" />", name, value);
            return MvcHtmlString.Create(markup);
        }
    }
}