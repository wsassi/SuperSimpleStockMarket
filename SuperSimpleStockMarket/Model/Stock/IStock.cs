namespace SuperSimpleStockMarket.Model.Stock
{
    public interface IStock
    {
        double lastDividend { get; }
        double parValue { get; }
        string symbol { get; }
        StockType type { get; }     

        double CalculateDividendYield(double marketPrice);
        double CalculatePriceEarningsRatio(double marketPrice);
    }
}
