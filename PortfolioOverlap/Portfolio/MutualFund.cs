using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PortfolioOverlap
{
    public class MutualFund
    {
        public List<string> MutualFunds { get; set; }
        public string Name { get; set; }
        public List<string> Stocks { get; set; }
        public static Dictionary<string,List<string>> Funds { get; set; } //MutualFundNameAndStocks

        public static void SetListedMutualFunds()
        {
            var uri = new Uri("https://geektrust.s3.ap-southeast-1.amazonaws.com/portfolio-overlap/stock_data.json");
            var client = new HttpClient();
            var httpResponse = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;
            var mutualFundsData = JsonConvert.DeserializeObject<Portfolio>(httpResponse).Funds;

            var mutualFunds = new Dictionary<string, List<string>>();
            foreach (var fund in mutualFundsData)
            {
                mutualFunds.Add(fund.Name, new List<string>(fund.Stocks));
            }
            Funds =  mutualFunds;
        }

        public static List<string> GetStocksByMutualFund(string fundName)
        {
            var stocks = new List<string>();
            if (Funds.ContainsKey(fundName))
            {
                stocks = Funds[fundName];
            }
            return stocks;
        }
    }
}
