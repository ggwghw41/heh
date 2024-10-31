using System.ComponentModel.DataAnnotations;

namespace MotoGP.Models
{
    public class test
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
