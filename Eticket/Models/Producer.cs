using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        [NotMapped]

        public IFormFile Image { get; set; }

        public string FullName { get; set; }
        public string Bio { get; set; }
        //RelationShips
        public List<Movie> Movies { get; set; }
    }
}
