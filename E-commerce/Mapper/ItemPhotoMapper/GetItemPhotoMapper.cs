using E_commerce.DTO.ItemPhotoDto;
using E_commerce.Model;

namespace E_commerce.Mapper.ItemPhotoMapper
{
    public static class GetItemPhotoMapper
    {
        public static GetItemPhotoDto getItemPhoto(this ItemPhoto item)
        {
            return new GetItemPhotoDto
            {
                id=item.id,
                urlPhoto=item.urlPhoto,
                itmeId=item.itmeId,
            };
        }
    }
}
