namespace Doit.Finance.MonetaryFinancialInstitutions
{
    public class MfiMinimunReserveRequirement
    {
        public string RIAD_CODE { get; set; }
        public string BIC { get; set; }
        public string COUNTRY_OF_REGISTRATION { get; set; }
        public string NAME { get; set; }
        public string BOX { get; set; }
        public string ADDRESS { get; set; }
        public string POSTAL { get; set; }
        public string CITY { get; set; }
        public string CATEGORY { get; set; }
        public string HEAD_COUNTRY_OF_REGISTRATION { get; set; }
        public string HEAD_NAME { get; set; }
        public string HEAD_RIAD_CODE { get; set; }
        public string RESERVE { get; set; }
        public string EXEMPT { get; set; }
    }

    public class MfiMinimunReserveRequirementUpdate : MfiMinimunReserveRequirement
    {
        public string CHANGE { get; set; }
    }
}
