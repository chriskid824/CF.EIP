using Convience.Entity.Entity.EIP;
using Convience.EntityFrameWork.Repositories;
using Convience.Model.Models;
using Convience.Model.Models.EIP;
using Convience.Util.Extension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
    public class HrInterviewService : IHrInterviewService
    {
        private readonly EIPContext _context;
        private readonly IRepository<HrInterview> _hrInterviewRepository;
        public HrInterviewService(IRepository<HrInterview> hrInterviewRepository, EIPContext context)
        {
            //_mapper = mapper;
            _hrInterviewRepository = hrInterviewRepository;
            _context = context;
        }

        public PagingResultModel<ViewhrInterview> GetInterviewList(QueryInterview query)
        {
            int skip = (query.Page - 1) * query.Size;

            var resultQuery = (from hr in _context.HrInterviews

                               select new ViewhrInterview
                               {
                                   InterviewId = hr.InterviewId,
                                   Candidate = hr.Candidate,
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
                          .AndIfHaveValue(query.date, r => r.InterviewDate.ToString().Contains(query.date));


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
                            select new ViewhrInterview
                            {
                                InterviewId = hr.InterviewId,

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
            };
        }
        public bool UpdateInterview(ViewhrInterview data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.InterviewId == data.InterviewId).FirstOrDefault();

            hr.Candidate = data.Candidate;
            hr.Dept = data.Dept;
            hr.NoticeDate = data.NoticeDate;
            hr.InterviewDate = data.InterviewDate;
            hr.Interviewer = data.Interviewer;
            hr.Place = data.Place;
            hr.ReplyDate = data.ReplyDate;
            hr.CheckDate = data.CheckDate;
            hr.ValidDate = data.ValidDate;
            hr.Status = data.Status;

            hr.LastUpdateDate = DateTime.Now;
            hr.LastUpdateBy = data.User;

            _context.HrInterviews.Update(hr);
            _context.SaveChanges();

            return true;
        }
        public bool DeleteList(ViewhrInterview data)
        {
            HrInterview Interview = _context.HrInterviews.Where(p => p.InterviewId == data.InterviewId).FirstOrDefault();


            //if (rfqm != null || inforecord != null || qoth != null)
            //{
            //    throw new Exception("此料號已有申請紀錄，無法刪除");
            //}

            _context.HrInterviews.Remove(Interview);
            _context.SaveChanges();
            return true;
        }
        public ViewhrInterview AddList(ViewhrInterview data)
        {
            HrInterview interview = _context.HrInterviews.Where(p => p.Interviewer == data.Interviewer && p.InterviewDate == data.InterviewDate).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.Username == data.Candidate).FirstOrDefault();


            if (interview != null)
            {
                throw new Exception("該面試官此時段已有其他面試");
            }
            if (user != null)
            {
                throw new Exception("查無此應聘者，請先建立人員資料");
            }


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

                CreateDate = DateTime.Now,
                CreateBy = data.User,
                LastUpdateDate = DateTime.Now,
                LastUpdateBy = data.User,
            };

            _context.HrInterviews.Add(hr);
            _context.SaveChanges();

            return new ViewhrInterview
            {
                //Username = hr.Username,
            };
        }
    }
}

