using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Utils
{
    public class ImageHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task DownloadAndSaveImageAsync(string imageUrl, string localPath)
        {
            if (Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                // Handle URL
                using (var client = _httpClientFactory.CreateClient())
                using (HttpResponseMessage response = await client.GetAsync(imageUrl))
                {
                    response.EnsureSuccessStatusCode();
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (FileStream fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
            }
            else if (File.Exists(imageUrl))
            {
                // Handle local file path
                File.Copy(imageUrl, localPath, true);
            }
            else
            {
                throw new Exception("Invalid photo path");
            }
        }
    }
}
