using SuperSimpleStockMarket.Model.Stock;
using System.Collections.Generic;

namespace SuperSimpleStockMarket.Service
{
    public interface IStockService
    {
        ICollection<IStock> stockList { get; }

        void AddStock(IStock stock); 
        IStock GetStockBySymbol(string symbol);

        double CalculateDividendYield(IStock stock, double marketPrice);
        double CalculatePriceEarningsRatio(IStock stock, double marketPrice);
    }
}
