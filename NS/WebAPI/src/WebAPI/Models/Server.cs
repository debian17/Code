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
            WebRequest request = null;
            WebResponse response = null;
            await Task.Run(async () =>
            {
                request = WebRequest.Create(path);
                response = await request.GetResponseAsync();
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
