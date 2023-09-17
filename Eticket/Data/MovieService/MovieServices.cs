using Eticket.Data.BaseEntity;
using Eticket.Data.ViewModels;
using Eticket.Helper;
using ETicket.data;
using ETicket.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eticket.Data.MovieService
{
    public class MovieServices:GenericEntityRepo<Movie>,IMovie
    {
        private readonly EticketDbContext _context;

        public MovieServices(EticketDbContext context ):base(context)
        {
            _context = context;
        }

        public async Task AddMovie(NewMovieVM movie)
        {

            var NewMovie = new Movie()
            {
                Name = movie.Name,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl,
                CinemaId = movie.CinemaId,
                Startdate = movie.Startdate,
                EndData = movie.EndData,
                Image = movie.Image,
                MovieCategory = movie.MovieCategory,
                ProducerId = movie.ProducerId,
            };


            await _context.Movies.AddAsync(NewMovie);

            await _context.SaveChangesAsync();

            foreach (var actorId in movie.ActorsId)
            {
                var NewActorMovie = new Actor_Movie()
                {
                    ActorId = actorId,
                    MovieId = NewMovie.Id
                };
                await _context.Actor_Movies.AddAsync(NewActorMovie);
            }
            await _context.SaveChangesAsync();
        }
            
        

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
          var MovieDetails=await _context.Movies
                .Include(c=>c.Cinema)
                .Include(p=>p.Producer)
                .Include(am=>am.Actor_Movies)
                .ThenInclude(a=>a.Actor)
                .FirstOrDefaultAsync(c=>c.Id==id);
            return  MovieDetails;
        }

        public async Task<NewMovieDropDownVM> GetNewMovieDropDownVMAsync()
        {
            var response = new NewMovieDropDownVM()
            {
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync(),
            };
            return response;
        }

        public async Task UpdateNewMoview(NewMovieVM movie)
        {
            var DbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);
            if(DbMovie!= null)
            {

                DbMovie.Name = movie.Name;
                DbMovie.Description = movie.Description;
                DbMovie.ImageUrl = movie.ImageUrl;
                DbMovie.CinemaId = movie.CinemaId;
                DbMovie.Startdate = movie.Startdate;
                DbMovie.EndData = movie.EndData;
                DbMovie.Image = movie.Image;
                DbMovie.MovieCategory = movie.MovieCategory;
                DbMovie.ProducerId = movie.ProducerId;
                DbMovie.Price=movie.Price;
                await _context.SaveChangesAsync();
            }

            var ExitingActorsDb =  _context.Actor_Movies.Where(n => n.MovieId == movie.Id);
            _context.Actor_Movies.RemoveRange(ExitingActorsDb);
            await _context.SaveChangesAsync();
            foreach (var actorId in movie.ActorsId)
            {
                var NewActorMovie = new Actor_Movie()
                {
                    ActorId = actorId,
                    MovieId = movie.Id
                };
                await _context.Actor_Movies.AddAsync(NewActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
