
using Eticket.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]

        public IFormFile Image { get; set; }

        public DateTime Startdate { get; set; }
        public DateTime EndData { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //relationShips
        public int CinemaId { get; set; }
        public Cinema  Cinema { get; set; }

        public int ProducerId { get; set; }

        public Producer  Producer { get; set; }


        public List<Actor_Movie>  Actor_Movies { get; set; }

    }
}
