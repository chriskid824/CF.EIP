﻿using Convience.EntityFrameWork.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable

namespace Convience.Entity.Entity.EIP
{
    [Entity(DbContextType = typeof(EIPContext))]
    public partial class HrFormWork
    {
        public int CandidateId { get; set; }
        public string UsernameCh { get; set; }
        public string UsernameEn { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Birthplace { get; set; }
        public string Identity { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public string Tellphone { get; set; }
        public string Bloodtype { get; set; }
        public string AddressR { get; set; }
        public string AddressM { get; set; }
        public int? Family1 { get; set; }
        public int? Family2 { get; set; }
        public int? Family3 { get; set; }
        public int? Family4 { get; set; }
        public int? Family5 { get; set; }
        public int? Family6 { get; set; }
        public string Military { get; set; }
        public string MilitaryReason { get; set; }
        public string MilitaryCategory { get; set; }
        public DateTime? MilitaryDateS { get; set; }
        public DateTime? MilitaryDateE { get; set; }
        public bool? MarriageCk1 { get; set; }
        public bool? MarriageCk2 { get; set; }
        public bool? MarriageCk3 { get; set; }
        public bool? MarriageCk4 { get; set; }
        public bool? MarriageCk5 { get; set; }
        public bool? MarriageCk6 { get; set; }
        public DateTime? MarriageDate { get; set; }
        public string DgName1 { get; set; }
        public string DgName2 { get; set; }
        public string DgName3 { get; set; }
        public string DgName4 { get; set; }
        public string DgName5 { get; set; }
        public string DgMajor1 { get; set; }
        public string DgMajor2 { get; set; }
        public string DgMajor3 { get; set; }
        public string DgMajor4 { get; set; }
        public string DgMajor5 { get; set; }
        public string DgDegree1 { get; set; }
        public string DgDegree2 { get; set; }
        public string DgDegree3 { get; set; }
        public string DgDegree4 { get; set; }
        public string DgDegree5 { get; set; }
        public string DgYear1 { get; set; }
        public string DgYear2 { get; set; }
        public string DgYear3 { get; set; }
        public string DgYear4 { get; set; }
        public string DgYear5 { get; set; }
        public string DgGrad1 { get; set; }
        public string DgGrad2 { get; set; }
        public string DgGrad3 { get; set; }
        public string DgGrad4 { get; set; }
        public string DgGrad5 { get; set; }
        public string DgGradyear1 { get; set; }
        public string DgGradyear2 { get; set; }
        public string DgGradyear3 { get; set; }
        public string DgGradyear4 { get; set; }
        public string DgGradyear5 { get; set; }
        public string DgTime1 { get; set; }
        public string DgTime2 { get; set; }
        public string DgTime3 { get; set; }
        public string DgTime4 { get; set; }
        public string DgTime5 { get; set; }
        public string ExpDept1 { get; set; }
        public string ExpDept2 { get; set; }
        public string ExpDept3 { get; set; }
        public string ExpDept4 { get; set; }
        public string ExpDept5 { get; set; }
        public string ExpPeriod1 { get; set; }
        public string ExpPeriod2 { get; set; }
        public string ExpPeriod3 { get; set; }
        public string ExpPeriod4 { get; set; }
        public string ExpPeriod5 { get; set; }
        public string ExpIncomeM1 { get; set; }
        public string ExpIncomeM2 { get; set; }
        public string ExpIncomeM3 { get; set; }
        public string ExpIncomeM4 { get; set; }
        public string ExpIncomeM5 { get; set; }
        public string ExpIncomeY1 { get; set; }
        public string ExpIncomeY2 { get; set; }
        public string ExpIncomeY3 { get; set; }
        public string ExpIncomeY4 { get; set; }
        public string ExpIncomeY5 { get; set; }
        public string ExpPosition1 { get; set; }
        public string ExpPosition2 { get; set; }
        public string ExpPosition3 { get; set; }
        public string ExpPosition4 { get; set; }
        public string ExpPosition5 { get; set; }
        public string ExpBoss1 { get; set; }
        public string ExpBoss2 { get; set; }
        public string ExpBoss3 { get; set; }
        public string ExpBoss4 { get; set; }
        public string ExpBoss5 { get; set; }
        public string ExpResign1 { get; set; }
        public string ExpResign2 { get; set; }
        public string ExpResign3 { get; set; }
        public string ExpResign4 { get; set; }
        public string ExpResign5 { get; set; }
        public string ExpReason1 { get; set; }
        public string ExpReason2 { get; set; }
        public string ExpReason3 { get; set; }
        public string ExpReason4 { get; set; }
        public string ExpReason5 { get; set; }
        public string FmTitle1 { get; set; }
        public string FmTitle2 { get; set; }
        public string FmTitle3 { get; set; }
        public string FmTitle4 { get; set; }
        public string FmTitle5 { get; set; }
        public string FmTitle6 { get; set; }
        public string FmName1 { get; set; }
        public string FmName2 { get; set; }
        public string FmName3 { get; set; }
        public string FmName4 { get; set; }
        public string FmName5 { get; set; }
        public string FmName6 { get; set; }
        public string FmAge1 { get; set; }
        public string FmAge2 { get; set; }
        public string FmAge3 { get; set; }
        public string FmAge4 { get; set; }
        public string FmAge5 { get; set; }
        public string FmAge6 { get; set; }
        public string FmEducation1 { get; set; }
        public string FmEducation2 { get; set; }
        public string FmEducation3 { get; set; }
        public string FmEducation4 { get; set; }
        public string FmEducation5 { get; set; }
        public string FmEducation6 { get; set; }
        public string FmJob1 { get; set; }
        public string FmJob2 { get; set; }
        public string FmJob3 { get; set; }
        public string FmJob4 { get; set; }
        public string FmJob5 { get; set; }
        public string FmJob6 { get; set; }
        public string SfQ11 { get; set; }
        public string SfQ21 { get; set; }
        public string SfQ22 { get; set; }
        public string SfQ31 { get; set; }
        public string SfQ32 { get; set; }
        public string SfQ33 { get; set; }
        public bool? SfQ4Ck1 { get; set; }
        public bool? SfQ4Ck2 { get; set; }
        public bool? SfQ4Ck3 { get; set; }
        public string SfQ43 { get; set; }
        public string SfQ51 { get; set; }
        public string SfQ52 { get; set; }
        public string SfQ53 { get; set; }
        public string SfQ54 { get; set; }
        public string SfQ61 { get; set; }
        public string SfQ62 { get; set; }
        public string SfQ71 { get; set; }
        public string SfQ72 { get; set; }
        public string SfQ73 { get; set; }
        public string SfQ74 { get; set; }
        public bool? SfQ8Ck1 { get; set; }
        public bool? SfQ8Ck2 { get; set; }
        public bool? SfQ8Ck3 { get; set; }
        public string SfQ83 { get; set; }
        public bool? SfQ8Ck4 { get; set; }
        public string SfQ84 { get; set; }
        public bool? SfQ8Ck5 { get; set; }
        public bool? SfQ9Ck1 { get; set; }
        public bool? SfQ9Ck2 { get; set; }
        public string SfQ92 { get; set; }
        public string SfQ93 { get; set; }
        public bool? SfQ9Ck4 { get; set; }
        public bool? SfQ9Ck5 { get; set; }
        public string WmName1 { get; set; }
        public string WmName2 { get; set; }
        public string WmDept1 { get; set; }
        public string WmDept2 { get; set; }
        public string WmTitle1 { get; set; }
        public string WmTitle2 { get; set; }
        public string WmAddress1 { get; set; }
        public string WmAddress2 { get; set; }
        public string WmPhone1 { get; set; }
        public string WmPhone2 { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
