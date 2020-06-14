using Exmo.Models;
using Xunit;

namespace Exmo.Tests
{
    public class PairCollectionTests
    {
        [Fact]
        public void DefaultCostructor_CreatesEmptyPairCollection()
        {
            var pairs = new CurrencyPairCollection();

            Assert.Empty(pairs);
        }

        [Fact]
        public void ToString_ReturnsPairsString()
        {
            var pairs = new CurrencyPairCollection("BTC_USD", "ETH_USD");

            Assert.Equal("BTC_USD,ETH_USD", pairs.ToString());
        }
    }
}
