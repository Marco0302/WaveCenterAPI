using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WaveCenter.Model;

public partial class WaveCenterContext : IdentityUserContext<User>
{
    public WaveCenterContext()
    {
    }

    public WaveCenterContext(DbContextOptions<WaveCenterContext> options): base(options)
    {
    }
<<<<<<< Updated upstream
    public virtual DbSet<Media> Galeria { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<TipoFuncionario> TipoFuncionarios { get; set; }
    public virtual DbSet<Funcionario> Funcionarios { get; set; }
=======
    public virtual DbSet<Galeria> Galeria { get; set; }
>>>>>>> Stashed changes


    public virtual DbSet<Voucher> Vouchers { get; set; }


    public virtual DbSet<CategoriaEquipamento> CategoriaEquipamentos { get; set; }
    public virtual DbSet<Equipamento> Equipamentos { get; set; }


    public virtual DbSet<TipoExperiencia> TipoExperiencias { get; set; }
    public virtual DbSet<CategoriaExperiencia> CategoriaExperiencias { get; set; }
    public virtual DbSet<Experiencia> Experiencias { get; set; }

    public virtual DbSet<Local> Locais { get; set; }
    public virtual DbSet<Marcacao> Marcacoes {  get; set; }
    public virtual DbSet<ClientesMarcacao> ClientesMarcacoes { get; set; }

    public virtual DbSet<PedidoReparacao> PedidoReparacao { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

<<<<<<< Updated upstream

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasOne(p => p.TipoFuncionario)
                .WithMany()
                .HasForeignKey(p => p.IdTipoFuncionario);
            entity.HasOne(p => p.Media)
                .WithMany()
                .HasForeignKey(p => p.IdMedia);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasOne(p => p.Galeria)
                .WithMany()
                .HasForeignKey(p => p.IdAvatar);
        });

=======
>>>>>>> Stashed changes
        modelBuilder.Entity<Equipamento>(entity =>
        {
            entity.HasOne(p => p.CategoriaEquipamento)
                .WithMany()
                .HasForeignKey(p => p.IdCategoriaEquipamento);
        });

        modelBuilder.Entity<Experiencia>(entity =>
        {
            entity.HasOne(p => p.CategoriaExperiencia)
                .WithMany()
                .HasForeignKey(p => p.IdCategoriaExperiencia);

            entity.HasOne(p => p.TipoExperiencia)
                .WithMany()
                .HasForeignKey(p => p.IdTipoExperiencia);

            entity.HasOne(p => p.Local)
                .WithMany()
                .HasForeignKey(p => p.IdLocal);
        });

<<<<<<< Updated upstream
        modelBuilder.Entity<PedidoReparacao>(entity =>
        {
            entity.HasOne(p => p.Equipamento)
                .WithMany()
                .HasForeignKey(p => p.IdEquipamento);
            entity.HasOne(p => p.Funcionario)
                .WithMany()
                .HasForeignKey(p => p.IdFuncionario);
        });

=======
        modelBuilder.Entity<Marcacao>(entity =>
        {
            entity.HasOne(e => e.Experiencia)
                .WithMany()
                .HasForeignKey(e => e.IdExperiencia) 
                .OnDelete(DeleteBehavior.Cascade);
        });


>>>>>>> Stashed changes
        //modelBuilder.Entity<Player>(entity =>
        //{
        //    entity.HasKey(e => e.Idplayer);

        //    entity.Property(e => e.Idplayer)
        //        .HasDefaultValueSql("(newid())")
        //        .HasColumnName("IDPlayer");
        //    entity.Property(e => e.Address).HasMaxLength(50);
        //    entity.Property(e => e.Birthdate).HasColumnType("date");
        //    entity.Property(e => e.Name).HasMaxLength(50);
        //});

        //modelBuilder.Entity<PlayersClub>(entity =>
        //{
        //    entity.HasNoKey();

        //    entity.Property(e => e.Idclub).HasColumnName("IDClub");
        //    entity.Property(e => e.Idplayer).HasColumnName("IDPlayer");
        //    entity.Property(e => e.SubscriptionDate).HasColumnType("date");

        //    entity.HasOne(d => d.IdclubNavigation).WithMany()
        //        .HasForeignKey(d => d.Idclub)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_PlayersClubs_Clubs");

        //    entity.HasOne(d => d.IdplayerNavigation).WithMany()
        //        .HasForeignKey(d => d.Idplayer)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_PlayersClubs_Players");
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
