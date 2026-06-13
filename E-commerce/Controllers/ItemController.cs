using DataBase.DBcon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Mappers.ItemMapper;
using E_commerce.Repositry.ItemRepspsitry;
using E_commerce.Interface.Reposotiry;
using E_commerce.DTO.ItemDto;
using E_commerce.Model;
using E_commerce.Mapper.ItemMapper;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItem _itemRepo;

        public ItemController(IItem itemRepo)
        {
            _itemRepo = itemRepo;
        }

        [HttpGet("getItem")]
        public async Task<IActionResult> GetItem()
        {
            return Ok(await _itemRepo.getItemDtoAsync());
        }

        [HttpPost("addItem")]
        public async Task<IActionResult> AddIUtem([FromBody] AddItemDto item)
        {
            var result=await _itemRepo.addItemAsync(item);
            if(result.Item==null)
            {
                return BadRequest(result.Massage);
            }
            return Ok(new
            {
                Massage = result.Massage,
                Item = result.Item.getItem()
            });
        }

        [HttpDelete("deletItem")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result=await _itemRepo.deleteItemAsync(id);
            if(!result.IsSuccess)
            {
                return BadRequest(result.Massage);
            }
            return Ok(new
            {
                Massage = result.Massage 
            });
        }

        [HttpPut("updateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemDto item, int id)
        {
            var result=await _itemRepo.updateItemAsync(item, id);
            if(result.Item==null)
            {
                return BadRequest(result.Massage);
            }
            else
            {
                return Ok(new
                {
                    Massage= result.Massage,
                    Item=result.Item
                });
            }
        }

        [HttpGet("getItemPhotoByItemId")]
        public async Task<IActionResult> GetItemPhotoByItemId(int id)
        {
            var photos = await _itemRepo.getItemPhotoByItemId(id);
            if (photos == null)
            {
                return NotFound(new { message = "العنصر الذي أدخلته غير موجود" });
            }
            return Ok(photos);
        }

        [HttpGet("getItemSizeByItemId")]
        public async Task<IActionResult> getItemSizeByItemId(int id)
        {
            var item = await _itemRepo.getItemSizeByItemId(id);
            if (item == null)
            {
                return NotFound(new { message = "العنصر الذي أدخلته غير موجود" });
            }
            return Ok(item);
        }

        [HttpGet("getItemPhotoOrSizeByItemId")]
        public async Task<IActionResult> getItemPhotoOrSizeByItemId(int id)
        {
            var item = await _itemRepo.getItemPhotoOrSizeByItemId(id);
            if (item == null)
            {
                return NotFound(new { message = "العنصر الذي أدخلته غير موجود" });
            }
            return Ok(item);
        }
        [HttpGet("getItemByCategoryid/{id}")]
        public IActionResult getItemByCategroyId(int id)
        {
            return Ok(_itemRepo.getItemByCategroyId(id));
        }
        [HttpGet("getItemByChildCategoryid/{id}")]

        public IActionResult getItemByChildCategroyId(int id)
        {
            return Ok(_itemRepo.getItemByChildCategroyId(id));

        }

    }
}








