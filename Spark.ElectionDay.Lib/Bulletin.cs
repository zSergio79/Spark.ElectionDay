using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.ElectionDay.Lib
{
    public class Bulletin
    {
        private List<int> listOfPreferences = new List<int>();
        public List<int> ListPreferences { get { return listOfPreferences; } set { listOfPreferences = value ?? throw new ArgumentNullException(); } }
    }
}
