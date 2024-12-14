using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
		private readonly IVillaRepository _villaRepo;
		public VillaController(IVillaRepository villaRepo)
		{
            _villaRepo = villaRepo; //dependency injection
        }
        public IActionResult Index()
        {
            var villas = _villaRepo.GetAll();
            return View(villas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] //sends data to a server to create or update a resource
        public IActionResult Create(Villa obj)
        {
            if(obj.Name==obj.Description)
            {
                ModelState.AddModelError("description","The description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _villaRepo.Add(obj);
                _villaRepo.Save();
                TempData["success"] = "The villa has been created successfully."; //notification
				return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }
        
        public IActionResult Update(int villaId)
        {
			
			Villa? obj = _villaRepo.Get(u => u.Id == villaId);
			
			if (obj == null)
            { 
                return RedirectToAction("Error","Home"); 
            }
			return View(obj);
		}

		[HttpPost]
		public IActionResult Update(Villa obj)
		{
			if (ModelState.IsValid && obj.Id>0)
			{
				_villaRepo.Update(obj);
                _villaRepo.Save();
                TempData["success"] = "The villa has been updated successfully."; //notification
                return RedirectToAction(nameof(Index));
			}
            TempData["error"] = "The villa could not be updated.";
            return View(obj);
		}

		public IActionResult Delete(int villaId)
		{

			Villa? obj = _villaRepo.Get(u => u.Id == villaId);

			if (obj is null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(obj);
		}

		[HttpPost] // endpoint for deleting a resource
		public IActionResult Delete(Villa obj)
		{
			Villa? objFromDb = _villaRepo.Get(u => u.Id == obj.Id);
			if (objFromDb is not null)
			{
				_villaRepo.Remove(objFromDb);
                _villaRepo.Save();
                TempData["success"] = "The villa has been deleted successfully.";

                return RedirectToAction(nameof(Index));
			}
			TempData["error"] = "The villa could not be deleted.";
            return View(obj);
		}

	}
}

