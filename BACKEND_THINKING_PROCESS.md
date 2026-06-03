# HUONG DAN TU DUY BACKEND

> Tai lieu giai thich logic thiet ke he thong

---

## 1. THIET KE CO SO DU LIEU

### 1.1 Quan he Many-to-Many (n-n)
- Doanh nghiep va San pham co quan he n-n.
- Bang trung gian `DoanhNghiepSanPhams` duoc tao ra de giai quyet quan he nay va luu tru them thong tin phu `SoLuong` (So luong nhap san pham cua tung doanh nghiep).
- Khoa chinh cua bang trung gian la composite key: `(DoanhNghiepId, SanPhamId)`.

### 1.2 UNIQUE Constraint o muc CSDL
- Rang buoc UNIQUE o muc database de dam bao tinh toan ven du lieu (TenDoanhNghiep, MaSoThue khong duoc trung).
- Neu chi check bang code C# (dung AnyAsync), van co nguy co xay ra Race Condition khi co nhieu request gui den cung luc. Rangan buoc o database la chot chan cuoi cung.

---

## 2. KIEU MAU THIET KE (ARCHITECTURAL PATTERNS)

### 2.1 Separation of Concerns (SoC)
Du an chia lam 3 tang ro ret:
- **Controllers:** Chi nhan request, dieu huong va tra ve ket qua HTTP (thin controllers).
- **Services:** Xu ly toan bo logic nghiep vu (validate, check trung, mappings).
- **Entities/DbContext:** Tuong tac truc tiep voi CSDL qua EF Core.

### 2.2 Dependency Injection (DI)
- Dang ky interface -> implementation voi lifetime **Scoped** trong `Program.cs`.
- Moi HTTP Request se co mot instance rieng biet de dam bao an toan luong (thread safety).

---

## 3. TOI UU HIEU NANG (PERFORMANCE PITFALLS)

### 3.1 IEnumerable vs IQueryable (Phan trang o DB level)
- Tranh dung `IEnumerable` vi no se tai toan bo du lieu tu DB len RAM roi moi Skip/Take.
- Dung `IQueryable` de EF Core bien dich Skip/Take thanh cau lenh SQL `OFFSET/FETCH` va chay truc tiep tren SQL Server. RAM chi chua dung PageSize records.

### 3.2 Giai quyet bai toan N+1 Query
- Tranh dung vong lap `foreach` de select tung chi tiet cua navigation property (gay ra N+1 queries).
- Su dung `.Select()` projection de EF Core sinh cau lenh `INNER JOIN` trong SQL duy nhat, lay dung truong mong muon.