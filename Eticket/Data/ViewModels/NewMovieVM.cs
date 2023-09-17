
using Eticket.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description Is Required")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Price Is Required")]

        public double Price { get; set; }
        [Required(ErrorMessage = "Picture is Required")]

        public string ImageUrl { get; set; }

        
        [Required(ErrorMessage = "Picture is Required")]

        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Startdate Is Required")]

        public DateTime Startdate { get; set; }
        [Required(ErrorMessage = "EndData Is Required")]

        public DateTime EndData { get; set; }
        [Required(ErrorMessage = "MovieCategory Is Required")]

        public MovieCategory MovieCategory { get; set; }

        [Required(ErrorMessage = "CinemaId Is Required")]

        public int CinemaId { get; set; }
        [Required(ErrorMessage = "ProducerId Is Required")]

        public int ProducerId { get; set; }


        [Required(ErrorMessage = "ActorsId Is Required")]

        public List<int>  ActorsId { get; set; }

    }
}
