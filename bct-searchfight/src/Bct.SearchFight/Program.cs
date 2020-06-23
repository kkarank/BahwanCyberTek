using System;
using System.Linq;
using System.Threading.Tasks;
using Bct.SearchFight.Core;

namespace Bct.SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No terms were specified for the Search Fight. Please execute again with the search terms.");
                return;
            }

            Console.WriteLine("Executing Search Fight....");
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            await SearchFightKernel.ExecuteSearchFight(args.ToList());
            SearchFightKernel.Reports.ForEach(report => Console.WriteLine(report));            
        }
    }
}