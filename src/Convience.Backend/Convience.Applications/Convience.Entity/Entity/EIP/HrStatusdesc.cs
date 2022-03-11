using Convience.EntityFrameWork.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable

namespace Convience.Entity.Entity.EIP
{
    [Entity(DbContextType = typeof(EIPContext))]
    public partial class HrStatusdesc
    {
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
    }
}
