using Eticket.Helper;
using ETicket.data;
using ETicket.data.IActorInterfaces;
using ETicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ETicket.Controllers
{
    [Authorize]
    public class ActorsController : Controller
    {
        private readonly IActor _IActorService;

        public ActorsController(IActor IActorService)
        {
            
            _IActorService = IActorService;
        }
        public async Task<IActionResult> Index()
        {
            var Allactors =await _IActorService.GetAll();
            return View(Allactors);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( Actor actor)
        {
            
            actor.ProfilePictureUrl = DocumentSettings.UploadFile(actor.Image, "Images");

                await _IActorService.AddAsync(actor);
                return RedirectToAction(nameof(Index));
        
        }

        public async Task<ActionResult> Details(int id)
        {
            var ActorDetails= await _IActorService.GetById(id);
            if (ActorDetails is null)
                return BadRequest();
            return View(ActorDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ActorDetails =await _IActorService.GetById(id);
            if (ActorDetails is null)
                return BadRequest();
            return View(ActorDetails);
        }
        [HttpPost]
        public  IActionResult Edit([FromRoute]int id,Actor actor)
        {
            actor.ProfilePictureUrl = DocumentSettings.UploadFile(actor.Image, "Images");

             _IActorService.Update(id, actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ActorDetails = await _IActorService.GetById(id);
            if (ActorDetails is null)
                return BadRequest();
            return View(ActorDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm([FromRoute] int id, Actor actor)
        {
            if (id != actor.Id)
                return BadRequest();
            try
            {
                var imageUrl = actor.ProfilePictureUrl; 
                DocumentSettings.DeletFile( "Images", imageUrl);


                await _IActorService.DeleteById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

               ModelState.AddModelError(string.Empty, ex.Message);
                return View(actor);
            }
           
        }
    }
}
