using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Application.Common.Interfaces;

namespace WhiteLagoon.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // _db retrieves all the Villa
        public IActionResult Index()
        {
			var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }
        public IActionResult Create()
        {
			AmenityVM amenityVM = new()
			{
				VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				})
			};
			return View(amenityVM);
		}
        [HttpPost] //sends data to a server to create or update a resource
        public IActionResult Create(AmenityVM obj)
        {

			if(ModelState.IsValid)
			{
				_unitOfWork.Amenity.Add(obj.Amenity);
				_unitOfWork.Save();
				TempData["success"] = "The amenity has been added successfully.";
				return RedirectToAction(nameof(Index));
			}
            
			obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			return View(obj);
        }

		public IActionResult Update(int amenityId)
		{
			AmenityVM amenityVM = new()
			{
				VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
			};

			if (amenityVM.Amenity == null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(amenityVM); // Pass the ViewModel to the view
		}


		[HttpPost]
		public IActionResult Update(AmenityVM amenityVM)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been updated successfully.";
				return RedirectToAction(nameof(Index));
			}

			amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			return View(amenityVM);
		}

		public IActionResult Delete(int amenityId)
		{
			AmenityVM amenityVM = new()
			{
				VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
			};

			if (amenityVM.Amenity == null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(amenityVM); 
		}
		[HttpPost] 
		public IActionResult Delete(AmenityVM amenityVM)
		{
			Amenity? objFromDb = _unitOfWork.Amenity.Get(u => u.Id == amenityVM.Amenity.Id);
			if (objFromDb is not null)
			{
				_unitOfWork.Amenity.Remove(objFromDb);
				_unitOfWork.Save();
				TempData["success"] = "The amenity has been deleted successfully.";

                return RedirectToAction(nameof(Index));
			}
			TempData["error"] = "The amenity could not be deleted.";
            return View();
		}

	}
}