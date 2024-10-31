using MotoGP.Models;

namespace MotoGP.ViewModel
{
    public class DetailProductViewModel
    {
        public ListDetailProduct MotoDetail { get; set; } // Chi tiết sản phẩm
        public List<Moto> Motos { get; set; } // Danh sách các sản phẩm nổi bật
    }
}
