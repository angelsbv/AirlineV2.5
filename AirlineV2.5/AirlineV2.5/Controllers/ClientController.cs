using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlineV2._5.Models;

namespace AirlineV2.Controllers
{
    public class ClientController : Controller
    {
        private readonly AirlineV2Context ctx;

        public ClientController(AirlineV2Context context)
        {
            ctx = context;
        }

        [Route("clientes"), Route("clientes/index")]
        public async Task<IActionResult> Index()
        {
            return View(await ctx.Clients.ToListAsync());
        }

        [Route("cliente/agregar")]
        public IActionResult AddCln()
        {
            return View();
        }

        [Route("cliente/agregar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCln([Bind("ClnID,ClnName,ClnLastName,ClnPhone,ClnEmail,ClnBirthdate")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.ClnRegisterDate = DateTime.Now;
                ctx.Add(client);
                await ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [Route("cliente/editar/{id}")]
        public async Task<IActionResult> EditCln(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await ctx.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [Route("cliente/editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCln(int id, [Bind("ClnID,ClnName,ClnLastName,ClnPhone,ClnEmail,ClnBirthdate,ClnRegisterDate")] Client client)
        {
            if (id != client.ClnID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ctx.Update(client);
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ctx.Clients.Any(e => e.ClnID == client.ClnID))
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
            return View(client);
        }

        [Route("cliente/eliminar/{id}")]
        [HttpGet]
        public async Task<IActionResult> DelCln(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await ctx.Clients
                .FirstOrDefaultAsync(m => m.ClnID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [Route("cliente/eliminar/{id}")]
        [HttpPost, ActionName("DelCln")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SBCDelCln(int id)
        {
            var client = await ctx.Clients.FindAsync(id);
            ctx.Clients.Remove(client);
            await ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
