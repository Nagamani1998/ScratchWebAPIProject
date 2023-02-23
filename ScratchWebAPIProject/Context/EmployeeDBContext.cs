using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ScratchWebAPIProject.DomainModels;

#nullable disable

namespace ScratchWebAPIProject.Context
{
    public partial class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext()
        {
        }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dept> Depts { get; set; }
        public virtual DbSet<Emp> Emps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("data source=medico.mysql.database.azure.com;initial catalog=sampleazure;userid=nmani;password=Mani3050@", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.40-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasKey(e => e.Deptno)
                    .HasName("PRIMARY");

                entity.ToTable("dept");

                entity.Property(e => e.Deptno)
                    .HasColumnType("smallint(2)")
                    .ValueGeneratedNever()
                    .HasColumnName("deptno");

                entity.Property(e => e.Dname)
                    .HasMaxLength(14)
                    .HasColumnName("dname");

                entity.Property(e => e.Loc)
                    .HasMaxLength(13)
                    .HasColumnName("loc");
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.HasKey(e => e.Empno)
                    .HasName("PRIMARY");

                entity.ToTable("emp");

                entity.HasIndex(e => e.Deptno, "fk_deptno");

                entity.Property(e => e.Empno)
                    .HasColumnType("smallint(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("empno");

                entity.Property(e => e.Comm)
                    .HasPrecision(7, 2)
                    .HasColumnName("comm");

                entity.Property(e => e.Deptno)
                    .HasColumnType("smallint(2)")
                    .HasColumnName("deptno");

                entity.Property(e => e.Ename)
                    .HasMaxLength(100)
                    .HasColumnName("ename");

                entity.Property(e => e.Hiredate)
                    .HasColumnType("date")
                    .HasColumnName("hiredate");

                entity.Property(e => e.Job)
                    .HasMaxLength(9)
                    .HasColumnName("job");

                entity.Property(e => e.Mgr)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("mgr");

                entity.Property(e => e.Sal)
                    .HasPrecision(7, 2)
                    .HasColumnName("sal");

                entity.HasOne(d => d.DeptnoNavigation)
                    .WithMany(p => p.Emps)
                    .HasForeignKey(d => d.Deptno)
                    .HasConstraintName("fk_deptno");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
