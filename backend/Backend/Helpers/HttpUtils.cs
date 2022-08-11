using Backend.Modals;

namespace Backend.Helpers
{
    public class HttpUtils
    {
        public static string GetIpAddr(IHeaderDictionary headers)
        {
            const string cfConnectingIp = "CF-Connecting-IP";

            if (headers.ContainsKey(cfConnectingIp))
            {
                try
                {
                    string ip = headers[cfConnectingIp];

                    if (!string.IsNullOrEmpty(ip))
                        return ip;
                }
                catch { }
            }

            var random = new Random();

            int Get() => random?.Next(1, 255) ?? 1;

            return string.Join(
                ".",
                Get(),
                Get(),
                Get(),
                Get()
                );

            return "0.0.0.0";
        }

        public static ErrorResBody Get500() => GetErr("Internal server error!");

        public static ErrorResBody GetErr(string msg) => new() { Message = msg };
    }
}