using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ServiceProject.Models
{
    public partial class ServiceContext : DbContext
    {
        public ServiceContext()
        {
        }

        public ServiceContext(DbContextOptions<ServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }
        public virtual DbSet<ServiceRequestReport> ServiceRequestReports { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KASHEESHWARA\\SQLEXPRESS;Database=Service;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("make");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("model");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ServiceRequest>(entity =>
            {
                entity.ToTable("ServiceRequest");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Problem)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("problem");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.ReqDate)
                    .HasColumnType("date")
                    .HasColumnName("reqDate");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppServiceReq_AppProduct");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppServiceReq_AppUser");
            });

            modelBuilder.Entity<ServiceRequestReport>(entity =>
            {
                entity.ToTable("ServiceRequestReport");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionTaken)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("actionTaken");

                entity.Property(e => e.DiagnosisDetails)
                    .IsRequired()
                    .HasColumnName("diagnosisDetails");

                entity.Property(e => e.IsPaid)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("isPaid");

                entity.Property(e => e.RepairDetails)
                    .IsRequired()
                    .HasColumnName("repairDetails");

                entity.Property(e => e.ReportDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("reportDate");

                entity.Property(e => e.SerReqId).HasColumnName("serReqId");

                entity.Property(e => e.ServiceType)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("serviceType");

                entity.Property(e => e.VisitFees)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("visitFees");

                entity.HasOne(d => d.SerReq)
                    .WithMany(p => p.ServiceRequestReports)
                    .HasForeignKey(d => d.SerReqId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppServiceReqReport_AppServiceReq");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Mobile)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("mobile");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.RegisteredDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("registeredDate");

                entity.Property(e => e.Role).HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
