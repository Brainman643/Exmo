using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Exmo.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Exmo.Tests
{
    public sealed class PublicApiTests : IDisposable
    {
        private readonly FakeHttpMessageHandler _fakeHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly IPublicApi _publicApi;

        public PublicApiTests()
        {
            _fakeHttpMessageHandler = new FakeHttpMessageHandler();
            _httpClient = new HttpClient(_fakeHttpMessageHandler, true);
            var apiClient = new ApiClient(Options.Create(new ExmoOptions()), _httpClient, new NullLogger<ApiClient>());
            _publicApi = new PublicApi(apiClient);
        }

        [Fact]
        public async Task GetTrades()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form => Assert.Equal("BTC_USD,ETH_USD", form["pair"]));
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"BTC_USD\":[{\"trade_id\":158710427,\"type\":\"sell\",\"quantity\":\"0.0831\",\"price\":\"6653.1417\",\"amount\":\"552.87607527\",\"date\":1584995002}],\"ETH_USD\":[{\"trade_id\":158710264,\"type\":\"buy\",\"quantity\":\"2.3\",\"price\":\"137.56743\",\"amount\":\"316.405089\",\"date\":1584994920}]}")
            };

            var pairs = new PairCollection("BTC_USD", "ETH_USD");
            var trades = await _publicApi.GetTradesAsync(pairs);

            Assert.Equal(new Pair[] { "BTC_USD", "ETH_USD" }, trades.Keys);
            var btcUsd = trades["BTC_USD"];
            Assert.Equal(158710427, btcUsd[0].TradeId);
            Assert.Equal(TradeType.Sell, btcUsd[0].Type);
            Assert.Equal(0.0831m, btcUsd[0].Quantity);
            Assert.Equal(6653.1417m, btcUsd[0].Price);
            Assert.Equal(552.87607527m, btcUsd[0].Amount);
            Assert.Equal(1584995002, btcUsd[0].Date.ToUnixTimeSeconds());
            var ethUsd = trades["ETH_USD"];
            Assert.Equal(158710264, ethUsd[0].TradeId);
            Assert.Equal(TradeType.Buy, ethUsd[0].Type);
            Assert.Equal(2.3m, ethUsd[0].Quantity);
            Assert.Equal(137.56743m, ethUsd[0].Price);
            Assert.Equal(316.405089m, ethUsd[0].Amount);
            Assert.Equal(1584994920, ethUsd[0].Date.ToUnixTimeSeconds());
        }

        [Fact]
        public async Task GetTicker()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"BTC_USD\":{\"buy_price\":\"6670.69590001\",\"sell_price\":\"6677.42\",\"last_trade\":\"6674.7568\",\"high\":\"6987.83212894\",\"low\":\"6120.000001\",\"avg\":\"6491.6380006\",\"vol\":\"630.08942001\",\"vol_curr\":\"4199592.1712305\",\"updated\":1584996175},\"ETH_USD\":{\"buy_price\":\"137.9\",\"sell_price\":\"138.0945098\",\"last_trade\":\"138.09451\",\"high\":\"145.00000061\",\"low\":\"128.42143678\",\"avg\":\"135.36235823\",\"vol\":\"10427.77085371\",\"vol_curr\":\"1439845.63966155\",\"updated\":1584996175}}")
            };

            var ticker = await _publicApi.GetTickerAsync();

            Assert.Equal(new Pair[] { "BTC_USD", "ETH_USD" }, ticker.Keys);
            var btcUsd = ticker["BTC_USD"];
            Assert.Equal(6670.69590001m, btcUsd.BuyPrice);
            Assert.Equal(6677.42m, btcUsd.SellPrice);
            Assert.Equal(6674.7568m, btcUsd.LastTrade);
            Assert.Equal(6987.83212894m, btcUsd.High);
            Assert.Equal(6120.000001m, btcUsd.Low);
            Assert.Equal(6491.6380006m, btcUsd.Avg);
            Assert.Equal(630.08942001m, btcUsd.Volume);
            Assert.Equal(4199592.1712305m, btcUsd.VolumeCurrent);
            Assert.Equal(1584996175, btcUsd.Updated.ToUnixTimeSeconds());
        }

        [Fact]
        public async Task GetCurrencies()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[\"EXM\",\"USD\",\"EUR\",\"RUB\",\"PLN\",\"TRY\",\"UAH\",\"KZT\",\"BTC\",\"LTC\",\"DOGE\",\"DASH\",\"ETH\",\"WAVES\",\"ZEC\",\"USDT\",\"XMR\",\"XRP\",\"KICK\",\"ETC\",\"BCH\",\"BTG\",\"EOS\",\"BTCZ\",\"DXT\",\"XLM\",\"MNX\",\"OMG\",\"TRX\",\"ADA\",\"INK\",\"NEO\",\"GAS\",\"ZRX\",\"GNT\",\"GUSD\",\"LSK\",\"XEM\",\"SMART\",\"QTUM\",\"HB\",\"DAI\",\"MKR\",\"MNC\",\"PTI\",\"ATMCASH\",\"ETZ\",\"USDC\",\"ROOBEE\",\"DCR\",\"XTZ\",\"ZAG\",\"BTT\",\"VLX\",\"HP\",\"CRON\",\"ONT\",\"ONG\"]")
            };
            var expected = new[] { "EXM", "USD", "EUR", "RUB", "PLN", "TRY", "UAH", "KZT", "BTC", "LTC", "DOGE", "DASH", "ETH", "WAVES", "ZEC", "USDT", "XMR", "XRP", "KICK", "ETC", "BCH", "BTG", "EOS", "BTCZ", "DXT", "XLM", "MNX", "OMG", "TRX", "ADA", "INK", "NEO", "GAS", "ZRX", "GNT", "GUSD", "LSK", "XEM", "SMART", "QTUM", "HB", "DAI", "MKR", "MNC", "PTI", "ATMCASH", "ETZ", "USDC", "ROOBEE", "DCR", "XTZ", "ZAG", "BTT", "VLX", "HP", "CRON", "ONT", "ONG" };

            var currencies = await _publicApi.GetCurrenciesAsync();

            Assert.Equal(expected, currencies);
        }

        [Fact]
        public async Task GetPairSettings()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"BTC_USD\":{\"min_quantity\":\"0.0001\",\"max_quantity\":\"1000\",\"min_price\":\"1\",\"max_price\":\"30000\",\"max_amount\":\"500000\",\"min_amount\":\"1\",\"price_precision\":8,\"commission_taker_percent\":\"0.2\",\"commission_maker_percent\":\"0.2\"},\"ETH_USD\":{\"min_quantity\":\"0.001\",\"max_quantity\":\"5000\",\"min_price\":\"0.01\",\"max_price\":\"100000\",\"max_amount\":\"500000\",\"min_amount\":\"3\",\"price_precision\":8,\"commission_taker_percent\":\"0.2\",\"commission_maker_percent\":\"0.2\"}}")
            };

            var pairSettings = await _publicApi.GetPairSettingsAsync();

            Assert.Equal(new Pair[] { "BTC_USD", "ETH_USD" }, pairSettings.Keys);
            var btcUsd = pairSettings["BTC_USD"];
            Assert.Equal(0.0001m, btcUsd.MinQuantity);
            Assert.Equal(1000m, btcUsd.MaxQuantity);
            Assert.Equal(1m, btcUsd.MinPrice);
            Assert.Equal(30000m, btcUsd.MaxPrice);
            Assert.Equal(500000m, btcUsd.MaxAmount);
            Assert.Equal(1m, btcUsd.MinAmount);
            Assert.Equal(8, btcUsd.PricePrecision);
            Assert.Equal(0.2m, btcUsd.CommissionTakerPercent);
            Assert.Equal(0.2m, btcUsd.CommissionMakerPercent);
        }

        [Fact]
        public async Task GetOrderBook()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("BTC_USD,ETH_USD", form["pair"]);
                Assert.Equal("100", form["limit"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"BTC_USD\":{\"ask_quantity\":\"235.41991633\",\"ask_amount\":\"3088819.24758049\",\"ask_top\":\"6610\",\"bid_quantity\":\"12611.67298966\",\"bid_amount\":\"893817.34468224\",\"bid_top\":\"6606.44420123\",\"ask\":[[\"6610\",\"0.00810449\",\"53.5706789\"],[\"6618.219999\",\"0.00035049\",\"2.31961992\"]],\"bid\":[[\"6606.44420123\",\"0.69964951\",\"4622.19544823\"],[\"6606.44420023\",\"0.035\",\"231.225547\"]]},\"ETH_USD\":{\"ask_quantity\":\"4478.66555066\",\"ask_amount\":\"3220720.74282515\",\"ask_top\":\"137.59999989\",\"bid_quantity\":\"12463.56016804\",\"bid_amount\":\"248035.82535663\",\"bid_top\":\"136.83096645\",\"ask\":[[\"137.59999989\",\"28\",\"3852.79999692\"],[\"137.5999999\",\"0.22542189\",\"31.01805204\"]],\"bid\":[[\"136.83096645\",\"2.648\",\"362.32839915\"],[\"136.83096644\",\"4\",\"547.32386576\"]]}}")
            };

            var request = new OrderBookRequest
            {
                Pairs = new PairCollection("BTC_USD", "ETH_USD"),
                Limit = 100
            };
            var orderBook = await _publicApi.GetOrderBookAsync(request);

            Assert.Equal(new Pair[] { "BTC_USD", "ETH_USD" }, orderBook.Keys);
            var btcUsd = orderBook["BTC_USD"];
            Assert.Equal(235.41991633m, btcUsd.AskQuantity);
            Assert.Equal(3088819.24758049m, btcUsd.AskAmount);
            Assert.Equal(6610m, btcUsd.AskTop);
            Assert.Equal(12611.67298966m, btcUsd.BidQuantity);
            Assert.Equal(893817.34468224m, btcUsd.BidAmount);
            Assert.Equal(6606.44420123m, btcUsd.BidTop);
            Assert.Equal(6610m, btcUsd.Ask[0].Price);
            Assert.Equal(0.00810449m, btcUsd.Ask[0].Quantity);
            Assert.Equal(53.5706789m, btcUsd.Ask[0].Amount);
            Assert.Equal(6618.219999m, btcUsd.Ask[1].Price);
            Assert.Equal(0.00035049m, btcUsd.Ask[1].Quantity);
            Assert.Equal(2.31961992m, btcUsd.Ask[1].Amount);
            Assert.Equal(6606.44420123m, btcUsd.Bid[0].Price);
            Assert.Equal(0.69964951m, btcUsd.Bid[0].Quantity);
            Assert.Equal(4622.19544823m, btcUsd.Bid[0].Amount);
            Assert.Equal(6606.44420023m, btcUsd.Bid[1].Price);
            Assert.Equal(0.035m, btcUsd.Bid[1].Quantity);
            Assert.Equal(231.225547m, btcUsd.Bid[1].Amount);
            var ethUsd = orderBook["ETH_USD"];
            Assert.Equal(4478.66555066m, ethUsd.AskQuantity);
            Assert.Equal(3220720.74282515m, ethUsd.AskAmount);
            Assert.Equal(137.59999989m, ethUsd.AskTop);
            Assert.Equal(12463.56016804m, ethUsd.BidQuantity);
            Assert.Equal(248035.82535663m, ethUsd.BidAmount);
            Assert.Equal(136.83096645m, ethUsd.BidTop);
            Assert.Equal(137.59999989m, ethUsd.Ask[0].Price);
            Assert.Equal(28m, ethUsd.Ask[0].Quantity);
            Assert.Equal(3852.79999692m, ethUsd.Ask[0].Amount);
            Assert.Equal(137.5999999m, ethUsd.Ask[1].Price);
            Assert.Equal(0.22542189m, ethUsd.Ask[1].Quantity);
            Assert.Equal(31.01805204m, ethUsd.Ask[1].Amount);
            Assert.Equal(136.83096645m, ethUsd.Bid[0].Price);
            Assert.Equal(2.648m, ethUsd.Bid[0].Quantity);
            Assert.Equal(362.32839915m, ethUsd.Bid[0].Amount);
            Assert.Equal(136.83096644m, ethUsd.Bid[1].Price);
            Assert.Equal(4m, ethUsd.Bid[1].Quantity);
            Assert.Equal(547.32386576m, ethUsd.Bid[1].Amount);
        }

        [Fact]
        public async Task CalculateRequiredAmount()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("BTC_USD", form["pair"]);
                Assert.Equal("0.01", form["quantity"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"quantity\":\"0.01\",\"amount\":\"69.07831279\",\"avg_price\":\"6907.831279\"}")
            };

            var request = new RequiredAmountRequest
            {
                Pair = "BTC_USD",
                Quantity = 0.01m
            };
            var result = await _publicApi.CalculateRequiredAmountAsync(request);

            Assert.Equal(0.01m, result.Quantity);
            Assert.Equal(69.07831279m, result.Amount);
            Assert.Equal(6907.831279m, result.AveragePrice);
        }

        [Fact]
        public async Task GetCandlesHistory()
        {
            const int fromSeconds = 1587844151;
            const int toSeconds = 1587844532;

            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleQueryString(form =>
            {
                Assert.Equal("BTC_USD", form["symbol"]);
                Assert.Equal("5", form["resolution"]);
                Assert.Equal(fromSeconds.ToString(CultureInfo.InvariantCulture), form["from"]);
                Assert.Equal(toSeconds.ToString(CultureInfo.InvariantCulture), form["to"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"candles\":[{\"t\":1587844200000,\"o\":7546.6259,\"c\":7540.4937,\"h\":7546.6259,\"l\":7539.06212565,\"v\":0.46677045000000006},{\"t\":1587844500000,\"o\":7543.4013,\"c\":7543.3275,\"h\":7545,\"l\":7539.53903772,\"v\":0.65390688}]}")
            };

            var request = new CandlesHistoryRequest
            {
                Pair = "BTC_USD",
                Resolution = Resolution.Minute5,
                From = DateTimeOffset.FromUnixTimeSeconds(fromSeconds),
                To = DateTimeOffset.FromUnixTimeSeconds(toSeconds),
            };
            var candles = await _publicApi.GetCandlesHistoryAsync(request);

            Assert.Equal(2, candles.Length);
            Assert.Equal(1587844200000, candles[0].Time.ToUnixTimeMilliseconds());
            Assert.Equal(7546.6259m, candles[0].Open);
            Assert.Equal(7540.4937m, candles[0].Close);
            Assert.Equal(7546.6259m, candles[0].High);
            Assert.Equal(7539.06212565m, candles[0].Low);
            Assert.Equal(0.46677045000000006m, candles[0].Volume);
            Assert.Equal(1587844500000, candles[1].Time.ToUnixTimeMilliseconds());
            Assert.Equal(7543.4013m, candles[1].Open);
            Assert.Equal(7543.3275m, candles[1].Close);
            Assert.Equal(7545m, candles[1].High);
            Assert.Equal(7539.53903772m, candles[1].Low);
            Assert.Equal(0.65390688m, candles[1].Volume);
        }

        [Theory]
        [InlineData("{\"result\":false,\"error\":\"Error 40013\"}", 40013, "")]
        [InlineData("{\"s\":\"error\",\"errmsg\":\"range period is too long: maximum 3000 candles allowed\"}", -1, "range period is too long: maximum 3000 candles allowed")]
        public async Task GetCandlesHistory_ErrorResponse_ThrowsExmoApiException(string response, int code, string message)
        {
            const int fromSeconds = 1587844151;
            const int toSeconds = 1587844532;

            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(response)
            };

            var request = new CandlesHistoryRequest
            {
                Pair = "BTC_USD",
                Resolution = Resolution.Minute5,
                From = DateTimeOffset.FromUnixTimeSeconds(fromSeconds),
                To = DateTimeOffset.FromUnixTimeSeconds(toSeconds),
            };
            var exception = await Assert.ThrowsAsync<ExmoApiException>(async () => await _publicApi.GetCandlesHistoryAsync(request));
            Assert.Equal(code, exception.Code);
            Assert.Equal(message, exception.Message);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
