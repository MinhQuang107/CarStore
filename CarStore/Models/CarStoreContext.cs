using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class CarStoreContext : DbContext
{
    public CarStoreContext()
    {
    }

    public CarStoreContext(DbContextOptions<CarStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accountants> Accountants { get; set; }

    public virtual DbSet<Appointment> Appointment { get; set; }

    public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }

    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

    public virtual DbSet<Car> Car { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Employees> Employees { get; set; }

    public virtual DbSet<Engine_Transmission> Engine_Transmission { get; set; }

    public virtual DbSet<Exterior> Exterior { get; set; }

    public virtual DbSet<ImageCar> ImageCar { get; set; }

    public virtual DbSet<Interior_Convenience> Interior_Convenience { get; set; }

    public virtual DbSet<Maintenance_Staff> Maintenance_Staff { get; set; }

    public virtual DbSet<Managers> Managers { get; set; }

    public virtual DbSet<Safety> Safety { get; set; }

    public virtual DbSet<Sales> Sales { get; set; }

    public virtual DbSet<Salespeople> Salespeople { get; set; }

    public virtual DbSet<Security> Security { get; set; }

    public virtual DbSet<Size_Weight> Size_Weight { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-8PHPFK41;Initial Catalog=CarStore;Persist Security Info=True;User ID=sa;Password=Anh307241;Encrypt=True;Trust Server Certificate=True;Command Timeout=300", x => x
                .UseNetTopologySuite()
                .UseHierarchyId()
                .UseNodaTime());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accountants>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Accounta__3214EC2710866E92");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.IDNavigation).WithOne(p => p.Accountants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accountants__ID__0F624AF8");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Appointm__3214EC274841DC6F");

            entity.HasOne(d => d.Customer).WithMany(p => p.Appointment).HasConstraintName("FK__Appointme__Custo__151B244E");

            entity.HasOne(d => d.Employee).WithMany(p => p.Appointment).HasConstraintName("FK__Appointme__Emplo__14270015");
        });

        modelBuilder.Entity<AspNetRoles>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUsers>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Car__3214EC27016AE550");
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Customer__3214EC27365AD784");

            entity.HasOne(d => d.User).WithMany(p => p.Customers).HasConstraintName("FK__Customers__User___1EA48E88");
        });

        modelBuilder.Entity<Employees>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Employee__3214EC27C9AB07A0");

            entity.HasOne(d => d.User).WithMany(p => p.Employees).HasConstraintName("FK__Employees__User___1CBC4616");
        });

        modelBuilder.Entity<Engine_Transmission>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Engine_T__3214EC27E9CE297E");

            entity.HasOne(d => d.Car).WithMany(p => p.Engine_Transmission).HasConstraintName("FK__Engine_Tr__Car_I__7D439ABD");
        });

        modelBuilder.Entity<Exterior>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Exterior__3214EC273849C3E4");

            entity.HasOne(d => d.Car).WithMany(p => p.Exterior).HasConstraintName("FK__Exterior__Car_ID__778AC167");
        });

        modelBuilder.Entity<ImageCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageCar__3214EC07667B8A83");

            entity.HasOne(d => d.Car).WithMany(p => p.ImageCar).HasConstraintName("FK__ImageCar__Car_Id__71D1E811");
        });

        modelBuilder.Entity<Interior_Convenience>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Interior__3214EC277B07A1A0");

            entity.HasOne(d => d.Car).WithMany(p => p.Interior_Convenience).HasConstraintName("FK__Interior___Car_I__7A672E12");
        });

        modelBuilder.Entity<Maintenance_Staff>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Maintena__3214EC27A74DF67C");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.IDNavigation).WithOne(p => p.Maintenance_Staff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenance___ID__09A971A2");
        });

        modelBuilder.Entity<Managers>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Managers__3214EC27C20ADD1D");

            entity.HasOne(d => d.User).WithMany(p => p.Managers).HasConstraintName("FK__Managers__User_I__1DB06A4F");
        });

        modelBuilder.Entity<Safety>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Safety__3214EC274BB5BE52");

            entity.HasOne(d => d.Car).WithMany(p => p.Safety).HasConstraintName("FK__Safety__Car_ID__00200768");
        });

        modelBuilder.Entity<Sales>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Sales__3214EC27BA9FD00B");

            entity.HasOne(d => d.Car).WithMany(p => p.Sales).HasConstraintName("FK__Sales__Car_ID__17F790F9");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales).HasConstraintName("FK__Sales__Customer___18EBB532");

            entity.HasOne(d => d.Salesperson).WithMany(p => p.Sales).HasConstraintName("FK__Sales__Salespers__19DFD96B");
        });

        modelBuilder.Entity<Salespeople>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Salespeo__3214EC270C6A13F7");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.IDNavigation).WithOne(p => p.Salespeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salespeople__ID__06CD04F7");
        });

        modelBuilder.Entity<Security>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Security__3214EC27819991D0");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.IDNavigation).WithOne(p => p.Security)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Security__ID__0C85DE4D");
        });

        modelBuilder.Entity<Size_Weight>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Size_Wei__3214EC27C34ACDB9");

            entity.HasOne(d => d.Car).WithMany(p => p.Size_Weight).HasConstraintName("FK__Size_Weig__Car_I__74AE54BC");
        });

        OnModelCreatingGeneratedProcedures(modelBuilder);
        OnModelCreatingGeneratedFunctions(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
