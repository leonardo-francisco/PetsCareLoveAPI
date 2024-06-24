using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Utils
{
    public class PhotoTransfer
    {
        private readonly HttpClient _httpClient;

        public PhotoTransfer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> GetPhotoAsync(string photoUrl)
        {
            var response = await _httpClient.GetAsync(photoUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                throw new Exception("Failed to retrieve photo");
            }
        }
    }
}
