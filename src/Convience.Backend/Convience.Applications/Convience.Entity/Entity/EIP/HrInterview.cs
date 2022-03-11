using Convience.EntityFrameWork.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable

namespace Convience.Entity.Entity.EIP
{
    [Entity(DbContextType = typeof(EIPContext))]
    public partial class HrInterview
    {
        public int InterviewId { get; set; }
        public int? Candidate { get; set; }
        public string Dept { get; set; }
        public DateTime? NoticeDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string Interviewer { get; set; }
        public string Place { get; set; }
        public DateTime? ReplyDate { get; set; }
        public DateTime? CheckDate { get; set; }
        public DateTime? ValidDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public int? Status { get; set; }
        public string Guid { get; set; }
    }
}
