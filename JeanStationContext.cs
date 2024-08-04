using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JeanStationapp.Models;

public partial class JeanStationContext : DbContext
{
    public JeanStationContext()
    {
    }

    public JeanStationContext(DbContextOptions<JeanStationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemsCart> ItemsCarts { get; set; }

    public virtual DbSet<ItemsOrder> ItemsOrders { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=WIN2K22-VM\\SQLEXPRESS;initial catalog=JeanStation;integrated security=true;trustservercertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__415B03B8651717FF");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cartId");
            entity.Property(e => e.CustId).HasColumnName("custId");

            entity.HasOne(d => d.Cust).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__Cart__custId__5165187F");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("PK__Customer__9725F2C6EA376A58");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Username, "UQ__Customer__F3DBC572FBFB6862").IsUnique();

            entity.Property(e => e.CustId).HasColumnName("custId");
            entity.Property(e => e.CtPhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ct_phoneNo");
            entity.Property(e => e.CustAddr)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("custAddr");
            entity.Property(e => e.CustName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("custName");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Pwd)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pwd");
            entity.Property(e => e.Username)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemCode).HasName("PK__Item__A22D0FD159B9F0C4");

            entity.ToTable("Item");

            entity.Property(e => e.ItemCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("itemCode");
            entity.Property(e => e.ItemImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("itemImage");
            entity.Property(e => e.ItemName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("itemName");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.StoreId).HasColumnName("storeId");

            entity.HasOne(d => d.Store).WithMany(p => p.Items)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Item__storeId__4BAC3F29");
        });

        modelBuilder.Entity<ItemsCart>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Items_Cart");

            entity.Property(e => e.CartId).HasColumnName("cartId");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("itemCode");
            entity.Property(e => e.Qty).HasColumnName("qty");

            entity.HasOne(d => d.Cart).WithMany()
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__Items_Car__cartI__5AEE82B9");

            entity.HasOne(d => d.ItemCodeNavigation).WithMany()
                .HasForeignKey(d => d.ItemCode)
                .HasConstraintName("FK__Items_Car__itemC__5BE2A6F2");
        });

        modelBuilder.Entity<ItemsOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Items_order");

            entity.Property(e => e.ItemCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("itemCode");
            entity.Property(e => e.OrderId).HasColumnName("orderId");

            entity.HasOne(d => d.ItemCodeNavigation).WithMany()
                .HasForeignKey(d => d.ItemCode)
                .HasConstraintName("FK__Items_ord__itemC__59063A47");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Items_ord__order__5812160E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__0809335DD4F709C9");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CustId).HasColumnName("custId");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("orderDate");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Order Placed")
                .HasColumnName("status");

            entity.HasOne(d => d.Cust).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__Orders__custId__5441852A");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__1EA7161390638827");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId).HasColumnName("storeId");
            entity.Property(e => e.Location)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.StPhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("st_phoneNo");
            entity.Property(e => e.StoreName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("storeName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
