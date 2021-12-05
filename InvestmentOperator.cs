using MyMoneySolution.Constants;
using MyMoneySolution.FundFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneySolution
{
    public class InvestmentOperator
    {
        private readonly Dictionary<InvestmentCategory, InvestmentFundFactory> InvestmentType;
        public InvestmentOperator()
        {
            InvestmentType = new Dictionary<InvestmentCategory, InvestmentFundFactory>();
            InvestmentInitialization();
        }

        public void AllocateAmount(InvestmentCategory investmentCategory, double amount)
        {
            var fundFactory = GetRuntimeFactory(investmentCategory);
            fundFactory.AllocateAmount(amount);
        }

        public void AllocateSipAmount(InvestmentCategory investmentCategory, double amount)
        {
            var fundFactory = GetRuntimeFactory(investmentCategory);
            fundFactory.AllocateSipAmount(amount);
        }

        public double GetAllocatedAmount(InvestmentCategory investmentCategory)
        {
            var fundFactory = GetRuntimeFactory(investmentCategory);
            return fundFactory.GetAllocatedAmount();
        }
        public void Change(InvestmentCategory investmentCategory, double percentage, Month month)
        {
            var fundFactory = GetRuntimeFactory(investmentCategory);
            fundFactory.Change(percentage, month);
            ListRebalance(new InvestmentCategory[]
            {
                InvestmentCategory.Equity,
                InvestmentCategory.Debt,
                InvestmentCategory.Gold
            }, month);
        }
        public double GetBalanceBasedOnMonth(InvestmentCategory investmentCategory, Month month)
        {
            var fundFactory = GetRuntimeFactory(investmentCategory);
            return fundFactory.GetInvestedAmountBasedOnMonth(month);
        }
        public double GetLastRebalanceAmount(InvestmentCategory investmentCategory)
        {
            var fundFactory = GetRuntimeFactory(investmentCategory);
            return fundFactory.GetLastRebalanceAmount();
        }


        private void ListRebalance(InvestmentCategory[] investmentCategories, Month month)
        {
            double totalAllocatedAmount = 0;
            double totalAccumulatedAmount = 0;
            bool isFail = false;
            foreach (var investmentCategory in investmentCategories)
            {
                var fundFactory = GetRuntimeFactory(investmentCategory);
                if (fundFactory.IsRebalanceRequired)
                {
                    totalAllocatedAmount += fundFactory.GetAllocatedAmount();
                    totalAccumulatedAmount += fundFactory.GetInvestedAmountBasedOnMonth(month);
                }
                else
                {
                    isFail = true;
                    break;
                }
            }
            RebalanceAmount(investmentCategories, totalAllocatedAmount, totalAccumulatedAmount, isFail);

        }

        private void RebalanceAmount(InvestmentCategory[] investmentCategories, double totalAllocatedAmount, double totalAccumultedAmount, bool isFail)
        {
            if (!isFail)
            {
                foreach (var investmentCategory in investmentCategories)
                {
                    var fundFactory = GetRuntimeFactory(investmentCategory);
                    fundFactory.RebalanceAmount(totalAccumultedAmount, totalAllocatedAmount);
                }
            }
        }
        private void InvestmentInitialization()
        {
            InvestmentType.Add(InvestmentCategory.Equity, new EquityFundFactory());
            InvestmentType.Add(InvestmentCategory.Debt, new DebtFundFactory());
            InvestmentType.Add(InvestmentCategory.Gold, new GoldFundFactory());
        }
        private InvestmentFundFactory GetRuntimeFactory(InvestmentCategory investmentCategory)
        {
            return InvestmentType[investmentCategory];
        }

    }
}
