using System.Linq;
using System.Threading.Tasks;
using Crud.Data.Context;
using Crud.Domain.Consultas;
using Crud.Domain.Pacientes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crud.Web.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var consulta = await _context.Consulta.Include(p=>p.Paciente).Include(m=>m.Medico).ToListAsync();
            return View(consulta);
        }

        public IActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoId,PacienteId,DataConsulta")] Consulta consulta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(consulta);
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
            return View(consulta);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta.SingleOrDefaultAsync(m => m.Id == id);
            if (consulta == null)
            {
                return NotFound();
            }

            PopularViewBag(consulta);
            return View(consulta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,MedicoId,PacienteId,DataConsulta")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.Id))
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
            return View(consulta);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta.SingleOrDefaultAsync(m => m.Id == id);
            if (consulta == null)
            {
                return NotFound();
            }
            return View(consulta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var consulta = _context.Consulta.SingleOrDefault(m => m.Id == id);
            _context.Consulta.Remove(consulta);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consulta.Any(e => e.Id == id);
        }

        private void PopularViewBag(Consulta consulta = null)
        {
            if (consulta == null)
            {
                ViewBag.PacienteId = new SelectList(_context.Paciente.OrderBy(e => e.Nome), "Id", "Nome");
                ViewBag.MedicoId = new SelectList(_context.Medico.OrderBy(e => e.Nome), "Id", "Nome");
            }
            else
            {
                ViewBag.PacienteId = new SelectList(_context.Paciente.OrderBy(e => e.Nome), "Id", "Nome", consulta.PacienteId);
                ViewBag.MedicoId = new SelectList(_context.Medico.OrderBy(e => e.Nome), "Id", "Nome", consulta.MedicoId);
            }
        }
    }
}