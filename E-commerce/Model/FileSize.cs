using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("FileSize")]
    public class FileSize
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string urlFile { get; set; }//S3 Key

        [Required]
        public string FileName { get; set; }

        [Required]
        public int itemSizeId { get; set; }

        [ForeignKey("itemSizeId")]
        public ItemSize itemSize { get; set; }

    }
}
