using E_commerce.DTO.FileSizeDto;
using E_commerce.Model;

namespace E_commerce.Mapper.FileSizeMapper
{
    public static class GetFileSizeMapper
    {
        public static GetFileSizeDto getFileSize(this FileSize fileSize)
        {
            return new GetFileSizeDto
            {
                id = fileSize.id,
                Extension = Path.GetExtension(fileSize.FileName),
                itemSizeId = fileSize.itemSizeId,
                FileName= fileSize.FileName,
            };
        }
    }
}
