using System.Text;
using WaterManagement;
using Xunit;

namespace WaterManagementProcessorTests
{
    public class CommandProcessorTest
    {
        private readonly CommandProcessor commandProcessor;
        public CommandProcessorTest()
        {
            commandProcessor = new CommandProcessor();
        }

        [Fact]
        public void AllotWater()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("ALLOT_WATER 3 2:1\n");
            string[] inputFormat = input.ToString().Split('\n');

            //Act
            commandProcessor.ProcessInput(inputFormat[0].ToString());

            //Assert
            Assert.NotNull(commandProcessor.Apartment);
        }
        [Fact]
        public void AddGuests()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("ALLOT_WATER 3 2:1\n");
            input.Append("ADD_GUESTS 4\n");
            input.Append("ADD_GUESTS 1\n");

            string[] inputFormat = input.ToString().Split('\n');

            //Act
            commandProcessor.ProcessInput(inputFormat[0].ToString());
            commandProcessor.ProcessInput(inputFormat[1].ToString());
            commandProcessor.ProcessInput(inputFormat[2].ToString());

            //Assert
            Assert.Equal(5, commandProcessor.Apartment.NumberOfGuests);

        }
        [Fact]
        public void Bill()
        {
            //Arrange
            StringBuilder input = new StringBuilder();
            input.Append("ALLOT_WATER 3 2:1\n");
            input.Append("ADD_GUESTS 4\n");
            input.Append("ADD_GUESTS 1\n");
            input.Append("BILL\n");

            string[] inputFormat = input.ToString().Split('\n');

            //Act
            commandProcessor.ProcessInput(inputFormat[0].ToString());
            commandProcessor.ProcessInput(inputFormat[1].ToString());
            commandProcessor.ProcessInput(inputFormat[2].ToString());
            commandProcessor.ProcessInput(inputFormat[3].ToString());

            //Assert
            Assert.Equal(5750, commandProcessor.Apartment.CalculateTotalPrice());
            Assert.Equal(3000, commandProcessor.Apartment.TotalWaterUsed());
            Assert.Equal("3000 5750", commandProcessor.DisplayTotalBill());

        }
    }
}
