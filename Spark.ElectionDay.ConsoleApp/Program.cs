using Spark.ElectionDay.Interface;
using Spark.ElectionDay.Lib;

namespace Spark.ElectionDay.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: Spark.ElectionDay.ConsoleApp.exe <file>");
                    Console.ReadKey();
                    return;
                }
                string filename = args[0];
                IElectionDataProvider electionDataProvider = new FileElectionDataProvider(filename);

                //Console.WriteLine("Считываем данные...");
                var elections = electionDataProvider.GetElection();
                //Console.WriteLine($"Считано {elections.Count()} блоков.");

                //Console.WriteLine("Идёт подсчет...");
                VoteCalculator voteCalculator = new VoteCalculator();
                foreach (var election in elections)
                {
                    var vote_result = voteCalculator.CalculateVote(election);
                    //Console.WriteLine("Победители в блоке:");
                    foreach (var candidate in vote_result)
                        Console.WriteLine($"{candidate.Candidate.Name} ");
                    Console.WriteLine();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        private void outElection(IEnumerable<Election> elections)
        {
            foreach (var elect in elections)
            {
                Console.WriteLine("Candidates:");
                elect.Candidates.ForEach(candidate => Console.WriteLine(candidate.Name));

                Console.WriteLine("Bulletins:");
                foreach (var bullet in elect.Bulletins)
                {
                    bullet.ListPreferences.ForEach(preferences => Console.Write($" {preferences}"));
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
