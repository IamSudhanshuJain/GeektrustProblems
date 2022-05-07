using MyMoneySolution.Model;
using Newtonsoft.Json;
using PortfolioOverlap.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneySolution
{
    public class LoadFundsInMemory
    {
        public static Fund Funds;
        public static void LoadJson()
        {
            var json= new WebClient().DownloadString(Resources.URL);
            Funds = JsonConvert.DeserializeObject<Fund>(json);
        }
    }
}
