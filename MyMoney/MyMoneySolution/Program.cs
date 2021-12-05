using MyMoneySolution.FundFactories;
using System;
using System.IO;

namespace MyMoneySolution
{
    class Program
    {
        static void Main(String[] args)
        {
            ProcessCommand(args[0]);
        }
        public static void ProcessCommand(string fileName)
        {
            CommandProcessor commandProcessor = new();
            string filename = fileName;
            FileStream fileStream = new(filename, FileMode.Open);
            string line;
            using StreamReader reader = new(fileStream);
            while ((line = reader.ReadLine()) != null)
            {
                commandProcessor.ProcessInput(line);
            }
        }
     
    }
}
