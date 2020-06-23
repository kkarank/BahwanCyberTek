using System.Web.Script.Serialization;

namespace Bct.SearchFight.Shared.Helpers
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(this string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
    }
}
