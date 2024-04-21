using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;
using ShopAPI.Repository;

namespace ShopAPI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly PostgresContext _context;
        private readonly IRepository<Product> _productRepository; // Добавленный репозиторий 

        public ProductsController(PostgresContext context, IRepository<Product> productRepository) // Добавленный репозиторий 
        {
            _context = context;
            _productRepository = productRepository; // Добавленный репозиторий 
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //var postgresContext = _context.Products.Include(p => p.Image).Include(p => p.Supplier);
            //return View(await postgresContext.ToListAsync());

            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products
            //    .Include(p => p.Image)
            //    .Include(p => p.Supplier)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var product = await _productRepository.GetByIdAsync(id.Value);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Price,AvailableStock,LastUpdateDate,SupplierId,ImageId")] Product product)
        {
            if (ModelState.IsValid)
            {
                //product.Id = Guid.NewGuid();
                //_context.Add(product);
                //await _context.SaveChangesAsync();
               
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", product.ImageId);
            //ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", product.SupplierId);
            // Повторно загрузите нужные данные для списков, если это необходимо
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", product.ImageId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Category,Price,AvailableStock,LastUpdateDate,SupplierId,ImageId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(product);
                    //await _context.SaveChangesAsync();
                    await _productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", product.ImageId);
            //ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Image)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var product = await _context.Products.FindAsync(id);
            //if (product != null)
            //{
            //    _context.Products.Remove(product);
            //}

            //await _context.SaveChangesAsync();
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
