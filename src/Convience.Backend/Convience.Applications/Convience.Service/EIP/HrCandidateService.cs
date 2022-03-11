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
    public interface IHrCandidateService
    {
        public PagingResultModel<ViewhrCandidate> GetCandidatelList(QueryCandidate query);
        public ViewhrCandidate GetCandidateDetail(QueryCandidate query);
        public bool UpdateCandidate(ViewhrCandidate data);
        public bool DeleteList(ViewhrCandidate data);
        public ViewhrCandidate AddList(ViewhrCandidate data);

    }
    public class HrCandidateService : IHrCandidateService
    {
        private readonly EIPContext _context;
        private readonly IRepository<HrCandidate> _hrCandidateRepository;
        public HrCandidateService(IRepository<HrCandidate> hrCandidateRepository, EIPContext context)
        {
            //_mapper = mapper;
            _hrCandidateRepository = hrCandidateRepository;
            _context = context;
        }

        public PagingResultModel<ViewhrCandidate> GetCandidatelList(QueryCandidate query)
        {
            int skip = (query.Page - 1) * query.Size;

            var resultQuery = (from user in _context.HrCandidates                   
                               join interview in _context.HrInterviews on new { user.CandidateId } equals new { CandidateId=interview.Candidate.Value }
                               into a from interview in a.DefaultIfEmpty()
                               join statusdesc in _context.HrStatusdescs on user.Status equals statusdesc.StatusId

                               select new ViewhrCandidate
                               {
                                   CandidateId = user.CandidateId,
                                   Username = user.Username,
                                   Cellphone = user.Cellphone,
                                   Email = user.Email,
                                   Status = user.Status,
                                   Guid = interview.Guid,
                                   StatusDesc = statusdesc.StatusDesc,
                               })
                          .AndIfHaveValue(query.name, r => r.Username.Contains(query.name));


            var candidates = resultQuery.Skip(skip).Take(query.Size).ToArray();

            return new PagingResultModel<ViewhrCandidate>
            {
                Data = JsonConvert.DeserializeObject<ViewhrCandidate[]>(JsonConvert.SerializeObject(candidates)),
                Count = resultQuery.Count()
            };
        }
        public ViewhrCandidate GetCandidateDetail(QueryCandidate query)
        {
            var detail = (from hr in _context.HrCandidates
                            select new ViewhrCandidate
                            {
                                CandidateId = hr.CandidateId,
                                Username = hr.Username,
                                Cellphone = hr.Cellphone,
                                Email = hr.Email,
                                Status = hr.Status,

                            })
                          .Where(r => r.CandidateId == query.CandidateId).FirstOrDefault()
                          //.Where(r => r.Org == query.Org)
                          //.Where(r => r.Ekorg == query.Ekorg)
                          ;

            //var suppliers = supplier.ToArray();
            //
            return new ViewhrCandidate
            {
                CandidateId = detail.CandidateId,
                Username = detail.Username,
                Cellphone = detail.Cellphone,
                Email = detail.Email,
                Status = detail.Status,
            };
        }
        public bool UpdateCandidate(ViewhrCandidate data)
        {
            HrCandidate hr = _context.HrCandidates.Where(p => p.CandidateId == data.CandidateId).FirstOrDefault();

            hr.Cellphone = data.Cellphone;
            hr.Email = data.Email;

            hr.LastUpdateDate = DateTime.Now;
            hr.LastUpdateBy = data.User;

            _context.HrCandidates.Update(hr);
            _context.SaveChanges();

            return true;
        }
        public bool DeleteList(ViewhrCandidate data)
        {
            HrCandidate candidate = _context.HrCandidates.Where(p => p.CandidateId == data.CandidateId).FirstOrDefault();


            //if (rfqm != null || inforecord != null || qoth != null)
            //{
            //    throw new Exception("此料號已有申請紀錄，無法刪除");
            //}

            _context.HrCandidates.Remove(candidate);
            _context.SaveChanges();
            return true;
        }
        public ViewhrCandidate AddList(ViewhrCandidate data)
        {
            string no = string.Empty;
            string year = DateTime.Now.ToString("yy");

            HrCandidate name = _context.HrCandidates.Where(p => p.Username == data.Username).FirstOrDefault();
            HrCandidate cellphone = _context.HrCandidates.Where(p => p.Cellphone == data.Cellphone).FirstOrDefault();


            if (name!=null && cellphone!=null)
            {
                throw new Exception("此應聘者已重複建立");
            }


            HrCandidate hr = new HrCandidate()
            {
                Username = data.Username,
                Cellphone = data.Cellphone,
                Email = data.Email,
                Status = 1,

                CreateDate = DateTime.Now,
                CreateBy = data.User,
                LastUpdateDate = DateTime.Now,
                LastUpdateBy = data.User,
            };

            _context.HrCandidates.Add(hr);
            _context.SaveChanges();

            return new ViewhrCandidate
            {
                Username = hr.Username,
            };
        }
    }
}

