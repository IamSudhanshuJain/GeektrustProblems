using MyMoneySolution.Constants;
using System;
using WaterManagement.Interfaces;

namespace WaterManagement
{
    public class CommandProcessor
    {
        public Apartment Apartment;

        public void ProcessInput(string command)
        {
            command = command.Replace(":", " ");
            string[] commandParams = command.Split(' ');


            switch (commandParams[0])
            {
                case InputCommand.ALLOT_WATER:
                    AllotWater(commandParams);
                    break;

                case InputCommand.ADD_GUESTS:
                    AddGuests(int.Parse(commandParams[1]));
                    break;

                case InputCommand.BILL:
                    Console.WriteLine(DisplayTotalBill());
                    break;

                default:
                    Console.WriteLine(Message.INVALID_COMMAND);
                    break;
            }
        }

        private Apartment GetApartmentInstance(int choice, int corporateWaterRatio, int borewellWaterRatio)
        {
            return choice switch
            {
                2 => new TwoBhkApartment(corporateWaterRatio, borewellWaterRatio),
                3 => new ThreeBhkApartment(corporateWaterRatio, borewellWaterRatio),
                _ => null,
            };
        }

        private void AllotWater(string[] commandParams)
        {
            Apartment = GetApartmentInstance(int.Parse(commandParams[1]), int.Parse(commandParams[2]), int.Parse(commandParams[3]));
        }

        private void AddGuests(int numberOfPeople)
        {
            Apartment.AddGuests(numberOfPeople);
        }

        public string DisplayTotalBill()
        {
            return $"{TotalWaterUsed()} {Bill()}";
        }
        private double Bill()
        {
            return Apartment.CalculateTotalPrice();
        }
        private int TotalWaterUsed()
        {
            return Apartment.TotalWaterUsed();
        }
    }
}
