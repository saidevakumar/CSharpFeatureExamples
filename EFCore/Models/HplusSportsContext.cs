using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Models;

public partial class HplusSportsContext : DbContext
{
    public HplusSportsContext()
    {
    }

    public HplusSportsContext(DbContextOptions<HplusSportsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<PerishableProduct> PerishableProducts { get; set; }

    public virtual DbSet<SalesGroup> SalesGroups { get; set; }

    public virtual DbSet<Salesperson> Salespeople { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SAILENOVO;Initial Catalog=HPlusSports;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasIndex(e => e.LastName);

            //entity.Property(e => e.CmpLastFirst)
            //    .HasMaxLength(102)
            //    .IsUnicode(false)
            //    .HasComputedColumnSql("(([str_fld_LastName]+', ')+[str_fld_FirstName])", true)
            //    .HasColumnName("cmp_LastFirst");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("str_fld_Address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("str_fld_City");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("str_fld_Email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("str_fld_FirstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("str_fld_LastName");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("str_fld_Phone");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("str_fld_State");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("str_fld_Zipcode");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.OrderDate, "IX_Order").IsDescending();

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            
            //entity.Property(e => e.LastUpdate)
            //    .IsRowVersion()
            //    .IsConcurrencyToken();

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.SalespersonId).HasColumnName("SalespersonID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('none')");
            entity.Property(e => e.TotalDue).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Salesperson).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SalespersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Salesperson");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Product1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("ProductID");

            entity.HasDiscriminator<bool>("Perishable")
                .HasValue<Product>(false)
                .HasValue<PerishableProduct>(true);

            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Variety)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SalesGroup>(entity =>
        {
            entity.ToTable("SalesGroup");

            entity.HasIndex(e => new { e.State, e.Type }, "IX_StateType").IsUnique();

            entity.Property(e => e.State).HasMaxLength(2);

            entity.HasMany(e => e.salesperson)
            .WithOne(e => e.SalesGroup)
            .HasPrincipalKey(e => new { e.State, e.Type });
        });

        modelBuilder.Entity<Salesperson>(entity =>
        {
            entity.ToTable("Salesperson");

            entity.Property(e => e.SalespersonId).HasColumnName("SalespersonID");
            entity.Property<string>("Address");
            entity.Property<string>("City");
            
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SalesGroupState)
                .HasMaxLength(2)
                .HasDefaultValue("CA");
            
            entity.Property(e => e.SalesGroupType).HasDefaultValue("1");

            entity.Property<string>("State");
            entity.Property<string>("Zipcode");

            entity.Ignore(e => e.FullName);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
