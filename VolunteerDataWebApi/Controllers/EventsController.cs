using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VolunteerDataWebApi.Models;

namespace VolunteerDataWebApi.Controllers
{
    public class EventsController : ApiController
    {
        private VolunteerDataWebApiContext db = new VolunteerDataWebApiContext();

        // GET: api/Events
        /// <summary>
        /// Get all scheduled events.
        /// </summary>
        /// <returns>IQueryable of all scheduled events.</returns>
        public IQueryable<Event> GetEvents()
        {
            return db.Events;
        }

        // GET: api/Events/5
        /// <summary>
        /// Get event data by ID.
        /// </summary>
        /// <param name="id">ID of the event to get.</param>
        /// <returns>Event data matching the ID.</returns>
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            
            return Ok(@event);
        }

        // GET: api/Events?startDate=2016-07-15T00:00:00&endDate=2016-08-30T23:59:59
        /// <summary>
        /// Get event data within the specified date-time range. 
        /// </summary>
        /// <param name="startDate">Start date-time of the range to match, inclusive. If using dates only, this includes the entire start day.</param>
        /// <param name="endDate">Stop date-time of the range to match, exclusive. If using dates only, this excludes the entire end day.</param>
        /// <returns>IQueryable of events withing the specified date-time range, if any.</returns>
        [ResponseType(typeof(IQueryable<Event>))]
        public async Task<IHttpActionResult> GetEvent(DateTime startDate, DateTime endDate)
        {
            var @event = await db.Events.Where(p => p.StartDateTime >= startDate && p.StartDateTime < endDate).ToListAsync();
            if (@event == null || @event.Count == 0)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/Events/5
        /// <summary>
        /// Update the event entry by ID. 
        /// </summary>
        /// <param name="id">ID of the event entry to update.</param>
        /// <param name="event">Updated event data to save.</param>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Events
        /// <summary>
        /// Add a new event.
        /// </summary>
        /// <param name="event">Event data to add.</param>
        /// <returns>Event data that was added.</returns>
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(@event);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        /// <summary>
        /// Delete an event by ID.
        /// </summary>
        /// <param name="id">ID of the event entry to delete.</param>
        /// <returns>Event data that was deleted.</returns>
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            await db.SaveChangesAsync();

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }
    }
}