using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.ViewModel;
using System.Threading.Tasks;

namespace MotoGP.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Account> _hasher;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<Account>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Kiểm tra xem tên, email hoặc số điện thoại đã tồn tại trong cơ sở dữ liệu chưa
            var kiemtra = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == model.Email || a.Name == model.Name || a.NumberPhone == model.NumberPhone);

            if (kiemtra != null)
            {
                ModelState.AddModelError("", "Tên người dùng hoặc email hoặc số điện thoại đã tồn tại.");
                return View(model);
            }

            // Kiểm tra mật khẩu và xác nhận mật khẩu có khớp hay không
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu không khớp");
                return View(model);
            }

            // Nếu mọi thứ hợp lệ, tiến hành lưu tài khoản mới vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                var NewAccount = new Account
                {
                    Id = Guid.NewGuid(), // Tạo GUID cho Id
                    Name = model.Name,
                    NumberPhone = model.NumberPhone,
                    Email = model.Email,
                    Password = _hasher.HashPassword(null, model.Password) // Mã hóa mật khẩu
                };

                // Thêm tài khoản mới vào cơ sở dữ liệu
                _context.Accounts.Add(NewAccount);
                await _context.SaveChangesAsync(); // Lưu thay đổi

                // Thêm quyền mặc định cho tài khoản
                var roleaccount = await _context.Roles.FirstOrDefaultAsync(p => p.Name == "Admin");
                if (roleaccount != null)
                {
                    var addRole = new UserRole
                    {
                        AccountId = NewAccount.Id,
                        RoleId = roleaccount.Id,
                    };
                    _context.UserRoles.Add(addRole);
                    await _context.SaveChangesAsync(); // Lưu thay đổi
                }

                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập
            }

            return View(model); // Trả lại model nếu không hợp lệ
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!ModelState.IsValid) return View();

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
            if (account == null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
                return View();
            }

            var passwordCheck = _hasher.VerifyHashedPassword(account, account.Password, password);
            if (passwordCheck == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Mật khẩu không đúng.");
                return View();
            }

            // Successful login
            return RedirectToAction("Index", "DanhMuc");
        }

        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email không tồn tại.");
                return View(model);
            }
            // Kiểm tra số điện thoại có 
            if (user.NumberPhone != model.NumberPhone)
            {
                ModelState.AddModelError("", "Số điện thoại không đúng.");
                return View(model);
            }

           

            if (user != null)
            {
                var resetPasswordModel = new ResetPasswordViewModel
                {
                    Email = model.Email,
                    NumberPhone = model.NumberPhone
                };
                return View("ResetPassword", resetPasswordModel);
            }

            ModelState.AddModelError("", "Email không tồn tại.");
            return View(model);
        }

        public IActionResult ResetPassword(string email, string numberPhone)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(numberPhone))
            {
                return BadRequest("Email và số điện thoại là bắt buộc.");
            }

            var model = new ResetPasswordViewModel { Email = email, NumberPhone = numberPhone };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == model.Email && a.NumberPhone == model.NumberPhone);

            if (user != null)
            {
                if (model.NewPassword == model.ConfirmPassword)
                {
                    user.Password = _hasher.HashPassword(user, model.NewPassword);
                    _context.Accounts.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "Account");
                }

                ModelState.AddModelError("", "Mật khẩu không khớp.");
            }
            else
            {
                ModelState.AddModelError("", "Email hoặc số điện thoại không đúng.");
            }

            return View(model);
        }
    }
}
