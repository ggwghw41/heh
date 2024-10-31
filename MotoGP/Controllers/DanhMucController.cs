using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.ViewModel;

namespace MotoGP.Controllers
{
    public class DanhMucController : Controller
    {
        public readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DanhMucController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index() => View(await _Context.DanhMucs.ToListAsync());

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DanhmucModel model)
        {
            bool hienThiValue = model.HienThi; // Xem xét giá trị của HienThi
            if (ModelState.IsValid)
            {
                var add = new DanhMuc
                {
                    Id = Guid.NewGuid(),
                    TenHangXe = model.TenHangXe,
                    NuocSanXuat = model.NuocSanXuat,
                    HienThi = hienThiValue,

                };
                if (model.HinhAnh != null)
                { 
                    var FileName = Path.GetFileName(model.HinhAnh.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.HinhAnh.CopyToAsync(stream);
                    }
                    add.HinhAnh = FileName;
                    
                }
                _Context.DanhMucs.Add(add);
                await _Context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            var danhMuc = await _Context.DanhMucs.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            var model = new DanhmucModel
            {
                Id = danhMuc.Id,
                TenHangXe = danhMuc.TenHangXe,
                NuocSanXuat = danhMuc.NuocSanXuat,
                HienThi = danhMuc.HienThi,
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DanhmucModel model)
        {
            if (ModelState.IsValid)
            {
                var danhMuc = await _Context.DanhMucs.FindAsync(model.Id);
                if (danhMuc == null)
                {
                    return NotFound();
                }

                danhMuc.TenHangXe = model.TenHangXe;
                danhMuc.NuocSanXuat = model.NuocSanXuat;
                danhMuc.HienThi = model.HienThi;

                if (model.HinhAnh != null)
                {
                    // Handle the image upload if a new image is provided
                    var fileName = Path.GetFileName(model.HinhAnh.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.HinhAnh.CopyToAsync(stream);
                    }
                    danhMuc.HinhAnh = fileName; // Update the image filename
                }

                _Context.Update(danhMuc);
                await _Context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            var danhmuc = await _Context.DanhMucs.FindAsync(id);
            if (danhmuc == null)
            {
                return NotFound();
            }
                return View(danhmuc);
        }
        [HttpPost ]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInfo(Guid? id)
        {
            var danhMuc = await _Context.DanhMucs.FindAsync(id);
            if (danhMuc != null)
            {
                _Context.DanhMucs.Remove(danhMuc);
                await _Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
