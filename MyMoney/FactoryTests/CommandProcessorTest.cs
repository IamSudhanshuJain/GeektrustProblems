using MyMoneySolution;
using MyMoneySolution.Constants;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace FactoryTests
{
    public class CommandProcessorTest
    {
        private readonly CommandProcessor command;
        public CommandProcessorTest()
        {
            command = new CommandProcessor();
        }
        [Fact]
        public void AllocateTheAmountForAllFund_InReturn_SameAllocatedAmount_ForAllFund()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("ALLOCATE 8000 6000 3500\n");
            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            var equityAllocatedAmount = command.InvestmentOperator.GetAllocatedAmount(InvestmentCategory.Equity);
            var goldAllocatedAmount = command.InvestmentOperator.GetAllocatedAmount(InvestmentCategory.Gold);
            var debtAllocatedAmount = command.InvestmentOperator.GetAllocatedAmount(InvestmentCategory.Debt);

            //Assert
            Assert.Equal(8000, equityAllocatedAmount);
            Assert.Equal(6000, debtAllocatedAmount);
            Assert.Equal(3500, goldAllocatedAmount);

        }
        [Fact]
        public void AllocateAmount_And_ChangeValue_Return_Equivalent_ChangedValues()
        {

            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("ALLOCATE 3000 2000 1000\n");
            input.Append("CHANGE 10.00% 10.00% 10.00% JANUARY\n");
            input.Append("CHANGE 5.00% 5.00% 5.00% FEBRUARY\n");
            input.Append("BALANCE FEBRUARY\n");

            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());
            command.ProcessInput(inputFormat[2].ToString());
            command.ProcessInput(inputFormat[3].ToString());

            var equityAmount = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity,Month.FEBRUARY);
            var debtAmount = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.FEBRUARY);
            var goldAmount = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.FEBRUARY);
            var message = command.DisplayMessage(new List<double>() { equityAmount, debtAmount, goldAmount });

            //Assert
            Assert.Equal(3465, equityAmount);
            Assert.Equal(2310, debtAmount);
            Assert.Equal(1155, goldAmount);
            Assert.Equal("3465 2310 1155", message);

        }
        [Fact]
        public void BalanceEnquiry_WithoutAllocation_Return_Zero()
        {    
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("BALANCE FEBRUARY\n");
            string[] inputFormat = input.ToString().Split('\n');

            //Act and Assert
            Assert.Throws<KeyNotFoundException>(() => command.ProcessInput(inputFormat[0].ToString()));
        }

        [Fact]
        public void NoAllocation_And_ChangeValue_Return_KeyNotFoundException()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("CHANGE 10.00% 10.00% 10.00% JANUARY\n");
            input.Append("CHANGE 5.00% 5.00% 5.00% FEBRUARY\n");

            string[] inputFormat = input.ToString().Split('\n');
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());

            var equityAllocatedAmount = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, Month.FEBRUARY);
            var debtAllocatedAmount = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.FEBRUARY);
            var goldAllocatedAmount = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.FEBRUARY);


            //Act and Assert
            Assert.Equal(0, equityAllocatedAmount);
            Assert.Equal(0, debtAllocatedAmount);
            Assert.Equal(0, goldAllocatedAmount);
        }

        [Fact]
        public void Rebalance_Till_June_Return_Appropriate_Value()
        {
            //Arrange
            StringBuilder input = new StringBuilder();

            input.Append("ALLOCATE 6000 3000 1000\n");
            input.Append("SIP 2000 1000 500\n");
            input.Append("CHANGE 4.00% 10.00% 2.00% JANUARY\n");
            input.Append("CHANGE -10.00% 40.00% 0.00% FEBRUARY\n");
            input.Append("CHANGE 12.50% 12.50% 12.50% MARCH\n");
            input.Append("CHANGE 8.00% -3.00% 7.00% APRIL\n");
            input.Append("CHANGE 13.00% 21.00% 10.50% MAY\n");
            input.Append("CHANGE 10.00% 8.00% -5.00% JUNE\n");
            input.Append("REBALANCE\n");

            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());
            command.ProcessInput(inputFormat[2].ToString());
            command.ProcessInput(inputFormat[3].ToString());
            command.ProcessInput(inputFormat[4].ToString());
            command.ProcessInput(inputFormat[5].ToString());
            command.ProcessInput(inputFormat[6].ToString());
            command.ProcessInput(inputFormat[7].ToString());
            command.ProcessInput(inputFormat[8].ToString());

            double? equityAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Equity);
            double? debtAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Debt);
            double? goldAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Gold);

            //Assert
            Assert.Equal(23619, equityAllocatedAmount);
            Assert.Equal(11809, debtAllocatedAmount);
            Assert.Equal(3936, goldAllocatedAmount);
            Assert.Equal("23619 11809 3936", $"{equityAllocatedAmount} {debtAllocatedAmount} {goldAllocatedAmount}");

        }


        [Fact]
        public void ChangeValue_Till_October_Return_Appropriate_Value()
        {
            //Arrange
            StringBuilder input = new StringBuilder();

            input.Append("ALLOCATE 15000 3000 12000\n");
            input.Append("SIP 1000 1000 2000\n");
            input.Append("CHANGE 10.00% 10.00% 10.00% JANUARY\n");
            input.Append("CHANGE -15.00% -15.00% -15.00% FEBRUARY\n");
            input.Append("CHANGE 20.00% 10.00% 10.00% MARCH\n");
            input.Append("CHANGE 5.00% -15.00% 20.00% APRIL\n");
            input.Append("CHANGE 15.00% 20.00% 10.00% MAY\n");
            input.Append("CHANGE 10.00% 5.00% -5.00% JUNE\n");
            input.Append("CHANGE 10.00% 10.00% 10% JULY\n");
            input.Append("CHANGE -15.00% -15.00% -15.00% AUGUST\n");
            input.Append("CHANGE 20.00% 10.00% 10.00% SEPTEMBER\n");
            input.Append("CHANGE 5.00% -15.00% 20.00% OCTOBER\n");
            input.Append("BALANCE MARCH\n");
            input.Append("REBALANCE\n");

            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());
            command.ProcessInput(inputFormat[2].ToString());
            command.ProcessInput(inputFormat[3].ToString());
            command.ProcessInput(inputFormat[4].ToString());
            command.ProcessInput(inputFormat[5].ToString());
            command.ProcessInput(inputFormat[6].ToString());
            command.ProcessInput(inputFormat[7].ToString());
            command.ProcessInput(inputFormat[8].ToString());
            command.ProcessInput(inputFormat[9].ToString());
            command.ProcessInput(inputFormat[10].ToString());
            command.ProcessInput(inputFormat[11].ToString());
            command.ProcessInput(inputFormat[12].ToString());
            command.ProcessInput(inputFormat[13].ToString());

            double equityBalance = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, Month.MARCH);
            double debtBalance = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.MARCH);
            double goldBalance = command.InvestmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.MARCH);

            double? equityAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Equity);
            double? debtAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Debt);
            double? goldAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Gold);

            //Assert
            Assert.Equal(19050, equityBalance);
            Assert.Equal(5120, debtBalance);
            Assert.Equal(16412, goldBalance);
            Assert.Equal(32467, equityAllocatedAmount);
            Assert.Equal(6493, debtAllocatedAmount);
            Assert.Equal(25974, goldAllocatedAmount);
        }

        [Fact]
        public void Rebalance_Before_June_Return_Null()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("ALLOCATE 5000 3000 2000\n");
            input.Append("CHANGE 10.00% 10.00% 10.00% JANUARY\n");
            input.Append("CHANGE 5.00% 5.00% 5.00% FEBRUARY\n");
            input.Append("REBALANCE\n");

            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());
            command.ProcessInput(inputFormat[2].ToString());
            command.ProcessInput(inputFormat[3].ToString());

            double? equityAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Equity);
            double? debtAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Debt);
            double? goldAllocatedAmount = command.InvestmentOperator.GetLastRebalanceAmount(InvestmentCategory.Gold);

            string message = command.Rebalance();
            //Assert
            Assert.Equal(0, equityAllocatedAmount);
            Assert.Equal(0, debtAllocatedAmount);
            Assert.Equal(0, goldAllocatedAmount);
            Assert.Equal(Message.CANNOT_REBALANCE, message);
        }
    }
}
