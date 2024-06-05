using Spark.ElectionDay.Lib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.ElectionDay.Interface
{
    public interface IElectionDataProvider
    {
        Election GetElection(); 
    }
}
