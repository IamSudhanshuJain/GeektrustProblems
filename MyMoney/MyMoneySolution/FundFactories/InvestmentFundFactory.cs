using MyMoneySolution.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoneySolution.FundFactories
{
    public abstract class InvestmentFundFactory
    {
        public bool IsRebalanceRequired { get; private set; }
        protected double AllocatedAmount { get; set; }
        protected double AllocatedSipAmount { get; set; }
        protected double LastRebalanceAmount { get; private set; }

        protected Dictionary<Month, double> Investment { get; set; }

        public InvestmentFundFactory()
        {
            Investment = new Dictionary<Month, double>();
        }

        public abstract void Change(double percentage, Month month);

        public double GetAllocatedAmount()
        {
            return AllocatedAmount;
        }

        public double GetInvestedAmountBasedOnMonth(Month month)
        {
            return Investment[month];
        }
        public double GetLastRebalanceAmount()
        {
            return LastRebalanceAmount;
        }

        public virtual void AllocateSipAmount(double amount)
        {
            IsAmountLessThanZero(amount);
            AllocatedSipAmount = amount;
        }
        public virtual void AllocateAmount(double amount)
        {
            IsAmountLessThanZero(amount);
            AllocatedAmount = amount;
        }
        public void RebalanceAmount(double totalAccumulatedAmount, double totalAllocatedAmount)
        {
            if (IsRebalanceRequired)
            {
                double differenceInPercentage = Utility.GetPercentage(AllocatedAmount, totalAllocatedAmount);
                LastRebalanceAmount = Utility.GetPercentageValue(totalAccumulatedAmount, differenceInPercentage);
                Investment[Investment.Last().Key] = LastRebalanceAmount;

            }
        }
        protected void ChangeInValue(double percentage, Month month)
        {
            Month calculativeMonth = month;
            double amount = AllocatedAmount;
            amount = AddSip(calculativeMonth, amount);
            amount = Utility.GetAccumulatedPercentageValue(amount, percentage);
            InsertCalculatedAmount(amount, month);
            IsRebalanceRequiredForThisMonth(month);
        }
        private double AddSip(Month calculativeMonth, double amount)
        {
            if (calculativeMonth != Month.JANUARY)
            {
                calculativeMonth -= 1;
                amount = Investment[calculativeMonth] + AllocatedSipAmount;
            }
            return amount;
        }

        private void IsRebalanceRequiredForThisMonth(Month month)
        {
            IsRebalanceRequired = month.CheckRebalanceMonth();
        }
        private void InsertCalculatedAmount(double amount, Month month)
        {
                Investment.Add(month, amount);
        }
        private void IsAmountLessThanZero(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount less than 0 is not permitted to allocate");
        }


    }
}
