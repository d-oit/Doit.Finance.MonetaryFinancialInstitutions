using Doit.Finance.MonetaryFinancialInstitutions;
using Shouldly;
using System.Linq;
using Xunit;

namespace Do.Finance.MonetaryFinancialInstitutions.Tests
{
    public class MonetaryFinancialInstitutionTestsnuget
    {
        [Fact]
        public void GetUpdatedList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var monetaryFinancialInstitution = new MonetaryFinancialInstitution();

            // Act
            var result = monetaryFinancialInstitution.GetMfiUpdateList();
            result.Count().ShouldBe(7);
        }

        [Fact]
        public async System.Threading.Tasks.Task DownloadAndGetCurrentMrrList_StateUnderTest_ExpectedBehaviorAsync()
        {
            // Arrange
            var monetaryFinancialInstitution = new MonetaryFinancialInstitution();

            var defaultCsvFileName = monetaryFinancialInstitution.CsvMfi_Mrr_FileName;
            var defaultCsvUpdateFileName = monetaryFinancialInstitution.CsvMfi_Mrr_UpdateFileName;

            // Act
            var result = await monetaryFinancialInstitution.GetCurentMrrListAsync(true).ConfigureAwait(true);
            result.Count().ShouldBeGreaterThan(1000);

            monetaryFinancialInstitution.CsvMfi_Mrr_FileName.ShouldNotBe(defaultCsvFileName);
            monetaryFinancialInstitution.CsvMfi_Mrr_UpdateFileName.ShouldNotBe(defaultCsvUpdateFileName);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCurrentMrrList_StateUnderTest_ExpectedBehaviorAsync()
        {
            // Arrange
            var monetaryFinancialInstitution = new MonetaryFinancialInstitution();

            var defaultCsvFileName = monetaryFinancialInstitution.CsvMfi_Mrr_FileName;
            var defaultCsvUpdateFileName = monetaryFinancialInstitution.CsvMfi_Mrr_UpdateFileName;
            // Act
            var result = await monetaryFinancialInstitution.GetCurentMrrListAsync(false).ConfigureAwait(true);
            result.Count().ShouldBe(4387);

            monetaryFinancialInstitution.CsvMfi_Mrr_FileName.ShouldBe(defaultCsvFileName);
            monetaryFinancialInstitution.CsvMfi_Mrr_UpdateFileName.ShouldBe(defaultCsvUpdateFileName);
        }

        [Fact]
        public async System.Threading.Tasks.Task DownloadAndGetCurrentList_StateUnderTest_ExpectedBehaviorAsync()
        {
            // Arrange
            var monetaryFinancialInstitution = new MonetaryFinancialInstitution();

            var defaultCsvFileName = monetaryFinancialInstitution.CsvMfiFileName;
            var defaultCsvUpdateFileName = monetaryFinancialInstitution.CsvMfiUpdateFileName;

            // Act
            var result = await monetaryFinancialInstitution.GetCurentListAsync(true).ConfigureAwait(true);
            result.Count().ShouldBeGreaterThan(1000);

            monetaryFinancialInstitution.CsvMfiFileName.ShouldNotBe(defaultCsvFileName);
            monetaryFinancialInstitution.CsvMfiUpdateFileName.ShouldNotBe(defaultCsvUpdateFileName);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCurrentList_StateUnderTest_ExpectedBehaviorAsync()
        {
            // Arrange
            var monetaryFinancialInstitution = new MonetaryFinancialInstitution();

            var defaultCsvFileName = monetaryFinancialInstitution.CsvMfiFileName;
            var defaultCsvUpdateFileName = monetaryFinancialInstitution.CsvMfiUpdateFileName;
            // Act
            var result = await monetaryFinancialInstitution.GetCurentListAsync(false).ConfigureAwait(true);
            result.Count().ShouldBe(5577);

            monetaryFinancialInstitution.CsvMfiFileName.ShouldBe(defaultCsvFileName);
            monetaryFinancialInstitution.CsvMfiUpdateFileName.ShouldBe(defaultCsvUpdateFileName);
        }

        [Fact]
        public void GetList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var monetaryFinancialInstitution = new MonetaryFinancialInstitution();

            // Act
            var result = monetaryFinancialInstitution.GetMfiList();
            result.Count().ShouldBe(5577);
        }

    }
}