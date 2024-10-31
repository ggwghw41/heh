using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.ViewModel;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MotoGP.Controllers
{
    public class MotoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MotoController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Lấy tất cả dữ liệu từ bảng MoTo
            List<Moto> motos = await _context.Motos
                .Include(m => m.DanhMuc) // Nếu bạn muốn bao gồm thông tin danh mục
                .ToListAsync();

            return View(motos);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MotoViewModel motoViewModel)
        {
            if (ModelState.IsValid)
            {
                var danhMuc = _context.DanhMucs.FirstOrDefault(d => d.Id == motoViewModel.IdDanhMuc);
                if (danhMuc == null)
                {
                    ModelState.AddModelError("IdDanhMuc", "Danh mục không hợp lệ.");
                    ViewBag.DanhMucs = _context.DanhMucs.ToList();
                    return View(motoViewModel);
                }

                var moto = new Moto
                {
                    Id = Guid.NewGuid(),
                    TenXe = motoViewModel.TenXe,
                    DongCo = motoViewModel.DongCo,
                    Gia = motoViewModel.Gia,
                    MauXe = motoViewModel.MauXe,
                    ChiTietDongCo = motoViewModel.ChiTietDongCo,
                    ChiTietHẹThongPhanh = motoViewModel.ChiTietHẹThongPhanh,
                    CongNghe = motoViewModel.CongNghe,
                    ThietKe = motoViewModel.ThietKe,
                    SanXuat = motoViewModel.SanXuat,
                    HieuSuat = motoViewModel.HieuSuat,
                    HienThi = motoViewModel.HienThi,
                    NoiBat = motoViewModel.NoiBat,
                    IdDanhMuc = motoViewModel.IdDanhMuc
                };

                // Xử lý file ảnh
                if (motoViewModel.HinhAnh != null)
                {
                    var fileName = Path.GetFileName(motoViewModel.HinhAnh.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await motoViewModel.HinhAnh.CopyToAsync(stream);
                    }

                    moto.HinhAnh = fileName;
                }

                // Lưu vào cơ sở dữ liệu
                _context.Motos.Add(moto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            return View(motoViewModel);
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var moto = await _context.Motos.FindAsync(Id);
            if (moto == null)
            {
                return NotFound();
            }
            return View(moto);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMoto(Guid Id)
        {
            var moto = await _context.Motos.FindAsync(Id);
            if (moto != null) 
            {
                
                _context.Motos.Remove(moto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var EditMoto = await _context.Motos.FindAsync(id);
            if (EditMoto == null)
            {
                return NotFound();
            }
            var model = new MotoViewModel
            {
                Id = EditMoto.Id,
                TenXe = EditMoto.TenXe,
                DongCo = EditMoto.DongCo,
                Gia = EditMoto.Gia,
                MauXe = EditMoto.MauXe,
                ChiTietDongCo = EditMoto.ChiTietDongCo,
                ChiTietHẹThongPhanh = EditMoto.ChiTietHẹThongPhanh,
                CongNghe = EditMoto.CongNghe,
                ThietKe = EditMoto.ThietKe,
                SanXuat = EditMoto.SanXuat,
                HieuSuat = EditMoto.HieuSuat,
                HienThi = EditMoto.HienThi,
                NoiBat = EditMoto.NoiBat,
                IdDanhMuc = EditMoto.IdDanhMuc
            };
            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MotoViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var EditMoto = await _context.Motos.FindAsync(model.Id);
                if (EditMoto == null)
                {
                    return NotFound();
                }
                EditMoto.TenXe = model.TenXe;
                EditMoto.DongCo = model.DongCo;
                EditMoto.Gia = model.Gia;
                EditMoto.MauXe = model.MauXe;
                EditMoto.IdDanhMuc = model.IdDanhMuc;
                EditMoto.HienThi = model.HienThi;
                EditMoto.NoiBat = model.NoiBat;
                if (model.HinhAnh != null)
                {
                    var fileName = Path.GetFileName(model.HinhAnh.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.HinhAnh.CopyToAsync(stream);
                    }

                    EditMoto.HinhAnh = fileName;
                }
                _context.Motos.Update(EditMoto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            return View(model);
        }
        public async Task<IActionResult>Detail(Guid id)
        {
            var detail = await _context.Motos.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            return View(detail);
        }
        public async Task<IActionResult> ImageMoTo(Guid id)
        {
            var imageProduct = await _context.Motos
                .Include(m => m.MotoImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (imageProduct == null)
            {
                return NotFound();
            }

            var model = new ImageProductViewModel
            {
                TenXe = imageProduct.TenXe,
                MotoImages = imageProduct.MotoImages.ToList() // Use the correct property name
            };

            return View(model); // Return the view model instead of the Moto object
        }
        public IActionResult CreateImg()
        {
            ViewBag.MoTos = _context.Motos.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImg(ImageMotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem ID xe máy có tồn tại không
                var moto = _context.Motos.FirstOrDefault(d => d.Id == model.IdMoTo);
                if (moto == null)
                {
                    ModelState.AddModelError("IdMoTo", "Danh mục không hợp lệ.");
                    ViewBag.MoTos = _context.Motos.ToList();
                    return View(model);
                }

                // Tạo mới MotoImage
                var addImg = new MotoImage
                {
                    Id = Guid.NewGuid(),
                    IdMoTo = model.IdMoTo,
                };

                if (model.HinhAnh != null)
                {
                    // Lưu hình ảnh vào thư mục images
                    var fileName = Path.GetFileName(model.HinhAnh.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.HinhAnh.CopyToAsync(stream);
                    }

                    addImg.HinhAnh = fileName;
                }

                // Thêm hình ảnh vào cơ sở dữ liệu
                _context.MotoImages.Add(addImg);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ImageMoTo), new {id = model.IdMoTo});
            }

            ViewBag.MoTos = _context.Motos.ToList();
            return View(model);
        }
        public async Task<IActionResult> Deleteimg(Guid id)
        {
            var ImgId = await _context.MotoImages
                .Include(m => m.Moto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ImgId == null)
            {
                return NotFound();
            }
            return View(ImgId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImg(Guid id)
        {
            var ImgId = await _context.MotoImages.FindAsync(id);
            if (ImgId != null)
            {
                var motoId = ImgId.IdMoTo;
                _context.MotoImages.Remove(ImgId);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ImageMoTo), new { id = motoId });
            }
            return NotFound();
        }

    }
}
