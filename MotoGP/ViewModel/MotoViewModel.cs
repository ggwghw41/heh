using System.ComponentModel.DataAnnotations;

namespace MotoGP.ViewModel
{
    public class MotoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên xe là bắt buộc.")]
        public string TenXe { get; set; }

        public string DongCo { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal Gia { get; set; }

        public string MauXe { get; set; }

        [Required(ErrorMessage = "Hình ảnh là bắt buộc.")]
        public IFormFile HinhAnh { get; set; }
        public bool HienThi { get; set; }
        public bool NoiBat { get; set; }
        public string ChiTietDongCo { get; set; }
        public string ChiTietHẹThongPhanh { get; set; }
        public string CongNghe { get; set; }
        public string ThietKe { get; set; }
        public string HieuSuat { get; set; }
        public string SanXuat { get; set; }
        
        public int DanhGia { get; set; }
        [Required(ErrorMessage = "Danh mục là bắt buộc.")]
        public Guid IdDanhMuc { get; set; }
    }
}
