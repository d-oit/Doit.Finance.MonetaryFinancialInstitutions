using CsvHelper;
using CsvHelper.Configuration;
using Doit.Finance.MonetaryFinancialInstitutions.Extensions;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Doit.Finance.MonetaryFinancialInstitutions
{
    public class MonetaryFinancialInstitution : IMonetaryFinancialInstitution
    {
        public string CsvMfiFileName { get; private set; } = "mfi_csv_240613.csv";
        public string CsvMfiUpdateFileName { get; private set; } = "mfi_csv_update_240613.csv";

        public string CsvMfi_Mrr_FileName { get; private set; } = "mfi_mrr_csv_200930.csv";
        public string CsvMfi_Mrr_UpdateFileName { get; private set; } = "mfi_mrr_csv_update_200930.csv";

        private const string CsvMfiUrl = "/stats/financial_corporations/list_of_financial_institutions/html/daily_list-MID.en.html";
        private const string CsvMfi_MrrUrl = "/stats/financial_corporations/list_of_financial_institutions/html/monthly_list-MID.en.html";

        public CsvType CsvType { get; private set; }


        /// <summary>Gets the curent Monetary Financial Institutions list asynchronous.
        /// Without BIC</summary>
        /// <param name="downloadCsv">if set to <c>true</c> [download CSV].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<IEnumerable<MfiCsv>> GetCurentListAsync(bool downloadCsv = false)
        {
            CsvType = CsvType.Mfi;
            if (downloadCsv)
            {
                var fileNames = await DownloadCurrentCsvAsync(CsvMfiUrl).ConfigureAwait(false);
                CsvMfiFileName = fileNames[0];
                CsvMfiUpdateFileName = fileNames[1];
            }

            return GetMfiList();
        }

        /// <summary>Gets the curent Monetary Financial Institutions (MFIs) subject to the Eurosystem's minimum reserve requirement (monthly data) asynchronous.
        /// (With BIC) </summary>
        /// <param name="downloadCsv">if set to <c>true</c> [download CSV].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<IEnumerable<MfiMinimunReserveRequirement>> GetCurentMrrListAsync(bool downloadCsv = false)
        {
            CsvType = CsvType.Mfi_Mrr;
            if (downloadCsv)
            {
                var fileNames = await DownloadCurrentCsvAsync(CsvMfi_MrrUrl).ConfigureAwait(true);
                CsvMfi_Mrr_FileName = fileNames[0];
                CsvMfi_Mrr_UpdateFileName = fileNames[1];
            }

            return GetMrrList();
        }

        /// <summary>Gets the list.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public IEnumerable<MfiCsv> GetMfiList()
        {
            return GetCsvList(CsvMfiFileName);
        }

        public IEnumerable<MfiMinimunReserveRequirement> GetMrrList()
        {
            return GetMrrCsvList(CsvMfi_Mrr_FileName);
        }

        /// <summary>Gets the update list.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public IEnumerable<MfiUpdateCsv> GetMfiUpdateList()
        {
            return GetMfiUpdateCsv(CsvMfiUpdateFileName);
        }

        public IEnumerable<MfiMinimunReserveRequirementUpdate> GetMrrUpdateList()
        {
            return GetMfiMrrUpdateCsv(CsvMfi_Mrr_UpdateFileName);
        }

        /// <summary>Gets the Monetary Financial Institutions CSV as IEnumerable.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="ApplicationException">Check csv file for bad records!</exception>
        private static IEnumerable<MfiCsv> GetCsvList(string fileName)
        {
            var list = new List<MfiCsv>();
            var badRecods = new List<string>();
            bool isRecordBad = false;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Delimiter = "\t",
                HasHeaderRecord = true,
                Mode = CsvMode.NoEscape,
                BadDataFound = context =>
                {
                    isRecordBad = true;
                    badRecods.Add(context.RawRecord);
                }
            };

            using (var stream = File.OpenRead(fileName))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, config))
            {

                while (csv.Read())
                {
                    var record = csv.GetRecord<MfiCsv>();
                    if (!isRecordBad)
                    {
                        list.Add(record);
                    }

                    isRecordBad = false;
                }
                if (badRecods.Any())
                {
                    throw new ApplicationException("Check csv file for bad records!");
                }
                return list;
            }
        }

        /// <summary>
        /// Gets the Monetary Financial Institutions (MFIs) subject to the Eurosystem's minimum reserve requirement (monthly data) CSV as IEnumerable.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Returns a list of MfiMinimunReserveRequirement objects.
        /// Throws an ApplicationException if there are any bad records in the CSV file.
        /// </returns>
        /// <exception cref="ApplicationException">Check csv file for bad records!</exception>
        private static IEnumerable<MfiMinimunReserveRequirement> GetMrrCsvList(string fileName)
        {
            // Initialize a list to store the MfiMinimunReserveRequirement objects
            var list = new List<MfiMinimunReserveRequirement>();

            // Initialize a list to store the raw records of bad data
            var badRecods = new List<string>();

            // Initialize a flag to track if a record is bad
            bool isRecordBad = false;

            // Configure the CSV reader
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Delimiter = "\t", // Tab-separated values
                Mode = CsvMode.NoEscape,
                HasHeaderRecord = true, // The CSV file has a header record
                                        // Handle bad data found in the CSV file
                BadDataFound = context =>
                {
                    isRecordBad = true; // Set the flag to true
                    badRecods.Add(context.RawRecord); // Add the raw record to the bad records list
                }
            };

            // Read the CSV file
            using (var stream = File.OpenRead(fileName))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                // Read each record in the CSV file
                while (csv.Read())
                {
                    // Get the MfiMinimunReserveRequirement object for the current record
                    var record = csv.GetRecord<MfiMinimunReserveRequirement>();

                    // If the record is not bad, add it to the list
                    if (!isRecordBad)
                    {
                        list.Add(record);
                    }

                    // Reset the flag for the next record
                    isRecordBad = false;
                }

                // If there are any bad records, throw an exception
                if (badRecods.Any())
                {
                    throw new ApplicationException("Check csv file for bad records!");
                }

                // Return the list of MfiMinimunReserveRequirement objects
                return list;
            }
        }

        /// <summary>Gets the Monetary Financial Institutions update CSV.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="ApplicationException">Check csv file for bad records!</exception>
        private static IEnumerable<MfiUpdateCsv> GetMfiUpdateCsv(string fileName)
        {
            var list = new List<MfiUpdateCsv>();
            var badRecods = new List<string>();
            bool isRecordBad = false;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Delimiter = "\t",
                HasHeaderRecord = true,
                Mode = CsvMode.NoEscape,
                BadDataFound = context =>
                {
                    isRecordBad = true;
                    badRecods.Add(context.RawRecord);
                }
            };

            using (var stream = File.OpenRead(fileName))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {
                    var record = csv.GetRecord<MfiUpdateCsv>();
                    if (!isRecordBad)
                    {
                        list.Add(record);
                    }

                    isRecordBad = false;
                }
                if (badRecods.Any())
                {
                    throw new ApplicationException("Check csv file for bad records!");
                }
                return list;
            }
        }

        /// <summary>
        /// Gets the Monetary Financial Institutions (MFIs) subject to the Eurosystem's minimum reserve requirement (monthly data) update CSV.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Returns a list of MfiMinimunReserveRequirementUpdate objects.
        /// Throws an ApplicationException if there are any bad records in the CSV file.
        /// </returns>
        /// <exception cref="ApplicationException">Check csv file for bad records!</exception>
        private static IEnumerable<MfiMinimunReserveRequirementUpdate> GetMfiMrrUpdateCsv(string fileName)
        {
            // Initialize a list to store the MfiMinimunReserveRequirementUpdate objects
            var list = new List<MfiMinimunReserveRequirementUpdate>();

            // Initialize a list to store the raw records of bad data
            var badRecods = new List<string>();

            // Initialize a flag to track if a record is bad
            bool isRecordBad = false;

            // Configure the CSV reader
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Delimiter = "\t", // Tab-separated values
                Mode = CsvMode.NoEscape,
                HasHeaderRecord = true, // The CSV file has a header record
                                        // Handle bad data found in the CSV file
                BadDataFound = context =>
                {
                    isRecordBad = true; // Set the flag to true
                    badRecods.Add(context.RawRecord); // Add the raw record to the bad records list
                }
            };

            // Read the CSV file
            using (var stream = File.OpenRead(fileName))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                // Read each record in the CSV file
                while (csv.Read())
                {
                    // Get the MfiMinimunReserveRequirementUpdate object for the current record
                    var record = csv.GetRecord<MfiMinimunReserveRequirementUpdate>();

                    // If the record is not bad, add it to the list
                    if (!isRecordBad)
                    {
                        list.Add(record);
                    }

                    // Reset the flag for the next record
                    isRecordBad = false;
                }

                // If there are any bad records, throw an exception
                if (badRecods.Any())
                {
                    throw new ApplicationException("Check csv file for bad records!");
                }

                // Return the list of MfiMinimunReserveRequirementUpdate objects
                return list;
            }
        }


        // Download Current CSV from https://www.ecb.europa.eu/stats/financial_corporations/list_of_financial_institutions/html/daily_list-MID.en.html
        /// <summary>Downloads the current CSV asynchronous.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="Exception">Website has changed. Check for Full Database and Update table row description!</exception>
        private async Task<List<string>> DownloadCurrentCsvAsync(string downloadUrl)
        {
            var ecbDomain = "https://www.ecb.europa.eu";
            HttpClient client = new HttpClient();
            var requestUri = ecbDomain + downloadUrl;
            var response = await client.GetAsync(requestUri);
            var pageContents = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var fileNames = new List<string>();
            var nodes = pageDocument.DocumentNode.SelectNodes("//td");
            foreach (var item in nodes)
            {
                if (item.InnerText.EndsWith(".csv"))
                {
                    string hrefValue = item.SelectSingleNode(".//a").Attributes["href"].Value;
                    var url = ecbDomain + hrefValue;
                    var lastIndex = url.LastIndexOf("/");
                    var fileInfo = new FileInfo($"{url.Substring(lastIndex + 1)}");
                    fileNames.Add(await DownloadExtension.DownloadFileAsync(url, fileInfo).ConfigureAwait(true));
                }
            }

            if (fileNames.Count > 2)
            {
                throw new Exception("Website has changed. Check for Full Database and Update table row description!");
            }
            return fileNames;
        }
    }
}