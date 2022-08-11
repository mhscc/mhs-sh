using Backend.Modals;

namespace Backend.Helpers
{
    public class ReCaptcha
    {
        public static async Task<bool> Validate(string? captchaResp)
        {
            if (string.IsNullOrEmpty(captchaResp))
                return false;

            var secret = Globals.ReCaptchaSecret;

            if (string.IsNullOrEmpty(secret))
                throw new Exception("ReCaptcha secret is invalid!");

            using var client = new HttpClient();

            string url = string.Format(
                "https://google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                secret,
                captchaResp
                );

            try
            {
                var res = await client.GetAsync(url);
                var json = await res.Content.ReadFromJsonAsync<ReCaptchaResp>();

                return json?.Success ?? false;
            }
            catch { return false; }
        }
    }
}