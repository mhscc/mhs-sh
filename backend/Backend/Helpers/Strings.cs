using System.Text.RegularExpressions;

namespace Backend.Helpers
{
    public static class Strings
    {
        public static bool IsValidURL(string? url)
        {
            if (string.IsNullOrEmpty(url) || !url.StartsWith("http"))
                return false;

            var regex = new Regex(
                @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase
                );

            return regex.IsMatch(url);
        }

        public static string Slugify(this string phrase)
        {
            string str = phrase.ToLower().Trim();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();

            return Regex.Replace(str, @"\s", "-");
        }
    }
}