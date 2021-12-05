using System;
using WaterManagement;
using Xunit;

namespace WaterManagementProcessorTests
{
    public class TwoBhkAparmentTest
    {
        private TwoBhkApartment TwoBhkAparment;
        private readonly int corporationWaterRatio;
        private readonly int borewellWaterRatio;

        public TwoBhkAparmentTest()
        {     
            corporationWaterRatio = 1;
            borewellWaterRatio = 2;
            TwoBhkAparment = new TwoBhkApartment(corporationWaterRatio, borewellWaterRatio);
        }
        [Fact]
        public void ShouldReturnPrice_WhenPassedCorporateAndBorewellRatio()
        {
            //Act and Assert

            Assert.Equal(900, TwoBhkAparment.AllocatedWater);
            Assert.Equal(1200,TwoBhkAparment.CalculateTotalPrice());
        }
        [Fact]
        public void ShouldAddGuests_CheckAllocatedWaterForAMonth()
        {
            TwoBhkAparment.AddGuests(2);
            TwoBhkAparment.AddGuests(4);
            TwoBhkAparment.AddGuests(6);
            Assert.Equal(12, TwoBhkAparment.NumberOfGuests);
            Assert.Equal(3600, TwoBhkAparment.AllocatedWaterToGuests);
        }

        [Fact]
        public void AddGuestsWithCorporationAndBorewellWater_ReturnTotalPrice()
        {

            TwoBhkAparment.AddGuests(2);
            Assert.Equal(600, TwoBhkAparment.AllocatedWaterToGuests);
            Assert.Equal(2500, TwoBhkAparment.CalculateTotalPrice());
        }

        [Fact]
        public void ShouldAddGuestsAsZero_ReturnException()
        {
            var exception = Assert.Throws<ArgumentException>(() => TwoBhkAparment.AddGuests(0));
            Assert.Equal("guests", exception.ParamName);
        }
        [Fact]
        public void RealScenario_ReturnTotalPrice()
        {
            TwoBhkAparment = new TwoBhkApartment(3, 7);
            TwoBhkAparment.AddGuests(2);
            TwoBhkAparment.AddGuests(3);

            Assert.Equal(2400, TwoBhkAparment.TotalWaterUsed());
            Assert.Equal(5215, TwoBhkAparment.CalculateTotalPrice());
        }

    }
}
