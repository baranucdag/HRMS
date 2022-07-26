using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CandidateQueryParams
    {
        public bool SortType { get; set; } = true;
        public string QueryString { get; set; }  //first-last name/ email/ 
    }
}
