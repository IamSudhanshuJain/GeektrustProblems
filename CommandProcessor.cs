using MyMoneySolution.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMoneySolution
{
    public class CommandProcessor
    {
        internal InvestmentOperator InvestmentOperator;
        public CommandProcessor()
        {
            InvestmentOperator = new InvestmentOperator();
        }
        public void ProcessInput(string command)
        {
            command = command.Replace("%", "");
            string[] commandParams = command.Split(' ');


            switch (commandParams[0])
            {
                case InputCommand.ALLOCATE:
                    AllocateAmount(commandParams);
                    break;

                case InputCommand.SIP:
                    AllocateSipAmount(commandParams);
                    break;

                case InputCommand.CHANGE:
                    ChangeInValue(commandParams);
                    break;

                case InputCommand.BALANCE:
                    Console.WriteLine(GetBalance(commandParams));
                    break;

                case InputCommand.REBALANCE:
                    Console.WriteLine(Rebalance());
                    Console.WriteLine();
                    break;

                default:
                    Console.WriteLine(Message.INVALID_COMMAND);
                    break;
            }
        }

        public string Rebalance()
        {
            StringBuilder rebalance = new StringBuilder();
            var equityRebalanceAmount = InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Equity);
            var debtRebalanceAmount = InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Debt);
            var goldRebalanceAmount = InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Gold);

            rebalance.Append(Message.CANNOT_REBALANCE);
            if (equityRebalanceAmount != 0 && debtRebalanceAmount != 0 && goldRebalanceAmount != 0)
            {
                rebalance.Clear();
                rebalance.Append(DisplayMessage(new List<double>() { equityRebalanceAmount, debtRebalanceAmount, goldRebalanceAmount}));
            }
            return rebalance.ToString();
        }

        private string GetBalance(string[] commandParams)
        {
            object getMonth = Enum.Parse(typeof(Month), commandParams[1], true);
            var equityBalance = InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, (Month)getMonth);
            var debtBalance = InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, (Month)getMonth);
            var goldBalance = InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, (Month)getMonth);
            return DisplayMessage(new List<double>() { equityBalance, debtBalance, goldBalance });
        }

        private void ChangeInValue(string[] commandParams)
        {
            object month = Enum.Parse(typeof(Month), commandParams[4], true);
            InvestmentOperator.Change(InvestmentCategory.Equity, double.Parse(commandParams[1]), (Month)month);
            InvestmentOperator.Change(InvestmentCategory.Debt, double.Parse(commandParams[2]), (Month)month);
            InvestmentOperator.Change(InvestmentCategory.Gold, double.Parse(commandParams[3]), (Month)month);
        }

        private void AllocateSipAmount(string[] commandParams)
        {
            InvestmentOperator.AllocateSipAmount(InvestmentCategory.Equity, double.Parse(commandParams[1]));
            InvestmentOperator.AllocateSipAmount(InvestmentCategory.Debt, double.Parse(commandParams[2]));
            InvestmentOperator.AllocateSipAmount(InvestmentCategory.Gold, double.Parse(commandParams[3]));
        }

        private void AllocateAmount(string[] commandParams)
        {
            InvestmentOperator.AllocateAmount(InvestmentCategory.Equity, double.Parse(commandParams[1]));
            InvestmentOperator.AllocateAmount(InvestmentCategory.Debt, double.Parse(commandParams[2]));
            InvestmentOperator.AllocateAmount(InvestmentCategory.Gold, double.Parse(commandParams[3]));
        }

        internal string DisplayMessage(List<double> amounts)
        {
            StringBuilder result = new();
            foreach (var item in amounts)
            {
                result.Append($"{item} ");
            }

            return result.ToString().Trim();
        }
    }
}
