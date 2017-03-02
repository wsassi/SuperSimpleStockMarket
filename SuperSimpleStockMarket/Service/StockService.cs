using SuperSimpleStockMarket.Model.Stock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperSimpleStockMarket.Service
{
    public class StockService : IStockService
    {
        public ICollection<IStock> stockList { get; set; } = new List<IStock>();

        public void AddStock(IStock stock)
        {
            stockList.Add(stock);
        }

        /// <summary>
        /// Get stock from the added stocks using the specified stock symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <returns>the stock corresponding to the specified symbol or null if not found</returns>
        public IStock GetStockBySymbol(string symbol)
        {
            return stockList.FirstOrDefault(x => x.symbol == symbol);
        }

        public double CalculateDividendYield(IStock stock, double marketPrice)
        {
            return stock.CalculateDividendYield(marketPrice);
        }

        public double CalculatePriceEarningsRatio(IStock stock, double marketPrice)
        {
            return stock.CalculatePriceEarningsRatio(marketPrice);
        }
    }
}
