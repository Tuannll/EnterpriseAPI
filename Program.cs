using Microsoft.EntityFrameworkCore;
using NguyenVietTuanAnh0204068De1.DbContexts;
using NguyenVietTuanAnh0204068De1.Middleware;
using NguyenVietTuanAnh0204068De1.Services.Implementations;
using NguyenVietTuanAnh0204068De1.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext0204068De1>(options =>
    options.UseSqlite("Data Source=test.db")
);

builder.Services.AddScoped<IDoanhNghiepService0204068De1, DoanhNghiepService0204068De1>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext0204068De1>();
    context.Database.EnsureCreated();
    if (!context.DoanhNghieps.Any())
    {
        var dn1 = new NguyenVietTuanAnh0204068De1.Entities.DoanhNghiep0204068De1 { TenDoanhNghiep = "Cong ty A", MaSoThue = "0123456789", DiaChi = "Ha Noi" };
        var dn2 = new NguyenVietTuanAnh0204068De1.Entities.DoanhNghiep0204068De1 { TenDoanhNghiep = "Cong ty B", MaSoThue = "0987654321", DiaChi = "TP HCM" };
        context.DoanhNghieps.AddRange(dn1, dn2);
        context.SaveChanges();

        var sp1 = new NguyenVietTuanAnh0204068De1.Entities.SanPham0204068De1 { TenSanPham = "Laptop Dell", MaSanPham = "SP001", NgayNhap = DateTime.Now };
        var sp2 = new NguyenVietTuanAnh0204068De1.Entities.SanPham0204068De1 { TenSanPham = "Ban phim co", MaSanPham = "SP002", NgayNhap = DateTime.Now };
        context.SanPhams.AddRange(sp1, sp2);
        context.SaveChanges();

        context.DoanhNghiepSanPhams.AddRange(
            new NguyenVietTuanAnh0204068De1.Entities.DoanhNghiepSanPham0204068De1 { DoanhNghiepId = dn1.Id, SanPhamId = sp1.Id, SoLuong = 100 },
            new NguyenVietTuanAnh0204068De1.Entities.DoanhNghiepSanPham0204068De1 { DoanhNghiepId = dn1.Id, SanPhamId = sp2.Id, SoLuong = 50 },
            new NguyenVietTuanAnh0204068De1.Entities.DoanhNghiepSanPham0204068De1 { DoanhNghiepId = dn2.Id, SanPhamId = sp1.Id, SoLuong = 10 },
            new NguyenVietTuanAnh0204068De1.Entities.DoanhNghiepSanPham0204068De1 { DoanhNghiepId = dn2.Id, SanPhamId = sp2.Id, SoLuong = 200 }
        );
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "NguyenVietTuanAnh0204068De1 API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<GlobalExceptionMiddleware0204068De1>();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.Run();