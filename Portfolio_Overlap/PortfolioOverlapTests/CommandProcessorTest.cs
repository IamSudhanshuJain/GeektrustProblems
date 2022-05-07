using MyMoneySolution;
using System.Text;
using Xunit;

namespace FactoryTests
{
    public class CommandProcessorTest
    {
        private readonly CommandProcessor command;
        public CommandProcessorTest()
        {
            command = new CommandProcessor();
        }
        [Fact]
        public void Add_Funds_In_Current_Portfolio()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("CURRENT_PORTFOLIO UTI_NIFTY_INDEX AXIS_MIDCAP PARAG_PARIKH_FLEXI_CAP");

            //Act
            command.ProcessInput(input.ToString());
            var AllocatedFunds= FundOperator.Funds;
            
            //Assert
            Assert.Equal(3, AllocatedFunds.Length);
            Assert.Equal("UTI_NIFTY_INDEX", AllocatedFunds[0]);
            Assert.Equal("AXIS_MIDCAP", AllocatedFunds[1]);
            Assert.Equal("PARAG_PARIKH_FLEXI_CAP", AllocatedFunds[2]);

        }
        [Fact]
        public void Calculate_Overlap_And_Check_Result()
        {

            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("CURRENT_PORTFOLIO UTI_NIFTY_INDEX AXIS_MIDCAP PARAG_PARIKH_FLEXI_CAP\n");
            input.Append("CALCULATE_OVERLAP ICICI_PRU_NIFTY_NEXT_50_INDEX\n");

            string compareFund = "ICICI_PRU_NIFTY_NEXT_50_INDEX";
            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());
            var fundResponse = FundOperator.CalculateOverlap(compareFund);

            //Assert
            Assert.Equal("UTI_NIFTY_INDEX", fundResponse[0].SourceFund);
            Assert.Equal(compareFund, fundResponse[0].CompareFund);
            Assert.Equal("20.37", fundResponse[0].Percentage.ToString("0.00"));

        }
        [Fact]
        public void Calculate_Overlap_And_Fund_Not_Found()
        {
            //Arrange
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("CURRENT_PORTFOLIO UTI_NIFTY_INDEX AXIS_MIDCAP PARAG_PARIKH_FLEXI_CAP\n");
            input.Append("CALCULATE_OVERLAP NIPPON_INDIA_PHARMA_FUND\n");

            string compareFund = "NIPPON_INDIA_PHARMA_FUND";
            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());
            var fundResponse = FundOperator.CalculateOverlap(compareFund);

            //Assert
            Assert.Null(fundResponse);
        }
        [Fact]
        public void StockAdded_With_Space_Calculate_Overlap_If_Found_Match()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("CURRENT_PORTFOLIO ICICI_PRU_NIFTY_NEXT_50_INDEX AXIS_BLUECHIP AXIS_MIDCAP\n");
            input.Append("CALCULATE_OVERLAP ICICI_PRU_BLUECHIP\n");
            input.Append("ADD_STOCK ICICI_PRU_NIFTY_NEXT_50_INDEX HDFC BANK LIMITED\n");
            input.Append("CALCULATE_OVERLAP ICICI_PRU_BLUECHIP\n");

            string compareFund = "ICICI_PRU_BLUECHIP";
            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());
            command.ProcessInput(inputFormat[2].ToString());
            command.ProcessInput(inputFormat[3].ToString());
            var fundResponse = FundOperator.CalculateOverlap(compareFund);

            //Assert
            Assert.Equal("ICICI_PRU_NIFTY_NEXT_50_INDEX", fundResponse[0].SourceFund);
            Assert.Equal(compareFund, fundResponse[0].CompareFund);
            Assert.Equal("26.89", fundResponse[0].Percentage.ToString("0.00"));
        }
        [Fact]
        public void Calculate_Overlap_If_Other_Stocks_Has_0_Overlap_Ignored()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("CURRENT_PORTFOLIO ICICI_PRU_NIFTY_NEXT_50_INDEX PARAG_PARIKH_CONSERVATIVE_HYBRID ICICI_PRU_BLUECHIP\n");
            input.Append("CALCULATE_OVERLAP AXIS_MIDCAP\n");
            input.Append("CALCULATE_OVERLAP SBI_LARGE_&_MIDCAP\n");

            string compareFund = "SBI_LARGE_&_MIDCAP";
            string[] inputFormat = input.ToString().Split('\n');

            //Act
            command.ProcessInput(inputFormat[0].ToString());
            command.ProcessInput(inputFormat[1].ToString());
            command.ProcessInput(inputFormat[2].ToString());
            var fundResponse = FundOperator.CalculateOverlap(compareFund);

            //Assert
            Assert.Equal("PARAG_PARIKH_CONSERVATIVE_HYBRID", fundResponse[0].SourceFund);
            Assert.Equal(compareFund, fundResponse[0].CompareFund);
            Assert.Equal("8.47", fundResponse[0].Percentage.ToString("0.00"));
        }

      






    }
}
