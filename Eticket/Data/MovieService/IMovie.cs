using Eticket.Data.BaseEntity;
using Eticket.Data.ViewModels;
using ETicket.Models;

namespace Eticket.Data.MovieService
{
    public interface IMovie:IEntityBase<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropDownVM> GetNewMovieDropDownVMAsync();
        Task AddMovie(NewMovieVM movie);
        Task UpdateNewMoview( NewMovieVM movie);
    }
}
