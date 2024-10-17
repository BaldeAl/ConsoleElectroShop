using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleElectroShop.Models;

public partial class DbElectroShopContext : DbContext
{
    public DbElectroShopContext()
    {
    }

    public DbElectroShopContext(DbContextOptions<DbElectroShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<LignesCommande> LignesCommandes { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<VCommandesAvecTotal> VCommandesAvecTotals { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\balde\\source\\repos\\ConsoleElectroShop\\Data\\DbElectroShop.mdf;Database=DbElectroShop;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC0742D0F7A6");

            entity.Property(e => e.Adresse).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Nom).HasMaxLength(50);
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Commande__3214EC07A85FDED2");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Statut).HasMaxLength(50);

            entity.HasOne(d => d.Client).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Commandes__Clien__3E52440B");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fourniss__3214EC07052ADDD3");

            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.Nom).HasMaxLength(50);
        });

        modelBuilder.Entity<LignesCommande>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LignesCo__3214EC071E90B5DC");

            entity.HasOne(d => d.Commande).WithMany(p => p.LignesCommandes)
                .HasForeignKey(d => d.CommandeId)
                .HasConstraintName("FK__LignesCom__Comma__412EB0B6");

            entity.HasOne(d => d.Produit).WithMany(p => p.LignesCommandes)
                .HasForeignKey(d => d.ProduitId)
                .HasConstraintName("FK__LignesCom__Produ__4222D4EF");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Produits__3214EC07CDE7B7C6");

            entity.Property(e => e.Nom).HasMaxLength(50);

            entity.HasMany(d => d.Fournisseurs).WithMany(p => p.Produits)
                .UsingEntity<Dictionary<string, object>>(
                    "ProduitFournisseur",
                    r => r.HasOne<Fournisseur>().WithMany()
                        .HasForeignKey("FournisseurId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProduitFo__Fourn__4AB81AF0"),
                    l => l.HasOne<Produit>().WithMany()
                        .HasForeignKey("ProduitId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProduitFo__Produ__49C3F6B7"),
                    j =>
                    {
                        j.HasKey("ProduitId", "FournisseurId").HasName("PK__ProduitF__208C9AFD9B08E862");
                        j.ToTable("ProduitFournisseur");
                    });
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.ProduitId).HasName("PK__Stock__6E79AEDC35587B92");

            entity.ToTable("Stock");

            entity.Property(e => e.ProduitId)
                .ValueGeneratedNever()
                .HasColumnName("ProduitID");

            entity.HasOne(d => d.Produit).WithOne(p => p.StockNavigation)
                .HasForeignKey<Stock>(d => d.ProduitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__ProduitID__46E78A0C");
        });

        modelBuilder.Entity<VCommandesAvecTotal>(entity =>
        {

            entity.HasNoKey();
            entity.ToView("V_CommandesAvecTotal");
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Total).HasColumnType("Total");
        }); 

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
