using MyMoneySolution.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMoneySolution.FundFactories
{
    public class EquityFundFactory : InvestmentFundFactory
    {
     

     
        public override void Change(double percentage, Month month)
        {
            ChangeInValue(percentage, month);
        }
    }
}
