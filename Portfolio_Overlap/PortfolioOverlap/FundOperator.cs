using MyMoneySolution.Constants;
using MyMoneySolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoneySolution
{
    public class FundOperator
    {
        public static string[] Funds;
        public static void AddFundsInPortfolio(string[] funds)
        {
            Funds = funds;
        }

        public static List<FundResponseModel> CalculateOverlap(string compareFund)
        {
            List<string> initialStocks = new List<string>();
            initialStocks = GetStocksByFundName(compareFund);

            if (initialStocks == null)
            {
                return null;
            }

            List<FundResponseModel> FundResponse = new();


            foreach (var fund in Funds)
            {
                var getFundStocks = GetStocksByFundName(fund);
                var duplicateFund = initialStocks.Where(x => getFundStocks.Contains(x)).ToList();
                double calculateOverlap = (double)(2 * duplicateFund.Count) * 100 / (initialStocks.Count() + getFundStocks.Count);
                if (calculateOverlap != 0)
                    FundResponse.Add(new FundResponseModel()
                    {
                        CompareFund = compareFund,
                        SourceFund = fund,
                        Percentage = calculateOverlap
                    });
            }
            return FundResponse;
        }

        private static List<string> GetStocksByFundName(string fundName)
        {
            return LoadFundsInMemory.Funds.Funds.FirstOrDefault(x => x.Name == fundName)?.Stocks.ToList();
        }

        public static void AddStockInFund(string fundName, string stockName)
        {
            var stocks = LoadFundsInMemory.Funds.Funds.FirstOrDefault(x => x.Name == fundName).Stocks.Append(stockName);
            LoadFundsInMemory.Funds.Funds.FirstOrDefault(x => x.Name == fundName).Stocks = stocks.ToArray();
        }
    }
}
