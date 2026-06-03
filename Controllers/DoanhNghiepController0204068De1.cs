using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NguyenVietTuanAnh0204068De1.Dtos.DoanhNghiep;
using NguyenVietTuanAnh0204068De1.Services.Interfaces;

namespace NguyenVietTuanAnh0204068De1.Controllers
{
    [ApiController]
    [Route("api/doanhnghiep")]
    public class DoanhNghiepController0204068De1 : ControllerBase
    {
        private readonly IDoanhNghiepService0204068De1 _doanhNghiepService;

        public DoanhNghiepController0204068De1(IDoanhNghiepService0204068De1 doanhNghiepService)
        {
            _doanhNghiepService = doanhNghiepService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaoDoanhNghiepDto0204068De1 dto)
        {
            var result = await _doanhNghiepService.CreateAsync(dto);
            return StatusCode(201, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SuaDoanhNghiepDto0204068De1 dto)
        {
            var result = await _doanhNghiepService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _doanhNghiepService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedList([FromQuery] LocDoanhNghiepDto0204068De1 filterDto)
        {
            var result = await _doanhNghiepService.GetPagedListAsync(filterDto);
            return Ok(result);
        }

        [HttpGet("{id}/sanpham-nhap-nhieu-nhat")]
        public async Task<IActionResult> GetTopImportedProducts([FromRoute] int id)
        {
            var result = await _doanhNghiepService.GetTopImportedProductsAsync(id);
            return Ok(result);
        }
    }
}