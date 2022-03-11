using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Convience.Entity.Entity.EIP
{
    public partial class EIPContext : DbContext
    {
        public EIPContext()
        {
        }

        public EIPContext(DbContextOptions<EIPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HrCandidate> HrCandidates { get; set; }
        public virtual DbSet<HrInterview> HrInterviews { get; set; }
        public virtual DbSet<HrFormImformation> HrFormImformations { get; set; }
        public virtual DbSet<HrFormWork> HrFormWorks { get; set; }
        public virtual DbSet<HrFormConsent> HrFormConsents { get; set; }
        public virtual DbSet<HrStatusdesc> HrStatusdescs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=10.1.1.181;Initial Catalog=EIP;User ID=sa;Password=Chen@full");
                //optionsBuilder.UseSqlServer("Data Source=10.1.1.181;Initial Catalog=SPM;User ID=sa;Password=Chen@full");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<HrCandidate>(entity =>
            {
                entity.HasKey(e => e.CandidateId);

                entity.ToTable("HR_CANDIDATE");

                entity.Property(e => e.CandidateId).HasColumnName("CANDIDATE_ID");

                entity.Property(e => e.Cellphone)
                    .HasMaxLength(12)
                    .HasColumnName("CELLPHONE");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(8)
                    .HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.LastUpdateBy)
                    .HasMaxLength(8)
                    .HasColumnName("LAST_UPDATE_BY");

                entity.Property(e => e.LastUpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LAST_UPDATE_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .HasColumnName("USERNAME");
            });

            modelBuilder.Entity<HrInterview>(entity =>
            {
                entity.HasKey(e => e.InterviewId);

                entity.ToTable("HR_INTERVIEW");

                entity.Property(e => e.InterviewId).HasColumnName("INTERVIEW_ID");

                entity.Property(e => e.Candidate).HasColumnName("CANDIDATE");

                entity.Property(e => e.CheckDate)
                    .HasColumnType("date")
                    .HasColumnName("CHECK_DATE");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(10)
                    .HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.Dept)
                    .HasMaxLength(20)
                    .HasColumnName("DEPT");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.InterviewDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INTERVIEW_DATE");

                entity.Property(e => e.Interviewer)
                    .HasMaxLength(10)
                    .HasColumnName("INTERVIEWER");

                entity.Property(e => e.LastUpdateBy)
                    .HasMaxLength(10)
                    .HasColumnName("LAST_UPDATE_BY");

                entity.Property(e => e.LastUpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LAST_UPDATE_DATE");

                entity.Property(e => e.NoticeDate)
                    .HasColumnType("date")
                    .HasColumnName("NOTICE_DATE");

                entity.Property(e => e.Place)
                    .HasMaxLength(30)
                    .HasColumnName("PLACE");

                entity.Property(e => e.ReplyDate)
                    .HasColumnType("date")
                    .HasColumnName("REPLY_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.ValidDate)
                    .HasColumnType("date")
                    .HasColumnName("VALID_DATE");
            });

            modelBuilder.Entity<HrFormImformation>(entity =>
            {
                entity.HasKey(e => e.CandidateId);

                entity.ToTable("HR_FORM_IMFORMATION");

                entity.Property(e => e.CandidateId)
                    .ValueGeneratedNever()
                    .HasColumnName("CANDIDATE_ID");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(10)
                    .HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.LastUpdateBy)
                    .HasMaxLength(10)
                    .HasColumnName("LAST_UPDATE_BY");

                entity.Property(e => e.LastUpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LAST_UPDATE_DATE");

                entity.Property(e => e.Q1).HasMaxLength(500);

                entity.Property(e => e.Q10).HasMaxLength(500);

                entity.Property(e => e.Q11).HasMaxLength(500);

                entity.Property(e => e.Q12).HasMaxLength(500);

                entity.Property(e => e.Q2).HasMaxLength(500);

                entity.Property(e => e.Q3).HasMaxLength(500);

                entity.Property(e => e.Q4).HasMaxLength(500);

                entity.Property(e => e.Q5).HasMaxLength(500);

                entity.Property(e => e.Q6).HasMaxLength(500);

                entity.Property(e => e.Q7).HasMaxLength(500);

                entity.Property(e => e.Q8).HasMaxLength(500);

                entity.Property(e => e.Q9).HasMaxLength(500);
            });

            modelBuilder.Entity<HrFormWork>(entity =>
            {
                entity.HasKey(e => e.CandidateId);

                entity.ToTable("HR_FORM_WORK");

                entity.Property(e => e.CandidateId)
                    .ValueGeneratedNever()
                    .HasColumnName("CANDIDATE_ID");

                entity.Property(e => e.AddressM)
                    .HasMaxLength(50)
                    .HasColumnName("ADDRESS_M");

                entity.Property(e => e.AddressR)
                    .HasMaxLength(50)
                    .HasColumnName("ADDRESS_R");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("BIRTHDAY");

                entity.Property(e => e.Birthplace)
                    .HasMaxLength(50)
                    .HasColumnName("BIRTHPLACE");

                entity.Property(e => e.Bloodtype)
                    .HasMaxLength(50)
                    .HasColumnName("BLOODTYPE");

                entity.Property(e => e.Cellphone)
                    .HasMaxLength(50)
                    .HasColumnName("CELLPHONE");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.DgDegree1)
                    .HasMaxLength(50)
                    .HasColumnName("DG_DEGREE_1");

                entity.Property(e => e.DgDegree2)
                    .HasMaxLength(50)
                    .HasColumnName("DG_DEGREE_2");

                entity.Property(e => e.DgDegree3)
                    .HasMaxLength(50)
                    .HasColumnName("DG_DEGREE_3");

                entity.Property(e => e.DgDegree4)
                    .HasMaxLength(50)
                    .HasColumnName("DG_DEGREE_4");

                entity.Property(e => e.DgDegree5)
                    .HasMaxLength(50)
                    .HasColumnName("DG_DEGREE_5");

                entity.Property(e => e.DgGrad1)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRAD_1");

                entity.Property(e => e.DgGrad2)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRAD_2");

                entity.Property(e => e.DgGrad3)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRAD_3");

                entity.Property(e => e.DgGrad4)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRAD_4");

                entity.Property(e => e.DgGrad5)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRAD_5");

                entity.Property(e => e.DgGradyear1)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRADYEAR_1");

                entity.Property(e => e.DgGradyear2)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRADYEAR_2");

                entity.Property(e => e.DgGradyear3)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRADYEAR_3");

                entity.Property(e => e.DgGradyear4)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRADYEAR_4");

                entity.Property(e => e.DgGradyear5)
                    .HasMaxLength(50)
                    .HasColumnName("DG_GRADYEAR_5");

                entity.Property(e => e.DgMajor1)
                    .HasMaxLength(50)
                    .HasColumnName("DG_MAJOR_1");

                entity.Property(e => e.DgMajor2)
                    .HasMaxLength(50)
                    .HasColumnName("DG_MAJOR_2");

                entity.Property(e => e.DgMajor3)
                    .HasMaxLength(50)
                    .HasColumnName("DG_MAJOR_3");

                entity.Property(e => e.DgMajor4)
                    .HasMaxLength(50)
                    .HasColumnName("DG_MAJOR_4");

                entity.Property(e => e.DgMajor5)
                    .HasMaxLength(50)
                    .HasColumnName("DG_MAJOR_5");

                entity.Property(e => e.DgName1)
                    .HasMaxLength(50)
                    .HasColumnName("DG_NAME_1");

                entity.Property(e => e.DgName2)
                    .HasMaxLength(50)
                    .HasColumnName("DG_NAME_2");

                entity.Property(e => e.DgName3)
                    .HasMaxLength(50)
                    .HasColumnName("DG_NAME_3");

                entity.Property(e => e.DgName4)
                    .HasMaxLength(50)
                    .HasColumnName("DG_NAME_4");

                entity.Property(e => e.DgName5)
                    .HasMaxLength(50)
                    .HasColumnName("DG_NAME_5");

                entity.Property(e => e.DgTime1)
                    .HasMaxLength(50)
                    .HasColumnName("DG_TIME_1");

                entity.Property(e => e.DgTime2)
                    .HasMaxLength(50)
                    .HasColumnName("DG_TIME_2");

                entity.Property(e => e.DgTime3)
                    .HasMaxLength(50)
                    .HasColumnName("DG_TIME_3");

                entity.Property(e => e.DgTime4)
                    .HasMaxLength(50)
                    .HasColumnName("DG_TIME_4");

                entity.Property(e => e.DgTime5)
                    .HasMaxLength(50)
                    .HasColumnName("DG_TIME_5");

                entity.Property(e => e.DgYear1)
                    .HasMaxLength(50)
                    .HasColumnName("DG_YEAR_1");

                entity.Property(e => e.DgYear2)
                    .HasMaxLength(50)
                    .HasColumnName("DG_YEAR_2");

                entity.Property(e => e.DgYear3)
                    .HasMaxLength(50)
                    .HasColumnName("DG_YEAR_3");

                entity.Property(e => e.DgYear4)
                    .HasMaxLength(50)
                    .HasColumnName("DG_YEAR_4");

                entity.Property(e => e.DgYear5)
                    .HasMaxLength(50)
                    .HasColumnName("DG_YEAR_5");

                entity.Property(e => e.ExpBoss1)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_BOSS_1");

                entity.Property(e => e.ExpBoss2)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_BOSS_2");

                entity.Property(e => e.ExpBoss3)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_BOSS_3");

                entity.Property(e => e.ExpBoss4)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_BOSS_4");

                entity.Property(e => e.ExpBoss5)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_BOSS_5");

                entity.Property(e => e.ExpDept1)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_DEPT_1");

                entity.Property(e => e.ExpDept2)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_DEPT_2");

                entity.Property(e => e.ExpDept3)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_DEPT_3");

                entity.Property(e => e.ExpDept4)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_DEPT_4");

                entity.Property(e => e.ExpDept5)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_DEPT_5");

                entity.Property(e => e.ExpIncomeM1)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_M_1");

                entity.Property(e => e.ExpIncomeM2)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_M_2");

                entity.Property(e => e.ExpIncomeM3)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_M_3");

                entity.Property(e => e.ExpIncomeM4)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_M_4");

                entity.Property(e => e.ExpIncomeM5)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_M_5");

                entity.Property(e => e.ExpIncomeY1)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_Y_1");

                entity.Property(e => e.ExpIncomeY2)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_Y_2");

                entity.Property(e => e.ExpIncomeY3)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_Y_3");

                entity.Property(e => e.ExpIncomeY4)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_Y_4");

                entity.Property(e => e.ExpIncomeY5)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_INCOME_Y_5");

                entity.Property(e => e.ExpPeriod1)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_PERIOD_1");

                entity.Property(e => e.ExpPeriod2)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_PERIOD_2");

                entity.Property(e => e.ExpPeriod3)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_PERIOD_3");

                entity.Property(e => e.ExpPeriod4)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_PERIOD_4");

                entity.Property(e => e.ExpPeriod5)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_PERIOD_5");

                entity.Property(e => e.ExpPosition1)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_POSITION_1");

                entity.Property(e => e.ExpPosition2)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_POSITION_2");

                entity.Property(e => e.ExpPosition3)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_POSITION_3");

                entity.Property(e => e.ExpPosition4)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_POSITION_4");

                entity.Property(e => e.ExpPosition5)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_POSITION_5");

                entity.Property(e => e.ExpReason1)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_REASON_1");

                entity.Property(e => e.ExpReason2)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_REASON_2");

                entity.Property(e => e.ExpReason3)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_REASON_3");

                entity.Property(e => e.ExpReason4)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_REASON_4");

                entity.Property(e => e.ExpReason5)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_REASON_5");

                entity.Property(e => e.ExpResign1)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_RESIGN_1");

                entity.Property(e => e.ExpResign2)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_RESIGN_2");

                entity.Property(e => e.ExpResign3)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_RESIGN_3");

                entity.Property(e => e.ExpResign4)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_RESIGN_4");

                entity.Property(e => e.ExpResign5)
                    .HasMaxLength(50)
                    .HasColumnName("EXP_RESIGN_5");

                entity.Property(e => e.Family1).HasColumnName("FAMILY_1");

                entity.Property(e => e.Family2).HasColumnName("FAMILY_2");

                entity.Property(e => e.Family3).HasColumnName("FAMILY_3");

                entity.Property(e => e.Family4).HasColumnName("FAMILY_4");

                entity.Property(e => e.Family5).HasColumnName("FAMILY_5");

                entity.Property(e => e.Family6).HasColumnName("FAMILY_6");

                entity.Property(e => e.FmAge1)
                    .HasMaxLength(50)
                    .HasColumnName("FM_AGE_1");

                entity.Property(e => e.FmAge2)
                    .HasMaxLength(50)
                    .HasColumnName("FM_AGE_2");

                entity.Property(e => e.FmAge3)
                    .HasMaxLength(50)
                    .HasColumnName("FM_AGE_3");

                entity.Property(e => e.FmAge4)
                    .HasMaxLength(50)
                    .HasColumnName("FM_AGE_4");

                entity.Property(e => e.FmAge5)
                    .HasMaxLength(50)
                    .HasColumnName("FM_AGE_5");

                entity.Property(e => e.FmAge6)
                    .HasMaxLength(50)
                    .HasColumnName("FM_AGE_6");

                entity.Property(e => e.FmEducation1)
                    .HasMaxLength(50)
                    .HasColumnName("FM_EDUCATION_1");

                entity.Property(e => e.FmEducation2)
                    .HasMaxLength(50)
                    .HasColumnName("FM_EDUCATION_2");

                entity.Property(e => e.FmEducation3)
                    .HasMaxLength(50)
                    .HasColumnName("FM_EDUCATION_3");

                entity.Property(e => e.FmEducation4)
                    .HasMaxLength(50)
                    .HasColumnName("FM_EDUCATION_4");

                entity.Property(e => e.FmEducation5)
                    .HasMaxLength(50)
                    .HasColumnName("FM_EDUCATION_5");

                entity.Property(e => e.FmEducation6)
                    .HasMaxLength(50)
                    .HasColumnName("FM_EDUCATION_6");

                entity.Property(e => e.FmJob1)
                    .HasMaxLength(50)
                    .HasColumnName("FM_JOB_1");

                entity.Property(e => e.FmJob2)
                    .HasMaxLength(50)
                    .HasColumnName("FM_JOB_2");

                entity.Property(e => e.FmJob3)
                    .HasMaxLength(50)
                    .HasColumnName("FM_JOB_3");

                entity.Property(e => e.FmJob4)
                    .HasMaxLength(50)
                    .HasColumnName("FM_JOB_4");

                entity.Property(e => e.FmJob5)
                    .HasMaxLength(50)
                    .HasColumnName("FM_JOB_5");

                entity.Property(e => e.FmJob6)
                    .HasMaxLength(50)
                    .HasColumnName("FM_JOB_6");

                entity.Property(e => e.FmName1)
                    .HasMaxLength(50)
                    .HasColumnName("FM_NAME_1");

                entity.Property(e => e.FmName2)
                    .HasMaxLength(50)
                    .HasColumnName("FM_NAME_2");

                entity.Property(e => e.FmName3)
                    .HasMaxLength(50)
                    .HasColumnName("FM_NAME_3");

                entity.Property(e => e.FmName4)
                    .HasMaxLength(50)
                    .HasColumnName("FM_NAME_4");

                entity.Property(e => e.FmName5)
                    .HasMaxLength(50)
                    .HasColumnName("FM_NAME_5");

                entity.Property(e => e.FmName6)
                    .HasMaxLength(50)
                    .HasColumnName("FM_NAME_6");

                entity.Property(e => e.FmTitle1)
                    .HasMaxLength(50)
                    .HasColumnName("FM_TITLE_1");

                entity.Property(e => e.FmTitle2)
                    .HasMaxLength(50)
                    .HasColumnName("FM_TITLE_2");

                entity.Property(e => e.FmTitle3)
                    .HasMaxLength(50)
                    .HasColumnName("FM_TITLE_3");

                entity.Property(e => e.FmTitle4)
                    .HasMaxLength(50)
                    .HasColumnName("FM_TITLE_4");

                entity.Property(e => e.FmTitle5)
                    .HasMaxLength(50)
                    .HasColumnName("FM_TITLE_5");

                entity.Property(e => e.FmTitle6)
                    .HasMaxLength(50)
                    .HasColumnName("FM_TITLE_6");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .HasColumnName("GENDER");

                entity.Property(e => e.Identity)
                    .HasMaxLength(50)
                    .HasColumnName("IDENTITY");

                entity.Property(e => e.LastUpdateBy)
                    .HasMaxLength(50)
                    .HasColumnName("LAST_UPDATE_BY");

                entity.Property(e => e.LastUpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LAST_UPDATE_DATE");

                entity.Property(e => e.MarriageCk1).HasColumnName("MARRIAGE_CK_1");

                entity.Property(e => e.MarriageCk2).HasColumnName("MARRIAGE_CK_2");

                entity.Property(e => e.MarriageCk3).HasColumnName("MARRIAGE_CK_3");

                entity.Property(e => e.MarriageCk4).HasColumnName("MARRIAGE_CK_4");

                entity.Property(e => e.MarriageCk5).HasColumnName("MARRIAGE_CK_5");

                entity.Property(e => e.MarriageCk6).HasColumnName("MARRIAGE_CK_6");

                entity.Property(e => e.MarriageDate)
                    .HasColumnType("date")
                    .HasColumnName("MARRIAGE_DATE");

                entity.Property(e => e.Military)
                    .HasMaxLength(50)
                    .HasColumnName("MILITARY");

                entity.Property(e => e.MilitaryCategory)
                    .HasMaxLength(50)
                    .HasColumnName("MILITARY_CATEGORY");

                entity.Property(e => e.MilitaryDateE)
                    .HasColumnType("date")
                    .HasColumnName("MILITARY_DATE_E");

                entity.Property(e => e.MilitaryDateS)
                    .HasColumnType("date")
                    .HasColumnName("MILITARY_DATE_S");

                entity.Property(e => e.MilitaryReason)
                    .HasMaxLength(50)
                    .HasColumnName("MILITARY_REASON");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("PHONE");

                entity.Property(e => e.SfQ11)
                    .HasMaxLength(200)
                    .HasColumnName("SF_Q1_1");

                entity.Property(e => e.SfQ21)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q2_1");

                entity.Property(e => e.SfQ22)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q2_2");

                entity.Property(e => e.SfQ31)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q3_1");

                entity.Property(e => e.SfQ32)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q3_2");

                entity.Property(e => e.SfQ33)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q3_3");

                entity.Property(e => e.SfQ43)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q4_3");

                entity.Property(e => e.SfQ4Ck1).HasColumnName("SF_Q4_CK_1");

                entity.Property(e => e.SfQ4Ck2).HasColumnName("SF_Q4_CK_2");

                entity.Property(e => e.SfQ4Ck3).HasColumnName("SF_Q4_CK_3");

                entity.Property(e => e.SfQ51)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q5_1");

                entity.Property(e => e.SfQ52)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q5_2");

                entity.Property(e => e.SfQ53)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q5_3");

                entity.Property(e => e.SfQ54)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q5_4");

                entity.Property(e => e.SfQ61)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q6_1");

                entity.Property(e => e.SfQ62)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q6_2");

                entity.Property(e => e.SfQ71)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q7_1");

                entity.Property(e => e.SfQ72)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q7_2");

                entity.Property(e => e.SfQ73)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q7_3");

                entity.Property(e => e.SfQ74)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q7_4");

                entity.Property(e => e.SfQ83)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q8_3");

                entity.Property(e => e.SfQ84)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q8_4");

                entity.Property(e => e.SfQ8Ck1).HasColumnName("SF_Q8_CK_1");

                entity.Property(e => e.SfQ8Ck2).HasColumnName("SF_Q8_CK_2");

                entity.Property(e => e.SfQ8Ck3).HasColumnName("SF_Q8_CK_3");

                entity.Property(e => e.SfQ8Ck4).HasColumnName("SF_Q8_CK_4");

                entity.Property(e => e.SfQ8Ck5).HasColumnName("SF_Q8_CK_5");

                entity.Property(e => e.SfQ92)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q9_2");

                entity.Property(e => e.SfQ93)
                    .HasMaxLength(50)
                    .HasColumnName("SF_Q9_3");

                entity.Property(e => e.SfQ9Ck1).HasColumnName("SF_Q9_CK_1");

                entity.Property(e => e.SfQ9Ck2).HasColumnName("SF_Q9_CK_2");

                entity.Property(e => e.SfQ9Ck4).HasColumnName("SF_Q9_CK_4");

                entity.Property(e => e.SfQ9Ck5).HasColumnName("SF_Q9_CK_5");

                entity.Property(e => e.Tellphone)
                    .HasMaxLength(50)
                    .HasColumnName("TELLPHONE");

                entity.Property(e => e.UsernameCh)
                    .HasMaxLength(50)
                    .HasColumnName("USERNAME_CH");

                entity.Property(e => e.UsernameEn)
                    .HasMaxLength(50)
                    .HasColumnName("USERNAME_EN");

                entity.Property(e => e.WmAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("WM_ADDRESS_1");

                entity.Property(e => e.WmAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("WM_ADDRESS_2");

                entity.Property(e => e.WmDept1)
                    .HasMaxLength(50)
                    .HasColumnName("WM_DEPT_1");

                entity.Property(e => e.WmDept2)
                    .HasMaxLength(50)
                    .HasColumnName("WM_DEPT_2");

                entity.Property(e => e.WmName1)
                    .HasMaxLength(50)
                    .HasColumnName("WM_NAME_1");

                entity.Property(e => e.WmName2)
                    .HasMaxLength(50)
                    .HasColumnName("WM_NAME_2");

                entity.Property(e => e.WmPhone1)
                    .HasMaxLength(50)
                    .HasColumnName("WM_PHONE_1");

                entity.Property(e => e.WmPhone2)
                    .HasMaxLength(50)
                    .HasColumnName("WM_PHONE_2");

                entity.Property(e => e.WmTitle1)
                    .HasMaxLength(50)
                    .HasColumnName("WM_TITLE_1");

                entity.Property(e => e.WmTitle2)
                    .HasMaxLength(50)
                    .HasColumnName("WM_TITLE_2");
            });

            modelBuilder.Entity<HrFormConsent>(entity =>
            {
                entity.HasKey(e => e.CandidateId);

                entity.ToTable("HR_FORM_CONSENT");

                entity.Property(e => e.CandidateId)
                    .ValueGeneratedNever()
                    .HasColumnName("CANDIDATE_ID");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(10)
                    .HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.Identity)
                    .HasMaxLength(10)
                    .HasColumnName("IDENTITY");

                entity.Property(e => e.LastUpdateBy)
                    .HasMaxLength(10)
                    .HasColumnName("LAST_UPDATE_BY");

                entity.Property(e => e.LastUpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LAST_UPDATE_DATE");

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .HasColumnName("USERNAME");
            });

            modelBuilder.Entity<HrStatusdesc>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("HR_STATUSDESC");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.StatusDesc)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("STATUS_DESC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
