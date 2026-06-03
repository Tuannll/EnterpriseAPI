using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NguyenVietTuanAnh0204068De1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoanhNghieps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDoanhNghiep = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaSoThue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanhNghieps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoanhNghiepSanPhams",
                columns: table => new
                {
                    DoanhNghiepId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanhNghiepSanPhams", x => new { x.DoanhNghiepId, x.SanPhamId });
                    table.ForeignKey(
                        name: "FK_DoanhNghiepSanPhams_DoanhNghieps_DoanhNghiepId",
                        column: x => x.DoanhNghiepId,
                        principalTable: "DoanhNghieps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoanhNghiepSanPhams_SanPhams_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_DoanhNghieps_MaSoThue",
                table: "DoanhNghieps",
                column: "MaSoThue",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_DoanhNghieps_TenDoanhNghiep",
                table: "DoanhNghieps",
                column: "TenDoanhNghiep",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoanhNghiepSanPhams_SanPhamId",
                table: "DoanhNghiepSanPhams",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "UQ_SanPhams_MaSanPham",
                table: "SanPhams",
                column: "MaSanPham",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_SanPhams_TenSanPham",
                table: "SanPhams",
                column: "TenSanPham",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoanhNghiepSanPhams");

            migrationBuilder.DropTable(
                name: "DoanhNghieps");

            migrationBuilder.DropTable(
                name: "SanPhams");
        }
    }
}
