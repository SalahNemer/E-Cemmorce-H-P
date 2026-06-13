using DataBase.DBcon;
using E_commerce.DTO.ItemPhotoDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Interface.Serves;
using E_commerce.Mapper.ItemPhotoMapper;
using E_commerce.Model;
using E_commerce.Repositry.ItemPhotoRepsitry;
using E_commerce.Repositry.ItemRepspsitry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPhotoController : ControllerBase
    {
        private readonly IItemPhoto _repo;
        private readonly IFileStorageService _storage;
        public ItemPhotoController(IItemPhoto repo, IFileStorageService storage)
        {
            _repo = repo;
            _storage = storage;
        }

        [HttpGet("getItemPhoto")]
        public async Task<IActionResult> GetItemPhoto()
        {
            return Ok(await _repo.getItemPhoto());
        }

        [HttpPost("addItemPhoto")]
        public async Task<IActionResult> AddItemPhoto([FromForm] AddItemPhotoDto dto)
        {
            if (!await _repo.ItemExistsAsync(dto.itmeId))
                return NotFound("العنصر الذي تحول اضافة صور له غير موجود");

            if (dto.urlPhoto == null || !dto.urlPhoto.Any())
                return BadRequest("لم يتم اضافة اي صورة");

            var uploadedPhotos = new List<string>();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            for (int i = 0; i < dto.urlPhoto.Count; i++)
            {
                var photo = dto.urlPhoto[i];

                if (!photo.ContentType.StartsWith("image/"))
                    return BadRequest(new
                    {
                        Message = "يُسمح فقط باضافة صور\r\n",
                        PhotoIndex = i + 1,
                        FileName = photo.FileName
                    });
                var extension = Path.GetExtension(photo.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    return BadRequest(new
                    {
                        Message = "امتداد الصورة غير صحيح يسمح باضافة صورة بالامتداات التلية فقط jpg jpeg png webp",
                        PhotoIndex = i + 1,
                        FileName = photo.FileName,
                        AllowedExtensions = allowedExtensions
                    });
            }

            foreach (var photo in dto.urlPhoto)
            {
                var fileName = await _storage.UploadAsync(photo, "items");

                var entity = AddItemPhotoMapper.addItemPhoto(dto.itmeId, fileName);

                await _repo.AddAsync(entity);

                uploadedPhotos.Add(fileName);
            }

            return Ok(new
            {
                Message = "Photo uploaded successfully",
                FileName = uploadedPhotos
            });
        }

        [HttpPut("{id}updateItemPhoto")]
        public async Task<IActionResult> UpdateItemPhoto(int id,[FromForm] UpdateItemPhotoDto dto)
        {
            var result = await _repo.updateItem(dto, id);

            if (result.item == null)
                return BadRequest(new { message = result.massage });

            return Ok(new
            {
                message = result.massage,
                data = result.item
            });
        }

        [HttpDelete("deleteItemPhoto")]
        public async Task<IActionResult> DeleteItemPhoto(int id)
        {
            var result = await _repo.deleteItemAsync(id);
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