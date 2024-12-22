using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using WhiteLagoon.Application.Common.Utility;

namespace WhiteLagoon.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // _db retrieves all the Villa
        public IActionResult Index()
        {
			var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");
            return View(villaNumbers);
        }
        public IActionResult Create()
        {
			VillaNumberVM villaNumberVM = new()
			{
				VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
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
			bool roomNumberExists = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

			if(ModelState.IsValid && !roomNumberExists)
			{
				_unitOfWork.VillaNumber.Add(obj.VillaNumber);
				_unitOfWork.Save();
				TempData["success"] = "The villa number has been added successfully.";
				return RedirectToAction(nameof(Index));
			}
            if(roomNumberExists)
            {
                TempData["error"] = "The villa number already exists.";
			}
			obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
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
				VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
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
				_unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa number has been updated successfully.";
				return RedirectToAction(nameof(Index));
			}

			villaNumberVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
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
				VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
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
			VillaNumber? objFromDb = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
			if (objFromDb is not null)
			{
				_unitOfWork.VillaNumber.Remove(objFromDb);
				_unitOfWork.Save();
				TempData["success"] = "The villa number has been deleted successfully.";

                return RedirectToAction(nameof(Index));
			}
			TempData["error"] = "The villa number could not be deleted.";
            return View();
		}

	}
}