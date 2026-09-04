using System.Text.RegularExpressions;

namespace Soso.UI.Animation
{
    public static class StringUtils
    {
        public static string InsertSpacesAroundCaps(object value) => InsertSpacesAroundCaps(value.ToString());
        public static string InsertSpacesAroundCaps(string value)
        {
            return Regex.Replace(value, "(?<=[a-z])(?=[A-Z])", " ");
        }
    }
}