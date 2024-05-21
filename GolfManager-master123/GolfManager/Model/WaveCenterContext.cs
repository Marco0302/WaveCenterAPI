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
    public virtual DbSet<Media> Medias { get; set; }

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
    public virtual DbSet<TipoUser> TipoUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(p => p.TipoUser)
                .WithMany()
                .HasForeignKey(p => p.IdTipoUser);

            entity.HasOne(p => p.Media)
                .WithMany()
                .HasForeignKey(p => p.IdMedia);
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

            entity.HasOne(p => p.Local)
                .WithMany()
                .HasForeignKey(p => p.IdLocal);
        });

        modelBuilder.Entity<PedidoReparacao>(entity =>
        {
            entity.HasOne(p => p.Equipamento)
                .WithMany()
                .HasForeignKey(p => p.IdEquipamento);
            entity.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);
        });

        modelBuilder.Entity<Marcacao>(entity =>
        {
            entity.HasOne(e => e.Experiencia)
                .WithMany()
                .HasForeignKey(e => e.IdExperiencia);
        });

        modelBuilder.Entity<ClientesMarcacao>()
         .HasOne(cm => cm.Marcacao)
         .WithMany(m => m.ClientesMarcacoes)
         .HasForeignKey(cm => cm.MarcacaoId);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
