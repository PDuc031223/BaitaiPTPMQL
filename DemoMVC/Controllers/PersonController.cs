using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Models.Process;

namespace MvcMovie.Controllers
{

    public class PersonController(ApplicationDbContext context) : Controller
    {

        private readonly ApplicationDbContext _context = context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public async Task<IActionResult> Index()
        {
            var model = await _context.Persons.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,Fullname,Address")]Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);

        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            } 
            return View(person);                                       
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(string id, [Bind("PersonId,FUllname,Address")]Person person)
        {
            if(id != person.PersonId)
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
                    if(!PersonExitsts(person.PersonId))
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

        private bool PersonExitsts(string personId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult>Delete(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return (_context.Persons?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    
     public async Task<IActionResult> Upload()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file!=null)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension !=".xlx" && fileExtension != ".xlsx")
            {
                ModelState.AddModelError("","Please choose excel file to upload!");
            }
            else 
            {
                var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excel", fileName);
                var filelocation = new FileInfo(filePath).ToString();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    var dt = _excelProcess.ExcelToDataTable(filelocation);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       var ps = new Person();
                       ps.PersonId = dt.Rows[i][0].ToString();
                       ps.FullName = dt.Rows[i][1].ToString();
                       ps.Address  = dt.Rows[i][2].ToString();
                       _context.Add(ps);
                        
                    }
                    await _context.SaveChangesAsync();
                    return RediRectToAction(nameof(Index));
                }
            }
        }
        return View();
    }

        private IActionResult RediRectToAction(string v)
        {
            throw new NotImplementedException();
        }
    }
}

    
       