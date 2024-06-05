using Spark.ElectionDay.Interface;
using Spark.ElectionDay.Lib;

namespace Spark.ElectionDay.ConsoleApp
{
    internal class Program
    {
        private const string filename = "elect.txt";
        static void Main(string[] args)
        {
            try
            {
                IElectionDataProvider electionDataProvider = new FileElectionDataProvider(filename);

                var elections = electionDataProvider.GetElection();

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
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
