using Convience.Entity.Entity.EIP;
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

    }
    public class HrCandidateService : IHrCandidateService
    {
        private readonly EIPContext _context;

        public PagingResultModel<ViewhrCandidate> GetCandidatelList(QueryCandidate query)
        {
            int skip = (query.Page - 1) * query.Size;

            var resultQuery = (from hr in _context.HrCandidates

                               select new ViewhrCandidate
                               {
                                   Username = hr.Username,
                                   Cellphone = hr.Cellphone,
                                   Email = hr.Email,
                                   Status = hr.Status,

                               })
                          .AndIfHaveValue(query.name, r => r.Username.Contains(query.name));


            var candidates = resultQuery.Skip(skip).Take(query.Size).ToArray();

            return new PagingResultModel<ViewhrCandidate>
            {
                Data = JsonConvert.DeserializeObject<ViewhrCandidate[]>(JsonConvert.SerializeObject(candidates)),
                Count = resultQuery.Count()
            };
        }
    }
}

