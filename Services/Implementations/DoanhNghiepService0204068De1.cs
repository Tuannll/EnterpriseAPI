using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NguyenVietTuanAnh0204068De1.Constants;
using NguyenVietTuanAnh0204068De1.DbContexts;
using NguyenVietTuanAnh0204068De1.Dtos.Common;
using NguyenVietTuanAnh0204068De1.Dtos.DoanhNghiep;
using NguyenVietTuanAnh0204068De1.Dtos.SanPham;
using NguyenVietTuanAnh0204068De1.Entities;
using NguyenVietTuanAnh0204068De1.Exceptions;
using NguyenVietTuanAnh0204068De1.Services.Interfaces;

namespace NguyenVietTuanAnh0204068De1.Services.Implementations
{
    public class DoanhNghiepService0204068De1 : IDoanhNghiepService0204068De1
    {
        private readonly AppDbContext0204068De1 _dbContext;

        public DoanhNghiepService0204068De1(AppDbContext0204068De1 dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DoanhNghiepResponseDto0204068De1> CreateAsync(TaoDoanhNghiepDto0204068De1 dto)
        {
            var tenExists = await _dbContext.DoanhNghieps.AnyAsync(e => e.TenDoanhNghiep == dto.TenDoanhNghiep);
            if (tenExists)
            {
                throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseNameDuplicated, 409);
            }

            var mstExists = await _dbContext.DoanhNghieps.AnyAsync(e => e.MaSoThue == dto.MaSoThue);
            if (mstExists)
            {
                throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseTaxCodeDuplicated, 409);
            }

            var doanhNghiep = new DoanhNghiep0204068De1
            {
                TenDoanhNghiep = dto.TenDoanhNghiep,
                MaSoThue = dto.MaSoThue,
                DiaChi = dto.DiaChi
            };

            _dbContext.DoanhNghieps.Add(doanhNghiep);
            await _dbContext.SaveChangesAsync();

            return MapToResponseDto(doanhNghiep);
        }

        public async Task<DoanhNghiepResponseDto0204068De1> UpdateAsync(int id, SuaDoanhNghiepDto0204068De1 dto)
        {
            var doanhNghiep = await _dbContext.DoanhNghieps.FindAsync(id);
            if (doanhNghiep == null)
            {
                throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseNotFound, 404);
            }

            var tenExists = await _dbContext.DoanhNghieps.AnyAsync(e => e.TenDoanhNghiep == dto.TenDoanhNghiep && e.Id != id);
            if (tenExists)
            {
                throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseNameDuplicated, 409);
            }

            var mstExists = await _dbContext.DoanhNghieps.AnyAsync(e => e.MaSoThue == dto.MaSoThue && e.Id != id);
            if (mstExists)
            {
                throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseTaxCodeDuplicated, 409);
            }

            doanhNghiep.TenDoanhNghiep = dto.TenDoanhNghiep;
            doanhNghiep.MaSoThue = dto.MaSoThue;
            doanhNghiep.DiaChi = dto.DiaChi;

            await _dbContext.SaveChangesAsync();

            return MapToResponseDto(doanhNghiep);
        }

        public async Task DeleteAsync(int id)
        {
            var doanhNghiep = await _dbContext.DoanhNghieps.FindAsync(id);
            if (doanhNghiep == null)
            {
                throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseNotFound, 404);
            }

            var hasRelatedProducts = await _dbContext.DoanhNghiepSanPhams.AnyAsync(ep => ep.DoanhNghiepId == id);
            if (hasRelatedProducts)
            {
                throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseHasRelatedData, 400);
            }

            _dbContext.DoanhNghieps.Remove(doanhNghiep);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResultDto0204068De1<DoanhNghiepResponseDto0204068De1>> GetPagedListAsync(LocDoanhNghiepDto0204068De1 filterDto)
        {
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
                .Select(e => new DoanhNghiepResponseDto0204068De1
                {
                    Id = e.Id,
                    TenDoanhNghiep = e.TenDoanhNghiep,
                    MaSoThue = e.MaSoThue,
                    DiaChi = e.DiaChi
                })
                .ToListAsync();

            return new PageResultDto0204068De1<DoanhNghiepResponseDto0204068De1>
            {
                TotalCount = totalCount,
                PageIndex = filterDto.PageIndex,
                PageSize = filterDto.PageSize,
                Items = items
            };
        }

        public async Task<List<SanPhamResponseDto0204068De1>> GetTopImportedProductsAsync(int doanhNghiepId)
        {
            var doanhNghiepExists = await _dbContext.DoanhNghieps.AnyAsync(e => e.Id == doanhNghiepId);
            if (!doanhNghiepExists)
            {
                throw new UserFriendlyException0204068De1(ErrorMessages0204068De1.EnterpriseNotFound, 404);
            }

            var products = await _dbContext.DoanhNghiepSanPhams
                .Where(ep => ep.DoanhNghiepId == doanhNghiepId)
                .OrderByDescending(ep => ep.SoLuong)
                .Select(ep => new SanPhamResponseDto0204068De1
                {
                    TenSanPham = ep.SanPham.TenSanPham,
                    MaSanPham = ep.SanPham.MaSanPham
                })
                .ToListAsync();

            return products;
        }

        private static DoanhNghiepResponseDto0204068De1 MapToResponseDto(DoanhNghiep0204068De1 entity)
        {
            return new DoanhNghiepResponseDto0204068De1
            {
                Id = entity.Id,
                TenDoanhNghiep = entity.TenDoanhNghiep,
                MaSoThue = entity.MaSoThue,
                DiaChi = entity.DiaChi
            };
        }
    }
}