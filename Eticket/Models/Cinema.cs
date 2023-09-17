using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Picture is Required")]
        public string? CinemaLogo { get; set; }
        [NotMapped]
      

        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //RealtionShips
        public List<Movie>  Movies { get; set; }
    }
}
