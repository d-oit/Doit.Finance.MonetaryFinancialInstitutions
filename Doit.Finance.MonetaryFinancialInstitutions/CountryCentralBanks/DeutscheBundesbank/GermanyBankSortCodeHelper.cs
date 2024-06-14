using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using Doit.Finance.MonetaryFinancialInstitution.CountryCentralBanks.DeutscheBundesbank;
using Doit.Finance.MonetaryFinancialInstitutions.CountryCentralBanks;
using Doit.Finance.MonetaryFinancialInstitutions.Extensions;
using Doit.Finance.MonetaryFinancialInstitutions.Model;
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
    // Check https://www.bundesbank.de/en/homepage/terms-of-use
    public class GermanyBankSortCodeHelper : INationalBankSortCode
    {
        private const string DownloadPageDomain = "https://www.bundesbank.de";
        private const string DownloadFileName = "blz-aktuell-csv-data.csv";

        public const string DownloadPageUrl = DownloadPageDomain + "/en/tasks/payment-systems/services/bank-sort-codes/download-bank-sort-codes-626218";

        [Obsolete("Use CsvFileGermanBankSortCode instead.")]
        public string ExcelFileGermanBankSortCode { get; private set; } = "blz-neu-xls-data.xlsx";

        public string CsvFileGermanBankSortCode { get; private set; } = "blz-aktuell-csv-data.csv";

        /// <summary>
        /// Retrieves the current list of German bank sort codes.
        /// </summary>
        /// <param name="download">A flag indicating whether to download the latest CSV file from the Bundesbank website. Default is true.</param>
        /// <returns>A list of BankAccount objects containing the current German bank sort codes.</returns>
        /// <exception cref="ApplicationException">Thrown when bad records are found in the CSV file.</exception>
        /// <exception cref="Exception">Thrown when the website has changed and the href containing the download file name is not found.</exception>
        public async Task<List<BankAccount>> GetCurrentBankSortList(bool download = true)
        {
            // If download is true, download the latest CSV file from the Bundesbank website
            if (download)
            {
                // Update the CsvFileGermanBankSortCode property with the downloaded file name
                CsvFileGermanBankSortCode = await DownloadFileAsync(DownloadPageUrl);
            }

            // Return the list of BankAccount objects obtained from the CSV file
            return GetListFromCsv();
        }

        /// <summary>
        /// Retrieves a list of BankAccount objects from the CSV file.
        /// </summary>
        /// <returns>A list of BankAccount objects.</returns>
        /// <exception cref="ApplicationException">Thrown when bad records are found in the CSV file.</exception>
        private List<BankAccount> GetListFromCsv()
        {
            var badRecods = new List<string>();
            var list = new List<BankAccount>();
            bool isRecordBad = false;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Delimiter = ";",
                HasHeaderRecord = true,
                Mode = CsvMode.RFC4180,
                BadDataFound = context =>
                {
                    isRecordBad = true;
                    badRecods.Add(context.RawRecord);
                }
            };

            using (var stream = File.OpenRead(CsvFileGermanBankSortCode))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {
                    var record = csv.GetRecord<BundesbankCsvModel>();
                    if (!isRecordBad)
                    {
                        // ignore old records
                        if (!record.Bezeichnung.Contains("-alt-"))
                        {
                            list.Add(new BankAccount {
                                Name = record.Bezeichnung,
                                ShortName = record.Kurzbezeichnung,
                                BankCode = record.Bankleitzahl,
                                BIC = record.BIC,
                                Postal = record.PLZ,
                                City = record.Ort,
                                Country = "DE"
                            });
                        }
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

        [Obsolete("Version 1.0.4 > Use GetListFromCsv instead.")]
        private List<BankAccount> GetListFromExcel()
        {
            var wb = new XLWorkbook(ExcelFileGermanBankSortCode);
            var ws = wb.Worksheet(1);

            // Look for the first row used
            var firstRowUsed = ws.FirstRowUsed();

            var firstPossidbleUsed = firstRowUsed.FirstCell().Address;
            // Last possible address of the company table:
            var lastPossibleUsed = ws.LastCellUsed().Address;

            // Get a range with the remainder of the worksheet data (the range used)
            var bankCodeRange = ws.Range(firstPossidbleUsed, lastPossibleUsed).RangeUsed();

            var table = bankCodeRange.AsTable();
            // Treat the range as a table (to be able to use the column names)
            var list = table.DataRange.Rows()
               .Select(row =>
                new BankAccount {
                    Name = row.Field(2).Value.ToString(),
                    ShortName = row.Field(5).Value.ToString(),
                    BankCode = row.Field(0).Value.ToString(),
                    BIC = row.Field(7).Value.ToString(),
                    Postal = row.Field(3).Value.ToString(),
                    City = row.Field(4).Value.ToString(),
                    Country = "DE"
                }
            ).ToList();

            // remove old records
            list = list.Where(x => !x.Name.Contains("-alt-")).ToList();
            wb.Dispose();

            return list;
        }

        /// <summary>
        /// Downloads the latest CSV file from the Bundesbank website.
        /// </summary>
        /// <param name="url">The URL of the Bundesbank website containing the download link.</param>
        /// <returns>The name of the downloaded CSV file.</returns>
        /// <exception cref="Exception">Thrown when the website has changed and the href containing the download file name is not found.</exception>
        public async Task<string> DownloadFileAsync(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var fileName = string.Empty;
            var nodes = pageDocument.DocumentNode.SelectNodes("(//a[contains(@class,'collection__link linklist__link linklist__link--blocklist')])");
            foreach (var item in nodes)
            {
                string hrefValue = item.GetAttributeValue("href", string.Empty);
                if (hrefValue.Contains(DownloadFileName))
                {
                    var downloadUrl = hrefValue;
                    var lastIndex = downloadUrl.LastIndexOf("/");
                    var fileInfo = new FileInfo($"{downloadUrl.Substring(lastIndex + 1)}");
                    fileName = await DownloadExtension.DownloadFileAsync(downloadUrl, fileInfo);
                    break; // download only first found 
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("Website has changed. Check for a href contains '" + DownloadFileName + "'!");
            }
            return fileName;
        }
    }
}
