using E_commerce.DTO.ItemPhotoDto;
using E_commerce.Model;

namespace E_commerce.Mapper.ItemPhotoMapper
{
    public static class AddItemPhotoMapper
    {
        public static ItemPhoto addItemPhoto(int itemId, string namePhoto)
        {
            return new ItemPhoto
            {
                itmeId = itemId,
                urlPhoto = namePhoto
            };
        }
    }
}
