using Convience.Entity.Entity.EIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convience.Model.Models.EIP
{
    public class ViewhrCandidate : HrCandidate
    {
        public string StatusDesc { get; set; }
        //public string UnitDesc { get; set; }
        public string User { get; set; }
    }
    public record QueryCandidate : PageQueryModel
    {
        public int CandidateId { get; set; }
        public string name { get; set; }
    }
}
