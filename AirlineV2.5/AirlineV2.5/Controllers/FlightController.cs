using AirlineV2._5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.Controllers
{
    public class FlightController : Controller
    {
        private readonly AirlineV2Context ctx;
        public FlightController(AirlineV2Context context)
        {
            ctx = context;
        }
        [Route("vuelos"), Route("vuelos/index")]
        public async Task<IActionResult> Index()
        {
            var model = ctx.Flights.Include(f => f.aircraft);
            return View(await model.ToListAsync());
        }

        [Route("vuelos/agregar")]
        [HttpGet]
        public IActionResult AddFlg()
        {
            ViewData["AcID"] = new SelectList(ctx.Aircrafts, "AcID", "AcModel");
            return View();
        }

        [Route("vuelos/agregar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFlg(Flight vmFr)
        {
            if (ModelState.IsValid)
            {
                ctx.Flights.Add(vmFr);
                await ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcID"] = new SelectList(ctx.Aircrafts, "AcID", "AcModel", vmFr.AcID);
            return View(vmFr);
        }

        [Route("vuelos/editar/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditFlg(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await ctx.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AcID"] = new SelectList(ctx.Aircrafts, "AcID", "AcModel", flight.AcID);
            return View(flight);
        }

        [Route("vuelos/editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFlg(int id, [Bind("FlgID,AcID,FlgDeparture,FlgArrival,FlgFare,FlgCategory")] Flight flight)
        {
            if (id != flight.FlgID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ctx.Update(flight);
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ctx.Flights.Any(e => e.FlgID == flight.FlgID))
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
            ViewData["AcID"] = new SelectList(ctx.Aircrafts, "AcID", "AcModel", flight.AcID);
            return View(flight);
        }

        [Route("vuelos/eliminar/{id}")]
        [HttpGet]
        public async Task<IActionResult> DelFlg(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await ctx.Flights
                .Include(f => f.aircraft)
                .FirstOrDefaultAsync(m => m.FlgID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        [Route("vuelos/eliminar/{id}")]
        [HttpPost, ActionName("DelFlg")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SBCDelFlg(int id)
        {
            var flight = await ctx.Flights.FindAsync(id);
            ctx.Flights.Remove(flight);
            await ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //summary:
        //PASAJEROS
        //psngrs => passengers
        [Route("vuelo/pasajeros/{id}")]
        [HttpGet]
        public async Task<IActionResult> Passengers(int id)//id del vuelo
        {
            var psngrs = ctx.ClnFlgRels.Where(e => e.FlgID == id)
                                       .Include(f => f.client)
                                       .Include(f => f.flight);
            ViewData["FlgID"] = id;
            return View(await psngrs.ToListAsync());

        }
        [Route("vuelo/pasajeros/agregar/{FlgID}")]
        [HttpGet]
        public IActionResult AddPassenger(int FlgID)//id del vuelo
        {
            ViewData["ClnID"] = new SelectList(ctx.Clients, "ClnID", "ClnName");
            ViewData["FlgID"] = FlgID;
            return View();
        }

        [Route("vuelo/pasajeros/agregar/{FlgID}")]
        [HttpPost]
        public async Task<IActionResult> AddPassenger(int id, [Bind("ClnID, FlgID")] ClnRelFlg rel)//id del vuelo
        {
            if (ModelState.IsValid)
            {
                bool oneZ = (from i in ctx.ClnFlgRels
                          where i.ClnID == rel.ClnID && i.FlgID == rel.FlgID
                          select i).Count() >= 1;
                if (oneZ)
                    ModelState.AddModelError("oneZ", "Este pasajero ya se encuentra en este vuelo.");
                else
                {
                    ctx.ClnFlgRels.Add(rel); await ctx.SaveChangesAsync();
                    return RedirectToAction
                        (
                            nameof(Passengers), 
                            new { id = rel.FlgID }
                        );
                }
            }
            ViewData["FlgID"] = rel.FlgID;
            ViewData["ClnID"] = new SelectList(ctx.Clients, "ClnID", "ClnName");
            return View();
        }

        [Route("vuelo/pasajeros/eliminar/{ClnFlgID}/{FlgID}")]
        [HttpGet]
        public async Task<IActionResult> DelPassenger(int ClnFlgID, int FlgID)
        {
            var rel = await ctx.ClnFlgRels.FindAsync(ClnFlgID);
            ctx.ClnFlgRels.Remove(rel);
            await ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Passengers), new { id = FlgID });
        }
    }
}
