using Convience.Entity.Entity.EIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convience.Model.Models.EIP
{
    public class ViewhrInterview : HrInterview
    {
        public string StatusDesc { get; set; }
        //public string UnitDesc { get; set; }
        public string User { get; set; }
        public string UserName { get; set; }
        public string FileName { get; set; }
        public string N_date { get { return NoticeDate.HasValue ? NoticeDate.Value.ToString("yyyy/MM/dd") : ""; } }
        public string I_date { get { return InterviewDate.HasValue ? InterviewDate.Value.ToString("yyyy/MM/dd HH:mm") : ""; } }
        public string V_date { get { return ValidDate.HasValue ? ValidDate.Value.ToString("yyyy/MM/dd") : ""; } }
        public string R_date { get { return ReplyDate.HasValue ? ReplyDate.Value.ToString("yyyy/MM/dd") : ""; } }
    }
    public record QueryInterview : PageQueryModel
    {
        public int InterviewId { get; set; }
        public DateTime? date { get; set; }

        public string _date { get { return date.HasValue ? date.Value.ToString("yyyy-MM-dd") : ""; } }

    }
}
