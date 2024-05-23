using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace son_gun.Models
{
    public class Expert
    {
        public int Id { get; set; }
        [MinLength(5)]
        [MaxLength(25)]
        public string FullName { get; set; }
        public string Position { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile PhotoFile { get; set; }
    }
}
