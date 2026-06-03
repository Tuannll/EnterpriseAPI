using System.Collections.Generic;
using System.Threading.Tasks;
using NguyenVietTuanAnh0204068De1.Dtos.Common;
using NguyenVietTuanAnh0204068De1.Dtos.DoanhNghiep;
using NguyenVietTuanAnh0204068De1.Dtos.SanPham;

namespace NguyenVietTuanAnh0204068De1.Services.Interfaces
{
    public interface IDoanhNghiepService0204068De1
    {
        Task<DoanhNghiepResponseDto0204068De1> CreateAsync(TaoDoanhNghiepDto0204068De1 dto);
        Task<DoanhNghiepResponseDto0204068De1> UpdateAsync(int id, SuaDoanhNghiepDto0204068De1 dto);
        Task DeleteAsync(int id);
        Task<PageResultDto0204068De1<DoanhNghiepResponseDto0204068De1>> GetPagedListAsync(LocDoanhNghiepDto0204068De1 filterDto);
        Task<List<SanPhamResponseDto0204068De1>> GetTopImportedProductsAsync(int doanhNghiepId);
    }
}