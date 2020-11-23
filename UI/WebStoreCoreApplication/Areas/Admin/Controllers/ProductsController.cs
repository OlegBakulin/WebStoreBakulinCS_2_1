using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStoreCoreApplicatioc.DAL;
using WebStoreCoreApplication.Domain.Entities;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace WebStoreCoreApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Boss, Admin, Manager")]
    public class ProductsController : Controller
    {
        private readonly WebStoreContext _context;

        public ProductsController(WebStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var webStoreContext = _context.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(await webStoreContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Id");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Order,CategoryId,BrandId,ImageUrl,Price,Id,Name")] Product product)
        {
            if (ModelState.IsValid)
            {

                #region LoadImage
                var path = ("wwwroot/images/product-details/"+product.ImageUrl);
                FileStream fs = new FileStream(path, FileMode.Create) ;


                /*if (!file.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = file.CreateText(path))
                    {
                        sw.WriteLine("Hello");
                        sw.WriteLine("And");
                        sw.WriteLine("Welcome");
                    }
                }*/
                var reclameContent = new byte[fs.Length];
                fs.Read(reclameContent, 0, Convert.ToInt32(fs.Length));


                var myconnection = new SqlConnection();
                SqlCommand comm = myconnection.CreateCommand();
                comm.CommandType = CommandType.StoredProcedure;
                /*SqlConnection sqlConnection = new SqlConnection($"../wwwroot/images/product - details/{product.ImageUrl}");
                comm.Connection = sqlConnection;*/
                comm.CommandText = $"~/wwwroot/images/product-details/{product.ImageUrl}";
                comm.Parameters.AddWithValue($"~/wwwroot/images/product-details/{product.ImageUrl}", reclameContent);
                fs.Close();
                //comm.ExecuteNonQuery();
                
                #endregion


                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Id", product.CategoryId);
            return View(product);
        }

        

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Order,CategoryId,BrandId,ImageUrl,Price,Id,Name")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
