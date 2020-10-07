using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doit.Finance.MonetaryFinancialInstitutions
{
    public interface IMonetaryFinancialInstitution
    {
        Task<IEnumerable<MfiCsv>> GetCurentListAsync(bool downloadCsv = false);

        Task<IEnumerable<MfiMinimunReserveRequirement>> GetCurentMrrListAsync(bool downloadCsv = false);
    }
}