using System;

namespace SuperSimpleStockMarket.Model.Stock
{
    public class CommonStock : AbstractStock
    {
        public override StockType type { get; set; } = StockType.COMMON;

        /// <summary>
        /// Calculate dividend yield for common stock
        /// </summary>
        /// <param name="marketPrice">price non zero</param>
        /// <returns>dividend yield</returns>
        public override double CalculateDividendYield(double marketPrice)
        {
            if (marketPrice == 0)
                throw new ArgumentException("Dividend yield cannot be calculated when Market Price is zero.");

            return lastDividend / marketPrice;
        }
    }
}
