﻿using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        // _db retrieves all the Villa
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.ToList();
            return View(villaNumbers);
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
                _db.Villas.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        
        public IActionResult Update(int villaId)
        {
			
			Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
			
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
				_db.Villas.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "The villa has been updated successfully."; //notification
                return RedirectToAction("Index");
			}
            TempData["error"] = "The villa could not be updated.";
            return View(obj);
		}

		public IActionResult Delete(int villaId)
		{

			Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);

			if (obj is null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(obj);
		}

		[HttpPost] // endpoint for deleting a resource
		public IActionResult Delete(Villa obj)
		{
			Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);
			if (objFromDb is not null)
			{
				_db.Villas.Remove(objFromDb);
				_db.SaveChanges();
				TempData["success"] = "The villa has been deleted successfully.";

                return RedirectToAction("Index");
			}
			TempData["error"] = "The villa could not be deleted.";
            return View(obj);
		}

	}
}

