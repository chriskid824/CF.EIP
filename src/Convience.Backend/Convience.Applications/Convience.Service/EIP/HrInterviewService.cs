using Convience.Entity.Entity.EIP;
using Convience.EntityFrameWork.Repositories;
using Convience.Model.Models;
using Convience.Model.Models.EIP;
using Convience.Util.Extension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Convience.Service.EIP
{
    public interface IHrInterviewService
    {
        public PagingResultModel<ViewhrInterview> GetInterviewList(QueryInterview query);
        public ViewhrInterview GetInterviewDetail(QueryInterview query);
        public bool UpdateInterview(ViewhrInterview data);
        public bool DeleteList(ViewhrInterview data);
        public ViewhrInterview AddList(ViewhrInterview data);
        public PagingResultModel<HrCandidate> GetCandidateList(QueryCandidate query);
        public ViewhrInterview SendMail(ViewhrInterview data);

    }
    public class HrInterviewService : IHrInterviewService
    {
        private readonly EIPContext _context;
        private readonly SPMContext _context2;
        private readonly IRepository<HrInterview> _hrInterviewRepository;
        public HrInterviewService(IRepository<HrInterview> hrInterviewRepository, EIPContext context ,SPMContext context2)
        {
            //_mapper = mapper;
            _hrInterviewRepository = hrInterviewRepository;
            _context = context;
            _context2 = context2;
        }

        public PagingResultModel<ViewhrInterview> GetInterviewList(QueryInterview query)
        {
            int skip = (query.Page - 1) * query.Size;


            var resultQuery = (from hr in _context.HrInterviews
                               join user in _context.HrCandidates on hr.Candidate equals user.CandidateId
                               join work in _context.HrFormWorks on new { user.CandidateId } equals new { work.CandidateId }
                               into a from work in a.DefaultIfEmpty()
                               join statusdesc in _context.HrStatusdescs on user.Status equals statusdesc.StatusId
                               select new ViewhrInterview
                               {
                                   InterviewId = hr.InterviewId,
                                   UserName = user.Username,
                                   Dept = hr.Dept,
                                   NoticeDate = hr.NoticeDate,
                                   InterviewDate = hr.InterviewDate,
                                   Interviewer = hr.Interviewer,
                                   Place = hr.Place,
                                   ReplyDate = work.LastUpdateDate,
                                   //CheckDate = hr.CheckDate,
                                   ValidDate = hr.ValidDate,
                                   StatusDesc = statusdesc.StatusDesc,
                               })                               
                          .AndIfHaveValue(query.date, r => r.InterviewDate.Value.Date.Equals(query.date.Value.Date)).ToList();
            //.AndIfHaveValue(query._date, r => r.InterviewDate.ToString().IndexOf(query._date)>0).ToList();


            var Interviews = resultQuery.Skip(skip).Take(query.Size).ToArray();


            return new PagingResultModel<ViewhrInterview>
            {
                Data = JsonConvert.DeserializeObject<ViewhrInterview[]>(JsonConvert.SerializeObject(Interviews)),
                Count = resultQuery.Count()
            };
        }
        public ViewhrInterview GetInterviewDetail(QueryInterview query)
        {
            var detail = (from hr in _context.HrInterviews
                          join user in _context.HrCandidates on hr.Candidate equals user.CandidateId
                          select new ViewhrInterview
                            {
                                InterviewId = hr.InterviewId,
                                UserName = user.Username,
                                Dept = hr.Dept,
                                NoticeDate = hr.NoticeDate,
                                InterviewDate = hr.InterviewDate,
                                Interviewer = hr.Interviewer,
                                Place = hr.Place,
                                ReplyDate = hr.ReplyDate,
                                CheckDate = hr.CheckDate,
                                ValidDate = hr.ValidDate,
                                Status = hr.Status,
                            })
                          .Where(r => r.InterviewId == query.InterviewId).FirstOrDefault()
                          //.Where(r => r.Org == query.Org)
                          //.Where(r => r.Ekorg == query.Ekorg)
                          ;

            //var suppliers = supplier.ToArray();
            //
            return new ViewhrInterview
            {
                InterviewId = detail.InterviewId,
                UserName = detail.UserName,
                Dept = detail.Dept,
                NoticeDate = detail.NoticeDate,
                InterviewDate = detail.InterviewDate,
                Interviewer = detail.Interviewer,
                Place = detail.Place,
                ReplyDate = detail.ReplyDate,
                CheckDate = detail.CheckDate,
                ValidDate = detail.ValidDate,
                Status = detail.Status,
            };
        }
        public bool UpdateInterview(ViewhrInterview data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.InterviewId == data.InterviewId).FirstOrDefault();

            //hr.Candidate = data.Candidate;
            hr.Dept = data.Dept;
            hr.NoticeDate = data.NoticeDate;
            hr.InterviewDate = data.InterviewDate;
            hr.Interviewer = data.Interviewer;
            hr.Place = data.Place;
            hr.ReplyDate = data.ReplyDate;
            hr.CheckDate = data.CheckDate;
            hr.ValidDate = data.ValidDate;
            //hr.Status = data.Status;

            hr.LastUpdateDate = DateTime.Now;
            hr.LastUpdateBy = data.User;
            
            _context.HrInterviews.Update(hr);
            _context.SaveChanges();

            return true;
        }
        public bool DeleteList(ViewhrInterview data)
        {
            HrInterview Interview = _context.HrInterviews.Where(p => p.InterviewId == data.InterviewId).FirstOrDefault();


            _context.HrInterviews.Remove(Interview);
            _context.SaveChanges();
            return true;
        }
        public ViewhrInterview AddList(ViewhrInterview data)
        {
            HrInterview repit = _context.HrInterviews.Where(p => p.Candidate == data.Candidate).FirstOrDefault();
            HrInterview interview = _context.HrInterviews.Where(p => p.Interviewer == data.Interviewer && p.InterviewDate == data.InterviewDate).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == data.Candidate).FirstOrDefault();
            User interviewer = _context2.Users.Where(p => p.Username == data.Interviewer && p.Enable == "Y" && p.Job!="9" ).FirstOrDefault();

            if (repit != null)
            {
                throw new Exception("此應聘者已重複建立面試申請");
            }
            if (interview != null)
            {
                throw new Exception("該面試官此時段已有其他面試");
            }
            if (user == null)
            {
                throw new Exception("查無此應聘者，請先建立人員資料");
            }
            if (interviewer == null)
            {
                throw new Exception("查無此面試官，請重新輸入");
            }
            Guid g = Guid.NewGuid();
            data.Guid = g.ToString();

            HrInterview hr = new HrInterview()
            {
                Candidate = data.Candidate,
                Dept = data.Dept,
                NoticeDate = data.NoticeDate,
                InterviewDate = data.InterviewDate,
                Interviewer = data.Interviewer,
                Place = data.Place,
                ReplyDate = data.ReplyDate,
                CheckDate = data.CheckDate,
                ValidDate = data.ValidDate,
                Status = 1,
                Guid = g.ToString(),

                CreateDate = DateTime.Now,
                CreateBy = data.User,
                LastUpdateDate = DateTime.Now,
                LastUpdateBy = data.User,
            };

            _context.HrInterviews.Add(hr);
            _context.SaveChanges();

            //Mail(data);

            return new ViewhrInterview
            {
                //Username = hr.Username,
            };
        }
        public PagingResultModel<HrCandidate> GetCandidateList(QueryCandidate query)
        {
            int skip = (query.Page - 1) * query.Size;
            var resultQuery = (from hr in _context.HrCandidates
                               select hr);
            var list = resultQuery.Skip(skip).Take(query.Size).ToArray();


            return new PagingResultModel<HrCandidate>
            {
                Data = JsonConvert.DeserializeObject<HrCandidate[]>(JsonConvert.SerializeObject(list)),
                Count = resultQuery.Count()
            };
        }
        public ViewhrInterview SendMail(ViewhrInterview data)
        {
            HrInterview interview = _context.HrInterviews.Where(p => p.InterviewId == data.InterviewId).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == interview.Candidate).FirstOrDefault();
            if (interview == null)
            {
                throw new Exception("查無此筆面試申請");
            }
            if (interview.Status == 2)
            {
                throw new Exception("此筆面試申請已發送邀請");
            }
            if (interview != null)
            {
                Mail(interview);
                interview.Status = 2;
                interview.LastUpdateDate = DateTime.Now;
                interview.LastUpdateBy = data.User;
                user.Status = 2;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateBy = data.User;

                _context.HrCandidates.Update(user);
                _context.HrInterviews.Update(interview);
                _context.SaveChanges();
            }
            return new ViewhrInterview();
        }
        public void Mail(HrInterview data)
        {
            User boss = _context2.Users.Where(p => p.Username == data.Interviewer && p.Enable == "Y" && p.Job != "9").FirstOrDefault();
            HrCandidate candidate = _context.HrCandidates.Where(p => p.CandidateId == data.Candidate).FirstOrDefault();


            if (boss == null || string.IsNullOrWhiteSpace(boss.Email))
            {
                throw new Exception("查無面試主管mail");
            }
            if (candidate == null || string.IsNullOrWhiteSpace(candidate.Email))
            {
                throw new Exception("查無應聘人員mail");
            }

            SmtpClient cv = new SmtpClient("mail.chenfull.com.tw", 25);
            //啟用SSL
            //cv.EnableSsl = true;
            //憑證            AgentFlow文件管理[AgentFlow@chenfull.com.tw]
            cv.Credentials = new NetworkCredential("mis@chenfull.com.tw", "Chen@full");
            try
            {
                StringBuilder sb = new StringBuilder();

                StringBuilder sbHeading = new StringBuilder();
                sbHeading.AppendLine("<html>");
                sbHeading.AppendLine("<body style='font-family:標楷體;'>");
                sbHeading.AppendLine("<p>您好，我是千附實業HR，誠摯地邀請您前來參加面談，屆時請準時參加。</p><br/>");
                sbHeading.AppendLine($"<p>應聘者：{candidate.Username}</p>");
                sbHeading.AppendLine($"<p>面試職位：{data.Dept}</p>");
                sbHeading.AppendLine($"<p>面試時間：{data.InterviewDate.Value.ToString("yyyy/MM/dd HH:mm")}</p>");
                sbHeading.AppendLine($"<p>面試地點：{data.Place}</p><br/>");

                sbHeading.AppendLine("<p>請於面試前將以下資料填寫完畢：</p>");
                sbHeading.AppendLine($"<p><a href='http://localhost:4200/hr-form/hr-form-work?GUID={data.Guid}'>工作申請表</a></p>");
                sbHeading.AppendLine($"<p><a href='http://localhost:4200/hr-form/hr-form-consent?GUID={data.Guid}'>個資使用同意書</a></p>");
                sbHeading.AppendLine($"<p><a href='http://localhost:4200/hr-form/hr-form-imformation?GUID={data.Guid}'>基本資料問題表</a></p><br/><br/>");

                sbHeading.AppendLine($"<p>Best Regards,</p><br/>");
                sbHeading.AppendLine($"<p>總管理處 管理部 周欣妤 Memin Chou</p>");
                sbHeading.AppendLine($"<p>CHENFULL INTERNATIONAL CO., LTD</p>");
                sbHeading.AppendLine($"<p>E-mail：Memin.chou@chenfull.com.tw</p>");
                sbHeading.AppendLine($"<p>電    話：(02)6626-3000 #3017</p>");
                sbHeading.AppendLine($"<p>地    址：10690台北市大安區忠孝東路四段107號12樓</p>");
                sbHeading.AppendLine("</body>");
                sbHeading.AppendLine("</html>");


                MailMessage msg = new MailMessage("mis@chenfull.com.tw","j556631235@gmail.com", "面試邀請通知", sbHeading.ToString());
                //MailMessage msg = new MailMessage("mis@chenfull.com.tw", candidate.Email, "面試邀請通知", sbHeading.ToString());

                msg.IsBodyHtml = true;
                msg.Bcc.Add("jerrychen@chenfull.com.tw");
                //msg.Bcc.Add(boss.Email);
                cv.Send(msg);


            }
            catch (Exception ex)
            {
                throw new Exception("面試邀請寄送失敗，"+ex.ToString());
            }
        }
    }
}

