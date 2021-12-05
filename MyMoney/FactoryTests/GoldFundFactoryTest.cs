using MyMoneySolution;
using MyMoneySolution.Constants;
using MyMoneySolution.FundFactories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FactoryTests
{
    public class GoldFundFactoryTest
    {
        private readonly InvestmentFundFactory gold;

        public GoldFundFactoryTest()
        {
            gold = new GoldFundFactory();
        }

        [Fact]
        public void AllocategoldFund_ReturnSame()
        {
            //Arrange
            double expected = 2000;
            //Act
            gold.AllocateAmount(expected);
            var actual = gold.GetAllocatedAmount();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AllocategoldFundLessThanZero_ReturnException()
        {
            //Arrange
            double allocationAmount = -2000;
            //Act and Assert
            Assert.Throws<ArgumentException>(() => gold.AllocateAmount(allocationAmount));
        }
        [Fact]
        public void AllocategoldFundEqualToZero_ReturnException()
        {
            //Arrange
            double allocationAmount = 0;
            //Act and Assert
            Assert.Throws<ArgumentException>(() => gold.AllocateAmount(allocationAmount));
        }
        [Fact]
        public void AllocategoldAndApplyChangeValue_ReturnChangedgoldAmount()
        {
            //Arrange
            double expected = 2200;
            gold.AllocateAmount(2000);

            //Act
            gold.Change(10, Month.JANUARY);
            var actual = gold.GetInvestedAmountBasedOnMonth(Month.JANUARY);

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ChangedValueWithoutAllocation_ReturnChangedgoldAmount()
        {
            //Arrange
            double expected = 0;
            //Act
            gold.Change(10, Month.JANUARY);
            var actual = gold.GetInvestedAmountBasedOnMonth(Month.JANUARY);
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenAllocationAndChangeValue_ReturnInvestment_BasedOnMonth()
        {
            //Arrange
            double expected = 2200;
            gold.AllocateAmount(2000);

            //Act
            gold.Change(10, Month.JANUARY);
            var actual = gold.GetInvestedAmountBasedOnMonth(Month.JANUARY);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithoutAllocation_ReturnException_OnInvestmentBasedOnMonth()
        {
            //Arrange

            //Act and Assert
            Assert.Throws<KeyNotFoundException>(()=>gold.GetInvestedAmountBasedOnMonth(Month.JANUARY));
        }

    }
}
