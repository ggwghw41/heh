using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.ViewModel;

namespace MotoGP.Controllers
{
    public class ImageMoToController : Controller
    {
        public readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _env;
        public ImageMoToController (ApplicationDbContext context, IWebHostEnvironment env)
        {
            _Context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            // Lấy tất cả dữ liệu từ bảng MoTo
            List<MotoImage> images = await _Context.MotoImages
                .Include(m => m.Moto) // Nếu bạn muốn bao gồm thông tin danh mục
                .ToListAsync();

            return View(images);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MoTos = _Context.Motos.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImageMotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem ID xe máy có tồn tại không
                var moto = _Context.Motos.FirstOrDefault(d => d.Id == model.IdMoTo);
                if (moto == null)
                {
                    ModelState.AddModelError("IdMoTo", "Danh mục không hợp lệ.");
                    ViewBag.MoTos = _Context.Motos.ToList();
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
                _Context.MotoImages.Add(addImg);
                await _Context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MoTos = _Context.Motos.ToList();
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var ImgId = await _Context.MotoImages
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
            var ImgId = await _Context.MotoImages.FindAsync(id);
            if (ImgId != null)
            {
                _Context.MotoImages.Remove(ImgId);

                await _Context.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
