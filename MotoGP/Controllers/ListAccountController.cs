using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.ViewModel;

namespace MotoGP.Controllers
{
    public class ListAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListAccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Lấy tất cả dữ liệu từ bảng MoTo
            var users = await _context.Accounts
         .Include(a => a.UserRoles) // Bao gồm UserRoles
             .ThenInclude(ur => ur.Role) // Bao gồm Role từ UserRole
         .ToListAsync();

            // Chuyển đổi dữ liệu thành ViewModel nếu cần thiết
            var ListRoleUserViewModels = users.Select(user => new ListRoleUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList() // Lấy tên quyền
            }).ToList();

            return View(ListRoleUserViewModels); // Trả về View với danh sách người dùng
        }
        [HttpGet]
        public async Task<IActionResult> ListAccout() => View(await _context.Accounts.ToListAsync());
        [HttpGet]
        public async Task<IActionResult> ListRole() => View(await _context.Roles.ToListAsync());
    }
}
