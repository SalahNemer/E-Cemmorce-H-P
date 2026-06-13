using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.ChileCategroyDTO
{
    public class GetChildCategoryDTO
    {
        public int id { get; set; }

        public string childCategoryName { get; set; }
        public string categoryName { get; set; }

    }
}
