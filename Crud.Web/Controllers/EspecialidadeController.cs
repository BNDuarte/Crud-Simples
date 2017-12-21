using System.Linq;
using System.Threading.Tasks;
using Crud.Data.Context;
using Crud.Domain.Especialidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud.Web.Controllers
{
    public class EspecialidadeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EspecialidadeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var especialidade = await _context.Especialidade.ToListAsync();
            return View(especialidade);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao")] Especialidade especialidade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(especialidade);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(especialidade);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidade = await _context.Especialidade.SingleOrDefaultAsync(m => m.Id == id);
            if (especialidade == null)
            {
                return NotFound();
            }

            return View(especialidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Descricao")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadeExists(especialidade.Id))
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
            return View(especialidade);
        }

        public async Task<ActionResult> Details(int id)
        {

            var especialidade = await _context.Especialidade.SingleOrDefaultAsync(m => m.Id == id);
            if (especialidade == null)
                return NotFound();

            return View(especialidade);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidade = await _context.Especialidade.SingleOrDefaultAsync(m => m.Id == id);
            if (especialidade == null)
            {
                return NotFound();
            }
            return View(especialidade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var especialidade = _context.Especialidade.SingleOrDefault(m => m.Id == id);
            _context.Especialidade.Remove(especialidade);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadeExists(int id)
        {
            return _context.Especialidade.Any(e => e.Id == id);
        }
    }
}