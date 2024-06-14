using CsvHelper.Configuration.Attributes;
using System;

namespace Doit.Finance.MonetaryFinancialInstitution.CountryCentralBanks.DeutscheBundesbank
{
    public class BundesbankCsvModel
    {
        [Index(0)]
        public string Bankleitzahl { get; set; }
        [Index(1)]
        public string Merkmal { get; set; }
        [Index(2)]
        public string Bezeichnung { get; set; }
        [Index(3)]
        public string PLZ { get; set; }
        [Index(4)]
        public string Ort { get; set; }
        [Index(5)]
        public string Kurzbezeichnung { get; set; }
        [Index(6)]
        public string PAN { get; set; }
        [Index(7)]
        public string BIC { get; set; }
        [Index(8)]
        public string Prüfzifferberechnungsmethode { get; set; }
        [Index(9)]
        public string Datensatznummer { get; set; }
        [Index(10)]
        public string Änderungskennzeichen { get; set; }
        [Index(11)]
        public string Bankleitzahllöschung { get; set; }
        [Index(12)]
        public string Nachfolge_Bankleitzahl { get; set; }
    }
}
