using Microsoft.AspNetCore.Mvc;
using MotoGP.Data;
using MotoGP.Models;

namespace MotoGP.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Tests.ToList(); // Lấy danh sách danh mục từ cơ sở dữ liệu
            return View(categories); // Truyền danh sách cho view
        }

        public IActionResult Create()
        {
            return PartialView("Create"); // Trả về partial view cho modal
        }

        [HttpPost]
        public async Task<IActionResult> Create(test category)
        {
            if (ModelState.IsValid)
            {
                category.Id = Guid.NewGuid(); // Tạo ID mới
                _context.Add(category);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Danh mục đã được thêm." });
            }
            return Json(new { success = false, message = "Có lỗi xảy ra." });
        }
    }
}
