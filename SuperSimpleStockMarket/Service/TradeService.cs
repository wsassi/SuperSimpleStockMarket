using System;
using System.Collections.Generic;
using System.Linq;
using SuperSimpleStockMarket.Model.Trade;

namespace SuperSimpleStockMarket.Service
{
    public class TradeService : ITradeService
    {
        public ICollection<Trade> tradeList { get; set; } = new List<Trade>();

        public void RecordTrade(Trade trade)
        {
            tradeList.Add(trade);
        }

        /// <summary>
        /// Calculate Volume Weighted Stock Price based on trades in the past specified number of minutes
        /// </summary>
        /// <param name="stockSymbol">stock symbol</param>
        /// <param name="numberOfMinutes">number of minutes</param>
        /// <returns>The Volume Weighted Stock Price</returns>
        public double CalculateVolumeWeightedStockPrice(string stockSymbol, int numberOfMinutes)
        {
            if (tradeList == null || tradeList.Count == 0)
                return 0;

            var latestTrades = GetLatestTradesByStockSymbol(stockSymbol, numberOfMinutes);
            return CalculateVolumeWeightedStockPrice(latestTrades);
        }

        private double CalculateVolumeWeightedStockPrice(ICollection<Trade> latestTrades)
        {
            if (latestTrades.Count == 0)
                return 0;

            double tradingValue = 0;
            int tradingVolume = 0;
            foreach (var trade in latestTrades)
            {
                tradingValue += trade.price * trade.quantity;
                tradingVolume += trade.quantity;
            }
            return tradingValue / tradingVolume;
        }

        private ICollection<Trade> GetLatestTradesByStockSymbol(string stockSymbol, int numberOfMinutes)
        {
            var now = DateTime.Now;
            return tradeList.Where(trade => trade.stockSymbol.Equals(stockSymbol)).Where(trade => trade.timestamp > now.AddMinutes(-numberOfMinutes)).ToList();
        }

        public double? GetLatestPrice(string stockSymbol)
        {
            return tradeList.Where(trade => trade.stockSymbol.Equals(stockSymbol)).ToList().OrderByDescending(i => i.timestamp).First().price;
        }
    }
}
