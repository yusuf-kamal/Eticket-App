using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Picture is Required")]

        
        public string ProfilePictureUrl { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Picture is Required")]

        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Name is Required")]

        public string FullName { get; set; }

        [Required(ErrorMessage = "Description is Required")]

        public string Bio { get; set; }

        public List<Actor_Movie> Actor_Movies { get; set; }
    }
}
