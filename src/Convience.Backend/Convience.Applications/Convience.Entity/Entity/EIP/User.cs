using Convience.EntityFrameWork.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable

namespace Convience.Entity.Entity.EIP
{
    [Entity(DbContextType = typeof(SPMContext))]
    public partial class User
    {
        public int Userid { get; set; }
        public string Logonid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Title { get; set; }
        public string Job { get; set; }
        public int Calendarid { get; set; }
        public string Enable { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string TelHome { get; set; }
        public string TelOffice { get; set; }
        public string TelMobil { get; set; }
        public DateTime? Lastlogon { get; set; }
        public int Logontimes { get; set; }
        public DateTime? LogDatetime { get; set; }
        public int? LogLogonid { get; set; }
        public DateTime? Enabledate { get; set; }
        public string Cust1 { get; set; }
        public string Cust2 { get; set; }
        public string Cust3 { get; set; }
        public string Cust4 { get; set; }
        public string Cust5 { get; set; }
        public string Cust6 { get; set; }
        public string Cust7 { get; set; }
        public string Cust8 { get; set; }
        public string Cust9 { get; set; }
        public string Cust10 { get; set; }
        public string Signright { get; set; }
        public string Cust11 { get; set; }
        public string Cust12 { get; set; }
        public string Cust13 { get; set; }
        public string Cust14 { get; set; }
        public string Cust15 { get; set; }
        public string Cust16 { get; set; }
        public string Cust17 { get; set; }
        public string Cust18 { get; set; }
        public string Cust19 { get; set; }
        public string Cust20 { get; set; }
        public string PasswordE { get; set; }
    }
}
