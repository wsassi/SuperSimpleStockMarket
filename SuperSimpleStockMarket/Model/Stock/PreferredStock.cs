using System;

namespace SuperSimpleStockMarket.Model.Stock
{
    public class PreferredStock : AbstractStock
    {
        public override StockType type { get; set; } = StockType.PREFERRED;
        public double fixedDividend { get; set; }

        /// <summary>
        ///  Calculate dividend yield for preferred stock
        /// </summary>
        /// <param name="marketPrice">price non zero</param>
        /// <returns>dividend yield</returns>
        public override double CalculateDividendYield(double marketPrice)
        {
            if (marketPrice == 0)
                throw new ArgumentException("Dividend yield cannot be calculated when Market Price is zero.");

            return (fixedDividend * parValue) / marketPrice;
        }
    }
}

