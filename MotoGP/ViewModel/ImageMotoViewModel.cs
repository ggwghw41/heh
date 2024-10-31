using MotoGP.Models;
using System.ComponentModel.DataAnnotations;

namespace MotoGP.ViewModel
{
    public class ImageMotoViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public IFormFile HinhAnh {  get; set; }
        [Required]
        public Guid IdMoTo { get; set; }
    }
}
