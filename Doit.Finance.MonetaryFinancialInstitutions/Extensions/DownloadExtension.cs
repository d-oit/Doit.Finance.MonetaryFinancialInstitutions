using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Doit.Finance.MonetaryFinancialInstitutions.Extensions
{
    public static class DownloadExtension
    {
        /// <summary>
        /// Downloads a file from a given URL and saves it to the specified file path.
        /// </summary>
        /// <param name="url">The URL from which to download the file.</param>
        /// <param name="fileInfo">The FileInfo object representing the destination file path.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation.
        /// The task result is the full path of the downloaded file if successful, otherwise null.
        /// </returns>
        public static async Task<string> DownloadFileAsync(string url, FileInfo fileInfo)
        {
            // Create an HttpClient instance
            using (var client = new HttpClient())
            {
                // Send a GET request to the specified URL
                using (var response = await client.GetAsync(url))
                {
                    // Check if the response status code indicates success
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a stream
                        await using var ms = await response.Content.ReadAsStreamAsync();

                        // Create a new file stream to write the downloaded content to
                        await using var fs = File.Create(fileInfo.FullName);

                        // Reset the stream position to the beginning
                        ms.Seek(0, SeekOrigin.Begin);

                        // Copy the downloaded content to the file stream
                        ms.CopyTo(fs);

                        // Return the full path of the downloaded file
                        return fileInfo.FullName;
                    }
                }
            }

            // Return null if the download failed
            return null;
        }
    }
}
