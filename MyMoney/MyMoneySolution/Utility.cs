using System;

namespace MyMoneySolution
{
    public static class Utility
    {
        public static double GetAccumulatedPercentageValue(double amount, double percentage)
        {
            return amount + GetPercentageValue(amount, percentage);
        }
        public static double GetPercentageValue(double amount, double percentage)
        {
            return Math.Floor(amount * percentage / 100);
        }
        public static double GetPercentage(double amount, double totalAmount)
        {
            return amount / totalAmount * 100;
        }
    }
}
