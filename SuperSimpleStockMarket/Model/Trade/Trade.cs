using System;

namespace SuperSimpleStockMarket.Model.Trade
{
    public class Trade
    {
        public double price { get; set; }
        public int quantity { get; set; }
        public string stockSymbol { get; set; }
        public DateTime timestamp { get; set; }
        public TradeDirection direction { get; set; }
    }
}

