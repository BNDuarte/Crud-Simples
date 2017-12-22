using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Crud.Data.Context;
using Microsoft.EntityFrameworkCore;
using Crud.Domain.Medicos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crud.Domain.Especialidades;

namespace Crud.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var medico = await _context.Medico.Include(e=>e.Especialidade).ToListAsync();
            return View(medico);
        }

        public IActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,EspecialidadeId")] Medico medico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(medico);
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
            return View(medico);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.SingleOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            PopularViewBag(medico);
            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Nome,EspecialidadeId")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id))
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
            return View(medico);
        }

        public async Task<ActionResult> Details(int id)
        {

            var medico = await _context.Medico.Include(e => e.Especialidade).SingleOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.SingleOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var medico = _context.Medico.SingleOrDefault(m => m.Id == id);
            _context.Medico.Remove(medico);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.Id == id);
        }

        private void PopularViewBag(Medico medico = null)
        {
            if (medico == null)
            {
                ViewBag.EspecialidadeId = new SelectList(_context.Especialidade.OrderBy(e => e.Descricao), "Id", "Descricao");
            }
            else
            {
                ViewBag.EspecialidadeId = new SelectList(_context.Especialidade.OrderBy(e => e.Descricao), "Id", "Descricao", medico.EspecialidadeId);
            }
        }
    }
}