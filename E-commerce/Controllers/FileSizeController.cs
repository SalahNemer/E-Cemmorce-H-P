using E_commerce.DTO.FileSizeDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Interface.Serves;
using E_commerce.Mapper.FileSizeMapper;
using E_commerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileSizeController : ControllerBase
    {
        private readonly IFileSize _repo;
        private readonly IFileStorageServicePDF _storage;

        public FileSizeController(IFileSize repo, IFileStorageServicePDF storage)
        {
            _repo = repo;
            _storage = storage;
        }

        [HttpGet("getFileSize")]
        public async Task<IActionResult> GetFileSize()
        {
            return Ok(await _repo.GetFileSize());
        }

        [RequestSizeLimit(100_000_000)] // 100 MB
        [HttpPost("addFileSize")]
        public async Task<IActionResult> AddFileSize([FromForm] AddFileSizeDto dto)
        {
            if (!await _repo.ItemExistsAsync(dto.itemSizeId))
                return NotFound("\"النمرة الذي تحول اضافة ملفات لها غير موجودة");

            if (dto.urlFile == null || !dto.urlFile.Any())
                return BadRequest("لم يتم اضافة اي ملف");

            var uploadedFiles = new List<UploadedFileDto>();


            var allowedExtensions = new[]
            {
                ".jpg", ".jpeg", ".png", ".webp", ".gif", ".bmp", ".tiff", ".svg",

                ".pdf",

                ".doc", ".docx",

                ".zip", ".rar", ".7z", ".tar", ".gz"
            };


            for (int i = 0; i < dto.urlFile.Count; i++)
            {
                var file = dto.urlFile[i];

                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    return BadRequest(new
                    {
                        Message = "امتداد الصورة غير صحيح يسمح باضافة صورة بالامتداات التلية فقط jpg jpeg png webp",
                        PhotoIndex = i + 1,
                        FileName = file.FileName,
                        AllowedExtensions = allowedExtensions
                    });
            }

            foreach (var File in dto.urlFile)
            {
                var originalFileName = Path.GetFileName(File.FileName);
                var storedFileName = await _storage.UploadAsync(File, "items");

                var entity = new FileSize
                {
                    FileName = originalFileName,
                    urlFile = storedFileName,
                    itemSizeId = dto.itemSizeId
                };


                await _repo.AddFileSize(entity);

                uploadedFiles.Add(new UploadedFileDto
                {
                    FileName = originalFileName,
                    Extension = Path.GetExtension(originalFileName),
                });
            }


            return Ok(new
            {
                Message = "تمت الاضافة بنجاح",
                FileName = uploadedFiles
            });
        }

        [HttpDelete("deleteFileSize")]
        public async Task<IActionResult> DeleteItemSize(int id)
        {
            var result = await _repo.deleteFileSizeAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Massage);
            }
            return Ok(new
            {
                Massage = result.Massage
            });
        }

        [HttpPut("{id}updateFileSize")]
        public async Task<IActionResult> UpdateFileSize(int id, [FromForm] UpdateFileSizeDto dto)
        {
            var result = await _repo.updateFileSize(dto, id);

            if (result.item == null)
                return BadRequest(new { message = result.massage });

            return Ok(new
            {
                message = result.massage,
                data = result.item
            });
        }
    }
}
