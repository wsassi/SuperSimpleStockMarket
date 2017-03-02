using SuperSimpleStockMarket.Model.Trade;
using System.Collections.Generic;

namespace SuperSimpleStockMarket.Service
{
    public interface ITradeService
    {
        ICollection<Trade> tradeList { get; }

        void RecordTrade(Trade trade);
        double? GetLatestPrice(string stockSymbol);
        double CalculateVolumeWeightedStockPrice(string stockSymbol, int numberOfMinutes);
    }
}
