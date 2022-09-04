


using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class InsidersTradeMonitorContext : DbContext
    {
        public InsidersTradeMonitorContext()
        {
        }

        public InsidersTradeMonitorContext(DbContextOptions<InsidersTradeMonitorContext> options)
            : base(options)
        {
        }

        public InsidersTradeMonitorContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public virtual DbSet<DerivativeTransaction> DerivativeTransactions { get; set; }

        public virtual DbSet<Entity> Entities { get; set; }

        public virtual DbSet<EntityType> EntityTypes { get; set; }

        public virtual DbSet<Form4Report> Form4Reports { get; set; }

        public virtual DbSet<ImportRun> ImportRuns { get; set; }

        public virtual DbSet<ImportRunForm4Report> ImportRunForm4Reports { get; set; }

        public virtual DbSet<ImportRunState> ImportRunStates { get; set; }

        public virtual DbSet<NonDerivativeTransaction> NonDerivativeTransactions { get; set; }

        public virtual DbSet<OwnershipType> OwnershipTypes { get; set; }

        public virtual DbSet<TransactionCode> TransactionCodes { get; set; }

        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        public virtual DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=InsidersTradeMonitor;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DerivativeTransaction>(entity =>
    {
        entity.ToTable("DerivativeTransaction");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Form4ReportID).HasColumnName("Form4ReportID")
                .IsRequired()
;
        entity.Property(e => e.TitleOfDerivative).HasColumnName("TitleOfDerivative")
                .IsRequired()
                .HasMaxLength(250)
;
        entity.Property(e => e.ConversionExercisePrice).HasColumnName("ConversionExercisePrice")
                .IsRequired()
;
        entity.Property(e => e.TransactionDate).HasColumnName("TransactionDate")
                .IsRequired()
;
        entity.Property(e => e.TransactionCodeID).HasColumnName("TransactionCodeID")
                .IsRequired()
;
        entity.Property(e => e.EarlyVoluntarilyReport).HasColumnName("EarlyVoluntarilyReport")
                .IsRequired()
;
        entity.Property(e => e.SharesAmount).HasColumnName("SharesAmount")
;
        entity.Property(e => e.DerivativeSecurityPrice).HasColumnName("DerivativeSecurityPrice")
;
        entity.Property(e => e.TransactionTypeID).HasColumnName("TransactionTypeID")
;
        entity.Property(e => e.DateExercisable).HasColumnName("DateExercisable")
;
        entity.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate")
;
        entity.Property(e => e.UnderlyingTitle).HasColumnName("UnderlyingTitle")
                .IsRequired()
                .HasMaxLength(250)
;
        entity.Property(e => e.UnderlyingSharesAmount).HasColumnName("UnderlyingSharesAmount")
                .IsRequired()
;
        entity.Property(e => e.AmountFollowingReport).HasColumnName("AmountFollowingReport")
                .IsRequired()
;
        entity.Property(e => e.OwnershipTypeID).HasColumnName("OwnershipTypeID")
                .IsRequired()
;
        entity.Property(e => e.NatureOfIndirectOwnership).HasColumnName("NatureOfIndirectOwnership")
                .HasMaxLength(250)
;
        entity.HasOne(e => e.Form4Report)
                .WithMany(p => p.DerivativeTransactions)
                .HasForeignKey(e => e.Form4ReportID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DerivativeTransaction_Form4Report");

        entity.HasOne(e => e.TransactionCode)
                .WithMany(p => p.DerivativeTransactions)
                .HasForeignKey(e => e.TransactionCodeID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DerivativeTransaction_TransactionCode");

        entity.HasOne(e => e.TransactionType)
                .WithMany(p => p.DerivativeTransactions)
                .HasForeignKey(e => e.TransactionTypeID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DerivativeTransaction_TransactionType");

        entity.HasOne(e => e.OwnershipType)
                .WithMany(p => p.DerivativeTransactions)
                .HasForeignKey(e => e.OwnershipTypeID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DerivativeTransaction_OwnershipType");

    });

            modelBuilder.Entity<Entity>(entity =>
    {
        entity.ToTable("Entity");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.EntityTypeID).HasColumnName("EntityTypeID")
                .IsRequired()
;
        entity.Property(e => e.CIK).HasColumnName("CIK")
                .IsRequired()
;
        entity.Property(e => e.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(250)
;
        entity.Property(e => e.TradingSymbol).HasColumnName("TradingSymbol")
                .HasMaxLength(50)
;
        entity.Property(e => e.IsMonitored).HasColumnName("IsMonitored")
                .IsRequired()
;
        entity.HasOne(e => e.EntityType)
                .WithMany(p => p.Entities)
                .HasForeignKey(e => e.EntityTypeID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Entity_EntityType");
    });

            modelBuilder.Entity<EntityType>(entity =>
    {
        entity.ToTable("EntityType");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.TypeName).HasColumnName("TypeName")
                .IsRequired()
                .HasMaxLength(50)
;
    });

            modelBuilder.Entity<Form4Report>(entity =>
    {
        entity.ToTable("Form4Report");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.IssuerID).HasColumnName("IssuerID")
                .IsRequired()
;
        entity.Property(e => e.ReporterID).HasColumnName("ReporterID")
                .IsRequired()
;
        entity.Property(e => e.ReportID).HasColumnName("ReportID")
                .IsRequired()
                .HasMaxLength(50)
;
        entity.Property(e => e.IsOfficer).HasColumnName("IsOfficer")
                .IsRequired()
;
        entity.Property(e => e.IsDirector).HasColumnName("IsDirector")
                .IsRequired()
;
        entity.Property(e => e.Is10PctHolder).HasColumnName("Is10PctHolder")
                .IsRequired()
;
        entity.Property(e => e.IsOther).HasColumnName("IsOther")
                .IsRequired()
;
        entity.Property(e => e.OtherText).HasColumnName("OtherText")
                .HasMaxLength(250)
;
        entity.Property(e => e.OfficerTitle).HasColumnName("OfficerTitle")
                .HasMaxLength(50)
;
        entity.Property(e => e.Date).HasColumnName("Date")
                .IsRequired()
;
        entity.Property(e => e.DateSubmitted).HasColumnName("DateSubmitted")
                .IsRequired()
;

        entity.HasOne(e => e.Issuer)
                .WithMany(d => d.IssuerForm4Reports)
                .HasForeignKey( e => e.IssuerID )
                .HasConstraintName("FK_Form4Report_Issuer")
                .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Reporter)
                .WithMany(d => d.ReporterForm4Reports)
                .HasForeignKey(e => e.ReporterID)
                .HasConstraintName("FK_Form4Report_Reporter")
                .OnDelete(DeleteBehavior.Cascade);
    });

            modelBuilder.Entity<ImportRun>(entity =>
    {
        entity.ToTable("ImportRun");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.TimeStart).HasColumnName("TimeStart")
                .IsRequired()
;
        entity.Property(e => e.TimeEnd).HasColumnName("TimeEnd")
;
        entity.Property(e => e.RequestJson).HasColumnName("RequestJson")
                .IsRequired()
                .HasMaxLength(1000)
;
        entity.Property(e => e.StateID).HasColumnName("StateID")
                .IsRequired()
;
    });

            modelBuilder.Entity<ImportRunForm4Report>(entity =>
    {
        entity.ToTable("ImportRunForm4Report");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.ImportRunID).HasColumnName("ImportRunID")
                .IsRequired()
;
        entity.Property(e => e.Form4ReportID).HasColumnName("Form4ReportID")
                .IsRequired()
;
        entity.Property(e => e.TimeStarted).HasColumnName("TimeStarted")
                .IsRequired()
;
        entity.Property(e => e.TimeCompleted).HasColumnName("TimeCompleted")
;
    });

            modelBuilder.Entity<ImportRunState>(entity =>
    {
        entity.ToTable("ImportRunState");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(50)
;
    });

            modelBuilder.Entity<NonDerivativeTransaction>(entity =>
    {
        entity.ToTable("NonDerivativeTransaction");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Form4ReportID).HasColumnName("Form4ReportID")
                .IsRequired()
;
        entity.Property(e => e.TitleOfSecurity).HasColumnName("TitleOfSecurity")
                .IsRequired()
                .HasMaxLength(250)
;
        entity.Property(e => e.TransactionDate).HasColumnName("TransactionDate")
                .IsRequired()
;
        entity.Property(e => e.DeemedExecDate).HasColumnName("DeemedExecDate")
;
        entity.Property(e => e.TransactionCodeID).HasColumnName("TransactionCodeID")
;
        entity.Property(e => e.EarlyVoluntarilyReport).HasColumnName("EarlyVoluntarilyReport")
                .IsRequired()
;
        entity.Property(e => e.SharesAmount).HasColumnName("SharesAmount")
;
        entity.Property(e => e.TransactionTypeID).HasColumnName("TransactionTypeID")
;
        entity.Property(e => e.Price).HasColumnName("Price")
                .IsRequired()
;
        entity.Property(e => e.AmountFollowingReport).HasColumnName("AmountFollowingReport")
                .IsRequired()
;
        entity.Property(e => e.OwnershipTypeID).HasColumnName("OwnershipTypeID")
;
        entity.Property(e => e.NatureOfIndirectOwnership).HasColumnName("NatureOfIndirectOwnership")
                .HasMaxLength(250)
;
    });

            modelBuilder.Entity<OwnershipType>(entity =>
    {
        entity.ToTable("OwnershipType");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Code).HasColumnName("Code")
                .IsRequired()
                .HasMaxLength(1)
;
        entity.Property(e => e.Description).HasColumnName("Description")
                .HasMaxLength(50)
;
    });

            modelBuilder.Entity<TransactionCode>(entity =>
    {
        entity.ToTable("TransactionCode");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Code).HasColumnName("Code")
                .IsRequired()
                .HasMaxLength(10)
;
        entity.Property(e => e.Description).HasColumnName("Description")
                .HasMaxLength(250)
;
    });

            modelBuilder.Entity<TransactionType>(entity =>
    {
        entity.ToTable("TransactionType");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Code).HasColumnName("Code")
                .IsRequired()
                .HasMaxLength(10)
;
        entity.Property(e => e.Description).HasColumnName("Description")
                .HasMaxLength(250)
;
    });

            modelBuilder.Entity<User>(entity =>
    {
        entity.ToTable("User");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Login).HasColumnName("Login")
                .IsRequired()
                .HasMaxLength(250)
;
        entity.Property(e => e.PwdHash).HasColumnName("PwdHash")
                .IsRequired()
                .HasMaxLength(250)
;
        entity.Property(e => e.Salt).HasColumnName("Salt")
                .IsRequired()
                .HasMaxLength(50)
;
        entity.Property(e => e.FirstName).HasColumnName("FirstName")
                .IsRequired()
                .HasMaxLength(50)
;
        entity.Property(e => e.MiddleName).HasColumnName("MiddleName")
                .HasMaxLength(50)
;
        entity.Property(e => e.LastName).HasColumnName("LastName")
                .IsRequired()
                .HasMaxLength(50)
;
        entity.Property(e => e.FriendlyName).HasColumnName("FriendlyName")
                .HasMaxLength(50)
;
        entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate")
                .IsRequired()
;
        entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate")
;
        entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID")
;
    });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
