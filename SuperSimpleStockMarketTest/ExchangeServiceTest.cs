using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperSimpleStockMarket.Service;
using SuperSimpleStockMarket.Model.Stock;
using System.Linq;
using SuperSimpleStockMarket.Model.Trade;

namespace SuperSimpleStockMarketTest
{
    [TestClass]
    public class ExchangeServiceTest
    {
        IExchangeService exchangeStock = new ExchangeService();

        [TestMethod]
        public void AddCommonStockTest()
        {
            var stockCommon = new CommonStock { symbol = "POP", lastDividend = 8, parValue = 100 };
            exchangeStock.AddStock(stockCommon);
            Assert.AreEqual(stockCommon, exchangeStock.GetStockBySymbol("POP"));
        }
        
        [TestMethod]
        public void AddPreferredStockTest()
        {
            var stockPreferred = new PreferredStock { symbol = "GIN", lastDividend = 8, fixedDividend = 0.02, parValue = 100 };
            exchangeStock.AddStock(stockPreferred);
            Assert.AreEqual(stockPreferred, exchangeStock.GetStockBySymbol("GIN"));
        }

        [TestMethod]
        public void BuyTradeRecordTest()
        {
            var stockCommon = new CommonStock { symbol = "POP", lastDividend = 8, parValue = 100 };
            exchangeStock.AddStock(stockCommon);
            exchangeStock.BuyStock(stockCommon.symbol, 10, 3);
            exchangeStock.SellStock(stockCommon.symbol, 5, 5);
            exchangeStock.BuyStock(stockCommon.symbol, 5, 2);

            var currentBuyTrades = exchangeStock.tradeList.Where(trade => trade.direction.Equals(TradeDirection.BUY)).ToList().Count;
            var expectedBuyTrades = 2;

            Assert.AreEqual(expectedBuyTrades, currentBuyTrades);
        }

        [TestMethod]
        public void SellTradeRecordTest()
        {
            var stockCommon = new CommonStock { symbol = "POP", lastDividend = 8, parValue = 100 };
            exchangeStock.AddStock(stockCommon);
            exchangeStock.BuyStock(stockCommon.symbol, 10, 3);
            exchangeStock.SellStock(stockCommon.symbol, 5, 5);
            exchangeStock.BuyStock(stockCommon.symbol, 5, 2);

            var currentBuyTrades = exchangeStock.tradeList.Where(trade => trade.direction.Equals(TradeDirection.SELL)).ToList().Count;
            var expectedBuyTrades = 1;

            Assert.AreEqual(expectedBuyTrades, currentBuyTrades);
        }

        [TestMethod]
        public void CalculateDividendYieldForCommonStockTest()
        {
            var stockCommon = new CommonStock { symbol = "TEA", lastDividend = 10, parValue = 100 };
            var currentDividendCommon = exchangeStock.CalculateDividendYield(stockCommon, 100);
            var expectedDividendYield = 0.1;
            Assert.AreEqual(expectedDividendYield, currentDividendCommon);
        }

        [TestMethod]
        public void CalculateDividendYieldForPreferredStockTest()
        {
            var stockPreferred = new PreferredStock { symbol = "GIN", lastDividend = 8, fixedDividend = 0.02, parValue = 100 };
            var currentDividendPreffered = exchangeStock.CalculateDividendYield(stockPreferred, 100);
            var expectedDividendYield = 0.02;
            Assert.AreEqual(expectedDividendYield, currentDividendPreffered);
        }

        [TestMethod]
        public void CalculatPriceEarningsRatioForComonStockTest()
        {
            var stockCommon = new CommonStock { symbol = "TEA", lastDividend = 10, parValue = 100 };
            var currentPeRatio = exchangeStock.CalculatePriceEarningsRatio(stockCommon, 100);
            var expectedPeRatio = 1000;
            Assert.AreEqual(expectedPeRatio, currentPeRatio);
        }

        [TestMethod]
        public void CalculatPriceEarningsRatioForPreferredStockTest()
        {
            var stockPreferred = new PreferredStock { symbol = "GIN", lastDividend = 8, fixedDividend = 0.02, parValue = 100 };
            var currentPeRatio = exchangeStock.CalculatePriceEarningsRatio(stockPreferred, 100);
            var expectedPeRatio = 5000;
            Assert.AreEqual(expectedPeRatio, currentPeRatio);
        }

        [TestMethod]
        public void CalculatPriceEarningsRatioWithZeroDividendTest()
        {
            var stockCommon = new CommonStock { symbol = "TEA", lastDividend = 0, parValue = 100 };
            var currentPeRatio = exchangeStock.CalculatePriceEarningsRatio(stockCommon, 100);
            var expectedPeRatio = 0;
            Assert.AreEqual(expectedPeRatio, currentPeRatio);
        }

        [TestMethod]
        public void CalculateVolumeWeightedStockPriceTest()
        {
            var stock = new CommonStock { symbol = "TEA", lastDividend = 10, parValue = 100 };
            var trade1 = new Trade { stockSymbol = "TEA", timestamp = DateTime.Now.AddMinutes(-60) , price = 5, direction = TradeDirection.BUY, quantity = 50 };
            exchangeStock.RecordTrade(trade1);
            var trade2 = new Trade { stockSymbol = "TEA", timestamp = DateTime.Now, price = 10, direction = TradeDirection.BUY, quantity = 100 };
            exchangeStock.RecordTrade(trade2);
            var trade3 = new Trade { stockSymbol = "TEA", timestamp = DateTime.Now, price = 100, direction = TradeDirection.BUY, quantity = 3 };
            exchangeStock.RecordTrade(trade3);

            var currentStockPrice =exchangeStock.CalculateVolumeWeightedStockPrice("TEA", 15);
            var expectedStockPrice = 1300d/103d;

            Assert.AreEqual(expectedStockPrice, currentStockPrice);
        }

        [TestMethod]
        public void CalculateAllShareIndexTest()
        {
            exchangeStock.AddStock(new CommonStock { symbol = "TEA", lastDividend = 10, parValue = 100 });
            exchangeStock.RecordTrade(new Trade { stockSymbol = "TEA", direction = TradeDirection.BUY, price = 100, quantity = 50, timestamp = DateTime.Now.AddMinutes(-5) });
            exchangeStock.RecordTrade(new Trade { stockSymbol = "TEA", direction = TradeDirection.BUY, price = 200, quantity = 50, timestamp = DateTime.Now });

            exchangeStock.AddStock(new CommonStock { symbol = "POP", lastDividend = 10, parValue = 100 });
            exchangeStock.RecordTrade(new Trade { stockSymbol = "POP", direction = TradeDirection.BUY, price = 450, quantity = 50, timestamp = DateTime.Now.AddMinutes(-5) });
            exchangeStock.RecordTrade(new Trade { stockSymbol = "POP", direction = TradeDirection.BUY, price = 180, quantity = 50, timestamp = DateTime.Now });

            exchangeStock.AddStock(new CommonStock { symbol = "ALE", lastDividend = 10, parValue = 100 });     
            exchangeStock.RecordTrade(new Trade { stockSymbol = "ALE", direction = TradeDirection.BUY, price = 130, quantity = 50, timestamp = DateTime.Now.AddMinutes(-5) });
            exchangeStock.RecordTrade(new Trade { stockSymbol = "ALE", direction = TradeDirection.BUY, price = 120, quantity = 50, timestamp = DateTime.Now });

            double currentResult = exchangeStock.CalculateAllShareIndex();
            double expectedResult = Math.Pow(200 * 180 * 120, 1.0d / 3);

            Assert.AreEqual(currentResult, expectedResult);
        }
    }
}
