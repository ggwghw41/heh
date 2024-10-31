using MotoGP.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotoGP.ViewModel
{
    public class ListDetailProduct
    {
        // Thuộc tính Id để định danh sản phẩm
        public Guid Id { get; set; }

        // Tên xe
        [Required(ErrorMessage = "Tên xe là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên xe không được vượt quá 100 ký tự.")]
        public string TenXe { get; set; }

        // Động cơ
        [Required(ErrorMessage = "Động cơ là bắt buộc.")]
        public string DongCo { get; set; }

        // Giá sản phẩm
        [Column(TypeName = "decimal(18,3)")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Giá phải là một giá trị dương.")]
        public decimal Gia { get; set; }

        // Màu xe
        public string MauXe { get; set; }

        // Hình ảnh chính
        public string HinhAnh { get; set; }

        // Chi tiết động cơ
        public string ChiTietDongCo { get; set; }

        // Chi tiết hệ thống phanh
        public string ChiTietHẹThongPhanh { get; set; }

        // Công nghệ
        public string CongNghe { get; set; }

        // Thiết kế
        public string ThietKe { get; set; }

        // Hiệu suất
        public string HieuSuat { get; set; }

        // Nhà sản xuất
        public string SanXuat { get; set; }

        // Đánh giá
        [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5.")]
        public int DanhGia { get; set; }

        // Id danh mục
        public Guid IdDanhMuc { get; set; }

        // Danh sách hình ảnh mô tô
        public List<string> MotoImages { get; set; } // Lưu trữ danh sách đường dẫn hình ảnh

        // Constructor khởi tạo
        public ListDetailProduct()
        {
            MotoImages = new List<string>(); // Khởi tạo danh sách hình ảnh
        }
    }
}
