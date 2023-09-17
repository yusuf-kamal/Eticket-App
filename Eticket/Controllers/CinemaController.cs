using Eticket.Data.CinemaService;
using Eticket.Helper;
using ETicket.data;
using ETicket.data.IActorInterfaces.ActorService;
using ETicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicket.Controllers
{
	[Authorize]

	public class CinemaController : Controller
    {
        private readonly ICinema _cinema;

        public CinemaController(ICinema cinema)
        {
            _cinema = cinema;
        }
        public async Task<IActionResult> Index()
        {
            var AllCinemas = await _cinema.GetAll();
            return View(AllCinemas);
        }



        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {

            cinema.CinemaLogo = DocumentSettings.UploadFile(cinema.Image, "Images");

            await _cinema.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int id)
        {
            var ActorDetails = await _cinema.GetById(id);
            if (ActorDetails is null)
                return BadRequest();
            return View(ActorDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ActorDetails = await _cinema.GetById(id);
            if (ActorDetails is null)
                return BadRequest();
            return View(ActorDetails);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Cinema cinema)
        {
            cinema.CinemaLogo = DocumentSettings.UploadFile(cinema.Image, "Images");

            _cinema.Update(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ActorDetails = await _cinema.GetById(id);
            if (ActorDetails is null)
                return BadRequest();
            return View(ActorDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm([FromRoute] int id, Cinema cinema)
        {
            if (id != cinema.Id)
                return BadRequest();
            try
            {

                await _cinema.DeleteById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(cinema);
            }

        }
    }
}
