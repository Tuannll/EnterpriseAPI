# BAO CAO DOI CHIEU CLO

> **Mon hoc:** Phat trien Ung dung Phia Server  
> **Sinh vien:** Nguyen Viet Tuan Anh — MSSV: 0204068 — De so: 1  
> **Framework:** ASP.NET Core Web API · EF Core · SQL Server

---

## CLO 1.1 — Cau truc Entity & Migration

### Minh chung — File: `Entities/DoanhNghiep0204068De1.cs`
```csharp
public class DoanhNghiep0204068De1
{
    public int Id { get; set; }
    public string TenDoanhNghiep { get; set; } = string.Empty;
    public string MaSoThue { get; set; } = string.Empty;
    public string? DiaChi { get; set; }
    public ICollection<DoanhNghiepSanPham0204068De1> DoanhNghiepSanPhams { get; set; }
        = new List<DoanhNghiepSanPham0204068De1>();
}
```

### Minh chung — File: `DbContexts/AppDbContext0204068De1.cs`
```csharp
modelBuilder.Entity<DoanhNghiep0204068De1>(entity =>
{
    entity.ToTable("DoanhNghieps");
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).ValueGeneratedOnAdd();
    entity.Property(e => e.TenDoanhNghiep).IsRequired().HasMaxLength(200);
    entity.HasIndex(e => e.TenDoanhNghiep).IsUnique().HasDatabaseName("UQ_DoanhNghieps_TenDoanhNghiep");
    entity.Property(e => e.MaSoThue).IsRequired().HasMaxLength(20);
    entity.HasIndex(e => e.MaSoThue).IsUnique().HasDatabaseName("UQ_DoanhNghieps_MaSoThue");
    entity.Property(e => e.DiaChi).HasMaxLength(500);
});
```

---

## CLO 1.2 — Boc tach nghiep vu quan he n-n

### Minh chung — File: `Entities/DoanhNghiepSanPham0204068De1.cs`
```csharp
public class DoanhNghiepSanPham0204068De1
{
    public int DoanhNghiepId { get; set; }
    public int SanPhamId { get; set; }
    public int SoLuong { get; set; }

    public DoanhNghiep0204068De1 DoanhNghiep { get; set; } = null!;
    public SanPham0204068De1 SanPham { get; set; } = null!;
}
```

### Minh chung — File: `DbContexts/AppDbContext0204068De1.cs`
```csharp
modelBuilder.Entity<DoanhNghiepSanPham0204068De1>(entity =>
{
    entity.ToTable("DoanhNghiepSanPhams");
    entity.HasKey(ep => new { ep.DoanhNghiepId, ep.SanPhamId });

    entity.HasOne(ep => ep.DoanhNghiep)
        .WithMany(e => e.DoanhNghiepSanPhams)
        .HasForeignKey(ep => ep.DoanhNghiepId)
        .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(ep => ep.SanPham)
        .WithMany(p => p.DoanhNghiepSanPhams)
        .HasForeignKey(ep => ep.SanPhamId)
        .OnDelete(DeleteBehavior.Restrict);

    entity.Property(ep => ep.SoLuong).IsRequired().HasDefaultValue(0);
});
```

---

## CLO 2.1 — Cau truc ma nguon & Dependency Injection

### Minh chung — DI Registration trong `Program.cs`
```csharp
builder.Services.AddScoped<IDoanhNghiepService0204068De1, DoanhNghiepService0204068De1>();
```

### Minh chung — Inject vao Controller trong `Controllers/DoanhNghiepController0204068De1.cs`
```csharp
public class DoanhNghiepController0204068De1 : ControllerBase
{
    private readonly IDoanhNghiepService0204068De1 _doanhNghiepService;

    public DoanhNghiepController0204068De1(IDoanhNghiepService0204068De1 doanhNghiepService)
    {
        _doanhNghiepService = doanhNghiepService;
    }
}
```

---

## CLO 2.2 — Chuc nang CRUD & Toan ven du lieu

### Minh chung — Auto-Trim trong `Dtos/DoanhNghiep/TaoDoanhNghiepDto0204068De1.cs`
```csharp
public class TaoDoanhNghiepDto0204068De1
{
    private string _tenDoanhNghiep = string.Empty;
    public string TenDoanhNghiep
    {
        get => _tenDoanhNghiep;
        set => _tenDoanhNghiep = value?.Trim() ?? string.Empty;
    }
}
```

### Minh chung — Xoa an toan va Check trung trong `Services/Implementations/DoanhNghiepService0204068De1.cs`
```csharp
var tenExists = await _dbContext.DoanhNghieps.AnyAsync(e => e.TenDoanhNghiep == dto.TenDoanhNghiep);
if (tenExists) throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseNameDuplicated, 409);

// Xoa an toan
var hasRelatedProducts = await _dbContext.DoanhNghiepSanPhams.AnyAsync(ep => ep.DoanhNghiepId == id);
if (hasRelatedProducts) throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseHasRelatedData, 400);
```

---

## CLO 2.3 — Xu ly ngoai le

### Minh chung — Middleware `Middleware/GlobalExceptionMiddleware0204068De1.cs`
```csharp
public async Task InvokeAsync(HttpContext context)
{
    try
    {
        await _next(context);
    }
    catch (UserFriendlyException0204068De1 ex)
    {
        await WriteErrorResponseAsync(context, ex.StatusCode, ex.Message);
    }
}
```

---

## CLO 3.1 — Truv van phan trang & Tim kiem

### Minh chung — IQueryable Skip/Take trong `Services/Implementations/DoanhNghiepService0204068De1.cs`
```csharp
IQueryable<DoanhNghiep0204068De1> query = _dbContext.DoanhNghieps.AsQueryable();
if (!string.IsNullOrWhiteSpace(filterDto.Keyword))
{
    var keyword = filterDto.Keyword;
    query = query.Where(e => e.TenDoanhNghiep.Contains(keyword) || e.MaSoThue.Contains(keyword));
}
var totalCount = await query.CountAsync();
var items = await query
    .OrderBy(e => e.Id)
    .Skip((filterDto.PageIndex - 1) * filterDto.PageSize)
    .Take(filterDto.PageSize)
    .ToListAsync();
```

---

## CLO 3.2 — Truv van thong ke & Toi uu N+1

### Minh chung — Select Projection trong `Services/Implementations/DoanhNghiepService0204068De1.cs`
```csharp
var products = await _dbContext.DoanhNghiepSanPhams
    .Where(ep => ep.DoanhNghiepId == doanhNghiepId)
    .OrderByDescending(ep => ep.SoLuong)
    .Select(ep => new SanPhamResponseDto0204068De1
    {
        TenSanPham = ep.SanPham.TenSanPham,
        MaSanPham = ep.SanPham.MaSanPham
    })
    .ToListAsync();