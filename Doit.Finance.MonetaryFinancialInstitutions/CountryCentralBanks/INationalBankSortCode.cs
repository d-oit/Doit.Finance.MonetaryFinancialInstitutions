using Doit.Finance.MonetaryFinancialInstitutions.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Doit.Finance.MonetaryFinancialInstitutions.CountryCentralBanks
{
    public interface INationalBankSortCode
    {
        Task<string> DownloadFileAsync(string url);
        Task<List<BankAccount>> GetCurrentBankSortList(bool download = true);
    }
}
