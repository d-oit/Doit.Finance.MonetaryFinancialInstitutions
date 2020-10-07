using CsvHelper;
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
        public string CsvMfiFileName { get; private set; } = "mfi_csv_201001.csv";
        public string CsvMfiUpdateFileName { get; private set; } = "mfi_csv_update_201001.csv";

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
                var fileNames = await DownloadCurrentCsvAsync(CsvMfiUrl);
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
                var fileNames = await DownloadCurrentCsvAsync(CsvMfi_MrrUrl);
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
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.IgnoreQuotes = true;
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.BadDataFound = context =>
                {
                    isRecordBad = true;
                    badRecods.Add(context.RawRecord);
                };
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

        private static IEnumerable<MfiMinimunReserveRequirement> GetMrrCsvList(string fileName)
        {
            var list = new List<MfiMinimunReserveRequirement>();
            var badRecods = new List<string>();
            bool isRecordBad = false;
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.IgnoreQuotes = true;
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.BadDataFound = context =>
                {
                    isRecordBad = true;
                    badRecods.Add(context.RawRecord);
                };
                while (csv.Read())
                {
                    var record = csv.GetRecord<MfiMinimunReserveRequirement>();
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
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.IgnoreQuotes = true;
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.BadDataFound = context =>
                {
                    isRecordBad = true;
                    badRecods.Add(context.RawRecord);
                };
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

        private static IEnumerable<MfiMinimunReserveRequirementUpdate> GetMfiMrrUpdateCsv(string fileName)
        {
            var list = new List<MfiMinimunReserveRequirementUpdate>();
            var badRecods = new List<string>();
            bool isRecordBad = false;
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.IgnoreQuotes = true;
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.BadDataFound = context =>
                {
                    isRecordBad = true;
                    badRecods.Add(context.RawRecord);
                };
                while (csv.Read())
                {
                    var record = csv.GetRecord<MfiMinimunReserveRequirementUpdate>();
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
            var response = await client.GetAsync(ecbDomain + downloadUrl);
            var pageContents = await response.Content.ReadAsStringAsync();
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var fileNames = new List<string>();
            var nodes = pageDocument.DocumentNode.SelectNodes("(//a[contains(@class,'csv')])");
            foreach (var item in nodes)
            {
                string hrefValue = item.GetAttributeValue("href", string.Empty);
                var url = ecbDomain + hrefValue;
                var lastIndex = url.LastIndexOf("/");
                var fileInfo = new FileInfo($"{url.Substring(lastIndex + 1)}");
                fileNames.Add(await DownloadExtension.DownloadFileAsync(url, fileInfo));
            }

            if (fileNames.Count > 2)
            {
                throw new Exception("Website has changed. Check for Full Database and Update table row description!");
            }
            return fileNames;
        }
    }

    public enum CsvType
    {
        Mfi = 0,
        Mfi_Mrr = 1
    }
}