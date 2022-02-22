using Convience.EntityFrameWork.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable

namespace Convience.Entity.Entity.EIP
{
    [Entity(DbContextType = typeof(EIPContext))]
    public partial class HrCandidate
    {
        public int CandidateId { get; set; }
        public string Username { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public int? Status { get; set; }
    }
}
