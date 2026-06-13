using E_commerce.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.DTO.FileSizeDto
{
    public class GetFileSizeDto
    {
        public int id { get; set; }
        public string FileName { get; set; }

        public string Extension { get; set; }

        public int itemSizeId { get; set; }

    }
}
