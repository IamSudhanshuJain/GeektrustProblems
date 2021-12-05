using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterManagement
{
    public class Utility
    {
        public static double CalculateTankerPrice(int totalLitreOfWater)
        {
            var slabRates = new[]
                {
                    new { Lower = 0, Upper = 500, Rate = 2},
                    new { Lower = 500, Upper = 1500, Rate = 3},
                    new { Lower = 1500, Upper = 3000, Rate = 5},
                    new { Lower = 3000, Upper = int.MaxValue, Rate = 8}
                };

            var totalPrice = 0;

            foreach (var rate in slabRates)
            {
                if (totalLitreOfWater > rate.Lower)
                {
                    var taxableAtThisRate = Math.Min(rate.Upper - rate.Lower, totalLitreOfWater - rate.Lower);
                    var taxThisBand = taxableAtThisRate * rate.Rate;
                    totalPrice += taxThisBand;
                }
            }
            return totalPrice;
        }
    }
}
