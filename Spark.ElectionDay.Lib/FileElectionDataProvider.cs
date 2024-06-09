using Spark.ElectionDay.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.ElectionDay.Lib
{
    public class FileElectionDataProvider : IElectionDataProvider
    {
        #region file params
        private string filename;
        #endregion

        #region .ctor
        public FileElectionDataProvider(string file)
        {
            if(string.IsNullOrEmpty(file))
                throw new ArgumentException("file");
            if (File.Exists(file) != true)
                throw new Exception($"File '{file}' not founded.");
            filename = file;
        }
        #endregion

        #region Election Data
        public IEnumerable<Election> GetElection()
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    List<Election> result = new();
                    if (int.TryParse(reader.ReadLine(), out var blocksCount) == true)
                    {
                        reader.ReadLine(); //separator
                        for (int i = 0; i < blocksCount; i++)
                        {
                            // Read Candidates
                            List<Candidate> candidates = new List<Candidate>();
                            int candidatesCount = 0;
                            if (int.TryParse(reader.ReadLine(), out candidatesCount) == true)
                            {
                                for (int j = 0; j < candidatesCount; j++)
                                {
                                    if (reader.EndOfStream == true)
                                        throw new Exception($"Invalid file '{filename}' format.");

                                    var candidateName = reader.ReadLine();
                                    if (string.IsNullOrEmpty(candidateName) == false)
                                        candidates.Add(new Candidate() { Name = candidateName, Id = j + 1 });
                                    else
                                        throw new Exception($"Invalid candidate name in file '{filename}'.");
                                }
                            }
                            else
                                throw new Exception($"Invalid file '{filename}' format. Invalid Blocks count.");
                            if (candidates.Count == 0)
                                throw new Exception($"Invalid candidates counts.  Invalid file '{filename}' format.");

                            //Read Bulletins
                            List<Bulletin> bulletins = new List<Bulletin>();
                            while (reader.EndOfStream != true)
                            {
                                var preferencesLine = reader.ReadLine();

                                if (string.IsNullOrEmpty(preferencesLine) == true)
                                    break; // go next block
                                try
                                {
                                    var preferences = preferencesLine.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                                    if(preferences.Count != candidatesCount)
                                        throw new Exception($"Invalid bulletin counts: '{preferencesLine}' in file '{filename}'.");
                                    bulletins.Add(new Bulletin() {ListPreferences = preferences});
                                }
                                catch
                                {
                                    throw;
                                }

                            }
                            if (bulletins.Count == 0)
                                throw new Exception($"Invalid file '{filename}' format.");

                            result.Add(new Election() { Candidates = candidates, Bulletins = bulletins , Id = i + 1});
                           // reader.ReadLine();
                        }
                    }
                    else
                        throw new Exception($"Invalid file {filename} format.");
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
