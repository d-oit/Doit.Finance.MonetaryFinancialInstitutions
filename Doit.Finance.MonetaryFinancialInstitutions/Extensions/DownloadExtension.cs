using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Doit.Finance.MonetaryFinancialInstitutions.Extensions
{
    public static class DownloadExtension
    {
        public static async Task<string> DownloadFileAsync(string url, FileInfo fileInfo)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        await using var ms = await response.Content.ReadAsStreamAsync();
                        await using var fs = File.Create(fileInfo.FullName);
                        ms.Seek(0, SeekOrigin.Begin);
                        ms.CopyTo(fs);

                        return fileInfo.FullName;
                    }
                }
            }
            return null;
        }
    }
}
