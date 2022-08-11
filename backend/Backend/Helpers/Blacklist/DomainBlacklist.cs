namespace Backend.Helpers.Blacklist
{
    public class DomainBlacklist
    {
        public static bool IsBlacklisted(string? url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            var uri = new Uri(url);

            if (uri == null)
                return true;

            return Blacklist.Contains(
                uri.Host.Substring(4)
                );
        }

        private static readonly string[] Blacklist = {
            "compornhub.com",
            "discord.com",
            "discord.gg",
            "discord.gift",
            "discordapp.com",
            "discordapp.net",
            "enporn.com",
            "hentai.tv",
            "hulu.com",
            "kekma.live",
            "leagueoflegends.com",
            "mathpapa.com",
            "mathway.com",
            "mega.io",
            "mega.nz",
            "mhs.sh",
            "modelhub.com",
            "netflix.com",
            "onlyfans.com",
            "photomath.com",
            "pirnhub.com",
            "porbhub.com",
            "porhub.com",
            "pormhub.com",
            "pornh7b.com",
            "pornhb.com",
            "pornhib.com",
            "pornhub.com",
            "pornhub.co",
            "pornhub.es",
            "pornhub.fr",
            "pornhub.it",
            "pornhub.net",
            "pornhub.org",
            "pornhub.pl",
            "pornhub.pt",
            "pornhub.ru",
            "pornhyb.com",
            "pornmate.com",
            "potnhub.com",
            "pprnhub.com",
            "pronhub.com",
            "redd.it",
            "reddit.com",
            "redtube.com",
            "roblox.com",
            "slader.com",
            "t.co",
            "t.me",
            "telegram.org",
            "tiktok.com",
            "twitter.co",
            "twitter.com",
            "twttr.com",
            "usacrime.com",
            "wolframalpha.com",
            "xhamster.com",
            "xnxx.com",
            "xvideos.com"
        };
    }
}
