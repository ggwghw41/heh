using MotoGP.Models;

namespace MotoGP.ViewModel
{
    public class DanhmucModel
    {
        public Guid Id { get; set; } // Add Id property here
        public string TenHangXe { get; set; }
        public string NuocSanXuat { get; set; }
        public IFormFile HinhAnh { get; set; }
        public bool HienThi { get; set; }
    }
}
