using System;

namespace WaterManagement.Interfaces
{
    public abstract class Apartment
    {
        public Apartment(int corportationWaterRatio, int borewellWaterRatio)
        {
            CorporationWaterRatio = corportationWaterRatio;
            BorewellWaterRatio = borewellWaterRatio;
        }
        protected abstract int NumberOfPeopleAssigned { get; set; }
        protected virtual int MaximumWaterPerPerson { get; set; } = 10;
        protected virtual double CorporationWaterCharges { get; set; } = 1;
        protected virtual double BorewellWaterCharges { get; set; } = 1.5;
        protected int TotalNumberOfDaysInMonth { get; set; } = 30;
        protected int CorporationWaterRatio { get; set; }
        protected int BorewellWaterRatio { get; set; }
        public int NumberOfGuests { get; private set; }
        public int AllocatedWater => NumberOfPeopleAssigned * MaximumWaterPerPerson * TotalNumberOfDaysInMonth;
        public int AllocatedWaterToGuests => NumberOfGuests * MaximumWaterPerPerson * TotalNumberOfDaysInMonth;
        protected double TotalCorportationWaterCharges => Math.Round((double)
                    AllocatedWater * CorporationWaterRatio /
                    (CorporationWaterRatio + BorewellWaterRatio)) * CorporationWaterCharges;
        protected double TotalBorewellWaterCharges => Math.Round((double)
                    AllocatedWater * BorewellWaterRatio /
                    (CorporationWaterRatio + BorewellWaterRatio)) * BorewellWaterCharges;




        public void AddGuests(int guests)
        {
            if (guests == 0)
                throw new ArgumentException("guests cannot be zero", nameof(guests));
            NumberOfGuests += guests;
        }
        public int TotalWaterUsed()
        {
            return AllocatedWater + AllocatedWaterToGuests;
        }
        public virtual double CalculateTotalPrice()
        {
            return Math.Round(TotalCorportationWaterCharges 
                + TotalBorewellWaterCharges 
                + Utility.CalculateTankerPrice(AllocatedWaterToGuests));

        }

    }
}
