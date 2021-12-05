using MyMoneySolution.Constants;
namespace MyMoneySolution
{
    public static class InvestmentExtensions
    {
        internal static bool CheckRebalanceMonth(this Month month)
        {
            return month == Month.JUNE || month == Month.DECEMBER; 
        }
    }
}
