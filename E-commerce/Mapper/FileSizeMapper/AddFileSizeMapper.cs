using E_commerce.DTO.FileSizeDto;
using E_commerce.Model;

namespace E_commerce.Mapper.FileSizeMapper
{
    public static class AddFileSizeMapper
    {
        public static FileSize addFileSize(this AddFileSizeDto dto,string urlpdf)
        {
            return new FileSize
            {
               itemSizeId = dto.itemSizeId,
               urlFile=urlpdf
            };
        }
    }
}
