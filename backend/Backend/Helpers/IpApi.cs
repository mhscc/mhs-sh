using Backend.Modals;

namespace Backend.Helpers
{
    public class IpApi
    {
        public static async Task<IpInfo?> GetIpInfo(HttpClient req, string? ipaddy)
        {
            string key = Globals.IpApiKey;

            if (string.IsNullOrEmpty(key))
                throw new Exception("No ip-api.com API key!");

            if (ipaddy == null)
                return null;

            string[] prms = { "countryCode" };

            try
            {
                var res = await req.GetAsync(
                    $"http://pro.ip-api.com/json/{ipaddy}?fields=" +
                    string.Join(",", prms) +
                    "&key=" + key
                    );

                if (!res.IsSuccessStatusCode)
                    return null;

                var info = await res.Content.ReadFromJsonAsync<IpInfo>();

                return info;
            }
            catch { return null; }
        }
    }
}