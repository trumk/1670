using buoi22.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using buoi22.Data;
using buoi22.ViewModels;
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
        public IActionResult New()
        { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(EmployeeViewModel model)
        //public async Task<IActionResult> New(Employee model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Employee employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    //FullName = model.FirstName + " " + model.LastName,
                    Gender = model.Gender,
                    Age = model.Age,
                    Salary = model.Salary,
                    //ProfilePicture = model.ProfilePicture
                    ProfilePicture = uniqueFileName,
                };
                dbContext.Add(employee);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            };
            return View();
        }
        public IActionResult Edit(int id)
        {
            // Lấy thông tin của Employee từ cơ sở dữ liệu bằng Id
            var employee = dbContext.Employee.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Map dữ liệu từ Employee sang EmployeeViewModel
            var employeeViewModel = new EmployeeViewModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                Gender = employee.Gender,
                Salary = employee.Salary
                // Không cần map ProfileImage vì bạn chỉ muốn sửa thông tin văn bản
            };

            return View(employeeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tìm Employee cần chỉnh sửa trong cơ sở dữ liệu
                    var employee = await dbContext.Employee.FindAsync(id);

                    if (employee == null)
                    {
                        // Trả về NotFound nếu không tìm thấy Employee
                        return NotFound();
                    }

                    // Xóa hình ảnh cũ trước khi tải lên hình ảnh mới
                    if (!string.IsNullOrEmpty(employee.ProfilePicture))
                    {
                        var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, "image", employee.ProfilePicture);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Gọi hàm UploadedFile để xử lý tệp tải lên và lấy tên tệp duy nhất
                    string uniqueFileName = UploadedFile(model);

                    // Cập nhật thông tin Employee từ dữ liệu mới
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Gender = model.Gender;
                    employee.Age = model.Age;
                    employee.Salary = model.Salary;
                    employee.ProfilePicture = uniqueFileName;

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    await dbContext.SaveChangesAsync();

                    // Chuyển hướng đến action Index
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi
                    Console.WriteLine($"Error in Edit action: {ex.Message}");
                    return View(model);
                }
            }

            // Trả về View nếu ModelState không hợp lệ
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            // Find the employee by id
            var employee = await dbContext.Employee.FindAsync(id);

            if (employee == null)
            {
                // If employee not found, return NotFound
                return NotFound();
            }

            try
            {
                // Remove the employee from the DbContext
                dbContext.Employee.Remove(employee);

                // Delete the profile picture file if it exists
                if (!string.IsNullOrEmpty(employee.ProfilePicture))
                {
                    var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "image", employee.ProfilePicture);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Save changes to the database
                await dbContext.SaveChangesAsync();

                // Redirect to the Index action after successful deletion
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the error if an exception occurs
                Console.WriteLine($"Error in Delete action: {ex.Message}");

                // Redirect to the Index action with an error message
                TempData["ErrorMessage"] = "Error deleting employee.";
                return RedirectToAction(nameof(Index));
            }
        }


        private string UploadedFile(EmployeeViewModel model)
        //private string UploadedFile(Employee model) 
        {
            string uniqueFileName = null;
            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "image");
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