using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using son_gun.DAL;
using son_gun.Models;
using System.Net;

namespace son_gun.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ExpertController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _environment;

        public ExpertController(AppDbContext appDbContext, IWebHostEnvironment environment)
        {
            _appDbContext = appDbContext;
            _environment = environment;

        }

        public IActionResult Index()
        {
            var data = _appDbContext.Experts.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Expert expert)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string path = _environment.WebRootPath + @"\Upload\";
            string filename = Guid.NewGuid() + expert.PhotoFile.FileName;
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {

                expert.PhotoFile.CopyTo(stream);

            }
            expert.ImgUrl = filename;
            _appDbContext.Experts.Add(expert);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Update(int id)
        {
            var data=await _appDbContext.Experts.FirstOrDefaultAsync(p=>p.Id == id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Expert expert)
        {
            if (!ModelState.IsValid)
            {
                return View(expert);
            }
            if (expert.PhotoFile != null)
            {
                string path = _environment.WebRootPath + @"\Upload\";
                string filename = Guid.NewGuid() + expert.PhotoFile.FileName;
                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {

                    expert.PhotoFile.CopyTo(stream);
                }
                expert.ImgUrl= filename;
            }
            _appDbContext.Experts.Update(expert);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult>Remove(int id)
        {
            var item = await _appDbContext.Experts.FirstOrDefaultAsync(p => p.Id == id);
            if (item != null)
            {
                _appDbContext.Remove(item);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
