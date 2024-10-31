using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Models
{
    [Table("MoTo")]
    public class Moto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên xe là bắt buộc.")]
        public string TenXe { get; set; }

        public string DongCo { get; set; } // Dung tích xi lanh

        [Column(TypeName = "decimal(18,3)")] // Chỉ định loại cột
        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0.")]
        public decimal Gia { get; set; }

        public string MauXe { get; set; }
        public string HinhAnh { get; set; }
        
        public string ChiTietDongCo { get; set; }
        public string ChiTietHẹThongPhanh { get; set; }
        public string CongNghe { get; set; }
        public string ThietKe { get; set; }
        public string HieuSuat {  get; set; }
        public string SanXuat { get; set; }
        public bool HienThi { get; set; }
        public bool NoiBat { get; set; }
        public int DanhGia { get; set; }
        [Required(ErrorMessage = "Danh mục là bắt buộc.")]
        public Guid IdDanhMuc { get; set; }

        // Khai báo mối quan hệ với DanhMuc
        [ForeignKey("IdDanhMuc")]
        public DanhMuc DanhMuc { get; set; }
        public ICollection<MotoImage> MotoImages { get; set; }
    }
}
