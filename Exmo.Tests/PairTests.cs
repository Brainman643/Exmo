using System;
using Exmo.Models;
using Xunit;

namespace Exmo.Tests
{
    public class PairTests
    {
        [Fact]
        public void Costructor_ReturnsPairInstance()
        {
            var pair = new Pair("BTC", "USD");

            Assert.Equal("BTC", pair.BuyCurrency);
            Assert.Equal("USD", pair.SellCurrency);
        }

        [Fact]
        public void Parse_ReturnsPairInstance()
        {
            var pair = Pair.Parse("BTC_USD");

            Assert.Equal("BTC", pair.BuyCurrency);
            Assert.Equal("USD", pair.SellCurrency);
        }

        [Theory]
        [InlineData("BTC")]
        [InlineData("BTC_")]
        public void Parse_ValueIsInvalid_ThrowsFormatException(string str)
        {
            Assert.Throws<FormatException>(() => Pair.Parse(str));
        }

        [Fact]
        public void GetHashCode_TwoIdenticalPairs_AreEqual()
        {
            var pair1 = new Pair("BTC", "USD");
            var pair2 = new Pair("BTC", "USD");

            Assert.Equal(pair1.GetHashCode(), pair2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_TwoDifferentPairs_AreNotEqual()
        {
            var pair1 = new Pair("BTC", "USD");
            var pair2 = new Pair("ETH", "USD");

            Assert.NotEqual(pair1.GetHashCode(), pair2.GetHashCode());
        }

        [Fact]
        public void Equals_TwoIdenticalPairs_AreEqual()
        {
            var pair1 = new Pair("BTC", "USD");
            var pair2 = new Pair("BTC", "USD");

            Assert.Equal(pair1, pair2);
        }

        [Fact]
        public void Equals_WithNull_AreNotEqual()
        {
            var pair = new Pair("BTC", "USD");

            Assert.False(pair.Equals(null));
        }

        [Fact]
        public void EqualOperator_WithIdenticalPairString_AreEqual()
        {
            var pair = new Pair("BTC", "USD");

            Assert.True(pair == "BTC_USD");
            Assert.True("BTC_USD" == pair);
        }

        [Fact]
        public void EqualOperator_WithDifferentPairString_AreNotEqual()
        {
            var pair = new Pair("BTC", "USD");

            Assert.False(pair == "ETH_USD");
            Assert.False("ETH_USD" == pair);
        }

        [Fact]
        public void ImplicitOperator_ReturnsPairInstance()
        {
            Pair pair = "BTC_USD";

            Assert.Equal("BTC", pair.BuyCurrency);
            Assert.Equal("USD", pair.SellCurrency);
        }

        [Theory]
        [InlineData("BTC")]
        [InlineData("BTC_")]
        public void ImplicitOperator_ValueIsInvalid_ThrowsFormatException(string str)
        {
            Assert.Throws<FormatException>(() => (Pair)str);
        }

        [Fact]
        public void ToString_ReturnsPairString()
        {
            var pair = new Pair("BTC", "USD");

            Assert.Equal("BTC_USD", pair.ToString());
        }

        [Fact]
        public void ExplicitOperator_ReturnsString()
        {
            var pair = new Pair("BTC", "USD");
            var str = (string)pair;

            Assert.Equal("BTC_USD", str);
        }

        [Fact]
        public void ImplicitOperator_WithNull_ReturnsNull()
        {
            Pair pair = (string)null;

            Assert.Null(pair);
        }

        [Fact]
        public void ExplicitOperator_WithNull_ReturnsNull()
        {
            Pair pair = null;
            var str = (string)pair;

            Assert.Null(str);
        }
    }
}
