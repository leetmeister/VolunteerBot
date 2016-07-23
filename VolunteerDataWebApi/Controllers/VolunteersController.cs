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
    public class VolunteersController : ApiController
    {
        private VolunteerDataWebApiContext db = new VolunteerDataWebApiContext();

        // GET: api/Volunteers
        /// <summary>
        /// Get all registered volunteer records.
        /// </summary>
        /// <returns>IQueryable of all registered volunteers.</returns>
        public IQueryable<Volunteer> GetVolunteers()
        {
            return db.Volunteers;
        }

        // GET: api/Volunteers/5
        /// <summary>
        /// Get a volunteer record by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer record to get.</param>
        /// <returns>Volunteer data specified by ID.</returns>
        [ResponseType(typeof(Volunteer))]
        public async Task<IHttpActionResult> GetVolunteer(int id)
        {
            Volunteer volunteer = await db.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return Ok(volunteer);
        }

        // GET: api/Volunteers?Name=Debra%20Garcia
        /// <summary>
        /// Get volunteer records by name of the volunteer.
        /// </summary>
        /// <param name="name">Name of the volunteer to get the data for.</param>
        /// <returns>IQueryable of volunteer records with the matching name.</returns>
        [ResponseType(typeof(IQueryable<Volunteer>))]
        public async Task<IHttpActionResult> GetVolunteer(string name)
        {
            var volunteers = await db.Volunteers.Where(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();
            if (volunteers == null || volunteers.Count == 0)
            {
                return NotFound();
            }
            return Ok(volunteers);
        }

        // PUT: api/Volunteers/5
        /// <summary>
        /// Update the volunteer record by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer record to update.</param>
        /// <param name="volunteer">Updated volunteer data to save.</param>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVolunteer(int id, Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteer.Id)
            {
                return BadRequest();
            }

            db.Entry(volunteer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(id))
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

        // POST: api/Volunteers
        /// <summary>
        /// Add a new volunteer record.
        /// </summary>
        /// <param name="volunteer">Volunteer record to add.</param>
        /// <returns>Volunteer data that was added.</returns>
        [ResponseType(typeof(Volunteer))]
        public async Task<IHttpActionResult> PostVolunteer(Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Volunteers.Add(volunteer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = volunteer.Id }, volunteer);
        }

        // DELETE: api/Volunteers/5
        /// <summary>
        /// Delete a volunteer record by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer record to delete.</param>
        /// <returns>Volunteer data that was deleted.</returns>
        [ResponseType(typeof(Volunteer))]
        public async Task<IHttpActionResult> DeleteVolunteer(int id)
        {
            Volunteer volunteer = await db.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            db.Volunteers.Remove(volunteer);
            await db.SaveChangesAsync();

            return Ok(volunteer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VolunteerExists(int id)
        {
            return db.Volunteers.Count(e => e.Id == id) > 0;
        }
    }
}