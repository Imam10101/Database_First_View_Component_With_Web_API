using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vcwithwebapi.Models
{
    public partial class APContext : DbContext
    {
        public APContext()
        {
        }

        public APContext(DbContextOptions<APContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactUpdate> ContactUpdates { get; set; } = null!;
        public virtual DbSet<Glaccount> Glaccounts { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceArchive> InvoiceArchives { get; set; } = null!;
        public virtual DbSet<InvoiceLineItem> InvoiceLineItems { get; set; } = null!;
        public virtual DbSet<Term> Terms { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3AU5U1E;Database=AP;user id=sa;Password=1234; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactUpdate>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("VendorID");
            });

            modelBuilder.Entity<Glaccount>(entity =>
            {
                entity.HasKey(e => e.AccountNo);

                entity.ToTable("GLAccounts");

                entity.Property(e => e.AccountNo).ValueGeneratedNever();

                entity.Property(e => e.AccountDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(e => e.InvoiceDate, "IX_InvoiceDate");

                entity.HasIndex(e => e.TermsId, "IX_Invoices_TermsID");

                entity.HasIndex(e => e.VendorId, "IX_Invoices_VendorID");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.CreditTotal).HasColumnType("money");

                entity.Property(e => e.InvoiceDate).HasColumnType("smalldatetime");

                entity.Property(e => e.InvoiceDueDate).HasColumnType("smalldatetime");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceTotal).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasColumnType("smalldatetime");

                entity.Property(e => e.PaymentTotal).HasColumnType("money");

                entity.Property(e => e.TermsId).HasColumnName("TermsID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.HasOne(d => d.Terms)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.TermsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Terms");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Vendors");
            });

            modelBuilder.Entity<InvoiceArchive>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("InvoiceArchive");

                entity.Property(e => e.CreditTotal).HasColumnType("money");

                entity.Property(e => e.InvoiceDate).HasColumnType("smalldatetime");

                entity.Property(e => e.InvoiceDueDate).HasColumnType("smalldatetime");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceTotal).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasColumnType("smalldatetime");

                entity.Property(e => e.PaymentTotal).HasColumnType("money");

                entity.Property(e => e.TermsId).HasColumnName("TermsID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<InvoiceLineItem>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceId, e.InvoiceSequence });

                entity.HasIndex(e => e.AccountNo, "IX_InvoiceLineItems_AccountNo");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.InvoiceLineItemAmount).HasColumnType("money");

                entity.Property(e => e.InvoiceLineItemDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountNoNavigation)
                    .WithMany(p => p.InvoiceLineItems)
                    .HasForeignKey(d => d.AccountNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceLineItems_GLAccounts");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceLineItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_InvoiceLineItems_Invoices");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.HasKey(e => e.TermsId);

                entity.Property(e => e.TermsId).HasColumnName("TermsID");

                entity.Property(e => e.TermsDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasIndex(e => e.VendorName, "IX_VendorName");

                entity.HasIndex(e => e.DefaultAccountNo, "IX_Vendors_AccountNo");

                entity.HasIndex(e => e.DefaultTermsId, "IX_Vendors_TermsID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.DefaultAccountNo).HasDefaultValueSql("((570))");

                entity.Property(e => e.DefaultTermsId)
                    .HasColumnName("DefaultTermsID")
                    .HasDefaultValueSql("((3))");

                entity.Property(e => e.VendorAddress1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorAddress2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorContactFname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VendorContactFName");

                entity.Property(e => e.VendorContactLname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VendorContactLName");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.VendorZipCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DefaultAccountNoNavigation)
                    .WithMany(p => p.Vendors)
                    .HasForeignKey(d => d.DefaultAccountNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vendors_GLAccounts");

                entity.HasOne(d => d.DefaultTerms)
                    .WithMany(p => p.Vendors)
                    .HasForeignKey(d => d.DefaultTermsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vendors_Terms");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
