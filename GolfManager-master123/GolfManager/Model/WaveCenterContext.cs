using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WaveCenter.Model;

public partial class WaveCenterContext : IdentityUserContext<IdentityUser>
{
    public WaveCenterContext()
    {
    }

    public WaveCenterContext(DbContextOptions<WaveCenterContext> options): base(options)
    {
    }
    public virtual DbSet<Galeria> Galeria { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<TipoFuncionario> TipoFuncionarios { get; set; }
    public virtual DbSet<Funcionario> Funcionarios { get; set; }


    public virtual DbSet<Voucher> Vouchers { get; set; }


    public virtual DbSet<CategoriaEquipamento> CategoriaEquipamentos { get; set; }
    public virtual DbSet<Equipamento> Equipamentos { get; set; }


    public virtual DbSet<TipoExperiencia> TipoExperiencias { get; set; }
    public virtual DbSet<CategoriaExperiencia> CategoriaExperiencias { get; set; }
    public virtual DbSet<Experiencia> Experiencias { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasOne(p => p.TipoFuncionario)
                .WithMany()
                .HasForeignKey(p => p.IdTipoFuncionario);
                //entity.HasOne(p => p.Galeria)
                //    .WithMany()
                //    .HasForeignKey(p => p.IdGaleria);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasOne(p => p.Galeria)
                .WithMany()
                .HasForeignKey(p => p.IdAvatar);
        });

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
        });

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
