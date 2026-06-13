using DataBase.DBcon;
using E_commerce.DTO.CartUserDto;
using E_commerce.DTO.OrderHistory;
using E_commerce.Interface.Reposotiry;
using E_commerce.Interface.Serves;
using E_commerce.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {
        private readonly IOrderHistory _OrderHistory;
        private readonly IFileStorageServicePDF _fileStorageService;

        private readonly DBC _db;
        public OrderHistoryController(IOrderHistory OrderHistory, DBC db, IFileStorageServicePDF IFileStorageServicePDF)
        {
            _OrderHistory = OrderHistory;
            _db = db;
            _fileStorageService = IFileStorageServicePDF;
        }

        [HttpGet("download/itemSize/{itemSizeId}")]
        [Authorize]
        public async Task<IActionResult> DownloadFilesByItemSize(int itemSizeId)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            // 1️⃣ تحقق إنو الزبون اشترى النمرة
            var hasAccess = await _OrderHistory
                .UserBoughtItemSize(userId, itemSizeId);

            if (!hasAccess)
                return Forbid("لم تقم بشراء هذه النمرة");

            // 2️⃣ جلب الملفات المرتبطة بالنمرة
            var files = await _db.fileSizes
                .Where(f => f.itemSizeId == itemSizeId)
                .ToListAsync();

            if (!files.Any())
                return NotFound("لا يوجد ملفات لهذه النمرة");

            // 3️⃣ توليد روابط تحميل مباشرة
            var result = files.Select(f => new
            {
                fileId = f.id,
                fileName = f.FileName,
                downloadUrl = _fileStorageService
                    .GenerateSignedUrlDownload(f.urlFile)
            });

            return Ok(result);
        }


        //view and download pdf
        [HttpGet("downloadAndView/itemSize/{itemSizeId}")]
        public async Task<IActionResult> DownloadAndViewFilesByItemSize(int itemSizeId)
        {
            // 1️⃣ تأكد إن النمرة موجودة
            var itemSizeExists = await _db.itemsSize
                .AnyAsync(i => i.id == itemSizeId);

            if (!itemSizeExists)
                return NotFound("النمرة غير موجودة");

            var files = await _db.fileSizes
                .Where(f => f.itemSizeId == itemSizeId)
                .ToListAsync();

            if (!files.Any())
                return NotFound("لا يوجد ملفات لهذه النمرة");

            var result = files.Select(f => new
            {
                fileId = f.id,
                fileName = f.FileName,
                url = _fileStorageService.GenerateSignedUrlDownloadAndView(f.urlFile)
            });

            return Ok(result);
        }


        [HttpGet("downloadFileAndView/itemSize/{itemSizeId}")]
        [Authorize]
        public async Task<IActionResult> downloadFileAndViewByItemSizeId(int itemSizeId)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            // 1️⃣ تحقق إنو الزبون اشترى النمرة
            var hasAccess = await _OrderHistory
                .UserBoughtItemSize(userId, itemSizeId);

            if (!hasAccess)
                return Forbid("لم تقم بشراء هذه النمرة");

            // 2️⃣ جلب الملفات المرتبطة بالنمرة
            var files = await _db.fileSizes
                .Where(f => f.itemSizeId == itemSizeId)
                .ToListAsync();

            if (!files.Any())
                return NotFound("لا يوجد ملفات لهذه النمرة");

            // 3️⃣ توليد روابط تحميل مباشرة
            var result = files.Select(f => new
            {
                fileId = f.id,
                fileName = f.FileName,
                downloadUrl = _fileStorageService
                    .GenerateSignedUrlDownloadAndView(f.urlFile)
            });

            return Ok(result);
        }


        [HttpGet("getOrderHistory")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _OrderHistory.GetOrderHistoryAsync());
        }

        [HttpPost("addOrderHistory")]
        public async Task<IActionResult> add(AddOrderHistoryDto order)
        {
            var Order = await _OrderHistory.AddOrderHistoryAsync(order);
            if (Order.Data == null)
                return BadRequest(Order.Massage);

            return Ok(Order.Data);
        }

        [HttpDelete("deleteOrderHistory")]
        public async Task<IActionResult> delete(int id)
        {
            var result = await _OrderHistory.DeleteOrderHistoryAsync(id);
            if (result == null)
                return BadRequest("not found");
            return Ok(result);
        }

        [HttpPut("updateOrderHistory")]
        public async Task<IActionResult> update(AddOrderHistoryDto order, int id)
        {
            var result = await _OrderHistory.UpdateOrderHistoryAsync(order, id);
            if (result.Data == null)
            {
                return BadRequest(result.Massage);
            }
            else
            {
                return Ok(new
                {
                    Massage = result.Massage,
                    Item = result.Data
                });
            }

        }
    }
}
