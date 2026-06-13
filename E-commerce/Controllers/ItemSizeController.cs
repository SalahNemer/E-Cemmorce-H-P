using E_commerce.DTO.ItemPhotoDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Interface.Serves;
using E_commerce.Mapper.ItemPhotoMapper;
using E_commerce.Mapper.ItemSizeMapper;
using E_commerce.Repositry.ItemRepspsitry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemSizeController : ControllerBase
    {
        private readonly IItemSize _repo;
        private readonly IFileStorageServicePDF _storage;
        public ItemSizeController(IItemSize repo, IFileStorageServicePDF storage)
        {
            _repo = repo;
            _storage = storage;
        }

        [HttpGet("getItemSize")]
        public async Task<IActionResult> GetItemSize()
        {
            return Ok(await _repo.getItemSize());
        }
        [HttpGet("getItemByItemSizeId")]
        public async Task<IActionResult> getItemByItemSizeId(int id)
        {
            var itemSize = await _repo.getItemByItemSizeId(id);
            if (itemSize == null)
            {
                return NotFound(new { message = "العنصر الذي أدخلته غير موجود" });
            }
            return Ok(itemSize);
        }

        [HttpPost("addItemSize")]
        public async Task<IActionResult> AddItemSize([FromBody] AddItemSizeDto dto)
        {
            var size =await _repo.AddItemSizeAsync(dto);
            return Ok(size);
        }

        [HttpPut("{id}updateItemSize")]
        public async Task<IActionResult> UpdateItemSize(int id, [FromBody] UpdateItemSizeDto dto)
        {
            var result = await _repo.updateItemSize(dto, id);

            if (result.item == null)
                return BadRequest(new { message = result.massage });

            return Ok(new
            {
                message = result.massage,
                data = result.item
            });
        }

        [HttpDelete("deleteItemSize")]
        public async Task<IActionResult> DeleteItemSize(int id)
        {
            var result = await _repo.deleteItemSizeAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Massage);
            }
            return Ok(new
            {
                Massage = result.Massage
            });
        }
    }
}
