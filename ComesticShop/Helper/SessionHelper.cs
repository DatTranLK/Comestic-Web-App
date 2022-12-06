using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace ComesticShop.Helper
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, Formatting.Indented,
new JsonSerializerSettings
{
    PreserveReferencesHandling = PreserveReferencesHandling.Objects
}));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass)) cssClass = "active";
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];
            if (string.IsNullOrEmpty(controller)) controller = currentController;
            if (string.IsNullOrEmpty(action)) action = currentAction;
            return controller == currentController && action == currentAction ? cssClass : string.Empty;
        }
        public static string PageClass(this HtmlHelper html)
        {
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }


}
