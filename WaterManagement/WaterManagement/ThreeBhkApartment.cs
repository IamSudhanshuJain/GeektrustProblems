using System;
using WaterManagement.Interfaces;

namespace WaterManagement
{
    public class ThreeBhkApartment : Apartment
    {
        protected override int NumberOfPeopleAssigned { get; set; } = 5;

        public ThreeBhkApartment(int corportationWaterRatio, int borewellWaterRatio) : base(corportationWaterRatio, borewellWaterRatio)
        {
        }
   
    }
}
