﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects;
using Binance.Net.UnitTests.TestImplementations;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using NUnit.Framework;
using Moq;
using CryptoExchange.Net.Logging;

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class BinanceNetTest
    {
        [TestCase()]
        public void SubscribingToKlineStream_Should_TriggerWhenKlineStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamKlineData result = null;
            client.SubscribeToKlineStream("test", KlineInterval.OneMinute, (test) => result = test);

            var data = new BinanceCombinedStream<BinanceStreamKlineData>()
            {
                Stream = "test",
                Data = new BinanceStreamKlineData()
                {
                    Event = "TestKlineStream",
                    EventTime = new DateTime(2017, 1, 1),
                    Symbol = "test",
                    Data = new BinanceStreamKline()
                    {
                        TakerBuyBaseAssetVolume = 0.1m,
                        Close = 0.2m,
                        CloseTime = new DateTime(2017, 1, 2),
                        Final = true,
                        FirstTrade = 10000000000,
                        High = 0.3m,
                        Interval = KlineInterval.OneMinute,
                        LastTrade = 2000000000000,
                        Low = 0.4m,
                        Open = 0.5m,
                        TakerBuyQuoteAssetVolume = 0.6m,
                        QuoteAssetVolume = 0.7m,
                        OpenTime = new DateTime(2017, 1, 1),
                        Symbol = "test",
                        TradeCount = 10,
                        Volume = 0.8m
                    }
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data, result, "Data"));
            Assert.IsTrue(TestHelpers.AreEqual(data.Data.Data, result.Data));
        }

        [TestCase()]
        public void SubscribingToSymbolTicker_Should_TriggerWhenSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamTick result = null;
            client.SubscribeToSymbolTicker("test", (test) => result = test);

            var data = new BinanceStreamTick()
            {
                BestAskPrice = 0.1m,
                BestAskQuantity = 0.2m,
                BestBidPrice = 0.3m,
                BestBidQuantity = 0.4m,
                CloseTradesQuantity = 0.5m,
                CurrentDayClosePrice = 0.6m,
                FirstTradeId = 1,
                HighPrice = 0.7m,
                LastTradeId = 2,
                LowPrice = 0.8m,
                OpenPrice = 0.9m,
                PrevDayClosePrice = 1.0m,
                PriceChange = 1.1m,
                PriceChangePercentage = 1.2m,
                StatisticsCloseTime = new DateTime(2017, 1, 2),
                StatisticsOpenTime = new DateTime(2017, 1, 1),
                Symbol = "test",
                TotalTradedBaseAssetVolume = 1.3m,
                TotalTradedQuoteAssetVolume = 1.4m,
                TotalTrades = 3,
                WeightedAverage = 1.5m
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data, result));
        }

        [TestCase()]
        public void SubscribingToAllSymbolTicker_Should_TriggerWhenAllSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamTick[] result = null;
            client.SubscribeToAllSymbolTicker((test) => result = test);

            var data = new[]
            {
                new BinanceStreamTick()
                {
                    BestAskPrice = 0.1m,
                    BestAskQuantity = 0.2m,
                    BestBidPrice = 0.3m,
                    BestBidQuantity = 0.4m,
                    CloseTradesQuantity = 0.5m,
                    CurrentDayClosePrice = 0.6m,
                    FirstTradeId = 1,
                    HighPrice = 0.7m,
                    LastTradeId = 2,
                    LowPrice = 0.8m,
                    OpenPrice = 0.9m,
                    PrevDayClosePrice = 1.0m,
                    PriceChange = 1.1m,
                    PriceChangePercentage = 1.2m,
                    StatisticsCloseTime = new DateTime(2017, 1, 2),
                    StatisticsOpenTime = new DateTime(2017, 1, 1),
                    Symbol = "test",
                    TotalTradedBaseAssetVolume = 1.3m,
                    TotalTradedQuoteAssetVolume = 1.4m,
                    TotalTrades = 3,
                    WeightedAverage = 1.5m
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data[0], result[0]));
        }

        [TestCase()]
        public void SubscribingToTradeStream_Should_TriggerWhenTradeStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamTrade result = null;
            client.SubscribeToTradesStream("test", (test) => result = test);

            var data = new BinanceCombinedStream<BinanceStreamTrade>()
            {
                Stream = "test",
                Data = new BinanceStreamTrade()
                {
                    Event = "TestTradeStream",
                    EventTime = new DateTime(2017, 1, 1),
                    Symbol = "test",
                    TradeId = 1000000000000,
                    BuyerIsMaker = true,
                    BuyerOrderId = 10000000000000,
                    SellerOrderId = 2000000000000,
                    Price = 1.1m,
                    Quantity = 2.2m,
                    TradeTime = new DateTime(2017, 1, 1)
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data, result));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenAccountUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamAccountInfo result = null;
            client.SubscribeToUserStream("test", (test) => result = test, null, null, null);

            var data = new BinanceStreamAccountInfo()
            {
                Event = "outboundAccountInfo",
                EventTime = new DateTime(2017, 1, 1),
                BuyerCommission = 1.1m,
                CanDeposit = true,
                CanTrade = true,
                CanWithdraw = false,
                MakerCommission = 2.2m,
                SellerCommission = 3.3m,
                TakerCommission = 4.4m,
                Balances = new List<BinanceStreamBalance>()
                {
                    new BinanceStreamBalance(){ Asset = "test1", Free = 1.1m, Locked = 2.2m},
                    new BinanceStreamBalance(){ Asset = "test2", Free = 3.3m, Locked = 4.4m},
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data, result, "Balances"));
            Assert.IsTrue(TestHelpers.AreEqual(data.Balances[0], result.Balances[0]));
            Assert.IsTrue(TestHelpers.AreEqual(data.Balances[1], result.Balances[1]));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenOcoOrderUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket, new BinanceSocketClientOptions(){ LogVerbosity = LogVerbosity.Debug });

            BinanceStreamOrderList result = null;
            client.SubscribeToUserStream("test", null, null, (test) => result = test, null);

            var data = new BinanceStreamOrderList()
            {
                Event = "listStatus",
                EventTime = new DateTime(2017, 1, 1),
                Symbol = "BNBUSDT",
                ContingencyType = "OCO",
                ListStatusType = ListStatusType.Done,
                ListOrderStatus = ListOrderStatus.Done,
                OrderListId = 1,
                ListClientOrderId = "2",
                TransactionTime = new DateTime(2018, 1, 1),
                Orders = new []
                {
                    new BinanceStreamOrderId()
                    {
                        Symbol = "BNBUSDT",
                        OrderId = 2,
                        ClientOrderId = "3"
                    },
                    new BinanceStreamOrderId()
                    {
                        Symbol = "BNBUSDT",
                        OrderId = 3,
                        ClientOrderId = "4"
                    }
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data, result, "Orders"));
            Assert.IsTrue(TestHelpers.AreEqual(data.Orders[0], result.Orders[0]));
            Assert.IsTrue(TestHelpers.AreEqual(data.Orders[1], result.Orders[1]));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenOrderUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamOrderUpdate result = null;
            client.SubscribeToUserStream("test", null, (test) => result = test, null, null);

            var data = new BinanceStreamOrderUpdate()
            {
                Event = "executionReport",
                EventTime = new DateTime(2017, 1, 1),
                AccumulatedQuantityOfFilledTrades = 1.1m,
                BuyerIsMaker = true,
                Commission = 2.2m,
                CommissionAsset = "test",
                ExecutionType = ExecutionType.Trade,
                I = 100000000000,
                OrderId = 100000000000,
                Price = 6.6m,
                PriceLastFilledTrade = 7.7m,
                Quantity = 8.8m,
                QuantityOfLastFilledTrade = 9.9m,
                RejectReason = OrderRejectReason.AccountCannotSettle,
                Side = OrderSide.Buy,
                Status = OrderStatus.Filled,
                Symbol = "test",
                Time = new DateTime(2017, 1, 1),
                TimeInForce = TimeInForce.GoodTillCancel,
                TradeId = 10000000000000,
                Type = OrderType.Limit,
                ClientOrderId = "123",
                IcebergQuantity = 9.9m,
                IsWorking = true,
                OriginalClientOrderId = "456",
                StopPrice = 10.10m
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data, result, "Balances"));
        }
    }
}
