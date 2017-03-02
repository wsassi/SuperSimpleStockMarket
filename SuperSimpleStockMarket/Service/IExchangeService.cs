namespace SuperSimpleStockMarket.Service
{
    public interface IExchangeService : IStockService,ITradeService
    {
        IStockService stockService { get; }
        ITradeService tradeService { get; }

        void BuyStock(string stockSymbol, int tradeQuantity, double tradePrice);
        double CalculateAllShareIndex();
        void SellStock(string stockSymbol, int tradeQuantity, double tradePrice);
    }
}
