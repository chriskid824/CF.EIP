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
    public interface IHrFormService
    {
        public bool CheckFormURL(ViewhrInterview data);
    }
    public class HrFormService : IHrFormService
    {
        private readonly EIPContext _context;
        private readonly IRepository<HrInterview> _hrFormRepository;
        public HrFormService(IRepository<HrInterview> hrFormRepository, EIPContext context )
        {
            //_mapper = mapper;
            _hrFormRepository = hrFormRepository;
            _context = context;
        }       
        public bool CheckFormURL(ViewhrInterview data)
        {
            HrInterview hr = _context.HrInterviews.Where(p => p.Guid == data.Guid).FirstOrDefault();
            if (hr== null)
            {
                return false;
            }
            {
                return true;
            }
        }        
    }
}

