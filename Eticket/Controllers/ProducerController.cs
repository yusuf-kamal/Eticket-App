using Eticket.Data.ProducerService;
using Eticket.Helper;
using ETicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Controllers
{
	[Authorize]

	public class ProducerController : Controller
    {
        private readonly IProducer _producer;

        public ProducerController( IProducer  producer)
        {
            _producer = producer;
        }
        public async Task<IActionResult> Index()
        {
            var Allproducer = await _producer.GetAll();
            return View(Allproducer);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {

            producer.ProfilePictureUrl = DocumentSettings.UploadFile(producer.Image, "Images");

            await _producer.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int id)
        {
            var ActorDetails = await _producer.GetById(id);
            if (ActorDetails is null)
                return BadRequest();
            return View(ActorDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ActorDetails = await _producer.GetById(id);
            //if (ActorDetails is null)
            //    return BadRequest();
            return View(ActorDetails);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Producer producer)
        {
            producer.ProfilePictureUrl = DocumentSettings.UploadFile(producer.Image, "Images");

            _producer.Update(id, producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ActorDetails = await _producer.GetById(id);
            if (ActorDetails is null)
                return BadRequest();
            return View(ActorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm([FromRoute] int id, Producer producer)
        {
            if (id != producer.Id)
                return BadRequest();
            try
            {
                //DocumentSettings.DeletFile("Images", producer.ProfilePictureUrl);
                await _producer.DeleteById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(producer);
            }

        }
    }
}
