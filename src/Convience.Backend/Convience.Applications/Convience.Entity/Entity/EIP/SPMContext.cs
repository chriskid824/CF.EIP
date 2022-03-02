using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Convience.Entity.Entity.EIP
{
    public partial class SPMContext : DbContext
    {
        public SPMContext()
        {
        }

        public SPMContext(DbContextOptions<SPMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=10.1.1.181;Initial Catalog=SPM;User ID=sa;Password=Chen@full");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.HasIndex(e => e.Logonid, "IX_USER")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .ValueGeneratedNever()
                    .HasColumnName("USERID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Calendarid).HasColumnName("CALENDARID");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .HasColumnName("CITY");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .HasColumnName("COMPANY");

                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Cust1)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_1");

                entity.Property(e => e.Cust10)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_10");

                entity.Property(e => e.Cust11)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_11");

                entity.Property(e => e.Cust12)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_12");

                entity.Property(e => e.Cust13)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_13");

                entity.Property(e => e.Cust14)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_14");

                entity.Property(e => e.Cust15)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_15");

                entity.Property(e => e.Cust16)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_16");

                entity.Property(e => e.Cust17)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_17");

                entity.Property(e => e.Cust18)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_18");

                entity.Property(e => e.Cust19)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_19");

                entity.Property(e => e.Cust2)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_2");

                entity.Property(e => e.Cust20)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_20");

                entity.Property(e => e.Cust3)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_3");

                entity.Property(e => e.Cust4)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_4");

                entity.Property(e => e.Cust5)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_5");

                entity.Property(e => e.Cust6)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_6");

                entity.Property(e => e.Cust7)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_7");

                entity.Property(e => e.Cust8)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_8");

                entity.Property(e => e.Cust9)
                    .HasMaxLength(50)
                    .HasColumnName("CUST_9");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Enable)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ENABLE")
                    .IsFixedLength(true);

                entity.Property(e => e.Enabledate)
                    .HasColumnType("datetime")
                    .HasColumnName("ENABLEDATE");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Job)
                    .HasMaxLength(20)
                    .HasColumnName("JOB");

                entity.Property(e => e.Lastlogon)
                    .HasColumnType("datetime")
                    .HasColumnName("LASTLOGON");

                entity.Property(e => e.LogDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("LOG_DATETIME");

                entity.Property(e => e.LogLogonid).HasColumnName("LOG_LOGONID");

                entity.Property(e => e.Logonid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOGONID");

                entity.Property(e => e.Logontimes).HasColumnName("LOGONTIMES");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.PasswordE)
                    .HasMaxLength(100)
                    .HasColumnName("PASSWORD_E");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(5)
                    .HasColumnName("POSTCODE");

                entity.Property(e => e.Province)
                    .HasMaxLength(30)
                    .HasColumnName("PROVINCE");

                entity.Property(e => e.Signright)
                    .HasMaxLength(30)
                    .HasColumnName("SIGNRIGHT");

                entity.Property(e => e.TelHome)
                    .HasMaxLength(20)
                    .HasColumnName("TEL_HOME");

                entity.Property(e => e.TelMobil)
                    .HasMaxLength(20)
                    .HasColumnName("TEL_MOBIL");

                entity.Property(e => e.TelOffice)
                    .HasMaxLength(20)
                    .HasColumnName("TEL_OFFICE");

                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .HasColumnName("TITLE");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
