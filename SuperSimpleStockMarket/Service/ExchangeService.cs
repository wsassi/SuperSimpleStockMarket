using System;
using System.Collections.Generic;
using SuperSimpleStockMarket.Model.Stock;
using SuperSimpleStockMarket.Model.Trade;

namespace SuperSimpleStockMarket.Service
{
    public class ExchangeService : IExchangeService
    {
        public ICollection<IStock> stockList
        {
            get
            {
                return stockService.stockList;
            }
        }

        public ICollection<Trade> tradeList
        {
            get
            {
                return tradeService.tradeList;
            }
        }

        public IStockService stockService { get; set; } = new StockService();
        public ITradeService tradeService { get; set; } = new TradeService();


        public void AddStock(IStock stock)
        {
            stockService.AddStock(stock);
        }

        public void BuyStock(string stockSymbol, int tradeQuantity, double tradePrice)
        {
            tradeService.RecordTrade(new Trade { stockSymbol = stockSymbol, quantity = tradeQuantity, direction = TradeDirection.BUY, price = tradePrice, timestamp = DateTime.Now });
        }

        public double CalculateAllShareIndex()
        {
            var stockSize = stockList.Count;
            if (stockSize == 0)
                return 0;

            double totalPriceProduct = CalculateTotalPrice();
            return Math.Pow(totalPriceProduct, 1.0 / stockSize);
        }

        private double CalculateTotalPrice()
        {
            double totalPriceProduct = 1;
            foreach (var item in stockList)
            {
                double? stockPrice = GetLatestPrice(item.symbol);
                if (stockPrice != null)
                {
                    totalPriceProduct *= (double)stockPrice;
                }
            }
            return totalPriceProduct;
        }

        public double CalculateDividendYield(IStock stock, double marketPrice)
        {
            return stockService.CalculateDividendYield(stock, marketPrice);
        }

        public double CalculatePriceEarningsRatio(IStock stock, double marketPrice)
        {
            return stockService.CalculatePriceEarningsRatio(stock, marketPrice);
        }

        public double CalculateVolumeWeightedStockPrice(string stockSymbol, int numberOfMinutes)
        {
            return tradeService.CalculateVolumeWeightedStockPrice(stockSymbol, numberOfMinutes);
        }

        public double? GetLatestPrice(string stockSymbol)
        {
            return tradeService.GetLatestPrice(stockSymbol);
        }

        public void RecordTrade(Trade trade)
        {
            tradeService.RecordTrade(trade);
        }

        public void SellStock(string stockSymbol, int tradeQuantity, double tradePrice)
        {
            tradeService.RecordTrade(new Trade { stockSymbol = stockSymbol, quantity = tradeQuantity, direction = TradeDirection.SELL, price = tradePrice, timestamp = DateTime.Now });
        }

        public IStock GetStockBySymbol(string symbol)
        {
            return stockService.GetStockBySymbol(symbol);
        }
    }
}
