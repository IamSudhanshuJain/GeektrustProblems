using MyMoneySolution.Constants;
using MyMoneySolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMoneySolution
{
    public class CommandProcessor
    {
        public CommandProcessor()
        {
        }
        public void ProcessInput(string command)
        {
            string[] commandParams = command.Split(' ');


            switch (commandParams[0])
            {
                case InputCommand.CURRENT_PORTFOLIO:
                    LoadFunds();
                    FundOperator.AddFundsInPortfolio(commandParams.Skip(1).ToArray());
                    break;

                case InputCommand.CALCULATE_OVERLAP:
                    Console.WriteLine(DisplayMessage(FundOperator.CalculateOverlap(commandParams.Skip(1).FirstOrDefault())));
                    break;

                case InputCommand.ADD_STOCK:
                    FundOperator.AddStockInFund(commandParams[1], string.Join(" ",commandParams.Skip(2)));
                    break;

                default:
                    Console.WriteLine(Message.INVALID_COMMAND);
                    break;
            }
        }

        public void LoadFunds()
        {
            LoadFundsInMemory.LoadJson();
        }


        internal string DisplayMessage(List<FundResponseModel> fundResult)
        {
            StringBuilder result = new();
            if(fundResult==null)
            {
                return result.Append($"FUND_NOT_FOUND").ToString().Trim();
            }
            foreach (var fund in fundResult)
            {
                result.Append($"{fund.CompareFund} {fund.SourceFund} {fund.Percentage.ToString("0.00")+"%"}\n");
            }

            return result.ToString().Trim();
        }
    }
}
