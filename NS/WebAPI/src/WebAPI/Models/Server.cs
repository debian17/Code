using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public static class Server
    {
        public static async Task<string> GetResponseAsync(string path)
        {
            var res = string.Empty;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            await Task.Run(async () =>
            {
                request = (HttpWebRequest) HttpWebRequest.Create(path);
                response = (HttpWebResponse) await request.GetResponseAsync();
            });

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    res = await reader.ReadToEndAsync();
                }
            }
            response.Dispose();
            return res;
        }
    }
}
