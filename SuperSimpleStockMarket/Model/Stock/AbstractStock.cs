namespace SuperSimpleStockMarket.Model.Stock
{
    public abstract class AbstractStock : IStock
    {
        public double lastDividend { get; set; }
        public double parValue { get; set; }
        public string symbol { get; set; }
        public abstract StockType type { get; set; }

        public abstract double CalculateDividendYield(double marketPrice);

        /// <summary>
        /// Calculate Price Earning Ratio
        /// </summary>
        /// <param name="marketPrice">price</param>
        /// <returns>P/E ratio else zero when dividendYield is zero</returns>
        public double CalculatePriceEarningsRatio(double marketPrice)
        {
            double dividendYield = CalculateDividendYield(marketPrice);
            return (dividendYield == 0) ? 0 : (marketPrice / dividendYield);
        }
    }
}
