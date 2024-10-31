using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.ViewModel;
using System.Reflection.Metadata.Ecma335;

namespace MotoGP.Controllers
{
    public class NewsController : Controller
    {

        public readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index() => View(await _Context.news.ToListAsync());

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var New = new News
                {
                    Id = Guid.NewGuid(),
                    TieuDe = model.TieuDe,
                    NoiDung = model.NoiDung,
                    HienThi = model.HienThi,

                };
                if (model.HinhAnh != null)
                {
                    var FileName = Path.GetFileName(model.HinhAnh.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.HinhAnh.CopyToAsync(stream);
                    }
                    New.HinhAnh = FileName;
                }
                _Context.news.Add(New);
                await _Context.SaveChangesAsync();
                return RedirectToAction("Index", "News");
            }
            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _Context.news
                .FirstOrDefaultAsync(m => m.Id == id);

            if (danhMuc == null)
            {
                return NotFound();
            }

            var model = new NewsViewModel
            {
                Id = danhMuc.Id,
                TieuDe = danhMuc.TieuDe,
                NoiDung = danhMuc.NoiDung,
                NgayDang = danhMuc.NgayDang
            };

            return View(model);
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            var Idtintuc = await _Context.news.FindAsync(id);
            if (Idtintuc == null)
            {
                return NotFound();
            }
            return View(Idtintuc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNew(Guid? id)
        {
            var Idtintuc = await _Context.news.FindAsync(id);
            if (Idtintuc != null)
            {
                _Context.Remove(Idtintuc);
                await _Context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            var EditNew = await _Context.news.FindAsync(id);
            if (EditNew == null)
            {
                return NotFound();
            }
            var model = new NewsViewModel
            {
                Id = EditNew.Id,
                TieuDe = EditNew.TieuDe,
                NoiDung = EditNew.NoiDung,
                HienThi = EditNew.HienThi,
          
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var EditNew = await _Context.news.FindAsync(model.Id);
                if (EditNew != null)
                {
                    EditNew.TieuDe = model.TieuDe;
                    EditNew.NoiDung = model.NoiDung;
                    EditNew.HienThi = model.HienThi;

                    // Handle image update if a new image is uploaded
                    if (model.HinhAnh != null)
                    {
                        var fileName = Path.GetFileName(model.HinhAnh.FileName);
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.HinhAnh.CopyToAsync(stream);
                        }
                        EditNew.HinhAnh = fileName;
                    }

                    _Context.news.Update(EditNew);
                    await _Context.SaveChangesAsync();
                    return RedirectToAction("Index"); // Redirect to the Index action after successful edit
                }
            }
            return View(model); // Return the model to the view if the ModelState is invalid
        }

    }
}
