using Doit.Finance.MonetaryFinancialInstitutions;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Do.Finance.MonetaryFinancialInstitutions.Tests.CountryCentralBanks.DeutscheBundesbank
{
    public class GermanySortCodeHelperTests
    {
        [Fact]
        public async Task GetCurrentGermanBankSortList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var germanySortCodeHelper = new GermanyBankSortCodeHelper();
            bool download = false;
            // Act
            var result = await germanySortCodeHelper.GetCurrentBankSortList(
                download);

            result.Count.ShouldBeGreaterThan(1000);
        }

        [Fact]
        public async Task DownloadFileAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var germanySortCodeHelper = new GermanyBankSortCodeHelper();
            var result = await germanySortCodeHelper.GetCurrentBankSortList(download: true);

            result.ShouldNotBe(null);
        }
    }
}