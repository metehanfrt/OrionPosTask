using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrionPosTask.Models.Data;

namespace OrionPosTask.Controllers
{
    public class DirectorysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DirectorysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Directorys
        //public async Task<IActionResult> Index()
        //{
        //    var items = await _context.Directorys.OrderByDescending(item => item.Id).ToListAsync();
        //    return View(items);
        //}

        public async Task<IActionResult> Index(int? page, string searchName, string searchSurname, string searchTelephone)
        {
            int pageSize = 5;

            var query = from p in _context.Directorys where !p.IsDeleted select p; // Sadece silinmemiş olanları seç

            int pageNumber = page ?? 1;

            if (!String.IsNullOrEmpty(searchName))
            {
                query = query.Where(e => e.Name.Contains(searchName)); // SELECT * FROM Directorys WHERE Name LIKE '%searchName%'

                ViewBag.searchName = searchName;
            }
            if (!String.IsNullOrEmpty(searchSurname))
            {
                query = query.Where(e => e.Surname.Contains(searchSurname)); // SELECT * FROM Directorys WHERE Name LIKE '%searchName%' AND Surname LIKE '%searchSurname%'

                ViewBag.searchSurname = searchSurname;
            }
            if (!String.IsNullOrEmpty(searchTelephone))
            {
                query = query.Where(e => e.Telephone.Contains(searchTelephone)); // SELECT * FROM Directorys WHERE Name LIKE '%searchName%' AND Surname LIKE '%searchSurname%' AND Telephone LIKE '%searchTelephone%'

                ViewBag.searchTelephone = searchTelephone;
            }

            query = query.OrderByDescending(e => e.Id); // Sıralama yapılıyor

            var filtered = await query.ToListAsync(); // Filtrelenmiş veriler alınıyor

            var itemList = await _context.Directorys.OrderByDescending(item => item.Id).ToListAsync(); // Asenkron işlem bekleniyor

            ViewBag.itemList = itemList.Select(item => new SelectListItem
            {
                Text = $"{item.Name} {item.Surname}", // Text alanına örnek bir değer atanıyor, burada isim ve soyisim birleştirilerek gösteriliyor
                Value = item.Id.ToString() // Value alanı, seçilen öğenin değeri olarak atanıyor, bu örnek olarak Id kullanılmış
            });

            return View(filtered); // Filtrelenmiş veriler View'a gönderiliyor
        }


        // GET: Directorys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorys = await _context.Directorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorys == null)
            {
                return NotFound();
            }

            return View(directorys);
        }

        // GET: Directorys/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Telephone,Id")] Directorys directorys)
        {
            if (ModelState.IsValid)
            {
                if (_context.Directorys.Any(d => d.Telephone == directorys.Telephone))
                {
                    ModelState.AddModelError("directorys.Telephone", "Bu telefon numarası zaten kayıtlı.");
                    return View(directorys);
                }

                // CreatedBy alanını mevcut kullanıcı adıyla doldurun
                directorys.CreatedBy = User.Identity.Name; 
                directorys.CreatedDate = DateTime.UtcNow; 

                _context.Add(directorys);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(directorys);
        }



        // GET: Directorys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorys = await _context.Directorys.FindAsync(id);
            if (directorys == null)
            {
                return NotFound();
            }
            return View(directorys);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Surname,Telephone,Id")] Directorys directorys)
        {
            if (id != directorys.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directorys);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorysExists(directorys.Id))
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
            return View(directorys);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorys = await _context.Directorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorys == null)
            {
                return NotFound();
            }

            return View(directorys);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var directorys = await _context.Directorys.FindAsync(id);
            if (directorys != null)
            {
                directorys.IsDeleted = true; // Veriyi silinmiş olarak işaretle
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DirectorysExists(int id)
        {
            return _context.Directorys.Any(e => e.Id == id);
        }
    }
}
