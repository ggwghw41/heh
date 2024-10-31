using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.ViewModel;

namespace MotoGP.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Phương thức Index để hiển thị danh sách sản phẩm và tin tức
        public async Task<IActionResult> Index()
        {
            var listView = new ListViewModel
            {
                DanhMucs = await _context.DanhMucs.Where(dm => dm.HienThi).ToListAsync(),
                Motos = await _context.Motos.Where(m => m.NoiBat).Include(m => m.DanhMuc).ToListAsync(),
                News = await _context.news.Where(tt => tt.HienThi).Take(4).ToListAsync()
            };
            return View(listView);
        }

        // Phương thức DetailProduct để hiển thị chi tiết sản phẩm
        public async Task<IActionResult> DetailProduct(Guid id)
        {
            var motoDetail = await _context.Motos
                .Where(m => m.Id == id)
                .Select(m => new ListDetailProduct
                {
                    Id = m.Id,
                    TenXe = m.TenXe,
                    DongCo = m.DongCo,
                    Gia = m.Gia,
                    MauXe = m.MauXe,
                    HinhAnh = m.HinhAnh,
                    ChiTietDongCo = m.ChiTietDongCo,
                    ChiTietHẹThongPhanh = m.ChiTietHẹThongPhanh,
                    CongNghe = m.CongNghe,
                    ThietKe = m.ThietKe,
                    HieuSuat = m.HieuSuat,
                    SanXuat = m.SanXuat,
                    DanhGia = m.DanhGia,
                    IdDanhMuc = m.IdDanhMuc,
                    MotoImages = _context.MotoImages
                        .Where(mi => mi.IdMoTo == m.Id)
                        .Select(mi => mi.HinhAnh)
                        .ToList() // Lưu ý: Đoạn này cần phải thay đổi để phù hợp với kiểu List<string>
                })
                .FirstOrDefaultAsync();

            if (motoDetail == null)
            {
                return NotFound(); // Nếu không tìm thấy sản phẩm
            }

            return View(motoDetail); // Trả về view cùng với thông tin sản phẩm
        }
    }
}
