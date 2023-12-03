using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainee_Details.ViewModels;
using Trainee_Details.ViewModels.Input;
using System.Drawing;
using X.PagedList;
using Trainee_Details.Models;

namespace Trainee_Details.Controllers
{
    public class TraineesController : Controller
    {
        private readonly TraineeDbContext db;
        private readonly IWebHostEnvironment env;
        public TraineesController(TraineeDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            return View(await db.Trainees.OrderBy(x => x.TraineeId).Include(x => x.Courses).ToPagedListAsync(pg, 5));
        }
        public async Task<IActionResult> Aggregates()
        {
            var data = await db.Courses.Include(x => x.Trainee)
                .ToListAsync();
            return View(data);
        }
        public IActionResult Grouping()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Grouping(string groupby)
        {

            if (groupby == "TraineeName")
            {
                var data = db.Courses.Include(x => x.Trainee)
               .ToList()
               .GroupBy(x => x.Trainee?.TraineeName)
               .Select(g => new GroupedData { Key = g.Key, Data = g })
               .ToList();

                return View("GroupingResult", data);
            }
            if (groupby == "year month")
            {
                var data = db.Courses.Include(x => x.Trainee)
                    .OrderByDescending(x => x.AdmissionDate)
               .ToList()
               .GroupBy(x => $"{x.AdmissionDate:MMM, yyyy}")
               .Select(g => new GroupedData { Key = g.Key, Data = g })
               .ToList();

                return View("GroupingResult", data);
            }
            if (groupby == "count")
            {
                var data = db.Courses.Include(x => x.Trainee)
                    .OrderByDescending(x => x.AdmissionDate)
               .ToList()
                .GroupBy(x => x.Trainee?.TraineeName)
               .Select(g => new GroupedDataPrimitive<int> { Key = g.Key, Data = g.Count() })
               .ToList();

                return View("GroupingResultPrimitive", data);
            }

            return RedirectToAction("Grouping");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TraineeInputModel model)
        {
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlInterpolatedAsync($"EXEC InsertTrainee {model.TraineeName}, {model.Age}, {(int)model.Gender}, {model.Picture}, {(model.IsRegular ? 1 : 0)}");
                var id = await db.Trainees.MaxAsync(x => x.TraineeId);
                foreach (var s in model.Courses)
                {
                    await db.Database.ExecuteSqlInterpolatedAsync($"EXEC InsertCourse {s.CourseName}, {s.CourseFee}, {s.AdmissionDate}, {id}");
                }
                return Json(new { success = true });

            }
            return Json(new { success = true });
        }
        public IActionResult GetCoursesForm()
        {
            return PartialView("_CoursesForm");
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
            string savePath = Path.Combine(env.WebRootPath, "Pictures", fileName);
            FileStream fs = new FileStream(savePath, FileMode.Create);
            await file.CopyToAsync(fs);
            fs.Close();
            return Json(new { name = fileName });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await db.Trainees.FirstOrDefaultAsync(x => x.TraineeId == id);
            if (data == null) return NotFound();
            return View(new TraineeEditModel
            {
                TraineeId = data.TraineeId,
                TraineeName = data.TraineeName,
                Age = data.Age,
                Gender = data.Gender ?? Gender.Male,
                IsRegular = data.IsRegular ?? false

            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TraineeEditModel model)
        {
            if (ModelState.IsValid)
            {
                var tr = await db.Trainees.FirstOrDefaultAsync(x => x.TraineeId == model.TraineeId);
                if (tr == null) return NotFound();
                tr.TraineeId = model.TraineeId;
                tr.TraineeName = model.TraineeName;
                tr.Age = model.Age;
                tr.Gender = model.Gender;
                tr.IsRegular = model.IsRegular;

                if (model.Picture != null)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(env.WebRootPath, "Pictures", fileName);
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    await model.Picture.CopyToAsync(fs);
                    tr.Picture = fileName;
                    fs.Close();
                }
                db.Database.ExecuteSqlInterpolated($"EXEC UpdateTrainee {tr.TraineeId}, {tr.TraineeName}, {tr.Age}, {(int)tr.Gender}, {tr.Picture}, {(model.IsRegular ? 1 : 0)}");
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            db.Database.ExecuteSqlInterpolated($"EXEC DeleteTrainee {id}");
            return Json(new { success = true, id });
        }
    }
}













































































































































































































































































































































































































































































































































