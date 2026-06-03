# NguyenVietTuanAnh0204068De1

> **Mon hoc:** Phat trien Ung dung Phia Server  
> **Sinh vien:** Nguyen Viet Tuan Anh — MSSV: 0204068 — De so: 1  
> **Framework:** ASP.NET Core Web API · EF Core · SQL Server · .NET 10

---

## Yeu cau he thong

| Cong cu | Phien ban toi thieu |
|---|---|
| .NET SDK | 10.0 |
| SQL Server | 2019+ (hoac LocalDB) |
| dotnet-ef (CLI) | 10.0 |

---

## Cai dat & Chay

### 1. Di chuyen vao thu muc du an

```bash
cd NguyenVietTuanAnh0204068De1
```

### 2. Cau hinh Connection String

Mo `appsettings.json`, sua `DefaultConnection` cho phu hop:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=NguyenVietTuanAnh0204068De1Db;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
}
```

### 3. Cai EF Core CLI (neu chua co)

```bash
dotnet tool install --global dotnet-ef
```

### 4. Tao Migration & Cap nhat CSDL

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Chay ung dung

```bash
dotnet run
```

Server khoi dong tai: `http://localhost:5000`

> **Swagger UI:** `http://localhost:5000/swagger` (ASPNETCORE_ENVIRONMENT=Development)

---

## Cau truc thu muc

```
NguyenVietTuanAnh0204068De1/
├── Constants/
│   └── ErrorMessages0204068De1.cs          # Thong bao loi nghiep vu
├── Controllers/
│   └── DoanhNghiepController0204068De1.cs   # API endpoints doanh nghiep
├── DbContexts/
│   └── AppDbContext0204068De1.cs           # DbContext cau hinh Fluent API
├── Dtos/
│   ├── Common/
│   │   └── PageResultDto0204068De1.cs      # Wrapper phan trang
│   ├── DoanhNghiep/
│   │   ├── TaoDoanhNghiepDto0204068De1.cs
│   │   ├── SuaDoanhNghiepDto0204068De1.cs
│   │   ├── DoanhNghiepResponseDto0204068De1.cs
│   │   └── LocDoanhNghiepDto0204068De1.cs
│   └── SanPham/
│       └── SanPhamResponseDto0204068De1.cs
├── Entities/
│   ├── DoanhNghiep0204068De1.cs             # Entity DoanhNghiep
│   ├── SanPham0204068De1.cs                # Entity SanPham
│   └── DoanhNghiepSanPham0204068De1.cs      # Entity trung gian n-n + SoLuong
├── Exceptions/
│   └── UserFriendlyException0204068De1.cs  # Custom Exception nghiep vu
├── Middleware/
│   └── GlobalExceptionMiddleware0204068De1.cs  # Middleware bat loi toan cuc
├── Migrations/                             # EF Migrations
├── Services/
│   ├── Interfaces/
│   │   └── IDoanhNghiepService0204068De1.cs
│   └── Implementations/
│       └── DoanhNghiepService0204068De1.cs
├── Utils/
│   └── StringHelper0204068De1.cs           # Tien ich chuoi
├── Properties/launchSettings.json
├── appsettings.json
├── Program.cs
├── CLO_MAPPING_REPORT.md
└── BACKEND_THINKING_PROCESS.md
```

---

## Mo hinh du lieu

```
DoanhNghieps         DoanhNghiepSanPhams       SanPhams
─────────────        ───────────────────       ────────────────
Id (PK, int)    ◄──  DoanhNghiepId (FK)        Id (PK, int)
TenDoanhNghiep       SanPhamId (FK)     ────►  TenSanPham
MaSoThue             SoLuong (int)             MaSanPham
DiaChi                                         NgayNhap
```

---

## API Endpoints

Base URL: `http://localhost:5000/api/doanhnghiep`

### Them doanh nghiep
```http
POST /api/doanhnghiep
Content-Type: application/json

{
  "tenDoanhNghiep": "Cong ty TNHH ABC",
  "maSoThue": "0123456789",
  "diaChi": "123 Nguyen Hue, HCM"
}
```

### Sua doanh nghiep
```http
PUT /api/doanhnghiep/{id}
Content-Type: application/json

{
  "tenDoanhNghiep": "Cong ty TNHH ABC Cap nhat",
  "maSoThue": "0123456789",
  "diaChi": "456 Le Loi, HCM"
}
```

### Xoa doanh nghiep
```http
DELETE /api/doanhnghiep/{id}
```

### Xem danh sach doanh nghiep (Co phan trang & loc)
```http
GET /api/doanhnghiep?pageIndex=1&pageSize=10&keyword=ABC
```

### Top san pham nhap nhieu nhat
```http
GET /api/doanhnghiep/{id}/sanpham-nhap-nhieu-nhat