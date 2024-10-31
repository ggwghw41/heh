using System.ComponentModel.DataAnnotations;

namespace MotoGP.ViewModel
{
    public class NewsViewModel
    {
        public Guid Id { get; set; } 

        [Required]
        [MaxLength(200)]
        public string TieuDe { get; set; } // Tiêu đề của tin tức
        public IFormFile HinhAnh { get; set; }
        [Required]
        public string NoiDung { get; set; } // Nội dung của tin tức


        public DateTime NgayDang { get; set; } = DateTime.Now; // Ngày đăng tin tức

        public bool HienThi { get; set; } // Trạng thái hiển thị của tin tức
    }
}
