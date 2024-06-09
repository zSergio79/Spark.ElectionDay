namespace Spark.ElectionDay.Lib
{
    public class VoteCalculator
    {
        #region const
        private const double DEFAULT_TRESHOLD = 50.0;
        #endregion

        #region private fields
        private double treshold = 0.0;
        #endregion

        #region Calculate Method
        public List<CandidateWithVote> CalculateVote(Election election)
        {
            double all_votes = election.Bulletins.Count;

            //Первый этап подсчета голосов.
            var votesCandudates = CalculateFirstStage(election.Candidates, election.Bulletins);
            var winner = votesCandudates.OrderByDescending(x=>x.VoteCount).First();
            if ((winner.VoteCount / all_votes * 100.0) > treshold)
                return new List<CandidateWithVote> { winner };
            else
            {
                //Второй этап.
                all_votes = election.Bulletins.Count * election.Candidates.Count;
                do
                {
                    //Redistribute vote
                    election.Bulletins = Rebuild(election.Bulletins, votesCandudates.OrderByDescending(x=>x.VoteCount).Last());

                    //Calculate second stage
                    votesCandudates = CalculateSecondStage(election.Candidates, election.Bulletins);

                    //Verify conditions
                    var fifty = votesCandudates.Where(w=>w.VoteCount / all_votes * 100.0 > treshold);
                    //Набравшие более 50%; :) only one
                    if (fifty.Count() > 0)
                        return fifty.ToList();
                    //Если голосов у всех поровну
                    //if (votesCandudates.Max(x=>x.VoteCount) == votesCandudates.Min(x=>x.VoteCount))
                    if (votesCandudates.All(w => w.VoteCount == votesCandudates.First().VoteCount))
                        return votesCandudates;
                }
                while (true);
            }
        }

        /// <summary>
        /// Подсчет голосов. Первый этап. Считаются только первые значения
        /// из списка предпочтений.
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="bulletins"></param>
        /// <returns></returns>
        private List<CandidateWithVote> CalculateFirstStage(IEnumerable<Candidate> candidates, IEnumerable<Bulletin> bulletins)
        {
            var result = new List<CandidateWithVote>(candidates.Select(x => new CandidateWithVote(x) { VoteCount = 0 }));

            foreach(var b in bulletins)
                result.First(x=>x.Candidate.Id == b.ListPreferences.First()).VoteCount++;

            return result;
        }

        /// <summary>
        /// Подсчет голосов. Второй этап. 
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="bulletins"></param>
        /// <returns></returns>
        private List<CandidateWithVote> CalculateSecondStage(IEnumerable<Candidate> candidates, IEnumerable<Bulletin> bulletins)
        {
            var result = new List<CandidateWithVote>(candidates.Select(x => new CandidateWithVote(x) { VoteCount = 0 }));

            foreach (var candidate in candidates)
            {
                result.First(c=>c.Candidate.Id == candidate.Id).VoteCount = numberOfEntries(candidate.Id);
            }

            return result;

            int numberOfEntries(int id)
            {
                var count = 0;
                foreach (var b in bulletins)
                {
                    count += b.ListPreferences.Where(x => x == id).Count();
                }
                return count;
            }
        }

        /// <summary>
        /// Перезачет голосов выбывшего кандидата.
        /// </summary>
        /// <param name="bulletins"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        private List<Bulletin> Rebuild(List<Bulletin> bulletins, CandidateWithVote min)
        {
            foreach(var b in bulletins)
            {
                while (b.ListPreferences.Contains(min.Candidate.Id))
                {
                    var index = b.ListPreferences.IndexOf(min.Candidate.Id);
                    var newValue = index - 1 >= 0 ? b.ListPreferences[index - 1] : b.ListPreferences[b.ListPreferences.Count - 1];
                    b.ListPreferences[index] = newValue;
                }
            }
            return bulletins;
        }
        #endregion

        #region .ctor
        public VoteCalculator(double trshld)
        {
            treshold = trshld;
        }
        public VoteCalculator() : this(DEFAULT_TRESHOLD) { }
        #endregion
    }
}
