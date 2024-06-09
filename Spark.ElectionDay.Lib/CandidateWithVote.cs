using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.ElectionDay.Lib
{
    public class CandidateWithVote
    {
        #region private fields
        private Candidate candidate;
        private int voteCount;
        #endregion

        #region Public Properties
        public Candidate Candidate { get { return candidate; } set { candidate = value ?? throw new ArgumentNullException(nameof(Candidate)); } }
        public int VoteCount { get { return voteCount; } set { voteCount = value; } }
        #endregion

        #region .ctor
        public CandidateWithVote(Candidate candidate)
        {
            Candidate = candidate ?? throw new ArgumentNullException(nameof(Candidate));
        }
        #endregion
    }
}
