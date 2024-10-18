using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        // _db retrieves all the Villa
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] //sends data to a server to create or update a resource
        public IActionResult Create(Villa obj)
        {
            _db.Villas.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
