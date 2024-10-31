using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Models
{
    public class MotoImage
    {
        [Key]
        public Guid Id { get; set; }

        public string HinhAnh { get; set; }

        // Specify that IdMoTo is the foreign key for Moto
        public Guid IdMoTo { get; set; }

        [ForeignKey("IdMoTo")]
        public Moto Moto { get; set; }
    }
}
