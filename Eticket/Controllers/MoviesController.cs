using Eticket.Data.MovieService;
using Eticket.Helper;
using ETicket.data;
using ETicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ETicket.Controllers
{
    [Authorize]

    public class MoviesController : Controller
    {
        private readonly IMovie _movie;

        public MoviesController(IMovie movie )
        {
            _movie = movie;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
         

            var AllMovies = await _movie.GetAll(n=>n.Cinema); 
            return View(AllMovies);
        }
        [AllowAnonymous]

        public async Task<IActionResult> Filter(string SearchString)
        {

            var AllMovies = await _movie.GetAll(n => n.Cinema);
            if (!string.IsNullOrEmpty(SearchString))
            {
                var Filter = AllMovies.Where(m => m.Name.ToUpper().Contains(SearchString.ToUpper()) || m.Description.ToUpper().Contains(SearchString.ToUpper())).ToList();
                
                return View("Index",Filter);
            }
            return View("Index", AllMovies);
        }

        [AllowAnonymous]

        public async Task<IActionResult> Details( int id)
        { 
            var details= await _movie.GetMovieByIdAsync(id);
            return View(details);
           
        }

        public async Task<IActionResult> Create( )
        {

           
            var movieDropDownData = await _movie.GetNewMovieDropDownVMAsync();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas,"Id","Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(NewMovieVM movieVM)
        {

            var movieDropDownData = await _movie.GetNewMovieDropDownVMAsync();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");

            movieVM.ImageUrl = DocumentSettings.UploadFile(movieVM.Image, "Images");

            await _movie.AddMovie(movieVM);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int id)
        {
            var Details = await _movie.GetMovieByIdAsync(id);
            if (Details == null)
                return BadRequest();
            var response =new NewMovieVM()
             {
                Id=Details.Id,
                Name=Details.Name,
                Description=Details.Description,
                ImageUrl=Details.ImageUrl,
                Image=Details.Image,
                Price=Details.Price,
                MovieCategory=Details.MovieCategory,
                CinemaId=Details.CinemaId,
                ProducerId=Details.ProducerId,
                Startdate=Details.Startdate,
                EndData=Details.EndData,
                ActorsId = Details.Actor_Movies.Select(a => a.ActorId).ToList(),

            };
            var movieDropDownData = await _movie.GetNewMovieDropDownVMAsync();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");

            return View(response);
        }


        [HttpPost]

        public async Task<IActionResult>Edit(int id,NewMovieVM movieVM)
        {
            if(id !=movieVM.Id)
                return NotFound();

            var movieDropDownData = await _movie.GetNewMovieDropDownVMAsync();

            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
            movieVM.ImageUrl = DocumentSettings.UploadFile(movieVM.Image, "Images");

            await _movie.UpdateNewMoview(movieVM);
                return RedirectToAction(nameof(Index));



        }
    }
}
