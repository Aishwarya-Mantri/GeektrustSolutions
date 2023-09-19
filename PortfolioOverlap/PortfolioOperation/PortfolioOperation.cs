using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioOverlap
{
    public class PortfolioOperation
    {
        //private MutualFund _mutualFund;
        //public PortfolioOperation(MutualFund mutualFund)
        //{
        //    _mutualFund = mutualFund;
        //}
           
        public static void Execute(List<string> commands)
        {
            MutualFund.SetListedMutualFunds();
            foreach (var command in commands)
            {
                var instructions = command.Split(" ");
                var operation = Enum.Parse(typeof(Operation), instructions[0]);

                switch (operation)
                {
                    case Operation.CURRENT_PORTFOLIO:
                        Investor.SetCurrentPortfolio(instructions);
                        break;

                    case Operation.CALCULATE_OVERLAP:
                        CalculateOverlap(instructions);
                        break;

                    case Operation.ADD_STOCK:
                        UpdateMutualFund(instructions);
                        break;
                }
            }
        }

        public static void CalculateOverlap(string[] instructions)
        {
            var newFund = instructions[1];
            var NewFundStocks = MutualFund.GetStocksByMutualFund(newFund);

            if (NewFundStocks.Count == 0)
            {
                Console.WriteLine("FUND_NOT_FOUND");
                return;
            }

            foreach (var currentFund in Investor.Portfolio.Funds)
            {
                var overlapPercentage = CalculateOverlapPercentage(NewFundStocks, MutualFund.GetStocksByMutualFund(currentFund.Name));
                if (overlapPercentage > 0)
                {
                    Console.WriteLine(newFund + " " + currentFund.Name + " " + overlapPercentage.ToString("N2") + "%");
                }
            }
        }

        public static void UpdateMutualFund(string[] command)
        {
            var fundName = command[1];
            var stock = string.Join(" ", command.Skip(2));

            if (MutualFund.Funds.ContainsKey(fundName))
            {
                MutualFund.Funds[fundName].Add(stock);
            }
            else
            {
                Console.WriteLine("FUND_NOT_FOUND");
            }
        }

        private static double CalculateOverlapPercentage(List<string> newFundStocks, List<string> currentFundStocks)
        {
            var totalStocks = newFundStocks.Count + currentFundStocks.Count;
            var commonStocks = newFundStocks.Intersect(currentFundStocks).Count();
            var overlapingPercentage = 2 * ((double)commonStocks / totalStocks) * 100;
            return overlapingPercentage;
        }
    }
}
