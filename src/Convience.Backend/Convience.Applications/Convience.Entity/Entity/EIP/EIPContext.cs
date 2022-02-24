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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=10.1.1.181;Initial Catalog=KPI;User ID=sa;Password=Chen@full");
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

                entity.Property(e => e.Candidate)
                    .HasMaxLength(10)
                    .HasColumnName("CANDIDATE");

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
                    .HasMaxLength(10)
                    .HasColumnName("PLACE");

                entity.Property(e => e.ReplyDate)
                    .HasColumnType("date")
                    .HasColumnName("REPLY_DATE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.ValidDate)
                    .HasColumnType("date")
                    .HasColumnName("VALID_DATE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
