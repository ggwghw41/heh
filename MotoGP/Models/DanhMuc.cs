using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Models
{
    [Table("DanhMuc")]

    public class DanhMuc
    {
        [Key]
        public Guid Id { get; set; }
        public string TenHangXe { get; set; }
        public string NuocSanXuat {  get; set; }
        public string HinhAnh { get; set; }
        public bool HienThi {  get; set; } 
        public ICollection<Moto> Motos { get; set; }
    }
}
