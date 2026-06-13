using DataBase.DBcon;
using E_commerce.DTO.ItemDto;
using E_commerce.DTO.ItemPhotoDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Interface.Serves;
using E_commerce.Mapper.ItemPhotoMapper;
using E_commerce.Mapper.ItemSizeMapper;
using E_commerce.Model;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositry.ItemSizeRepsitry
{
    public class ItemSizeRepo : IItemSize
    {
        private readonly DBC _context;
        private readonly IFileStorageServicePDF _fileStorageService;

        public ItemSizeRepo(DBC context, IFileStorageServicePDF fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }
        public async Task<GetItemSizeDto> AddItemSizeAsync(AddItemSizeDto itemSize)
        {
            try
            {
                var ItemSize = itemSize.addItemSize();
                await _context.itemsSize.AddAsync(ItemSize);
                await _context.SaveChangesAsync();
                return ItemSize.getItemSize();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<(bool IsSuccess, string Massage)> deleteItemSizeAsync(int id)
        {
            var item = await _context.itemsSize.FirstOrDefaultAsync(i => i.id == id);
            try
            {
                if (item == null)
                {
                    return (false, "النمرة الذي تريد حذفها غير موجودة");
                }
                else
                {
                    _context.itemsSize.Remove(item);
                    await _context.SaveChangesAsync();
                    return (true, "تمت عملية الحذف بنجاح");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetItemDto> getItemByItemSizeId(int id)
        {
            try
            {
                bool itemSize = await _context.itemsSize.AnyAsync(I => I.id == id);
                if (!itemSize)
                    return null;

                int itemId = await _context.itemsSize
                     .Where(i => i.id == id)
                     .Select(i => i.itmeId)
                     .FirstOrDefaultAsync();

                return await _context.items.Where(i => i.Id == itemId).Select(i => new GetItemDto
                {
                    Id = i.Id,
                    title = i.title,
                    description = i.description,
                    categoryId = i.categoryId,
                    childCategoryId = i.childCategoryId,
                }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GetItemSizeDto>> getItemSize()
        {
            try
            {
                return await _context.itemsSize.Select(ItemSize => ItemSize.getItemSize()).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ItemExistsAsync(int itemId)
        {
            try
            {
                return await _context.items.AnyAsync(x => x.Id == itemId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(GetItemSizeDto item, string massage)> updateItemSize(UpdateItemSizeDto itemSize, int id)
        {
            var item = await _context.items.FirstOrDefaultAsync(i => i.Id == itemSize.itmeId);
            var ItemSize = await _context.itemsSize.FirstOrDefaultAsync(I => I.id == id);
            try
            {
                if (ItemSize != null)
                {
                    if (item != null)
                    {
                        ItemSize.price = itemSize.price;
                        ItemSize.size = itemSize.size;
                        ItemSize.itmeId = itemSize.itmeId;
                        
                        await _context.SaveChangesAsync();
                        return (new GetItemSizeDto
                        {
                            id = ItemSize.id,
                            itmeId = ItemSize.itmeId,
                            size = ItemSize.size,
                            price = ItemSize.price,
                        }, "done");
                    }
                    else
                    {
                        return (null, "Item not found");
                    }
                }
                else
                {
                    return (null, "Item size not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
 }
