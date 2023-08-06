using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace neilApp.Models
{
    public partial class prelimcrudContext : DbContext
    {
        public prelimcrudContext()
        {
        }

        public prelimcrudContext(DbContextOptions<prelimcrudContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderdetail> Orderdetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Stockhistory> Stockhistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=prelimcrud;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.CartId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cartID");

                entity.Property(e => e.CMockStock)
                    .HasColumnType("int(11)")
                    .HasColumnName("cMockStock");

                entity.Property(e => e.CMockTotal)
                    .HasColumnType("int(11)")
                    .HasColumnName("cMockTotal");

                entity.Property(e => e.CQuantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("cQuantity");

                entity.Property(e => e.ProdId)
                    .HasColumnType("int(11)")
                    .HasColumnName("prodID");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderID");

                entity.Property(e => e.Datepurchased)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("datepurchased");

                entity.Property(e => e.Deduction).HasColumnName("deduction");

                entity.Property(e => e.DiscountPercent).HasColumnName("discountPercent");

                entity.Property(e => e.PaidAmount).HasColumnName("paidAmount");

                entity.Property(e => e.SubTotal).HasColumnName("subTotal");

                entity.Property(e => e.Sukli).HasColumnName("sukli");

                entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId)
                    .HasName("PRIMARY");

                entity.ToTable("orderdetails");

                entity.Property(e => e.OrderDetailsId)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderDetailsID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("productID");

                entity.Property(e => e.ProductTotal).HasColumnName("productTotal");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("price");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("status");

                entity.Property(e => e.Stock)
                    .HasColumnType("int(11)")
                    .HasColumnName("stock");

                entity.Property(e => e.Units)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("units");
            });

            modelBuilder.Entity<Stockhistory>(entity =>
            {
                entity.HasKey(e => e.AStockId)
                    .HasName("PRIMARY");

                entity.ToTable("stockhistory");

                entity.Property(e => e.AStockId)
                    .HasColumnType("int(11)")
                    .HasColumnName("a_Stock_ID");

                entity.Property(e => e.AddedStock)
                    .HasColumnType("int(11)")
                    .HasColumnName("added_stock");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("date");

                entity.Property(e => e.ProdId)
                    .HasColumnType("int(11)")
                    .HasColumnName("prodID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
