using System;
using WaterManagement;
using Xunit;

namespace WaterManagementProcessorTests
{
    public class ThreeBhkAparmentTest
    {
        private ThreeBhkApartment ThreeBhkAparment;
        private readonly int corporationWaterRatio;
        private readonly int borewellWaterRatio;

        public ThreeBhkAparmentTest()
        {     
            corporationWaterRatio = 1;
            borewellWaterRatio = 2;
            ThreeBhkAparment = new ThreeBhkApartment(corporationWaterRatio, borewellWaterRatio);
        }
        [Fact]
        public void ShouldReturnPrice_WhenPassedCorporateAndBorewellRatio()
        {
            //Act and Assert

            Assert.Equal(1500, ThreeBhkAparment.AllocatedWater);
            Assert.Equal(2000, ThreeBhkAparment.CalculateTotalPrice());
        }
        [Fact]
        public void ShouldAddGuests_CheckAllocatedWaterForAMonth()
        {
            ThreeBhkAparment.AddGuests(2);
            ThreeBhkAparment.AddGuests(4);
            ThreeBhkAparment.AddGuests(6);
            Assert.Equal(12, ThreeBhkAparment.NumberOfGuests);
            Assert.Equal(3600, ThreeBhkAparment.AllocatedWaterToGuests);
        }

        [Fact]
        public void AddGuestsWithCorporationAndBorewellWater_ReturnTotalPrice()
        {

            ThreeBhkAparment.AddGuests(2);
            Assert.Equal(600, ThreeBhkAparment.AllocatedWaterToGuests);
            Assert.Equal(3300, ThreeBhkAparment.CalculateTotalPrice());
        }

        [Fact]
        public void ShouldAddGuestsAsZero_ReturnException()
        {
            var exception = Assert.Throws<ArgumentException>(() => ThreeBhkAparment.AddGuests(0));
            Assert.Equal("guests", exception.ParamName);
        }
        [Fact]
        public void RealScenario_ReturnTotalPrice()
        {
            ThreeBhkAparment = new ThreeBhkApartment(2, 1);
            ThreeBhkAparment.AddGuests(4);
            ThreeBhkAparment.AddGuests(1);

            Assert.Equal(3000, ThreeBhkAparment.TotalWaterUsed());
            Assert.Equal(5750, ThreeBhkAparment.CalculateTotalPrice());
        }
        [Fact]
        public void DecimalValueWhenDividing_WaterRatios_ReturnTotalPrice()
        {
            ThreeBhkAparment = new ThreeBhkApartment(5, 4);
            ThreeBhkAparment.AddGuests(3);
            ThreeBhkAparment.AddGuests(5);

            Assert.Equal(3900, ThreeBhkAparment.TotalWaterUsed());
            Assert.Equal(10334, ThreeBhkAparment.CalculateTotalPrice());
        }

    }
}
