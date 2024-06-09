using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.ElectionDay.Lib
{
    public class Election
    {
        #region private fields
        private List<Candidate> candidates;
        private List<Bulletin> bulletins;
        private int id;
        #endregion

        #region Public Properties
        public int Id {  get { return id; } set { id = value; } }
        public List<Candidate> Candidates { get { return candidates; } set { candidates = value ?? throw new ArgumentNullException(); } }
        public List<Bulletin> Bulletins { get { return bulletins; } set { bulletins = value ?? throw new ArgumentNullException(); } }
        #endregion

        #region .ctor
        public Election()
        {
            Candidates = new List<Candidate>();
            Bulletins = new List<Bulletin>();
        }
        #endregion
    }
}
