using ClosedXML.Excel;
using Doit.Finance.MonetaryFinancialInstitutions.CountryCentralBanks;
using Doit.Finance.MonetaryFinancialInstitutions.Extensions;
using Doit.Finance.MonetaryFinancialInstitutions.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Doit.Finance.MonetaryFinancialInstitutions
{
    // Check https://www.bundesbank.de/en/homepage/terms-of-use
    public class GermanySortCodeHelper : INationalBankSortCode
    {
        private const string DownloadPageDomain = "https://www.bundesbank.de";
        private const string DownloadFileName = "blz-neu-xls-data.xlsx";

        public const string DownloadPageUrl = DownloadPageDomain + "/en/tasks/payment-systems/services/bank-sort-codes/download-bank-sort-codes-626218";
        public string ExcelFileGermanBankSortCode { get; private set; } = "blz-neu-xls-data.xlsx";

        public async Task<List<BankAccount>> GetCurrentBankSortList(bool download = true)
        {
            if (download)
            {
                ExcelFileGermanBankSortCode = await DownloadFileAsync(DownloadPageUrl);
            }

            return GetListFromExcel();
        }

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
                new BankAccount 
                { 
                    Name = row.Field(3).Value.ToString(), 
                    ShortName = row.Field(6).Value.ToString(),
                    BankCode = row.Field(1).Value.ToString(),
                    BIC = row.Field(8).Value.ToString(),
                    Postal = row.Field(4).Value.ToString(),
                    City = row.Field(5).Value.ToString(),
                }
            ).ToList();

            // remove old records
            list = list.Where(x => !x.Name.Contains("-alt-")).ToList();

            return list;
        }

        public async Task<string> DownloadFileAsync(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var fileName = "";
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
                    break; // download only first found excel
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("Website has changed. Check for a href contains 'blz-aktuell-xls-data.xlsx'!");
            }
            return fileName;
        }
    }
}
