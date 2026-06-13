using DataBase.DBcon;
using E_commerce.DTO.ItemPhotoDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Interface.Serves;
using E_commerce.Mapper.ItemPhotoMapper;
using E_commerce.Mappers.ItemMapper;
using E_commerce.Model;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositry.ItemPhotoRepsitry
{
    public class ItemPhotoRepo : IItemPhoto
    {
        private readonly DBC _context;
        private readonly IFileStorageService _fileStorageService;

        public ItemPhotoRepo(DBC context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }
        public async Task AddAsync(ItemPhoto itemPhoto)
        {
            try
            {
                await _context.itemsPhoto.AddAsync(itemPhoto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string Massage)> deleteItemAsync(int id)
        {
            var item = await _context.itemsPhoto.FirstOrDefaultAsync(i => i.id == id);
            try
            {
                if (item == null)
                {
                    return (false, "الصورة الذي تريد حذفها غير موجودة");
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.urlPhoto))
                    {
                        var fileName = Path.GetFileName(item.urlPhoto);
                        await _fileStorageService.DeleteAsync(fileName);
                    }
                    _context.itemsPhoto.Remove(item);
                    await _context.SaveChangesAsync();
                    return (true, "تمت عملية الحذف بنجاح");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetItemPhotoDto>> getItemPhoto()
        {
            try
            {
                return await _context.itemsPhoto.Select(ItemPhoto => ItemPhoto.getItemPhoto()).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ItemExistsAsync(int id)
        {
            try
            {
                return await _context.items.AnyAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<(GetItemPhotoDto item, string massage)> updateItem(UpdateItemPhotoDto itemPhoto, int id)
        {
            var item = await _context.items.FirstOrDefaultAsync(i => i.Id == itemPhoto.itmeId);
            var ItemPhoto = await _context.itemsPhoto.FirstOrDefaultAsync(I => I.id == id);
            try
            {
                if (ItemPhoto != null)
                {
                    if (item != null)
                    {
                        ItemPhoto.itmeId = itemPhoto.itmeId;
                        if (itemPhoto.photo != null)
                        {
                            var oldFileUrl = ItemPhoto.urlPhoto;

                            var newFileUrl = await _fileStorageService.UploadAsync(itemPhoto.photo, "items");

                            ItemPhoto.urlPhoto = newFileUrl;

                            if (!string.IsNullOrWhiteSpace(oldFileUrl))
                            {
                                var oldFileKey = new Uri(oldFileUrl).AbsolutePath.TrimStart('/');
                                await _fileStorageService.UpdateAsync(oldFileKey);
                            }
                        }

                        await _context.SaveChangesAsync();
                        return (new GetItemPhotoDto
                        {
                            id = ItemPhoto.id,
                            itmeId = ItemPhoto.itmeId,
                            urlPhoto = ItemPhoto.urlPhoto,
                        }, "done");
                    }
                    else
                    {
                        return (null, "Item not found");
                    }
                }
                else
                {
                    return (null, "Item photo not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
