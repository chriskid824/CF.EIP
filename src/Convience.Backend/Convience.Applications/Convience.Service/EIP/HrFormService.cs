using Convience.Entity.Entity.EIP;
using Convience.EntityFrameWork.Repositories;
using Convience.Helper;
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
    public interface IHrFormService
    {        
        public bool CheckFormURL(ViewhrInterview data);
        public ViewhrFormImformation SendImformation(ViewhrFormImformation data);
        public ViewhrFormWork SendWork(ViewhrFormWork data);
        public ViewhrFormWork GetWorkData(ViewhrFormWork data);
        public ViewhrFormImformation GetImformationData(ViewhrFormImformation data);
        public ViewhrFormConsent SendConsent(ViewhrFormConsent data);
        public ViewhrFormConsent GetConsentData(ViewhrFormConsent data);

        public ViewhrInterview PrintImformation(ViewhrInterview data);        
        public ViewhrInterview PrintWork(ViewhrInterview data);
    }
    public class HrFormService : IHrFormService
    {
        private readonly EIPContext _context;
        private readonly IRepository<HrInterview> _hrFormRepository;
        public HrFormService(IRepository<HrInterview> hrFormRepository, EIPContext context)
        {
            //_mapper = mapper;
            _hrFormRepository = hrFormRepository;
            _context = context;
        }
        public bool CheckFormURL(ViewhrInterview data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.Guid == data.Guid).FirstOrDefault();
            if (hr == null || string.IsNullOrWhiteSpace(data.Guid) || 
                (DateTime.Now > hr.ValidDate && string.IsNullOrWhiteSpace(data.User)))
            {
                return false;
            }
            {
                return true;
            }
        }
        public ViewhrFormImformation SendImformation(ViewhrFormImformation data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.Guid == data.GUID).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == hr.Candidate).FirstOrDefault();
            HrFormImformation imfom_check = _context.HrFormImformations.Where(p => p.CandidateId == user.CandidateId).FirstOrDefault();
            HrFormWork work_check = _context.HrFormWorks.Where(p => p.CandidateId == user.CandidateId).FirstOrDefault();

            if (hr == null)
            {
                throw new Exception("無權限填寫表單，請聯繫HR");
            }
            
            if (imfom_check == null)
            {
                HrFormImformation imformation = new HrFormImformation()
                {
                    CandidateId = user.CandidateId,
                    Q1 = data.Q1,
                    Q2 = data.Q2,
                    Q3 = data.Q3,
                    Q4 = data.Q4,
                    Q5 = data.Q5,
                    Q6 = data.Q6,
                    Q7 = data.Q7,
                    Q8 = data.Q8,
                    Q9 = data.Q9,
                    Q10 = data.Q10,
                    Q11 = data.Q11,
                    Q12 = data.Q12,

                    CreateDate = DateTime.Now,
                    CreateBy = user.Username,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateBy = user.Username,
                };
                _context.HrFormImformations.Add(imformation);
                if (work_check != null)
                {
                    user.Status = 3;
                    hr.Status = 3;
                    _context.HrCandidates.Update(user);
                    _context.HrInterviews.Update(hr);
                }
            }
            else
            {
                imfom_check.Q1 = data.Q1;
                imfom_check.Q2 = data.Q2;
                imfom_check.Q3 = data.Q3;
                imfom_check.Q4 = data.Q4;
                imfom_check.Q5 = data.Q5;
                imfom_check.Q6 = data.Q6;
                imfom_check.Q7 = data.Q7;
                imfom_check.Q8 = data.Q8;
                imfom_check.Q9 = data.Q9;
                imfom_check.Q10 = data.Q10;
                imfom_check.Q11 = data.Q11;
                imfom_check.Q12 = data.Q12;
                imfom_check.LastUpdateDate = DateTime.Now;
                imfom_check.LastUpdateBy = user.Username;

                _context.HrFormImformations.Update(imfom_check);
            }
            _context.SaveChanges();

            return new ViewhrFormImformation();

        }
        public ViewhrFormImformation GetImformationData(ViewhrFormImformation data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.Guid == data.GUID).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == hr.Candidate).FirstOrDefault();
            HrFormImformation imform = _context.HrFormImformations.Where(p => p.CandidateId == user.CandidateId).FirstOrDefault();

            if (imform == null)
            {
                return new ViewhrFormImformation();
            }
            else
            {
                return new ViewhrFormImformation
                {
                    Q1 = imform.Q1,
                    Q2 = imform.Q2,
                    Q3 = imform.Q3,
                    Q4 = imform.Q4,
                    Q5 = imform.Q5,
                    Q6 = imform.Q6,
                    Q7 = imform.Q7,
                    Q8 = imform.Q8,
                    Q9 = imform.Q9,
                    Q10 = imform.Q10,
                    Q11 = imform.Q11,
                    Q12 = imform.Q12,
                };
            }
        }
        public ViewhrFormWork SendWork(ViewhrFormWork data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.Guid == data.GUID).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == hr.Candidate).FirstOrDefault();
            HrFormWork work_check = _context.HrFormWorks.Where(p => p.CandidateId == user.CandidateId).FirstOrDefault();
            HrFormImformation imfrom = _context.HrFormImformations.Where(p => p.CandidateId == user.CandidateId).FirstOrDefault();

            if (hr == null)
            {
                throw new Exception("無權限填寫表單，請聯繫HR");
            }

            if (work_check == null)
            {
                HrFormWork work = new HrFormWork()
                {
                    CandidateId = user.CandidateId,
                    UsernameCh = data.UsernameCh,
                    UsernameEn = data.UsernameEn,
                    Birthday = data.Birthday,
                    Gender = data.Gender,
                    Birthplace = data.Birthplace,
                    Identity = data.Identity,
                    Phone = data.Phone,
                    Cellphone = data.Cellphone,
                    Tellphone = data.Tellphone,
                    Bloodtype = data.Bloodtype,
                    AddressR = data.AddressR,
                    AddressM = data.AddressM,
                    Family1 = data.Family1,
                    Family2 = data.Family2,
                    Family3 = data.Family3,
                    Family4 = data.Family4,
                    Family5 = data.Family5,
                    Family6 = data.Family6,
                    Military = data.Military,
                    MilitaryReason = data.MilitaryReason,
                    MilitaryCategory = data.MilitaryCategory,
                    MilitaryDateS = data.MilitaryDateS,
                    MilitaryDateE = data.MilitaryDateE,
                    MarriageCk1 = data.MarriageCk1,
                    MarriageCk2 = data.MarriageCk2,
                    MarriageCk3 = data.MarriageCk3,
                    MarriageCk4 = data.MarriageCk4,
                    MarriageCk5 = data.MarriageCk5,
                    MarriageCk6 = data.MarriageCk6,
                    MarriageDate = data.MarriageDate,
                    DgName1 = data.DgName1,
                    DgName2 = data.DgName2,
                    DgName3 = data.DgName3,
                    DgName4 = data.DgName4,
                    DgName5 = data.DgName5,
                    DgMajor1 = data.DgMajor1,
                    DgMajor2 = data.DgMajor2,
                    DgMajor3 = data.DgMajor3,
                    DgMajor4 = data.DgMajor4,
                    DgMajor5 = data.DgMajor5,
                    DgDegree1 = data.DgDegree1,
                    DgDegree2 = data.DgDegree2,
                    DgDegree3 = data.DgDegree3,
                    DgDegree4 = data.DgDegree4,
                    DgDegree5 = data.DgDegree5,
                    DgYear1 = data.DgYear1,
                    DgYear2 = data.DgYear2,
                    DgYear3 = data.DgYear3,
                    DgYear4 = data.DgYear4,
                    DgYear5 = data.DgYear5,
                    DgGrad1 = data.DgGrad1,
                    DgGrad2 = data.DgGrad2,
                    DgGrad3 = data.DgGrad3,
                    DgGrad4 = data.DgGrad4,
                    DgGrad5 = data.DgGrad5,
                    DgGradyear1 = data.DgGradyear1,
                    DgGradyear2 = data.DgGradyear2,
                    DgGradyear3 = data.DgGradyear3,
                    DgGradyear4 = data.DgGradyear4,
                    DgGradyear5 = data.DgGradyear5,
                    DgTime1 = data.DgTime1,
                    DgTime2 = data.DgTime2,
                    DgTime3 = data.DgTime3,
                    DgTime4 = data.DgTime4,
                    DgTime5 = data.DgTime5,
                    ExpDept1 = data.ExpDept1,
                    ExpDept2 = data.ExpDept2,
                    ExpDept3 = data.ExpDept3,
                    ExpDept4 = data.ExpDept4,
                    ExpDept5 = data.ExpDept5,
                    ExpPeriod1 = data.ExpPeriod1,
                    ExpPeriod2 = data.ExpPeriod2,
                    ExpPeriod3 = data.ExpPeriod3,
                    ExpPeriod4 = data.ExpPeriod4,
                    ExpPeriod5 = data.ExpPeriod5,
                    ExpIncomeM1 = data.ExpIncomeM1,
                    ExpIncomeM2 = data.ExpIncomeM2,
                    ExpIncomeM3 = data.ExpIncomeM3,
                    ExpIncomeM4 = data.ExpIncomeM4,
                    ExpIncomeM5 = data.ExpIncomeM5,
                    ExpIncomeY1 = data.ExpIncomeY1,
                    ExpIncomeY2 = data.ExpIncomeY2,
                    ExpIncomeY3 = data.ExpIncomeY3,
                    ExpIncomeY4 = data.ExpIncomeY4,
                    ExpIncomeY5 = data.ExpIncomeY5,
                    ExpPosition1 = data.ExpPosition1,
                    ExpPosition2 = data.ExpPosition2,
                    ExpPosition3 = data.ExpPosition3,
                    ExpPosition4 = data.ExpPosition4,
                    ExpPosition5 = data.ExpPosition5,
                    ExpBoss1 = data.ExpBoss1,
                    ExpBoss2 = data.ExpBoss2,
                    ExpBoss3 = data.ExpBoss3,
                    ExpBoss4 = data.ExpBoss4,
                    ExpBoss5 = data.ExpBoss5,
                    ExpResign1 = data.ExpResign1,
                    ExpResign2 = data.ExpResign2,
                    ExpResign3 = data.ExpResign3,
                    ExpResign4 = data.ExpResign4,
                    ExpResign5 = data.ExpResign5,
                    ExpReason1 = data.ExpReason1,
                    ExpReason2 = data.ExpReason2,
                    ExpReason3 = data.ExpReason3,
                    ExpReason4 = data.ExpReason4,
                    ExpReason5 = data.ExpReason5,
                    FmTitle1 = data.FmTitle1,
                    FmTitle2 = data.FmTitle2,
                    FmTitle3 = data.FmTitle3,
                    FmTitle4 = data.FmTitle4,
                    FmTitle5 = data.FmTitle5,
                    FmTitle6 = data.FmTitle6,
                    FmName1 = data.FmName1,
                    FmName2 = data.FmName2,
                    FmName3 = data.FmName3,
                    FmName4 = data.FmName4,
                    FmName5 = data.FmName5,
                    FmName6 = data.FmName6,
                    FmAge1 = data.FmAge1,
                    FmAge2 = data.FmAge2,
                    FmAge3 = data.FmAge3,
                    FmAge4 = data.FmAge4,
                    FmAge5 = data.FmAge5,
                    FmAge6 = data.FmAge6,
                    FmEducation1 = data.FmEducation1,
                    FmEducation2 = data.FmEducation2,
                    FmEducation3 = data.FmEducation3,
                    FmEducation4 = data.FmEducation4,
                    FmEducation5 = data.FmEducation5,
                    FmEducation6 = data.FmEducation6,
                    FmJob1 = data.FmJob1,
                    FmJob2 = data.FmJob2,
                    FmJob3 = data.FmJob3,
                    FmJob4 = data.FmJob4,
                    FmJob5 = data.FmJob5,
                    FmJob6 = data.FmJob6,
                    SfQ11 = data.SfQ11,
                    SfQ21 = data.SfQ21,
                    SfQ22 = data.SfQ22,
                    SfQ31 = data.SfQ31,
                    SfQ32 = data.SfQ32,
                    SfQ33 = data.SfQ33,
                    SfQ43 = data.SfQ43,
                    SfQ4Ck1 = data.SfQ4Ck1,
                    SfQ4Ck2 = data.SfQ4Ck2,
                    SfQ4Ck3 = data.SfQ4Ck3,
                    SfQ51 = data.SfQ51,
                    SfQ52 = data.SfQ52,
                    SfQ53 = data.SfQ53,
                    SfQ54 = data.SfQ54,
                    SfQ61 = data.SfQ61,
                    SfQ62 = data.SfQ62,
                    SfQ71 = data.SfQ71,
                    SfQ72 = data.SfQ72,
                    SfQ73 = data.SfQ73,
                    SfQ74 = data.SfQ74,
                    SfQ8Ck1 = data.SfQ8Ck1,
                    SfQ8Ck2 = data.SfQ8Ck2,
                    SfQ8Ck3 = data.SfQ8Ck3,
                    SfQ83 = data.SfQ83,
                    SfQ8Ck4 = data.SfQ8Ck4,
                    SfQ84 = data.SfQ84,
                    SfQ8Ck5 = data.SfQ8Ck5,
                    SfQ92 = data.SfQ92,
                    SfQ93 = data.SfQ93,
                    SfQ9Ck1 = data.SfQ9Ck1,
                    SfQ9Ck2 = data.SfQ9Ck2,
                    SfQ9Ck4 = data.SfQ9Ck4,
                    SfQ9Ck5 = data.SfQ9Ck5,
                    WmName1 = data.WmName1,
                    WmName2 = data.WmName2,
                    WmDept1 = data.WmDept1,
                    WmDept2 = data.WmDept2,
                    WmTitle1 = data.WmTitle1,
                    WmTitle2 = data.WmTitle2,
                    WmAddress1 = data.WmAddress1,
                    WmAddress2 = data.WmAddress2,
                    WmPhone1 = data.WmPhone1,
                    WmPhone2 = data.WmPhone2,

                    CreateDate = DateTime.Now,
                    CreateBy = user.Username,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateBy = user.Username,
                };
                _context.HrFormWorks.Add(work);

                if (imfrom != null)
                {
                    user.Status = 3;
                    hr.Status = 3;
                    _context.HrCandidates.Update(user);
                    _context.HrInterviews.Update(hr);
                }
            }
            else
            {
                work_check.UsernameCh = data.UsernameCh;
                work_check.UsernameEn = data.UsernameEn;
                work_check.Birthday = data.Birthday;
                work_check.Gender = data.Gender;
                work_check.Birthplace = data.Birthplace;
                work_check.Identity = data.Identity;
                work_check.Phone = data.Phone;
                work_check.Cellphone = data.Cellphone;
                work_check.Tellphone = data.Tellphone;
                work_check.Bloodtype = data.Bloodtype;
                work_check.AddressR = data.AddressR;
                work_check.AddressM = data.AddressM;
                work_check.Family1 = data.Family1;
                work_check.Family2 = data.Family2;
                work_check.Family3 = data.Family3;
                work_check.Family4 = data.Family4;
                work_check.Family5 = data.Family5;
                work_check.Family6 = data.Family6;
                work_check.Military = data.Military;
                work_check.MilitaryReason = data.MilitaryReason;
                work_check.MilitaryCategory = data.MilitaryCategory;
                work_check.MilitaryDateS = data.MilitaryDateS;
                work_check.MilitaryDateE = data.MilitaryDateE;
                work_check.MarriageCk1 = data.MarriageCk1;
                work_check.MarriageCk2 = data.MarriageCk2;
                work_check.MarriageCk3 = data.MarriageCk3;
                work_check.MarriageCk4 = data.MarriageCk4;
                work_check.MarriageCk5 = data.MarriageCk5;
                work_check.MarriageCk6 = data.MarriageCk6;
                work_check.MarriageDate = data.MarriageDate;
                work_check.DgName1 = data.DgName1;
                work_check.DgName2 = data.DgName2;
                work_check.DgName3 = data.DgName3;
                work_check.DgName4 = data.DgName4;
                work_check.DgName5 = data.DgName5;
                work_check.DgMajor1 = data.DgMajor1;
                work_check.DgMajor2 = data.DgMajor2;
                work_check.DgMajor3 = data.DgMajor3;
                work_check.DgMajor4 = data.DgMajor4;
                work_check.DgMajor5 = data.DgMajor5;
                work_check.DgDegree1 = data.DgDegree1;
                work_check.DgDegree2 = data.DgDegree2;
                work_check.DgDegree3 = data.DgDegree3;
                work_check.DgDegree4 = data.DgDegree4;
                work_check.DgDegree5 = data.DgDegree5;
                work_check.DgYear1 = data.DgYear1;
                work_check.DgYear2 = data.DgYear2;
                work_check.DgYear3 = data.DgYear3;
                work_check.DgYear4 = data.DgYear4;
                work_check.DgYear5 = data.DgYear5;
                work_check.DgGrad1 = data.DgGrad1;
                work_check.DgGrad2 = data.DgGrad2;
                work_check.DgGrad3 = data.DgGrad3;
                work_check.DgGrad4 = data.DgGrad4;
                work_check.DgGrad5 = data.DgGrad5;
                work_check.DgGradyear1 = data.DgGradyear1;
                work_check.DgGradyear2 = data.DgGradyear2;
                work_check.DgGradyear3 = data.DgGradyear3;
                work_check.DgGradyear4 = data.DgGradyear4;
                work_check.DgGradyear5 = data.DgGradyear5;
                work_check.DgTime1 = data.DgTime1;
                work_check.DgTime2 = data.DgTime2;
                work_check.DgTime3 = data.DgTime3;
                work_check.DgTime4 = data.DgTime4;
                work_check.DgTime5 = data.DgTime5;
                work_check.ExpDept1 = data.ExpDept1;
                work_check.ExpDept2 = data.ExpDept2;
                work_check.ExpDept3 = data.ExpDept3;
                work_check.ExpDept4 = data.ExpDept4;
                work_check.ExpDept5 = data.ExpDept5;
                work_check.ExpPeriod1 = data.ExpPeriod1;
                work_check.ExpPeriod2 = data.ExpPeriod2;
                work_check.ExpPeriod3 = data.ExpPeriod3;
                work_check.ExpPeriod4 = data.ExpPeriod4;
                work_check.ExpPeriod5 = data.ExpPeriod5;
                work_check.ExpIncomeM1 = data.ExpIncomeM1;
                work_check.ExpIncomeM2 = data.ExpIncomeM2;
                work_check.ExpIncomeM3 = data.ExpIncomeM3;
                work_check.ExpIncomeM4 = data.ExpIncomeM4;
                work_check.ExpIncomeM5 = data.ExpIncomeM5;
                work_check.ExpIncomeY1 = data.ExpIncomeY1;
                work_check.ExpIncomeY2 = data.ExpIncomeY2;
                work_check.ExpIncomeY3 = data.ExpIncomeY3;
                work_check.ExpIncomeY4 = data.ExpIncomeY4;
                work_check.ExpIncomeY5 = data.ExpIncomeY5;
                work_check.ExpPosition1 = data.ExpPosition1;
                work_check.ExpPosition2 = data.ExpPosition2;
                work_check.ExpPosition3 = data.ExpPosition3;
                work_check.ExpPosition4 = data.ExpPosition4;
                work_check.ExpPosition5 = data.ExpPosition5;
                work_check.ExpBoss1 = data.ExpBoss1;
                work_check.ExpBoss2 = data.ExpBoss2;
                work_check.ExpBoss3 = data.ExpBoss3;
                work_check.ExpBoss4 = data.ExpBoss4;
                work_check.ExpBoss5 = data.ExpBoss5;
                work_check.ExpResign1 = data.ExpResign1;
                work_check.ExpResign2 = data.ExpResign2;
                work_check.ExpResign3 = data.ExpResign3;
                work_check.ExpResign4 = data.ExpResign4;
                work_check.ExpResign5 = data.ExpResign5;
                work_check.ExpReason1 = data.ExpReason1;
                work_check.ExpReason2 = data.ExpReason2;
                work_check.ExpReason3 = data.ExpReason3;
                work_check.ExpReason4 = data.ExpReason4;
                work_check.ExpReason5 = data.ExpReason5;
                work_check.FmTitle1 = data.FmTitle1;
                work_check.FmTitle2 = data.FmTitle2;
                work_check.FmTitle3 = data.FmTitle3;
                work_check.FmTitle4 = data.FmTitle4;
                work_check.FmTitle5 = data.FmTitle5;
                work_check.FmTitle6 = data.FmTitle6;
                work_check.FmName1 = data.FmName1;
                work_check.FmName2 = data.FmName2;
                work_check.FmName3 = data.FmName3;
                work_check.FmName4 = data.FmName4;
                work_check.FmName5 = data.FmName5;
                work_check.FmName6 = data.FmName6;
                work_check.FmAge1 = data.FmAge1;
                work_check.FmAge2 = data.FmAge2;
                work_check.FmAge3 = data.FmAge3;
                work_check.FmAge4 = data.FmAge4;
                work_check.FmAge5 = data.FmAge5;
                work_check.FmAge6 = data.FmAge6;
                work_check.FmEducation1 = data.FmEducation1;
                work_check.FmEducation2 = data.FmEducation2;
                work_check.FmEducation3 = data.FmEducation3;
                work_check.FmEducation4 = data.FmEducation4;
                work_check.FmEducation5 = data.FmEducation5;
                work_check.FmEducation6 = data.FmEducation6;
                work_check.FmJob1 = data.FmJob1;
                work_check.FmJob2 = data.FmJob2;
                work_check.FmJob3 = data.FmJob3;
                work_check.FmJob4 = data.FmJob4;
                work_check.FmJob5 = data.FmJob5;
                work_check.FmJob6 = data.FmJob6;
                work_check.SfQ11 = data.SfQ11;
                work_check.SfQ21 = data.SfQ21;
                work_check.SfQ22 = data.SfQ22;
                work_check.SfQ31 = data.SfQ31;
                work_check.SfQ32 = data.SfQ32;
                work_check.SfQ33 = data.SfQ33;
                work_check.SfQ43 = data.SfQ43;
                work_check.SfQ4Ck1 = data.SfQ4Ck1;
                work_check.SfQ4Ck2 = data.SfQ4Ck2;
                work_check.SfQ4Ck3 = data.SfQ4Ck3;
                work_check.SfQ51 = data.SfQ51;
                work_check.SfQ52 = data.SfQ52;
                work_check.SfQ53 = data.SfQ53;
                work_check.SfQ54 = data.SfQ54;
                work_check.SfQ61 = data.SfQ61;
                work_check.SfQ62 = data.SfQ62;
                work_check.SfQ71 = data.SfQ71;
                work_check.SfQ72 = data.SfQ72;
                work_check.SfQ73 = data.SfQ73;
                work_check.SfQ74 = data.SfQ74;
                work_check.SfQ8Ck1 = data.SfQ8Ck1;
                work_check.SfQ8Ck2 = data.SfQ8Ck2;
                work_check.SfQ8Ck3 = data.SfQ8Ck3;
                work_check.SfQ83 = data.SfQ83;
                work_check.SfQ8Ck4 = data.SfQ8Ck4;
                work_check.SfQ84 = data.SfQ84;
                work_check.SfQ8Ck5 = data.SfQ8Ck5;
                work_check.SfQ92 = data.SfQ92;
                work_check.SfQ93 = data.SfQ93;
                work_check.SfQ9Ck1 = data.SfQ9Ck1;
                work_check.SfQ9Ck2 = data.SfQ9Ck2;
                work_check.SfQ9Ck4 = data.SfQ9Ck4;
                work_check.SfQ9Ck5 = data.SfQ9Ck5;
                work_check.WmName1 = data.WmName1;
                work_check.WmName2 = data.WmName2;
                work_check.WmDept1 = data.WmDept1;
                work_check.WmDept2 = data.WmDept2;
                work_check.WmTitle1 = data.WmTitle1;
                work_check.WmTitle2 = data.WmTitle2;
                work_check.WmAddress1 = data.WmAddress1;
                work_check.WmAddress2 = data.WmAddress2;
                work_check.WmPhone1 = data.WmPhone1;
                work_check.WmPhone2 = data.WmPhone2;

                work_check.LastUpdateDate = DateTime.Now;
                work_check.LastUpdateBy = user.Username;

                _context.HrFormWorks.Update(work_check);
            }
            _context.SaveChanges();

            return new ViewhrFormWork();

        }
        public ViewhrFormWork GetWorkData(ViewhrFormWork data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.Guid == data.GUID).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == hr.Candidate).FirstOrDefault();
            HrFormWork work = _context.HrFormWorks.Where(p => p.CandidateId == user.CandidateId).FirstOrDefault();

            if (work == null)
            {
                return new ViewhrFormWork();
            }
            else
            {
                return new ViewhrFormWork
                {
                    UsernameCh = work.UsernameCh,
                    UsernameEn = work.UsernameEn,
                    Birthday = work.Birthday,
                    Gender = work.Gender,
                    Birthplace = work.Birthplace,
                    Identity = work.Identity,
                    Phone = work.Phone,
                    Cellphone = work.Cellphone,
                    Tellphone = work.Tellphone,
                    Bloodtype = work.Bloodtype,
                    AddressR = work.AddressR,
                    AddressM = work.AddressM,
                    Family1 = work.Family1,
                    Family2 = work.Family2,
                    Family3 = work.Family3,
                    Family4 = work.Family4,
                    Family5 = work.Family5,
                    Family6 = work.Family6,
                    Military = work.Military,
                    MilitaryReason = work.MilitaryReason,
                    MilitaryCategory = work.MilitaryCategory,
                    MilitaryDateS = work.MilitaryDateS,
                    MilitaryDateE = work.MilitaryDateE,
                    MarriageCk1 = work.MarriageCk1,
                    MarriageCk2 = work.MarriageCk2,
                    MarriageCk3 = work.MarriageCk3,
                    MarriageCk4 = work.MarriageCk4,
                    MarriageCk5 = work.MarriageCk5,
                    MarriageCk6 = work.MarriageCk6,
                    MarriageDate = work.MarriageDate,
                    DgName1 = work.DgName1,
                    DgName2 = work.DgName2,
                    DgName3 = work.DgName3,
                    DgName4 = work.DgName4,
                    DgName5 = work.DgName5,
                    DgMajor1 = work.DgMajor1,
                    DgMajor2 = work.DgMajor2,
                    DgMajor3 = work.DgMajor3,
                    DgMajor4 = work.DgMajor4,
                    DgMajor5 = work.DgMajor5,
                    DgDegree1 = work.DgDegree1,
                    DgDegree2 = work.DgDegree2,
                    DgDegree3 = work.DgDegree3,
                    DgDegree4 = work.DgDegree4,
                    DgDegree5 = work.DgDegree5,
                    DgYear1 = work.DgYear1,
                    DgYear2 = work.DgYear2,
                    DgYear3 = work.DgYear3,
                    DgYear4 = work.DgYear4,
                    DgYear5 = work.DgYear5,
                    DgGrad1 = work.DgGrad1,
                    DgGrad2 = work.DgGrad2,
                    DgGrad3 = work.DgGrad3,
                    DgGrad4 = work.DgGrad4,
                    DgGrad5 = work.DgGrad5,
                    DgGradyear1 = work.DgGradyear1,
                    DgGradyear2 = work.DgGradyear2,
                    DgGradyear3 = work.DgGradyear3,
                    DgGradyear4 = work.DgGradyear4,
                    DgGradyear5 = work.DgGradyear5,
                    DgTime1 = work.DgTime1,
                    DgTime2 = work.DgTime2,
                    DgTime3 = work.DgTime3,
                    DgTime4 = work.DgTime4,
                    DgTime5 = work.DgTime5,
                    ExpDept1 = work.ExpDept1,
                    ExpDept2 = work.ExpDept2,
                    ExpDept3 = work.ExpDept3,
                    ExpDept4 = work.ExpDept4,
                    ExpDept5 = work.ExpDept5,
                    ExpPeriod1 = work.ExpPeriod1,
                    ExpPeriod2 = work.ExpPeriod2,
                    ExpPeriod3 = work.ExpPeriod3,
                    ExpPeriod4 = work.ExpPeriod4,
                    ExpPeriod5 = work.ExpPeriod5,
                    ExpIncomeM1 = work.ExpIncomeM1,
                    ExpIncomeM2 = work.ExpIncomeM2,
                    ExpIncomeM3 = work.ExpIncomeM3,
                    ExpIncomeM4 = work.ExpIncomeM4,
                    ExpIncomeM5 = work.ExpIncomeM5,
                    ExpIncomeY1 = work.ExpIncomeY1,
                    ExpIncomeY2 = work.ExpIncomeY2,
                    ExpIncomeY3 = work.ExpIncomeY3,
                    ExpIncomeY4 = work.ExpIncomeY4,
                    ExpIncomeY5 = work.ExpIncomeY5,
                    ExpPosition1 = work.ExpPosition1,
                    ExpPosition2 = work.ExpPosition2,
                    ExpPosition3 = work.ExpPosition3,
                    ExpPosition4 = work.ExpPosition4,
                    ExpPosition5 = work.ExpPosition5,
                    ExpBoss1 = work.ExpBoss1,
                    ExpBoss2 = work.ExpBoss2,
                    ExpBoss3 = work.ExpBoss3,
                    ExpBoss4 = work.ExpBoss4,
                    ExpBoss5 = work.ExpBoss5,
                    ExpResign1 = work.ExpResign1,
                    ExpResign2 = work.ExpResign2,
                    ExpResign3 = work.ExpResign3,
                    ExpResign4 = work.ExpResign4,
                    ExpResign5 = work.ExpResign5,
                    ExpReason1 = work.ExpReason1,
                    ExpReason2 = work.ExpReason2,
                    ExpReason3 = work.ExpReason3,
                    ExpReason4 = work.ExpReason4,
                    ExpReason5 = work.ExpReason5,
                    FmTitle1 = work.FmTitle1,
                    FmTitle2 = work.FmTitle2,
                    FmTitle3 = work.FmTitle3,
                    FmTitle4 = work.FmTitle4,
                    FmTitle5 = work.FmTitle5,
                    FmTitle6 = work.FmTitle6,
                    FmName1 = work.FmName1,
                    FmName2 = work.FmName2,
                    FmName3 = work.FmName3,
                    FmName4 = work.FmName4,
                    FmName5 = work.FmName5,
                    FmName6 = work.FmName6,
                    FmAge1 = work.FmAge1,
                    FmAge2 = work.FmAge2,
                    FmAge3 = work.FmAge3,
                    FmAge4 = work.FmAge4,
                    FmAge5 = work.FmAge5,
                    FmAge6 = work.FmAge6,
                    FmEducation1 = work.FmEducation1,
                    FmEducation2 = work.FmEducation2,
                    FmEducation3 = work.FmEducation3,
                    FmEducation4 = work.FmEducation4,
                    FmEducation5 = work.FmEducation5,
                    FmEducation6 = work.FmEducation6,
                    FmJob1 = work.FmJob1,
                    FmJob2 = work.FmJob2,
                    FmJob3 = work.FmJob3,
                    FmJob4 = work.FmJob4,
                    FmJob5 = work.FmJob5,
                    FmJob6 = work.FmJob6,
                    SfQ11 = work.SfQ11,
                    SfQ21 = work.SfQ21,
                    SfQ22 = work.SfQ22,
                    SfQ31 = work.SfQ31,
                    SfQ32 = work.SfQ32,
                    SfQ33 = work.SfQ33,
                    SfQ43 = work.SfQ43,
                    SfQ4Ck1 = work.SfQ4Ck1,
                    SfQ4Ck2 = work.SfQ4Ck2,
                    SfQ4Ck3 = work.SfQ4Ck3,
                    SfQ51 = work.SfQ51,
                    SfQ52 = work.SfQ52,
                    SfQ53 = work.SfQ53,
                    SfQ54 = work.SfQ54,
                    SfQ61 = work.SfQ61,
                    SfQ62 = work.SfQ62,
                    SfQ71 = work.SfQ71,
                    SfQ72 = work.SfQ72,
                    SfQ73 = work.SfQ73,
                    SfQ74 = work.SfQ74,
                    SfQ8Ck1 = work.SfQ8Ck1,
                    SfQ8Ck2 = work.SfQ8Ck2,
                    SfQ8Ck3 = work.SfQ8Ck3,
                    SfQ83 = work.SfQ83,
                    SfQ8Ck4 = work.SfQ8Ck4,
                    SfQ84 = work.SfQ84,
                    SfQ8Ck5 = work.SfQ8Ck5,
                    SfQ92 = work.SfQ92,
                    SfQ93 = work.SfQ93,
                    SfQ9Ck1 = work.SfQ9Ck1,
                    SfQ9Ck2 = work.SfQ9Ck2,
                    SfQ9Ck4 = work.SfQ9Ck4,
                    SfQ9Ck5 = work.SfQ9Ck5,
                    WmName1 = work.WmName1,
                    WmName2 = work.WmName2,
                    WmDept1 = work.WmDept1,
                    WmDept2 = work.WmDept2,
                    WmTitle1 = work.WmTitle1,
                    WmTitle2 = work.WmTitle2,
                    WmAddress1 = work.WmAddress1,
                    WmAddress2 = work.WmAddress2,
                    WmPhone1 = work.WmPhone1,
                    WmPhone2 = work.WmPhone2,
                };
            }
        }
        public ViewhrFormConsent SendConsent(ViewhrFormConsent data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.Guid == data.GUID).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == hr.Candidate).FirstOrDefault();
            HrFormConsent imfom_check = _context.HrFormConsents.Where(p => p.CandidateId == user.CandidateId).FirstOrDefault();

            if (hr == null)
            {
                throw new Exception("無權限填寫表單，請聯繫HR");
            }

            if (imfom_check == null)
            {
                HrFormConsent Consent = new HrFormConsent()
                {
                    CandidateId = user.CandidateId,
                    Username = data.Username,
                    Identity = data.Identity,

                    CreateDate = DateTime.Now,
                    CreateBy = user.Username,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateBy = user.Username,
                };
                _context.HrFormConsents.Add(Consent);
            }
            else
            {
                imfom_check.Username = data.Username;
                imfom_check.Identity = data.Identity;

                imfom_check.LastUpdateDate = DateTime.Now;
                imfom_check.LastUpdateBy = user.Username;

                _context.HrFormConsents.Update(imfom_check);
            }
            _context.SaveChanges();

            return new ViewhrFormConsent();

        }
        public ViewhrFormConsent GetConsentData(ViewhrFormConsent data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.Guid == data.GUID).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == hr.Candidate).FirstOrDefault();
            HrFormConsent consent = _context.HrFormConsents.Where(p => p.CandidateId == user.CandidateId).FirstOrDefault();

            if (consent == null)
            {
                return new ViewhrFormConsent{};
            }
            else
            {
                return new ViewhrFormConsent
                {
                    Username = consent.Username,
                    Identity = consent.Identity,
                };
            }
        }
        public ViewhrInterview PrintImformation(ViewhrInterview data)
        {
            HrInterview interview = _context.HrInterviews.Where(p=> p.Guid == data.Guid).FirstOrDefault();
            HrFormImformation imform = _context.HrFormImformations.Where(p => p.CandidateId == interview.Candidate).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == interview.Candidate).FirstOrDefault();

            WordHelper helper = new WordHelper();
            string s = string.Empty;
            Dictionary<string, string> fields = new Dictionary<string, string>();
            fields.Add("q1", imform.Q1);
            fields.Add("q2", imform.Q2);
            fields.Add("q3", imform.Q3);
            fields.Add("q4", imform.Q4);
            fields.Add("q5", imform.Q5);
            fields.Add("q6", imform.Q6);
            fields.Add("q7", imform.Q7);
            fields.Add("q8", imform.Q8);
            fields.Add("q9", imform.Q9);
            fields.Add("q10", imform.Q10);
            fields.Add("q11", imform.Q11);
            fields.Add("q12", imform.Q12);

            s = helper.MakePdfFile($"基本資料問題表.docx", "基本資料問題表_" + user.Username, fields);


            return new ViewhrInterview 
            {
                FileName = "基本資料問題表_" + user.Username,
            };
        }
        public ViewhrInterview PrintWork(ViewhrInterview data)
        {
            HrInterview interview = _context.HrInterviews.Where(p => p.Guid == data.Guid).FirstOrDefault();
            HrFormWork work = _context.HrFormWorks.Where(p => p.CandidateId == interview.Candidate).FirstOrDefault();
            HrCandidate user = _context.HrCandidates.Where(p => p.CandidateId == interview.Candidate).FirstOrDefault();

            if (work == null)
            {
                throw new Exception("應聘者尚未填寫表單");
            }
            WordHelper helper = new WordHelper();
            string s = string.Empty;
            Dictionary<string, string> fields = new Dictionary<string, string>();

            fields.Add("name_ch", col(work.UsernameCh));
            fields.Add("name_en", col(work.UsernameEn));
            fields.Add("job", interview.Dept);
            string[] date1 = work.CreateDate.Value.ToString("yyyy/MM/dd").Split('/');
            fields.Add("y1",date1[0]);
            fields.Add("m1", date1[1]);
            fields.Add("d1", date1[2]);
            string[] date2 = work.Birthday.Value.ToString("yyyy/MM/dd").Split('/');
            fields.Add("y2", date2[0]);
            fields.Add("m2", date2[1]);
            fields.Add("d2", date2[2]);
            fields.Add("from",work.Birthplace);

            for (int i = 1; i <= 10; i++)
            {
                string id = work.Identity.Substring(i-1,1);
                fields.Add($"i{i}",id);
            }

            fields.Add("phone", col(work.Phone));
            fields.Add("tell", col(work.Tellphone));
            fields.Add("cell", col(work.Cellphone));
            fields.Add("address1", col(work.AddressM));
            fields.Add("address2", col(work.AddressR));
            fields.Add("gd", col(work.Gender));
            fields.Add("bd", col(work.Bloodtype));
            fields.Add("f1", col(work.Family1.ToString()));
            fields.Add("f2", col(work.Family2.ToString()));
            fields.Add("f3", col(work.Family3.ToString()));
            fields.Add("f4", col(work.Family4.ToString()));
            fields.Add("f5", col(work.Family5.ToString()));
            fields.Add("f6", col(work.Family6.ToString()));
            fields.Add("mi1", "□");
            fields.Add("mi2", "□");
            fields.Add("mi3", "□");
            switch (work.Military)
            {
                case "已服":
                    fields["mi1"] = "■";
                    break;
                case "未服役":
                    fields["mi2"] = "■";
                    break;
                case "免役":
                    fields["mi3"] = "■";
                    break;
            }
            fields.Add("mi4", col(work.MilitaryReason));
            if (string.IsNullOrWhiteSpace(work.MilitaryReason))
            {
                fields["mi4"] = "      ";
            }
            fields.Add("mi5", col(work.MilitaryCategory));
            fields.Add("mi6", date(work.MilitaryDateS)+ "~"+ date(work.MilitaryDateE));
            fields.Add("ma1", checkbox(work.MarriageCk1));
            fields.Add("ma2", checkbox(work.MarriageCk2));
            fields.Add("ma3", checkbox(work.MarriageCk3));
            fields.Add("ma4", checkbox(work.MarriageCk4));
            fields.Add("ma5", checkbox(work.MarriageCk5));
            fields.Add("ma6", date(work.MarriageDate));
            fields.Add("ma7", checkbox(work.MarriageCk6));

            fields.Add("dg_n1", col(work.DgName1));
            fields.Add("dg_n2", col(work.DgName2));
            fields.Add("dg_n3", col(work.DgName3));
            fields.Add("dg_n4", col(work.DgName4));
            fields.Add("dg_n5", col(work.DgName5));
            fields.Add("dg_m1", col(work.DgMajor1));
            fields.Add("dg_m2", col(work.DgMajor2));
            fields.Add("dg_m3", col(work.DgMajor3));
            fields.Add("dg_m4", col(work.DgMajor4));
            fields.Add("dg_m5", col(work.DgMajor5));
            fields.Add("dg_d1", col(work.DgDegree1));
            fields.Add("dg_d2", col(work.DgDegree2));
            fields.Add("dg_d3", col(work.DgDegree3));
            fields.Add("dg_d4", col(work.DgDegree4));
            fields.Add("dg_d5", col(work.DgDegree5));
            fields.Add("dg_y1", col(work.DgYear1));
            fields.Add("dg_y2", col(work.DgYear2));
            fields.Add("dg_y3", col(work.DgYear3));
            fields.Add("dg_y4", col(work.DgYear4));
            fields.Add("dg_y5", col(work.DgYear5));
            fields.Add("dg_g1", grad(work.DgGrad1));
            fields.Add("dg_g2", grad(work.DgGrad2));
            fields.Add("dg_g3", grad(work.DgGrad3));
            fields.Add("dg_g4", grad(work.DgGrad4));
            fields.Add("dg_g5", grad(work.DgGrad5));
            fields.Add("gy1", col(work.DgGradyear1));
            fields.Add("gy2", col(work.DgGradyear2));
            fields.Add("gy3", col(work.DgGradyear3));
            fields.Add("gy4", col(work.DgGradyear4));
            fields.Add("gy5", col(work.DgGradyear5));
       
            fields.Add("dg_t1", DgTime(col(work.DgTime1)));
            fields.Add("dg_t2", DgTime(col(work.DgTime2)));
            fields.Add("dg_t3", DgTime(col(work.DgTime3)));
            fields.Add("dg_t4", DgTime(col(work.DgTime4)));
            fields.Add("dg_t5", DgTime(col(work.DgTime5)));
            
            fields.Add("ed1", col(work.ExpDept1));
            fields.Add("ed2", col(work.ExpDept2));
            fields.Add("ed3", col(work.ExpDept3));
            fields.Add("ed4", col(work.ExpDept4));
            fields.Add("ed5", col(work.ExpDept5));
            fields.Add("ep1", col(work.ExpPeriod1));
            fields.Add("ep2", col(work.ExpPeriod2));
            fields.Add("ep3", col(work.ExpPeriod3));
            fields.Add("ep4", col(work.ExpPeriod4));
            fields.Add("ep5", col(work.ExpPeriod5));
            fields.Add("ei1", col(work.ExpIncomeM1));
            fields.Add("ei2", col(work.ExpIncomeM2));
            fields.Add("ei3", col(work.ExpIncomeM3));
            fields.Add("ei4", col(work.ExpIncomeM4));
            fields.Add("ei5", col(work.ExpIncomeM5));
            fields.Add("ey1", col(work.ExpIncomeY1));
            fields.Add("ey2", col(work.ExpIncomeY2));
            fields.Add("ey3", col(work.ExpIncomeY3));
            fields.Add("ey4", col(work.ExpIncomeY4));
            fields.Add("ey5", col(work.ExpIncomeY5));
            fields.Add("en1", col(work.ExpPosition1));
            fields.Add("en2", col(work.ExpPosition2));
            fields.Add("en3", col(work.ExpPosition3));
            fields.Add("en4", col(work.ExpPosition4));
            fields.Add("en5", col(work.ExpPosition5));
            fields.Add("eb1", col(work.ExpBoss1));
            fields.Add("eb2", col(work.ExpBoss2));
            fields.Add("eb3", col(work.ExpBoss3));
            fields.Add("eb4", col(work.ExpBoss4));
            fields.Add("eb5", col(work.ExpBoss5));


            fields = resign(work.ExpResign1, fields,1);
            fields = resign(work.ExpResign2, fields,2);
            fields = resign(work.ExpResign3, fields,3);
            fields = resign(work.ExpResign4, fields,4);
            fields = resign(work.ExpResign5, fields,5);



            fields.Add("er1", col(work.ExpReason1));
            fields.Add("er2", col(work.ExpReason2));
            fields.Add("er3", col(work.ExpReason3));
            fields.Add("er4", col(work.ExpReason4));
            fields.Add("er5", col(work.ExpReason5));

            fields.Add("ft1", col(work.FmTitle1));
            fields.Add("ft2", col(work.FmTitle2));
            fields.Add("ft3", col(work.FmTitle3));
            fields.Add("ft4", col(work.FmTitle4));
            fields.Add("ft5", col(work.FmTitle5));
            fields.Add("ft6", col(work.FmTitle6));
            fields.Add("fn1", col(work.FmName1));
            fields.Add("fn2", col(work.FmName2));
            fields.Add("fn3", col(work.FmName3));
            fields.Add("fn4", col(work.FmName4));
            fields.Add("fn5", col(work.FmName5));
            fields.Add("fn6", col(work.FmName6));
            fields.Add("fg1", col(work.FmAge1));
            fields.Add("fg2", col(work.FmAge2));
            fields.Add("fg3", col(work.FmAge3));
            fields.Add("fg4", col(work.FmAge4));
            fields.Add("fg5", col(work.FmAge5));
            fields.Add("fg6", col(work.FmAge6));
            fields.Add("fe1", col(work.FmEducation1));
            fields.Add("fe2", col(work.FmEducation2));
            fields.Add("fe3", col(work.FmEducation3));
            fields.Add("fe4", col(work.FmEducation4));
            fields.Add("fe5", col(work.FmEducation5));
            fields.Add("fe6", col(work.FmEducation6));
            fields.Add("fj1", col(work.FmJob1));
            fields.Add("fj2", col(work.FmJob2));
            fields.Add("fj3", col(work.FmJob3));
            fields.Add("fj4", col(work.FmJob4));
            fields.Add("fj5", col(work.FmJob5));
            fields.Add("fj6", col(work.FmJob6));

            fields.Add("sq1", col(work.SfQ11));
            fields.Add("sq2_1", col(work.SfQ21));
            fields.Add("sq2_2", col(work.SfQ22));
            fields.Add("sq3_1", col(work.SfQ31));
            fields.Add("sq3_2", col(work.SfQ32));
            fields.Add("sq3_3", col(work.SfQ33));
            fields.Add("sq4_1", checkbox(work.SfQ4Ck1));
            fields.Add("sq4_2", checkbox(work.SfQ4Ck2));
            fields.Add("sq4_3", checkbox(work.SfQ4Ck3));
            fields.Add("sq4_4", col(work.SfQ43));
            fields.Add("sq5_1", col(work.SfQ51));
            fields.Add("sq5_2", col(work.SfQ52));
            fields.Add("sq5_3", col(work.SfQ53));
            fields.Add("sq5_4", col(work.SfQ54));
            if (!string.IsNullOrWhiteSpace(work.SfQ61))
            {
                fields.Add("sq6_1ck", "■");
            }
            else
            {
                fields.Add("sq6_1ck", "□");
            }
            if (!string.IsNullOrWhiteSpace(work.SfQ62))
            {
                fields.Add("sq6_2ck", "■");
            }
            else
            {
                fields.Add("sq6_2ck", "□");
            }
            fields.Add("sq6_1", col(work.SfQ61));
            fields.Add("sq6_2", col(work.SfQ62));
            fields.Add("sq7_1", col(work.SfQ71));
            fields.Add("sq7_2", col(work.SfQ72));
            fields.Add("sq7_3", col(work.SfQ73));
            fields.Add("sq7_4", col(work.SfQ74));

            fields.Add("sq8ck1", checkbox(work.SfQ8Ck1));
            fields.Add("sq8ck2", checkbox(work.SfQ8Ck2));
            fields.Add("sq8ck3", checkbox(work.SfQ8Ck3));
            fields.Add("sq8ck4", checkbox(work.SfQ8Ck4));
            fields.Add("sq8ck5", checkbox(work.SfQ8Ck5));
            fields.Add("sq8_3", col(work.SfQ83));
            fields.Add("sq8_4", col(work.SfQ84));

            fields.Add("sq9ck1", checkbox(work.SfQ9Ck1));
            fields.Add("sq9ck2", checkbox(work.SfQ9Ck2));
            fields.Add("sq9ck4", checkbox(work.SfQ9Ck4));
            fields.Add("sq9ck5", checkbox(work.SfQ9Ck5));
            fields.Add("sq9_2", col(work.SfQ92));
            fields.Add("sq9_3", col(work.SfQ93));

            fields.Add("wn1", col(work.WmName1));
            fields.Add("wn2", col(work.WmName2));
            fields.Add("wd1", col(work.WmDept1));
            fields.Add("wd2", col(work.WmDept2));
            fields.Add("wt1", col(work.WmTitle1));
            fields.Add("wt2", col(work.WmTitle2));
            fields.Add("wa1", col(work.WmAddress1));
            fields.Add("wa2", col(work.WmAddress2));
            fields.Add("wp1", col(work.WmPhone1));
            fields.Add("wp2", col(work.WmPhone2));


            s = helper.MakeDocxFile($"工作申請表.docx", "工作申請表_" + user.Username, fields);


            return new ViewhrInterview
            {
                FileName = "工作申請表_" + user.Username,
            };
        }
        public string col(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return "";
            }
            else
            {
                return data;
            }
        }
        public string grad(string? data)
        {
            if (data == "畢")
            {
                return "■  □";
            }
            else if (data == "肄")
            {
                return "□  ■";
            }
            else
            {
                return "□  □";
            }
        }
        public Dictionary<string, string> resign(string data, Dictionary<string, string> fields,int i)
        {
            if (data == "自願")
            {
                fields.Add($"ry{i}", "■");
                fields.Add($"rn{i}", "□");
            }
            else if (data == "非自願")
            {

                fields.Add($"ry{i}", "□");
                fields.Add($"rn{i}", "■");

            }
            else
            {
                fields.Add($"ry{i}", "□");
                fields.Add($"rn{i}", "□");
            }
            return fields;
        }
        public string date(DateTime? data)
        {
            if (data==null)
            {
                return "";
            }
            else
            {
                return data.Value.ToString("yyyy/MM/dd");
            }
        }
        public string DgTime(string data)
        {
            if (data == "日間")
            {
                return "■     □     □";
            }
            else if (data == "夜間")
            {
                return "□     ■     □";
            }
            else if (data == "假日")
            {
                return "□     □     ■";
            }
            else
            {
                return "□     □     □";
            }
        }
        public string checkbox(bool? data)
        {
            if (data != null)
            {
                if (data.Value)
                {
                    return "■";
                }
                else
                {
                    return "□";
                }
            }
            else
            {
                return "□";
            }
        }
    }
}

