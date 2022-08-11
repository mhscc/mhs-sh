namespace Backend.Helpers.Blacklist
{
    public class SlugBlacklist
    {
        public static bool IsBlacklisted(string slug) =>
            ExactMatchArr.Any(i => i == slug) ||
            ContainsMatchArr.Any(i => slug.Contains(i));

        private static readonly string[] ExactMatchArr = {
            "boobs",
            "gay",
            "milf",
            "penis",
            "porn",
            "pussy",
            "tities",
            "tits",
            "vagina"
        };

        private static readonly string[] ContainsMatchArr = {
            "afrocoon",
            "antigay",
            "antilgbtq",
            "asshole",
            "autism",
            "autist",
            "autistic",
            "bitch",
            "blackboy",
            "bullshit",
            "chigger",
            "chimp",
            "chinaman",
            "chinavirus",
            "chink",
            "coochie",
            "crime",
            "currymuncher",
            "dick",
            "fagget",
            "faggot",
            "fuck",
            "gayguy",
            "isbad",
            "nigga",
            "nigger",
            "pornhub",
            "pussy",
            "retard",
            "sexist",
            "sexy",
            "slut",
            "sucks",
            "terrorism",
            "terrorist",
            "whore"
        };
    }
}