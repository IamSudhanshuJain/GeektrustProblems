using MyMoneySolution;
using MyMoneySolution.Constants;
using MyMoneySolution.FundFactories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FactoryTests
{
    public class EquityFundFactoryTest
    {
        private readonly InvestmentFundFactory equity;

        public EquityFundFactoryTest()
        {
            equity = new EquityFundFactory();
        }

        [Fact]
        public void AllocateEquityFund_ReturnSame()
        {
            //Arrange
            double expected = 2000;
            //Act
            equity.AllocateAmount(expected);
            var actual = equity.GetAllocatedAmount();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AllocateEquityFundLessThanZero_ReturnException()
        {
            //Arrange
            double allocationAmount = -2000;
            //Act and Assert
            Assert.Throws<ArgumentException>(() => equity.AllocateAmount(allocationAmount));
        }
        [Fact]
        public void AllocateEquityFundEqualToZero_ReturnException()
        {
            //Arrange
            double allocationAmount = 0;
            //Act and Assert
            Assert.Throws<ArgumentException>(() => equity.AllocateAmount(allocationAmount));
        }
        [Fact]
        public void AllocateEquityAndApplyChangeValue_ReturnChangedEquityAmount()
        {
            //Arrange
            double expected = 2200;
            equity.AllocateAmount(2000);

            //Act
            equity.Change(10, Month.JANUARY);
            var actual = equity.GetInvestedAmountBasedOnMonth(Month.JANUARY);

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ChangedValueWithoutAllocation_ReturnChangedEquityAmount()
        {
            //Arrange
            double expected = 0;
            //Act
            equity.Change(10, Month.JANUARY);
            var actual = equity.GetInvestedAmountBasedOnMonth(Month.JANUARY);
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenAllocationAndChangeValue_ReturnInvestment_BasedOnMonth()
        {
            //Arrange
            double expected = 2200;
            equity.AllocateAmount(2000);

            //Act
            equity.Change(10, Month.JANUARY);
            var actual = equity.GetInvestedAmountBasedOnMonth(Month.JANUARY);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithoutAllocation_ReturnException_OnInvestmentBasedOnMonth()
        {
            //Arrange

            //Act and Assert
            Assert.Throws<KeyNotFoundException>(()=>equity.GetInvestedAmountBasedOnMonth(Month.JANUARY));
        }

    }
}
