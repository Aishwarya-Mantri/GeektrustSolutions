using System;
using System.Collections.Generic;
using System.IO;

namespace PortfolioOverlap
{
    class Program
    {
        static void Main(string[] args)
        {
            String inputFile = "input.txt"; //args[0];
            List<string> commands = new List<string>();
            using (FileStream fileStream = new FileStream(inputFile, FileMode.Open))
            {
                StreamReader reader = new StreamReader(fileStream);
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    commands.Add(input);
                }
            }
            PortfolioOperation.Execute(commands);
            Console.ReadLine();
        }
    }
}
