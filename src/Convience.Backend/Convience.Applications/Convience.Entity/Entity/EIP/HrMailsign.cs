using Convience.EntityFrameWork.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable

namespace Convience.Entity.Entity.EIP
{
    [Entity(DbContextType = typeof(EIPContext))]
    public partial class HrMailsign
    {
        public string Logonid { get; set; }
        public string Sign1 { get; set; }
        public string Sign2 { get; set; }
        public string Sign3 { get; set; }
        public string Sign4 { get; set; }
        public string Sign5 { get; set; }
        public string Sign6 { get; set; }
        public string Sign7 { get; set; }
        public string Sign8 { get; set; }
    }
}
