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
    public sealed class AuthenticatedApiTests : IDisposable
    {
        private readonly FakeHttpMessageHandler _fakeHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly IAuthenticatedApi _authenticatedApi;

        public AuthenticatedApiTests()
        {
            _fakeHttpMessageHandler = new FakeHttpMessageHandler();
            _httpClient = new HttpClient(_fakeHttpMessageHandler, true);
            var apiClient = new AuthApiClient(Options.Create(new ExmoOptions { ApiPublicKey = string.Empty, ApiSecretKey = string.Empty }), _httpClient, new NullLogger<AuthApiClient>());
            _authenticatedApi = new AuthenticatedApi(apiClient);
        }

        [Fact]
        public async Task GetUserInfo()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"uid\":745215,\"server_date\":1585036795,\"balances\":{\"EXM\":\"0\",\"USD\":\"0.57606729\",\"BTC\":\"0.0601735\"},\"reserved\":{\"EXM\":\"0\",\"USD\":\"0\",\"BTC\":\"0.0001\"}}")
            };

            var userInfo = await _authenticatedApi.GetUserInfoAsync();

            Assert.Equal(745215, userInfo.Id);
            Assert.Equal(1585036795, userInfo.ServerDate.ToUnixTimeSeconds());
            Assert.Equal(0m, userInfo.Balances["EXM"]);
            Assert.Equal(0.57606729m, userInfo.Balances["USD"]);
            Assert.Equal(0.0601735m, userInfo.Balances["BTC"]);
            Assert.Equal(0m, userInfo.Reserved["EXM"]);
            Assert.Equal(0m, userInfo.Reserved["USD"]);
            Assert.Equal(0.0001m, userInfo.Reserved["BTC"]);
        }

        [Fact]
        public async Task CreateOrder()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("BTC_USD", form["pair"]);
                Assert.Equal("buy", form["type"]);
                Assert.Equal("9700", form["price"]);
                Assert.Equal("0.001", form["quantity"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\",\"order_id\":1234567890}")
            };

            var request = new CreateOrderRequest
            {
                Pair = "BTC_USD",
                Type = OrderType.Buy,
                Quantity = 0.001m,
                Price = 9700m
            };
            var orderId = await _authenticatedApi.CreateOrderAsync(request);

            Assert.Equal(1234567890, orderId);
        }

        [Fact]
        public async Task CreateOrder_QuantityLessThanMinimum_ThrowsException()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("BTC_USD", form["pair"]);
                Assert.Equal("buy", form["type"]);
                Assert.Equal("6600", form["price"]);
                Assert.Equal("0.000001", form["quantity"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":false,\"error\":\"Error 50277: Quantity by order is less than permissible minimum for this pair\"}")
            };

            var request = new CreateOrderRequest
            {
                Pair = "BTC_USD",
                Type = OrderType.Buy,
                Quantity = 0.000001m,
                Price = 6600m
            };
            await Assert.ThrowsAsync<ExmoApiException>(() => _authenticatedApi.CreateOrderAsync(request));
        }

        [Fact]
        public async Task GetOpenOrders()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"BTC_USD\":[{\"order_id\":\"1234567890\",\"created\":\"1581874352\",\"type\":\"sell\",\"pair\":\"BTC_USD\",\"quantity\":\"0.0001\",\"price\":\"11000\",\"amount\":\"1.1\"}]}")
            };

            var openOrders = await _authenticatedApi.GetOpenOrdersAsync();

            Assert.Equal(new Pair[] { "BTC_USD" }, openOrders.Keys);
            var orders = openOrders["BTC_USD"];
            Assert.Single(orders);
            var order = orders[0];
            Assert.Equal(1234567890, order.OrderId);
            Assert.Equal(1581874352, order.Created.ToUnixTimeSeconds());
            Assert.Equal(OrderType.Sell, order.Type);
            Assert.Equal("BTC_USD", order.Pair);
            Assert.Equal(0.0001m, order.Quantity);
            Assert.Equal(11000m, order.Price);
            Assert.Equal(1.1m, order.Amount);
        }

        [Fact]
        public async Task CancelOrder()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form => Assert.Equal("123", form["order_id"]));
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\"}")
            };

            await _authenticatedApi.CancelOrderAsync(123);
        }

        [Fact]
        public async Task CancelOrder_OrderNotFound_ThrowsException()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form => Assert.Equal("1", form["order_id"]));
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":false,\"error\":\"Error 50173: Order was not found '#1'\"}")
            };

            await Assert.ThrowsAsync<ExmoApiException>(() => _authenticatedApi.CancelOrderAsync(1));
        }

        [Fact]
        public async Task GetOrderTrades()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form => Assert.Equal("1234567890", form["order_id"]));
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"type\":\"sell\",\"in_currency\":\"USD\",\"in_amount\":\"9.78264791\",\"out_currency\":\"BTC\",\"out_amount\":\"0.001\",\"trades\":[{\"trade_id\":147967350,\"date\":1581873878,\"type\":\"sell\",\"pair\":\"BTC_USD\",\"order_id\":1234567890,\"quantity\":\"0.00080824\",\"price\":\"9781.18662574\",\"amount\":\"7.90554627\"},{\"trade_id\":147967349,\"date\":1581873878,\"type\":\"sell\",\"pair\":\"BTC_USD\",\"order_id\":1234567890,\"quantity\":\"0.00019176\",\"price\":\"9788.80708007\",\"amount\":\"1.87710164\"}]}")
            };

            var orderTrades = await _authenticatedApi.GetOrderTradesAsync(1234567890);

            Assert.Equal(OrderType.Sell, orderTrades.Type);
            Assert.Equal("USD", orderTrades.InCurrency);
            Assert.Equal(9.78264791m, orderTrades.InAmount);
            Assert.Equal("BTC", orderTrades.OutCurrency);
            Assert.Equal(0.001m, orderTrades.OutAmount);
            Assert.Equal(2, orderTrades.Trades.Length);
            Assert.Equal(147967350, orderTrades.Trades[0].TradeId);
            Assert.Equal(1581873878, orderTrades.Trades[0].Date.ToUnixTimeSeconds());
            Assert.Equal(TradeType.Sell, orderTrades.Trades[0].Type);
            Assert.Equal("BTC_USD", orderTrades.Trades[0].Pair);
            Assert.Equal(1234567890, orderTrades.Trades[0].OrderId);
            Assert.Equal(0.00080824m, orderTrades.Trades[0].Quantity);
            Assert.Equal(9781.18662574m, orderTrades.Trades[0].Price);
            Assert.Equal(7.90554627m, orderTrades.Trades[0].Amount);
            Assert.Equal(147967349, orderTrades.Trades[1].TradeId);
            Assert.Equal(1581873878, orderTrades.Trades[1].Date.ToUnixTimeSeconds());
            Assert.Equal(TradeType.Sell, orderTrades.Trades[1].Type);
            Assert.Equal("BTC_USD", orderTrades.Trades[1].Pair);
            Assert.Equal(1234567890, orderTrades.Trades[1].OrderId);
            Assert.Equal(0.00019176m, orderTrades.Trades[1].Quantity);
            Assert.Equal(9788.80708007m, orderTrades.Trades[1].Price);
            Assert.Equal(1.87710164m, orderTrades.Trades[1].Amount);
        }

        [Fact]
        public async Task GetOrderTrades_OrderNotFound_ThrowsException()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form => Assert.Equal("1", form["order_id"]));
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":false,\"error\":\"Error 50304: Order was not found '1'\"}")
            };

            await Assert.ThrowsAsync<ExmoApiException>(() => _authenticatedApi.GetOrderTradesAsync(1));
        }

        [Fact]
        public async Task GetUserTrades()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("BTC_USD", form["pair"]);
                Assert.Equal("100", form["limit"]);
                Assert.Equal("0", form["offset"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"BTC_USD\":[{\"trade_id\":148003689,\"date\":1581882677,\"type\":\"buy\",\"pair\":\"BTC_USD\",\"order_id\":1234567890,\"quantity\":\"0.001\",\"price\":\"9750\",\"amount\":\"9.75\"},{\"trade_id\":147967350,\"date\":1581873878,\"type\":\"sell\",\"pair\":\"BTC_USD\",\"order_id\":1234567890,\"quantity\":\"0.00080824\",\"price\":\"9781.18662574\",\"amount\":\"7.90554627\"}]}")
            };

            var request = new UserTradesRequest
            {
                Pairs = new PairCollection("BTC_USD")
            };
            var userTrades = await _authenticatedApi.GetUserTradesAsync(request);

            var btcUsd = userTrades["BTC_USD"];
            Assert.Equal(2, btcUsd.Length);
            Assert.Equal(148003689, btcUsd[0].TradeId);
            Assert.Equal(1581882677, btcUsd[0].Date.ToUnixTimeSeconds());
            Assert.Equal(148003689, btcUsd[0].TradeId);
            Assert.Equal(TradeType.Buy, btcUsd[0].Type);
            Assert.Equal(1234567890, btcUsd[0].OrderId);
            Assert.Equal(0.001m, btcUsd[0].Quantity);
            Assert.Equal(9750m, btcUsd[0].Price);
            Assert.Equal(9.75m, btcUsd[0].Amount);
        }

        [Fact]
        public async Task GetCancelledOrders()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("100", form["limit"]);
                Assert.Equal("0", form["offset"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[{\"date\":1581876575,\"order_id\":1234567890,\"order_type\":\"buy\",\"pair\":\"BTC_USD\",\"price\":9700,\"quantity\":0.001,\"amount\":9.7}]")
            };

            var cancelledOrders = await _authenticatedApi.GetCancelledOrdersAsync(new PagedRequest());

            Assert.Single(cancelledOrders);
            Assert.Equal(1581876575, cancelledOrders[0].Date.ToUnixTimeSeconds());
            Assert.Equal(1234567890, cancelledOrders[0].OrderId);
            Assert.Equal(OrderType.Buy, cancelledOrders[0].OrderType);
            Assert.Equal("BTC_USD", cancelledOrders[0].Pair);
            Assert.Equal(9700m, cancelledOrders[0].Price);
            Assert.Equal(0.001m, cancelledOrders[0].Quantity);
            Assert.Equal(9.7m, cancelledOrders[0].Amount);
        }

        [Fact]
        public async Task GetWalletHistory()
        {
            const int unixTimeSeconds = 1493942400;

            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal(unixTimeSeconds.ToString(CultureInfo.InvariantCulture), form["date"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\",\"begin\":\"1493942400\",\"end\":\"1494028800\",\"history\":[{\"dt\":1461841192,\"type\":\"deposit\",\"curr\":\"RUB\",\"status\":\"processing\",\"provider\":\"Qiwi (LA) [12345]\",\"amount\":\"1\",\"account\":\"\",\"txid\":\"ec46f784ad976fd7f7539089d1a129fe46\"},{\"dt\":1463414785,\"type\":\"withdrawal\",\"curr\":\"USD\",\"status\":\"paid\",\"provider\":\"EXCODE\",\"amount\":\"-1\",\"account\":\"EX-CODE_19371_USDda\",\"txid\":\"\"}]}")
            };

            var request = new WalletHistoryRequest
            {
                Date = UnixTime.Create(unixTimeSeconds)
            };
            var walletHistory = await _authenticatedApi.GetWalletHistoryAsync(request);

            Assert.Equal(1493942400, walletHistory.Begin.ToUnixTimeSeconds());
            Assert.Equal(1494028800, walletHistory.End.ToUnixTimeSeconds());
            Assert.Equal(2, walletHistory.History.Length);
            Assert.Equal(1461841192, walletHistory.History[0].Date.ToUnixTimeSeconds());
            Assert.Equal("deposit", walletHistory.History[0].Type);
            Assert.Equal("RUB", walletHistory.History[0].Currency);
            Assert.Equal("processing", walletHistory.History[0].Status);
            Assert.Equal("Qiwi (LA) [12345]", walletHistory.History[0].Provider);
            Assert.Equal(1m, walletHistory.History[0].Amount);
            Assert.Equal("", walletHistory.History[0].Account);
            Assert.Equal("ec46f784ad976fd7f7539089d1a129fe46", walletHistory.History[0].TransactionId);
            Assert.Equal(1463414785, walletHistory.History[1].Date.ToUnixTimeSeconds());
            Assert.Equal("withdrawal", walletHistory.History[1].Type);
            Assert.Equal("USD", walletHistory.History[1].Currency);
            Assert.Equal("paid", walletHistory.History[1].Status);
            Assert.Equal("EXCODE", walletHistory.History[1].Provider);
            Assert.Equal(-1m, walletHistory.History[1].Amount);
            Assert.Equal("EX-CODE_19371_USDda", walletHistory.History[1].Account);
            Assert.Equal("", walletHistory.History[1].TransactionId);
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
            var result = await _authenticatedApi.CalculateRequiredAmountAsync(request);

            Assert.Equal(0.01m, result.Quantity);
            Assert.Equal(69.07831279m, result.Amount);
            Assert.Equal(6907.831279m, result.AveragePrice);
        }

        [Fact]
        public async Task GetDepositAddresses()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"BTC\":\"16UM5DoeHkV7Eb7tMfXSuQ2ueir1yj4P7d\",\"DOGE\":\"DEVfhgKErG5Nzas2FZJJH8Y8pjoLfVfWq4\",\"LTC\":\"LSJFhsVJM6GCFtSgRj5hHuK9gReLhNuKFb\"}")
            };

            var result = await _authenticatedApi.GetDepositAddressesAsync();

            Assert.Equal("16UM5DoeHkV7Eb7tMfXSuQ2ueir1yj4P7d", result["BTC"]);
            Assert.Equal("DEVfhgKErG5Nzas2FZJJH8Y8pjoLfVfWq4", result["DOGE"]);
            Assert.Equal("LSJFhsVJM6GCFtSgRj5hHuK9gReLhNuKFb", result["LTC"]);
        }

        [Fact]
        public async Task CreateExcode()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("BTC", form["currency"]);
                Assert.Equal("0.1", form["amount"]);
                Assert.Equal("test_user", form["login"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\",\"task_id\":\"467757\",\"code\":\"EX-CODE_9004_BTC7c3f8adc0b158658....\",\"amount\":\"0.1\",\"currency\":\"BTC\",\"login\":\"test_user\",\"commission\":\"0.01\",\"balances\":{\"BTC\":940.994,\"USD\":949.472}}")
            };

            var request = new CreateExcodeRequest
            {
                Currency = "BTC",
                Amount = 0.1m,
                Login = "test_user"
            };
            var result = await _authenticatedApi.CreateExcodeAsync(request);

            Assert.Equal(467757, result.TaskId);
            Assert.Equal("EX-CODE_9004_BTC7c3f8adc0b158658....", result.Code);
            Assert.Equal(0.1m, result.Amount);
            Assert.Equal("BTC", result.Currency);
            Assert.Equal("test_user", result.Login);
            Assert.Equal(0.01m, result.Commission);
            Assert.Equal(940.994m, result.Balances["BTC"]);
            Assert.Equal(949.472m, result.Balances["USD"]);
        }

        [Fact]
        public async Task CreateExcode_LoginIsNull()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("BTC", form["currency"]);
                Assert.Equal("0.1", form["amount"]);
                Assert.DoesNotContain("login", form.AllKeys);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\",\"task_id\":\"467757\",\"code\":\"EX-CODE_9004_BTC7c3f8adc0b158658....\",\"amount\":\"0.1\",\"currency\":\"BTC\",\"commission\":\"0.01\",\"balances\":{\"BTC\":940.994,\"USD\":949.472}}")
            };

            var request = new CreateExcodeRequest
            {
                Currency = "BTC",
                Amount = 0.1m
            };
            var result = await _authenticatedApi.CreateExcodeAsync(request);

            Assert.Equal(467757, result.TaskId);
            Assert.Equal("EX-CODE_9004_BTC7c3f8adc0b158658....", result.Code);
            Assert.Equal(0.1m, result.Amount);
            Assert.Equal("BTC", result.Currency);
            Assert.Null(result.Login);
            Assert.Equal(0.01m, result.Commission);
            Assert.Equal(940.994m, result.Balances["BTC"]);
            Assert.Equal(949.472m, result.Balances["USD"]);
        }

        [Fact]
        public async Task LoadExcode()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
                Assert.Equal("EX-CODE_9004_BTC7c3f8adc0b158658....", form["code"]));

            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\",\"task_id\":\"467757\",\"amount\":\"0.1\",\"currency\":\"BTC\",\"reviewing\": true}")
            };

            var result = await _authenticatedApi.LoadExcodeAsync("EX-CODE_9004_BTC7c3f8adc0b158658....");

            Assert.Equal(467757, result.TaskId);
            Assert.Equal(0.1m, result.Amount);
            Assert.Equal("BTC", result.Currency);
            Assert.True(result.Reviewing);
        }

        [Fact]
        public async Task GetWithdrawTransaction()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form => Assert.Equal("123", form["task_id"]));
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\",\"status\":true,\"txid\":\"ec46f784ad976fd7f7539089d1a129fe46...\"}")
            };

            var result = await _authenticatedApi.GetWithdrawTransactionAsync(123);

            Assert.True(result.Status);
            Assert.Equal("ec46f784ad976fd7f7539089d1a129fe46...", result.TransactionId);
        }

        [Fact]
        public async Task WithdrawCrypt()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("XRP", form["currency"]);
                Assert.Equal("10.45", form["amount"]);
                Assert.Equal("16UM5DoeHkV7Eb7tMfXSu...", form["address"]);
                Assert.Equal("DESTINATION_TAG", form["invoice"]);
                Assert.Equal("ERC20", form["transport"]);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\",\"task_id\":\"467756\"}")
            };

            var request = new WithdrawCryptRequest
            {
                Currency = "XRP",
                Amount = 10.45m,
                Address= "16UM5DoeHkV7Eb7tMfXSu...",
                Invoice = "DESTINATION_TAG",
                Transport = "ERC20"
            };
            var taskId = await _authenticatedApi.WithdrawCryptAsync(request);

            Assert.Equal(467756, taskId);
        }


        [Fact]
        public async Task WithdrawCrypt_OptionalParametersAreNotSet()
        {
            _fakeHttpMessageHandler.HandleRequestAsync = TestHelper.HandleContent(form =>
            {
                Assert.Equal("XRP", form["currency"]);
                Assert.Equal("10.45", form["amount"]);
                Assert.Equal("16UM5DoeHkV7Eb7tMfXSu...", form["address"]);
                Assert.DoesNotContain("invoice", form.AllKeys);
                Assert.DoesNotContain("transport", form.AllKeys);
            });
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\":true,\"error\":\"\",\"task_id\":\"467756\"}")
            };

            var request = new WithdrawCryptRequest
            {
                Currency = "XRP",
                Amount = 10.45m,
                Address = "16UM5DoeHkV7Eb7tMfXSu..."
            };
            var taskId = await _authenticatedApi.WithdrawCryptAsync(request);

            Assert.Equal(467756, taskId);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
