using MyMoneySolution;
using MyMoneySolution.Constants;
using MyMoneySolution.FundFactories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FactoryTests
{
    public class DebtFundFactoryTest
    {
        private readonly InvestmentFundFactory debt;

        public DebtFundFactoryTest()
        {
            debt = new DebtFundFactory();
        }

        [Fact]
        public void AllocatedebtFund_ReturnSame()
        {
            //Arrange
            double expected = 2000;
            //Act
            debt.AllocateAmount(expected);
            var actual = debt.GetAllocatedAmount();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AllocatedebtFundLessThanZero_ReturnException()
        {
            //Arrange
            double allocationAmount = -2000;
            //Act and Assert
            Assert.Throws<ArgumentException>(() => debt.AllocateAmount(allocationAmount));
        }
        [Fact]
        public void AllocatedebtFundEqualToZero_ReturnException()
        {
            //Arrange
            double allocationAmount = 0;
            //Act and Assert
            Assert.Throws<ArgumentException>(() => debt.AllocateAmount(allocationAmount));
        }
        [Fact]
        public void AllocatedebtAndApplyChangeValue_ReturnChangeddebtAmount()
        {
            //Arrange
            double expected = 2200;
            debt.AllocateAmount(2000);

            //Act
            debt.Change(10, Month.JANUARY);
            var actual = debt.GetInvestedAmountBasedOnMonth(Month.JANUARY);

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ChangedValueWithoutAllocation_ReturnChangeddebtAmount()
        {
            //Arrange
            double expected = 0;
            //Act
            debt.Change(10, Month.JANUARY);
            var actual = debt.GetInvestedAmountBasedOnMonth(Month.JANUARY);
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenAllocationAndChangeValue_ReturnInvestment_BasedOnMonth()
        {
            //Arrange
            double expected = 2200;
            debt.AllocateAmount(2000);

            //Act
            debt.Change(10, Month.JANUARY);
            var actual = debt.GetInvestedAmountBasedOnMonth(Month.JANUARY);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithoutAllocation_ReturnException_OnInvestmentBasedOnMonth()
        {
            //Arrange

            //Act and Assert
            Assert.Throws<KeyNotFoundException>(()=>debt.GetInvestedAmountBasedOnMonth(Month.JANUARY));
        }

    }
}
