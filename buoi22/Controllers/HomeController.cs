using buoi22.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using buoi22.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace buoi22.Controllers
{
    public class HomeController : Controller
    {
        private readonly buoi22Context dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(buoi22Context dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var employee = await dbContext.Employee.ToListAsync();
            return View(employee);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult New() { 
        return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Employee model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFilename = UploadedFile(model);
            }
            return View();
        }
        private string UploadedFile(Employee model)
        {
            string uniqueFileName = null;
            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}