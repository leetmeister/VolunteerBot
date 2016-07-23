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
    public class VolunteerIntentsController : ApiController
    {
        private VolunteerDataWebApiContext db = new VolunteerDataWebApiContext();

        // GET: api/VolunteerIntents
        /// <summary>
        /// Get all volunteeer intent entries.
        /// </summary>
        /// <returns>IQueryable of all volunteer intent entries.</returns>
        public IQueryable<VolunteerIntent> GetVolunteerIntents()
        {
            return db.VolunteerIntents;
        }

        // GET: api/VolunteerIntents/5
        /// <summary>
        /// Get a volunteer intent entry by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer intent entry to get.</param>
        /// <returns>Volunteer intent entry.</returns>
        [ResponseType(typeof(VolunteerIntent))]
        public async Task<IHttpActionResult> GetVolunteerIntent(int id)
        {
            VolunteerIntent volunteerIntent = await db.VolunteerIntents.FindAsync(id);
            if (volunteerIntent == null)
            {
                return NotFound();
            }

            return Ok(volunteerIntent);
        }

        // GET: api/VolunteerIntents?volunteerId=3
        /// <summary>
        /// Get the collection of volunteer intents by ID of the volunteer.
        /// </summary>
        /// <param name="volunteerId">ID of volunteer to get the intents for.</param>
        /// <returns>IQueryable of volunteer intents for the specified volunteer.</returns>
        [ResponseType(typeof(IQueryable<VolunteerIntent>))]
        public async Task<IHttpActionResult> GetVolunteerIntentByVolunteer(int volunteerId)
        {
            var volunteerIntents = await db.VolunteerIntents.Where(p => p.VolunteerId == volunteerId).ToListAsync();
            if (volunteerIntents == null || volunteerIntents.Count == 0)
            {
                return NotFound();
            }

            return Ok(volunteerIntents);
        }

        // GET: api/VolunteerIntents?eventId=2
        /// <summary>
        /// Get the collection of volunteer intents by ID of the event.
        /// </summary>
        /// <param name="eventId">ID of the event to get the intents for.</param>
        /// <returns>IQueryable of volunteer intents for the specified event.</returns>
        [ResponseType(typeof(IQueryable<VolunteerIntent>))]
        public async Task<IHttpActionResult> GetVolunteerIntentByEvent(int eventId)
        {
            var volunteerIntents = await db.VolunteerIntents.Where(p => p.EventId == eventId).ToListAsync();
            if (volunteerIntents == null || volunteerIntents.Count == 0)
            {
                return NotFound();
            }

            return Ok(volunteerIntents);
        }

        // PUT: api/VolunteerIntents/5
        /// <summary>
        /// Update a volunteer intent entry by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer intent entry to update.</param>
        /// <param name="volunteerIntent">Updated volunteer intent entry to save.</param>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVolunteerIntent(int id, VolunteerIntent volunteerIntent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteerIntent.Id)
            {
                return BadRequest();
            }

            db.Entry(volunteerIntent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerIntentExists(id))
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

        // POST: api/VolunteerIntents
        /// <summary>
        /// Add a new volunteer intent entry.
        /// </summary>
        /// <param name="volunteerIntent">Volunteer intent entry to add.</param>
        /// <returns>Volunteer intent data that was added.</returns>
        [ResponseType(typeof(VolunteerIntent))]
        public async Task<IHttpActionResult> PostVolunteerIntent(VolunteerIntent volunteerIntent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VolunteerIntents.Add(volunteerIntent);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = volunteerIntent.Id }, volunteerIntent);
        }

        // DELETE: api/VolunteerIntents/5
        /// <summary>
        /// Delete a volunteer intent entry by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer intent entry to delete.</param>
        /// <returns>Volunteer intent data that was deleted.</returns>
        [ResponseType(typeof(VolunteerIntent))]
        public async Task<IHttpActionResult> DeleteVolunteerIntent(int id)
        {
            VolunteerIntent volunteerIntent = await db.VolunteerIntents.FindAsync(id);
            if (volunteerIntent == null)
            {
                return NotFound();
            }

            db.VolunteerIntents.Remove(volunteerIntent);
            await db.SaveChangesAsync();

            return Ok(volunteerIntent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VolunteerIntentExists(int id)
        {
            return db.VolunteerIntents.Count(e => e.Id == id) > 0;
        }
    }
}