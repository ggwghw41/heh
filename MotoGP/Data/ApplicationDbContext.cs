using Microsoft.EntityFrameworkCore;
using MotoGP.Models;

namespace MotoGP.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<MotoImage> MotoImages { get; set; }
        public DbSet<test > Tests { get; set; }
        // Tạo 2 khóa chính trong 1 bảng Phân quyền người dùng
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Khóa chính kết hợp cho bảng Phân quyền người dùng
            modelBuilder.Entity<UserRole>().HasKey(p => new { p.AccountId, p.RoleId });
            // Cấu hình mối quan hệ giữa Account  và Phân quyền người dùng
            modelBuilder.Entity<UserRole>()
                .HasOne(a => a.Account)
                .WithMany(pqnd => pqnd.UserRoles)
                .HasForeignKey(apq => apq.AccountId);
            // Cấu hình mối quan hệ giữa Phân quyền và Phân quyền người dùng
            modelBuilder.Entity<UserRole>()
                .HasOne(pq => pq.Role)
                .WithMany(pqnd => pqnd.UserRoles)
                .HasForeignKey(p => p.RoleId);

        }

    }
}
