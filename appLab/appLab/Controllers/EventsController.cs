using appLab.Data;
using appLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace appLab.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationContext _context;

        public EventsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var applicationContext = _context.Events.Include(e => e.Client);
            int count = await applicationContext.CountAsync();
            var items = await applicationContext.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pageView = new PageViewModel
            {
                PageCount = (int) Math.Ceiling(count / (double) pageSize),
                Items = items
            };
            return View(pageView);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            GetSelectListItemsClients();
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]            
        public async Task<IActionResult> Create(Event @event,string clientId)
        {
            @event.ClientId = int.Parse(clientId);
                
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            GetSelectListItemsClients();
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            GetSelectListItemsClients();
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            GetSelectListItemsClients();
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public JsonResult GetEvents() => Json(_context.Events.Select((e) => 
        $"{e.DurationDateEvent.Date}<//n>{e.DurationTimeEvent.ToLongTimeString()}<//n>{e.NameEvent}<//n>{e.DesctiptionEvent}" +
             $"<//n>{e.Client.LastName}<//n>{e.Client.FirstName}<//n>{e.Client.Patronymic}<//n>{e.Client.IdendificationNumber} "));
        

     

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        private void GetSelectListItemsClients()
        {
            List<SelectListItem> dropDownList = new List<SelectListItem>();
            foreach (var client in _context.Clients)
            {
                dropDownList.Add(new SelectListItem()
                {
                    Text = $"{client.LastName} {client.FirstName} {client.Patronymic} {client.IdendificationNumber}",
                    Value = client.Id.ToString()
                });
            }
            ViewData.Add("DropDownList", dropDownList);
        }

         
    }
}
