using System.Collections.Generic;

namespace PortfolioOverlap
{
    public class Investor
    {
        public static Portfolio Portfolio { get; set; }

        public static void SetCurrentPortfolio(string[] instructions)
        {
            var portfolio = new Portfolio
            {
                Funds = new List<MutualFund>()
            };

            for (int i = 1; i<instructions.Length; i++)
            {
                portfolio.Funds.Add(new MutualFund { Name = instructions[i]});
            }

            Portfolio = portfolio;
        }
    }
}
