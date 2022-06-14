using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_commerce.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace E_commerce.Controllers
{
    public class AProductController : Controller
    {
        private readonly Models.AppContext _context = new Models.AppContext();
        private readonly IHostingEnvironment hostingEnvironment;

        public AProductController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }


        // GET: AProduct
        public async Task<IActionResult> Index()
        {
            return View(await _context.products.ToListAsync());
        }

        // GET: AProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: AProduct/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Product newProduct = new Product()
                {
                    Name = model.Name,
                    Cost = model.Cost,
                    stock = model.stock,
                    dateTime = model.dateTime,
                    Description = model.Description,
                    Image = uniqueFileName
                };

                _context.products.Add(newProduct);
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        // GET: AProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.products.FindAsync(id);
            CreateViewModel data = new CreateViewModel
            {
                Name = product.Name,
                Cost = product.Cost,
                dateTime = product.dateTime,
                Description = product.Description,
                Id = product.Id,
                stock = product.stock
            };
            if (product == null)
            {
                return NotFound();
            }
            return View(data);
        }

        // POST: AProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = null;
                    if (model.Photo != null)
                    {

                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                    Product _product = await _context.products.FindAsync(id);
                    string upFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    string image_path = Path.Combine(upFolder, _product.Image);
                    System.IO.File.Delete(image_path);
                    _context.ChangeTracker.Clear();

                    Product newProduct = new Product()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Cost = model.Cost,
                        Image = uniqueFileName,
                        stock = model.stock,
                        dateTime = model.dateTime,
                        Description = model.Description
                    };
                    _context.Update(newProduct);
                    await _context.SaveChangesAsync();

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: AProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: AProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.products.FindAsync(id);

            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            string filePath = Path.Combine(uploadsFolder, product.Image);
            System.IO.File.Delete(filePath);

            _context.products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.products.Any(e => e.Id == id);
        }
    }
}
