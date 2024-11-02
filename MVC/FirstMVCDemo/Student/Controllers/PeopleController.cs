using Azure.Core;
using FirstMVC.Data;
using FirstMVC.Models.Entities;
using FirstMVC.Ulities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;


namespace FirstMVC.Controllers
{
    public class PeopleController : Controller
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.Person.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoTen,Age")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult ImportDataFromXlsx(IFormFile file)
        {

            if(file!= null && file.Length > 0)
            {
                var people = new List<Person>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 
                using(var stream = file.OpenReadStream())
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for(int row = 2; row < rowCount; row++)
                    {
                        var person = new Person
                        {
                            //Id = int.Parse(worksheet.Cells[row,1].Text),
                            HoTen = worksheet.Cells[row,2].Text,
                            Age = int.Parse(worksheet.Cells[row,3].Text)
                        };
                        people.Add(person);
                    }
                }
                _context.Person.AddRangeAsync(people);
                _context.SaveChanges();
                return View("Index",people);
            }
            return View("Index");
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoTen,Age")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }

        // GET : Export Excel
        public async Task<IActionResult> ExportExcel()
        {
            
            var tenSheet = "Person";
            List<string> headerExcel = new List<string>
            {
                "Id",
                "Họ tên",
                "Tuổi",
                
            };
            var listPerson = _context.Person.Select(x => new List<string>
            {
                x.Id.ToString(),
                x.HoTen,
                x.Age.ToString(),
            }).ToList();

            ExcelPackage file = await Utilities.ExportExcel(tenSheet, headerExcel, listPerson, "A1", 3, 4);
            using (MemoryStream ms = new MemoryStream())
            {
                file.SaveAs(ms);
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Person.xlsx");
            }
        }
    }
}
