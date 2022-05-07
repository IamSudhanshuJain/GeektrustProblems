using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneySolution.Model
{
    public class Fund
    {
        public List<FundModel> Funds { get; set; }
    }
    public class FundModel
    {
        public string Name { get; set; }
        public string[] Stocks { get; set; }
    }
    public class FundResponseModel
    {
        public string SourceFund { get; set; }
        public string CompareFund { get; set; }
        public double Percentage { get; set; }
    }
}
