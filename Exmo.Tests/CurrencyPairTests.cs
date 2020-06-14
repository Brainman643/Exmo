using System;
using Exmo.Models;
using Xunit;

namespace Exmo.Tests
{
    public class CurrencyPairTests
    {
        [Fact]
        public void Costructor_ReturnsCurrencyPairInstance()
        {
            var pair = new CurrencyPair("BTC", "USD");

            Assert.Equal("BTC", pair.BaseCurrency);
            Assert.Equal("USD", pair.QuoteCurrency);
        }

        [Fact]
        public void Parse_ReturnsCurrencyPairInstance()
        {
            var pair = CurrencyPair.Parse("BTC_USD");

            Assert.Equal("BTC", pair.BaseCurrency);
            Assert.Equal("USD", pair.QuoteCurrency);
        }

        [Theory]
        [InlineData("BTC")]
        [InlineData("BTC_")]
        public void Parse_ValueIsInvalid_ThrowsFormatException(string str)
        {
            Assert.Throws<FormatException>(() => CurrencyPair.Parse(str));
        }

        [Fact]
        public void GetHashCode_TwoIdenticalCurrencyPairs_AreEqual()
        {
            var pair1 = new CurrencyPair("BTC", "USD");
            var pair2 = new CurrencyPair("BTC", "USD");

            Assert.Equal(pair1.GetHashCode(), pair2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_TwoDifferentCurrencyPairs_AreNotEqual()
        {
            var pair1 = new CurrencyPair("BTC", "USD");
            var pair2 = new CurrencyPair("ETH", "USD");

            Assert.NotEqual(pair1.GetHashCode(), pair2.GetHashCode());
        }

        [Fact]
        public void Equals_TwoIdenticalCurrencyPairs_AreEqual()
        {
            var pair1 = new CurrencyPair("BTC", "USD");
            var pair2 = new CurrencyPair("BTC", "USD");

            Assert.Equal(pair1, pair2);
        }

        [Fact]
        public void Equals_WithNull_AreNotEqual()
        {
            var pair = new CurrencyPair("BTC", "USD");

            Assert.False(pair.Equals(null));
        }

        [Fact]
        public void EqualOperator_WithIdenticalCurrencyPairString_AreEqual()
        {
            var pair = new CurrencyPair("BTC", "USD");

            Assert.True(pair == "BTC_USD");
            Assert.True("BTC_USD" == pair);
        }

        [Fact]
        public void EqualOperator_WithDifferentCurrencyPairString_AreNotEqual()
        {
            var pair = new CurrencyPair("BTC", "USD");

            Assert.False(pair == "ETH_USD");
            Assert.False("ETH_USD" == pair);
        }

        [Fact]
        public void ImplicitOperator_ReturnsCurrencyPairInstance()
        {
            CurrencyPair pair = "BTC_USD";

            Assert.Equal("BTC", pair.BaseCurrency);
            Assert.Equal("USD", pair.QuoteCurrency);
        }

        [Theory]
        [InlineData("BTC")]
        [InlineData("BTC_")]
        public void ImplicitOperator_ValueIsInvalid_ThrowsFormatException(string str)
        {
            Assert.Throws<FormatException>(() => (CurrencyPair)str);
        }

        [Fact]
        public void ToString_ReturnsCurrencyPairString()
        {
            var pair = new CurrencyPair("BTC", "USD");

            Assert.Equal("BTC_USD", pair.ToString());
        }

        [Fact]
        public void ExplicitOperator_ReturnsString()
        {
            var pair = new CurrencyPair("BTC", "USD");
            var str = (string)pair;

            Assert.Equal("BTC_USD", str);
        }

        [Fact]
        public void ImplicitOperator_WithNull_ReturnsNull()
        {
            CurrencyPair pair = (string)null;

            Assert.Null(pair);
        }

        [Fact]
        public void ExplicitOperator_WithNull_ReturnsNull()
        {
            CurrencyPair pair = null;
            var str = (string)pair;

            Assert.Null(str);
        }
    }
}
