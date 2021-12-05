using MyMoneySolution.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMoneySolution.FundFactories
{
    public class GoldFundFactory : InvestmentFundFactory
    {
        public override void Change(double percentage, Month month)
        {
            ChangeInValue(percentage, month);
        }

    }
}
