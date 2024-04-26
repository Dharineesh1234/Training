using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class FileUpload
    {
        [Key]
        public int Image_Id { get; set; }

        [Required]
        public byte[] Data { get; set; }

    }
}
