using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Models
{
    [Table("TinTuc")]
    public class News
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string TieuDe { get; set; } // Tiêu đề của tin tức
        public string HinhAnh { get; set; }
        [Required]
        public string NoiDung { get; set; } // Nội dung của tin tức

       
        public DateTime NgayDang { get; set; } = DateTime.Now; // Ngày đăng tin tức

        public bool HienThi { get; set; } // Trạng thái hiển thị của tin tức
    }
}
