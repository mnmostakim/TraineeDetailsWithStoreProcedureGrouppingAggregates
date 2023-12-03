using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainee_Details.Models;

namespace Trainee_Details.Controllers
{
    public class CoursesController : Controller
    {
        private readonly TraineeDbContext db;
        public CoursesController(TraineeDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int id)
        {
            ViewBag.TraineeId = id;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course model)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlInterpolated($"EXEC InsertCourse {model.CourseName},{model.CourseFee}, {model.AdmissionDate}, {model.TraineeId}");
                return RedirectToAction("Index", "Trainees");

            }
            ViewBag.Trainees = db.Trainees.ToList();
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var data = db.Courses.FirstOrDefault(x => x.CourseId == id);
            if (data == null) { return NotFound(); }
            ViewBag.Trainees = db.Trainees.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Course model)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlInterpolated($"EXEC UpdateCourse {model.CourseId},{model.CourseName}, {model.CourseFee}, {model.AdmissionDate}, {model.TraineeId}");
                return RedirectToAction("Index", "Trainees");

            }
            ViewBag.Trainees = db.Trainees.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            db.Database.ExecuteSqlInterpolated($"EXEC DeleteCourse {id}");
            return Json(new { success = true, id });
        }
    }
}
