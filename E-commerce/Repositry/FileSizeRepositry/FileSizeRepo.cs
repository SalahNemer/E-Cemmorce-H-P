using DataBase.DBcon;
using E_commerce.DTO.FileSizeDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Interface.Serves;
using E_commerce.Mapper.FileSizeMapper;
using E_commerce.Model;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositry.FileSizeRepositry
{
    public class FileSizeRepo : IFileSize
    {
        private readonly DBC _context;
        private readonly IFileStorageServicePDF _fileStorageService;

        public FileSizeRepo(DBC context ,IFileStorageServicePDF fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task AddFileSize(FileSize fileSize)
        {
            await _context.fileSizes.AddAsync(fileSize);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetFileSizeDto>> GetFileSize()
        {
           return await _context.fileSizes.Select(id=>id.getFileSize()).ToListAsync();
        }

        public async Task<bool> ItemExistsAsync(int itemSizeId)
        {
            try
            {
                return await _context.itemsSize.AnyAsync(x => x.id == itemSizeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string Massage)> deleteFileSizeAsync(int id)
        {
            var fileSize = await _context.fileSizes.FirstOrDefaultAsync(i => i.id == id);

            if (fileSize == null)
                return (false, "الملف الذي تريد حذفه غير موجود");

            try
            {
                if (!string.IsNullOrEmpty(fileSize.urlFile))
                {
                    await _fileStorageService.DeleteAsync(fileSize.urlFile);
                }

                _context.fileSizes.Remove(fileSize);
                await _context.SaveChangesAsync();

                return (true, "تمت عملية الحذف بنجاح");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(GetFileSizeDto item, string massage)> updateFileSize(UpdateFileSizeDto fileSize, int id)
        {
            var file = await _context.fileSizes.FirstOrDefaultAsync(i => i.id == id);
            var ItemSize = await _context.itemsSize.FirstOrDefaultAsync(I => I.id == fileSize.itemSizeId);
            try
            {
                if (ItemSize != null)
                {
                    if (file != null)
                    {
                        file.itemSizeId = ItemSize.id;
                        if (fileSize.urlFile != null)
                        {
                            string oldFileKey = file.urlFile; 

                            string newFileKey = await _fileStorageService.UploadAsync(fileSize.urlFile, "items");

                            file.urlFile = newFileKey;
                            file.FileName = fileSize.urlFile.FileName;

                            await _context.SaveChangesAsync();

                            if (!string.IsNullOrWhiteSpace(oldFileKey))
                            {
                                await _fileStorageService.DeleteAsync(oldFileKey);
                            }
                        }


                        await _context.SaveChangesAsync();
                        return (new GetFileSizeDto
                        {
                            id = file.id,
                            FileName = file.FileName,
                            Extension = Path.GetExtension(file.FileName),
                            itemSizeId = file.itemSizeId,
                        }, "done");
                    }
                    else
                    {
                        return (null, "الملف الذي تريد اجراء علية عملية التعديل غير موجود");
                    }
                }
                else
                {
                    return (null, "النمرة المدخلة غير متوفرة");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
