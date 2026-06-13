using DataBase.DBcon;
using E_commerce.DTO.ItemDto;
using E_commerce.DTO.ItemPhotoDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Mapper.ItemMapper;
using E_commerce.Mappers.ItemMapper;
using E_commerce.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositry.ItemRepspsitry
{
    public class ItemRepo : IItem
    {
        private readonly DBC _context;
        public ItemRepo(DBC context)
        {
            _context = context;
        }

        public async Task<(Item Item, string Massage)> addItemAsync(AddItemDto item)
        {
            var categoryId =await _context.categories.FirstOrDefaultAsync(c => c.id == item.categoryId);
            var childCategoryId =await _context.childCategories.FirstOrDefaultAsync(c => c.id == item.childCategoryId);
            var Item =await _context.items.FirstOrDefaultAsync(i =>
            i.title == item.title &&
            i.description == item.description &&
            i.categoryId == item.categoryId &&
            i.childCategoryId == item.childCategoryId);
            try
            {
                if (Item == null)
                {
                    if (categoryId != null)
                    {
                        if (childCategoryId != null)
                        {
                            var ITEM = item.addItem();
                            _context.items.Add(ITEM);
                            await _context.SaveChangesAsync();
                            return (ITEM,"تمت الاضافة بنجاح");
                        }
                        else
                        {
                            return (null,"معرف الفئة الفرعية غير موجود");
                        }
                    }
                    else
                    {
                        return (null, "معرف الفئة الاساسية التي تحاول اضافته غير موجود");
                    }
                }
                else
                {
                    return (null,"العنصر الذي تحاول اضافتة موجود");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<(bool IsSuccess, string Massage)> deleteItemAsync(int id)
        {
            var item =await _context.items.FirstOrDefaultAsync(i => i.Id == id);
            try
            {
                if (item == null)
                {
                    return (false, "العنصر الذي تريد حذفة غير مووجود");
                }
                else
                {
                    _context.items.Remove(item);
                   await _context.SaveChangesAsync();
                    return (true, "تمت عملية الحذف بنجاح");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetItemDto>> getItemDtoAsync()
        {
            try
            {
                return await _context.items.Select(Item => Item.getItem()).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(GetItemDto Item, string Massage)> updateItemAsync(UpdateItemDto item, int id)
        {
            var categoryId =await _context.categories.FirstOrDefaultAsync(c => c.id == item.categoryId);
            var childCategoryId =await _context.childCategories.FirstOrDefaultAsync(c => c.id == item.childCategoryId);
            var Item =await _context.items.FirstOrDefaultAsync(i =>
            i.title == item.title &&
            i.description == item.description &&
            i.categoryId == item.categoryId &&
            i.childCategoryId == item.childCategoryId);
            var ITEM =await _context.items.FirstOrDefaultAsync(i => i.Id == id);
            try
            {
                if (ITEM == null)
                {
                    return (null,"العنصر الذي تريد اجراء علية عملية التحديث غير موجود");
                }
                else
                {
                    if (Item == null)
                    {
                        if (categoryId != null)
                        {
                            if (childCategoryId != null)
                            {
                                ITEM.title = item.title;
                                ITEM.description = item.description;
                                ITEM.categoryId = item.categoryId;
                                ITEM.childCategoryId = item.childCategoryId;
                               await _context.SaveChangesAsync();
                                return (ITEM.getItem(),"تمت العملية بنجاح");
                            }
                            else
                            {
                                return (null,"معرف الفئة الفرعية غير موجود");
                            }
                        }
                        else
                        {
                            return (null,"معرف الفئة الاساسية التي تحاول اضافته غير موجود");
                        }
                    }
                    else
                    {
                        return (null,"التحديث الذي اجريتة على العنصر بياناتة مطابقة لبيانات عنصر موجود مسبقا");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetItemPhotoDto>> getItemPhotoByItemId(int id)
        {
            bool item = await _context.items.AnyAsync(I => I.Id == id);
            try
            {
                if (!item)
                    return null;

                return await _context.itemsPhoto.Where(i => i.itmeId == id).Select(i => new GetItemPhotoDto
                {
                    id = i.id,
                    urlPhoto = i.urlPhoto,
                    itmeId = i.itmeId,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<GetItemDetailsDto> getItemPhotoOrSizeByItemId(int id)
        {
            try
            {
                bool item = await _context.items.AnyAsync(I => I.Id == id);
                if (!item)
                    return null;

                var photos = await _context.itemsPhoto.Where(i => i.itmeId == id).Select(i => new GetItemPhotoDto
                {
                    id = i.id,
                    urlPhoto = i.urlPhoto,
                    itmeId = i.itmeId,
                }).ToListAsync();

                var sizes = await _context.itemsSize.Where(i => i.itmeId == id).Select(i => new GetItemSizeDto
                {
                    id = i.id,
                    size = i.size,
                    price = i.price,
                    itmeId = i.itmeId,

                }).ToListAsync();

                return new GetItemDetailsDto
                {
                    photos = photos,
                    sizes = sizes,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<GetItemSizeDto>> getItemSizeByItemId(int id)
        {
            try
            {
                bool item = await _context.items.AnyAsync(I => I.Id == id);
                if (!item)
                    return null;

                return await _context.itemsSize.Where(i => i.itmeId == id).Select(i => new GetItemSizeDto
                {
                    id = i.id,
                    size = i.size,
                    price = i.price,
                    itmeId = i.itmeId,

                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<GetItemByCategroyDto> getItemByCategroyId(int id)
        {
            try
            {
                string sql = @"	 
                            select id , categoryId from Item where categoryId = @id 
                        ";

                var result = _context.Database.SqlQueryRaw<GetItemByCategroyDto>
                    (sql, new SqlParameter("id", id)).ToList();
                return result;

            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public List<GetItemQDto> getItemByChildCategroyId(int id)
        {
            try
            {
                string sql = @"	 
                            select id , childCategoryId from Item where childCategoryId = @id 
                        ";

                var result = _context.Database.SqlQueryRaw<GetItemQDto>
                    (sql, new SqlParameter("id", id)).ToList();
                return result;

            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
