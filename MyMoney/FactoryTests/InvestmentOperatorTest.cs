using MyMoneySolution;
using MyMoneySolution.Constants;
using Xunit;

namespace FactoryTests
{
    public class InvestmentOperatorTest
    {
        private readonly InvestmentOperator investmentOperator;

        public InvestmentOperatorTest()
        {
            investmentOperator = new  InvestmentOperator();
        }

        [Fact]
        public void AllocateFund_ReturnSameAllocation()
        {
            //Arrange
            double expected = 2000;
            //Act
            investmentOperator.AllocateAmount(InvestmentCategory.Equity, expected);
            investmentOperator.AllocateAmount(InvestmentCategory.Gold, expected);
            investmentOperator.AllocateAmount(InvestmentCategory.Debt, expected);

            //Assert
            Assert.Equal(expected, investmentOperator.GetAllocatedAmount(InvestmentCategory.Equity));
            Assert.Equal(expected, investmentOperator.GetAllocatedAmount(InvestmentCategory.Debt));
            Assert.Equal(expected, investmentOperator.GetAllocatedAmount(InvestmentCategory.Gold));
        }

        [Fact]
        public void AllocateFund_ChangeValueBeforeJune_ReturnRebalanceNull()
        {
            //Arrange
            investmentOperator.AllocateAmount(InvestmentCategory.Equity, 1000);
            investmentOperator.AllocateAmount(InvestmentCategory.Gold, 2000);
            investmentOperator.AllocateAmount(InvestmentCategory.Debt, 3000);
            //Act

            investmentOperator.Change(InvestmentCategory.Equity, 10, Month.JANUARY);
            investmentOperator.Change(InvestmentCategory.Gold, 10, Month.JANUARY);
            investmentOperator.Change(InvestmentCategory.Debt, 10, Month.JANUARY);
 
            //Assert
            Assert.Equal(0, investmentOperator.GetLastRebalanceAmount(InvestmentCategory.Equity));
            Assert.Equal(0, investmentOperator.GetLastRebalanceAmount(InvestmentCategory.Debt));
            Assert.Equal(0, investmentOperator.GetLastRebalanceAmount(InvestmentCategory.Gold));
        }

        [Fact]
        public void AllocateFundAndSip_ChangeValueTillJune_ReturnLastInvestment_AndRebalance()
        {
            //Arrange
            double equityAllotment= 6000;
            double debtAllotment = 3000;
            double goldAllotment = 1000;

            investmentOperator.AllocateAmount(InvestmentCategory.Equity, equityAllotment);
            investmentOperator.AllocateAmount(InvestmentCategory.Gold, goldAllotment);
            investmentOperator.AllocateAmount(InvestmentCategory.Debt, debtAllotment);

            investmentOperator.AllocateSipAmount(InvestmentCategory.Equity, 2000);
            investmentOperator.AllocateSipAmount(InvestmentCategory.Gold, 500);
            investmentOperator.AllocateSipAmount(InvestmentCategory.Debt, 1000);

            //Act
            investmentOperator.Change(InvestmentCategory.Equity, 4, Month.JANUARY);
            investmentOperator.Change(InvestmentCategory.Gold, 2, Month.JANUARY);
            investmentOperator.Change(InvestmentCategory.Debt, 10, Month.JANUARY);
            investmentOperator.Change(InvestmentCategory.Equity, -10, Month.FEBRUARY);
            investmentOperator.Change(InvestmentCategory.Gold, 0, Month.FEBRUARY);
            investmentOperator.Change(InvestmentCategory.Debt, 40, Month.FEBRUARY);

            investmentOperator.Change(InvestmentCategory.Equity, 12.50, Month.MARCH);
            investmentOperator.Change(InvestmentCategory.Gold, 12.50, Month.MARCH);
            investmentOperator.Change(InvestmentCategory.Debt, 12.50, Month.MARCH);

            investmentOperator.Change(InvestmentCategory.Equity, 8, Month.APRIL);
            investmentOperator.Change(InvestmentCategory.Gold, 7, Month.APRIL);
            investmentOperator.Change(InvestmentCategory.Debt, -3, Month.APRIL);

            investmentOperator.Change(InvestmentCategory.Equity, 13, Month.MAY);
            investmentOperator.Change(InvestmentCategory.Gold, 10.50, Month.MAY);
            investmentOperator.Change(InvestmentCategory.Debt, 21, Month.MAY);

            investmentOperator.Change(InvestmentCategory.Equity, 10, Month.JUNE);
            investmentOperator.Change(InvestmentCategory.Gold, -5, Month.JUNE);
            investmentOperator.Change(InvestmentCategory.Debt, 8, Month.JUNE);

            //Assert

            Assert.Equal(6240, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, Month.JANUARY));
            Assert.Equal(3300, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.JANUARY));
            Assert.Equal(1020, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.JANUARY));
            Assert.Equal(7416, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, Month.FEBRUARY));
            Assert.Equal(6020, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.FEBRUARY));
            Assert.Equal(1520, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.FEBRUARY));
            
            Assert.Equal(10593, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, Month.MARCH));
            Assert.Equal(7897, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.MARCH));
            Assert.Equal(2272, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.MARCH));
            
            Assert.Equal(13600, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, Month.APRIL));
            Assert.Equal(8630, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.APRIL));
            Assert.Equal(2966, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.APRIL));

            Assert.Equal(17628, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, Month.MAY));
            Assert.Equal(11652, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.MAY));
            Assert.Equal(3829, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.MAY));

            Assert.Equal(23619, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Equity, Month.JUNE));
            Assert.Equal(11809, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Debt, Month.JUNE));
            Assert.Equal(3936, investmentOperator.GetBalanceBasedOnMonth(InvestmentCategory.Gold, Month.JUNE));

            Assert.Equal(23619, investmentOperator.GetLastRebalanceAmount(InvestmentCategory.Equity));
            Assert.Equal(11809, investmentOperator.GetLastRebalanceAmount(InvestmentCategory.Debt));
            Assert.Equal(3936, investmentOperator.GetLastRebalanceAmount(InvestmentCategory.Gold));


        }


    }
}
