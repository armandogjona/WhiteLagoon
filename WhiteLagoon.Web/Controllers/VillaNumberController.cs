using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        // _db retrieves all the Villa
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.Include(u => u.Villa).ToList();
            return View(villaNumbers);
        }
        public IActionResult Create()
        {
			VillaNumberVM villaNumberVM = new()
			{
				VillaList = _db.Villas.ToList().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				})
			};
			return View(villaNumberVM);
		}
        [HttpPost] //sends data to a server to create or update a resource
        public IActionResult Create(VillaNumberVM obj)
        {
			bool roomNumberExists = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

			if(ModelState.IsValid && !roomNumberExists)
			{
				_db.VillaNumbers.Add(obj.VillaNumber);
				_db.SaveChanges();
				TempData["success"] = "The villa number has been added successfully.";
				return RedirectToAction(nameof(Index));
			}
            if(roomNumberExists)
            {
                TempData["error"] = "The villa number already exists.";
			}
			obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			return View(obj);
        }

		public IActionResult Update(int villaNumberId)
		{
			VillaNumberVM villaNumberVM = new()
			{
				VillaList = _db.Villas.ToList().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				VillaNumber = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
			};

			if (villaNumberVM.VillaNumber == null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(villaNumberVM); // Pass the ViewModel to the view
		}


		[HttpPost]
		public IActionResult Update(VillaNumberVM villaNumberVM)
		{
			if (ModelState.IsValid)
			{
				_db.VillaNumbers.Update(villaNumberVM.VillaNumber);
				_db.SaveChanges();
				TempData["success"] = "The villa number has been updated successfully.";
				return RedirectToAction(nameof(Index));
			}

			villaNumberVM.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			return View(villaNumberVM);
		}

		public IActionResult Delete(int villaNumberId)
		{
			VillaNumberVM villaNumberVM = new()
			{
				VillaList = _db.Villas.ToList().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				VillaNumber = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
			};

			if (villaNumberVM.VillaNumber == null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(villaNumberVM); 
		}
		[HttpPost] 
		public IActionResult Delete(VillaNumberVM villaNumberVM)
		{
			VillaNumber? objFromDb = _db.VillaNumbers
				.FirstOrDefault(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
			if (objFromDb is not null)
			{
				_db.VillaNumbers.Remove(objFromDb);
				_db.SaveChanges();
				TempData["success"] = "The villa number has been deleted successfully.";

                return RedirectToAction(nameof(Index));
			}
			TempData["error"] = "The villa number could not be deleted.";
            return View();
		}

	}
}