using System;
using WaterManagement.Interfaces;

namespace WaterManagement
{
    public class TwoBhkApartment : Apartment
    {
        protected override int NumberOfPeopleAssigned { get; set; } = 3;

        public TwoBhkApartment(int corportationWaterRatio, int borewellWaterRatio) : base(corportationWaterRatio, borewellWaterRatio)
        {
        }

    }
}
