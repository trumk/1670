using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using buoi4_SPCart.Data;
using buoi4_SPCart.Models;

namespace buoi4_SPCart.Controllers
{
    public class ProductsAdminController : Controller
    {
        private readonly buoi4_SPCartContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsAdminController(buoi4_SPCartContext db, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: ProductsAdmin
        public async Task<IActionResult> Index()
        {
            var product = await _db.Product.ToListAsync();
            return View(product);
        }   

        // GET: ProductsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Product == null)
            {
                return NotFound();
            }

            var product = await _db.Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductsAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Product employee = new Product
                {
                    Title = model.Title,
                    Detail = model.Detail,
                    Price = model.Price,
                    Picture = uniqueFileName,
                };
                _db.Add(employee);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            };
            return View();
        }

        // GET: ProductsAdmin/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _db.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            var productEdit = new Product
            {
                Title = product.Title,
                Detail = product.Detail,
                Price = product.Price,
            };

            return View(productEdit);
        }

        // POST: ProductsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _db.Product.FindAsync(id);

                    if (product == null)
                    {
                        return NotFound();
                    }

                    if (!string.IsNullOrEmpty(product.Picture))
                    {
                        var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, "images", product.Picture);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    string uniqueFileName = UploadedFile(model);

                    product.Title = model.Title;
                    product.Detail = model.Detail;
                    product.Price = model.Price;
                    product.Picture = uniqueFileName;

                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in Edit action: {ex.Message}");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: ProductsAdmin/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _db.Product.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                // Delete the profile picture file if it exists
                if (!string.IsNullOrEmpty(product.Picture))
                {
                    var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "images", product.Picture);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _db.Product.Remove(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteConfirmed action: {ex.Message}");
                TempData["ErrorMessage"] = "Error deleting product.";
                return RedirectToAction(nameof(Index));
            }
        }


        private string UploadedFile(Product model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
