using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class CareCenterContext : DbContext
{
    public CareCenterContext()
    {
    }

    public CareCenterContext(DbContextOptions<CareCenterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Resident> Residents { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CareCenterSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1646C52C1");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeFirstName).HasMaxLength(50);
            entity.Property(e => e.EmployeeLastName).HasMaxLength(50);
            entity.Property(e => e.Mail).HasMaxLength(50);
        });

        modelBuilder.Entity<Resident>(entity =>
        {
            entity.HasKey(e => e.ResidentId).HasName("PK__Resident__07FB00FC11FA58A5");

            entity.Property(e => e.ResidentId).HasColumnName("ResidentID");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.ResidentFirstName).HasMaxLength(50);
            entity.Property(e => e.ResidentLastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D1E522F7EE");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.ActualEndDate).HasColumnType("datetime");
            entity.Property(e => e.ExpectedEndDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.ResidentId).HasColumnName("ResidentID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Resident).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ResidentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResidentTask");

            entity.HasOne(d => d.SolvedByNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.SolvedBy)
                .HasConstraintName("FK_EmployeeTask");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
