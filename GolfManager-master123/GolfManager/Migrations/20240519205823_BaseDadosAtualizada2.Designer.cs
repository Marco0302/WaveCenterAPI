﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WaveCenter.Model;

#nullable disable

namespace WaveCenter.Migrations
{
    [DbContext(typeof(WaveCenterContext))]
    [Migration("20240519205823_BaseDadosAtualizada2")]
    partial class BaseDadosAtualizada2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WaveCenter.Model.CategoriaEquipamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Designacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoriaEquipamentos");
                });

            modelBuilder.Entity("WaveCenter.Model.CategoriaExperiencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Designacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoriaExperiencias");
                });

            modelBuilder.Entity("WaveCenter.Model.ClientesMarcacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MarcacaoId")
                        .HasColumnType("int");

                    b.Property<int>("NumeroParticipantesUser")
                        .HasColumnType("int");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MarcacaoId");

                    b.HasIndex("UserId");

                    b.ToTable("ClientesMarcacoes");
                });

            modelBuilder.Entity("WaveCenter.Model.Equipamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCategoriaEquipamento")
                        .HasColumnType("int");

                    b.Property<int>("IdPedidoReparacao")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoriaEquipamento");

                    b.ToTable("Equipamentos");
                });

            modelBuilder.Entity("WaveCenter.Model.Experiencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DuracaoMaxima")
                        .HasColumnType("float");

                    b.Property<double>("DuracaoMinima")
                        .HasColumnType("float");

                    b.Property<double>("HoraComecoDia")
                        .HasColumnType("float");

                    b.Property<double>("HoraFimDia")
                        .HasColumnType("float");

                    b.Property<int>("IdCategoriaExperiencia")
                        .HasColumnType("int");

                    b.Property<int>("IdLocal")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoExperiencia")
                        .HasColumnType("int");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroMaximoPessoas")
                        .HasColumnType("int");

                    b.Property<int>("NumeroMinimoPessoas")
                        .HasColumnType("int");

                    b.Property<double>("PrecoHora")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoriaExperiencia");

                    b.HasIndex("IdLocal");

                    b.HasIndex("IdTipoExperiencia");

                    b.ToTable("Experiencias");
                });

            modelBuilder.Entity("WaveCenter.Model.Local", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Coordenadas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locais");
                });

            modelBuilder.Entity("WaveCenter.Model.Marcacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ExperienciaPartilhada")
                        .HasColumnType("bit");

                    b.Property<double>("HoraFim")
                        .HasColumnType("float");

                    b.Property<double>("HoraInicio")
                        .HasColumnType("float");

                    b.Property<int>("IdExperiencia")
                        .HasColumnType("int");

                    b.Property<int>("NumeroParticipantesTotal")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdExperiencia");

                    b.ToTable("Marcacoes");
                });

            modelBuilder.Entity("WaveCenter.Model.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Caminho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeOrigem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tamanho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("WaveCenter.Model.PedidoReparacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataConclusao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("IdEquipamento")
                        .HasColumnType("int");

                    b.Property<string>("Notas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IdEquipamento");

                    b.HasIndex("UserId");

                    b.ToTable("PedidoReparacao");
                });

            modelBuilder.Entity("WaveCenter.Model.TipoExperiencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Designacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoExperiencias");
                });

            modelBuilder.Entity("WaveCenter.Model.TipoUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Designacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoUsers");
                });

            modelBuilder.Entity("WaveCenter.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apelido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("IdMedia")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoUser")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Morada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NIF")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("IdMedia");

                    b.HasIndex("IdTipoUser");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WaveCenter.Model.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Funcionario")
                        .HasColumnType("int");

                    b.Property<float>("LimiteGlobal")
                        .HasColumnType("real");

                    b.Property<int>("LimiteUsoCliente")
                        .HasColumnType("int");

                    b.Property<string>("MarcacaoTotalMin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.Property<float>("ValorPercentagem")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WaveCenter.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WaveCenter.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WaveCenter.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WaveCenter.Model.ClientesMarcacao", b =>
                {
                    b.HasOne("WaveCenter.Model.Marcacao", null)
                        .WithMany("ClientesMarcacoes")
                        .HasForeignKey("MarcacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WaveCenter.Model.User", null)
                        .WithMany("ClientesMarcacoes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WaveCenter.Model.Equipamento", b =>
                {
                    b.HasOne("WaveCenter.Model.CategoriaEquipamento", "CategoriaEquipamento")
                        .WithMany()
                        .HasForeignKey("IdCategoriaEquipamento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaEquipamento");
                });

            modelBuilder.Entity("WaveCenter.Model.Experiencia", b =>
                {
                    b.HasOne("WaveCenter.Model.CategoriaExperiencia", "CategoriaExperiencia")
                        .WithMany()
                        .HasForeignKey("IdCategoriaExperiencia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WaveCenter.Model.Local", "Local")
                        .WithMany()
                        .HasForeignKey("IdLocal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WaveCenter.Model.TipoExperiencia", "TipoExperiencia")
                        .WithMany()
                        .HasForeignKey("IdTipoExperiencia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaExperiencia");

                    b.Navigation("Local");

                    b.Navigation("TipoExperiencia");
                });

            modelBuilder.Entity("WaveCenter.Model.Marcacao", b =>
                {
                    b.HasOne("WaveCenter.Model.Experiencia", "Experiencia")
                        .WithMany()
                        .HasForeignKey("IdExperiencia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experiencia");
                });

            modelBuilder.Entity("WaveCenter.Model.PedidoReparacao", b =>
                {
                    b.HasOne("WaveCenter.Model.Equipamento", "Equipamento")
                        .WithMany()
                        .HasForeignKey("IdEquipamento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WaveCenter.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipamento");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WaveCenter.Model.User", b =>
                {
                    b.HasOne("WaveCenter.Model.Media", "Media")
                        .WithMany()
                        .HasForeignKey("IdMedia");

                    b.HasOne("WaveCenter.Model.TipoUser", "TipoUser")
                        .WithMany()
                        .HasForeignKey("IdTipoUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");

                    b.Navigation("TipoUser");
                });

            modelBuilder.Entity("WaveCenter.Model.Marcacao", b =>
                {
                    b.Navigation("ClientesMarcacoes");
                });

            modelBuilder.Entity("WaveCenter.Model.User", b =>
                {
                    b.Navigation("ClientesMarcacoes");
                });
#pragma warning restore 612, 618
        }
    }
}