using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveCenter.Migrations
{
    /// <inheritdoc />
    public partial class BaseDadosAtualizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaEquipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaEquipamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaExperiencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaExperiencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coordenadas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeOrigem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caminho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamanho = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoExperiencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoExperiencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    ValorPercentagem = table.Column<float>(type: "real", nullable: false),
                    LimiteGlobal = table.Column<float>(type: "real", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcacaoTotalMin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimiteUsoCliente = table.Column<int>(type: "int", nullable: false),
                    Funcionario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCategoriaEquipamento = table.Column<int>(type: "int", nullable: false),
                    IdPedidoReparacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipamentos_CategoriaEquipamentos_IdCategoriaEquipamento",
                        column: x => x.IdCategoriaEquipamento,
                        principalTable: "CategoriaEquipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroMinimoPessoas = table.Column<int>(type: "int", nullable: false),
                    NumeroMaximoPessoas = table.Column<int>(type: "int", nullable: false),
                    DuracaoMinima = table.Column<double>(type: "float", nullable: false),
                    DuracaoMaxima = table.Column<double>(type: "float", nullable: false),
                    HoraComecoDia = table.Column<double>(type: "float", nullable: false),
                    HoraFimDia = table.Column<double>(type: "float", nullable: false),
                    IdTipoExperiencia = table.Column<int>(type: "int", nullable: false),
                    PrecoHora = table.Column<double>(type: "float", nullable: false),
                    IdCategoriaExperiencia = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiencias_CategoriaExperiencias_IdCategoriaExperiencia",
                        column: x => x.IdCategoriaExperiencia,
                        principalTable: "CategoriaExperiencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiencias_Locais_IdLocal",
                        column: x => x.IdLocal,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiencias_TipoExperiencias_IdTipoExperiencia",
                        column: x => x.IdTipoExperiencia,
                        principalTable: "TipoExperiencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NIF = table.Column<int>(type: "int", nullable: false),
                    IdMedia = table.Column<int>(type: "int", nullable: true),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoUser = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Medias_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Medias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TipoUsers_IdTipoUser",
                        column: x => x.IdTipoUser,
                        principalTable: "TipoUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marcacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdExperiencia = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<double>(type: "float", nullable: false),
                    HoraFim = table.Column<double>(type: "float", nullable: false),
                    NumeroParticipantesTotal = table.Column<int>(type: "int", nullable: false),
                    ExperienciaPartilhada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marcacoes_Experiencias_IdExperiencia",
                        column: x => x.IdExperiencia,
                        principalTable: "Experiencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoReparacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEquipamento = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoReparacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoReparacao_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoReparacao_Equipamentos_IdEquipamento",
                        column: x => x.IdEquipamento,
                        principalTable: "Equipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientesMarcacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcacaoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    NumeroParticipantesUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesMarcacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesMarcacoes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientesMarcacoes_Marcacoes_MarcacaoId",
                        column: x => x.MarcacaoId,
                        principalTable: "Marcacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdMedia",
                table: "AspNetUsers",
                column: "IdMedia");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdTipoUser",
                table: "AspNetUsers",
                column: "IdTipoUser");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesMarcacoes_MarcacaoId",
                table: "ClientesMarcacoes",
                column: "MarcacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesMarcacoes_UserId",
                table: "ClientesMarcacoes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_IdCategoriaEquipamento",
                table: "Equipamentos",
                column: "IdCategoriaEquipamento");

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_IdCategoriaExperiencia",
                table: "Experiencias",
                column: "IdCategoriaExperiencia");

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_IdLocal",
                table: "Experiencias",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_IdTipoExperiencia",
                table: "Experiencias",
                column: "IdTipoExperiencia");

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_IdExperiencia",
                table: "Marcacoes",
                column: "IdExperiencia");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoReparacao_IdEquipamento",
                table: "PedidoReparacao",
                column: "IdEquipamento");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoReparacao_UserId",
                table: "PedidoReparacao",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClientesMarcacoes");

            migrationBuilder.DropTable(
                name: "PedidoReparacao");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Marcacoes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Experiencias");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "TipoUsers");

            migrationBuilder.DropTable(
                name: "CategoriaEquipamentos");

            migrationBuilder.DropTable(
                name: "CategoriaExperiencias");

            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropTable(
                name: "TipoExperiencias");
        }
    }
}
