using System.ComponentModel.DataAnnotations;

namespace Doit.Finance.MonetaryFinancialInstitutions.Model
{
    public class BankAccount
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string Box { get; set; }
        public string Postal { get; set; }
        public string City { get; set; }
        [MaxLength(2), MinLength(2)]
        public string Country { get; set; }
        public string BIC { get; set; }
        public string BankCode { get; set; }
    }
}
