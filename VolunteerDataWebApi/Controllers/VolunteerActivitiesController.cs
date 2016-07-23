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
    public class VolunteerActivitiesController : ApiController
    {
        private VolunteerDataWebApiContext db = new VolunteerDataWebApiContext();

        // GET: api/VolunteerActivities
        /// <summary>
        /// Get all volunteer activity entries.
        /// </summary>
        /// <returns>IQueryable of all volunter activity entries.</returns>
        public IQueryable<VolunteerActivity> GetVolunteerActivities()
        {
            return db.VolunteerActivities;
        }

        // GET: api/VolunteerActivities/5
        /// <summary>
        /// Get a volunteer activity entry by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer activity entry to get.</param>
        /// <returns>Volunteer activity entry.</returns>
        [ResponseType(typeof(VolunteerActivity))]
        public async Task<IHttpActionResult> GetVolunteerActivity(int id)
        {
            VolunteerActivity volunteerActivity = await db.VolunteerActivities.FindAsync(id);
            if (volunteerActivity == null)
            {
                return NotFound();
            }

            return Ok(volunteerActivity);
        }

        // GET: api/VolunteerActivities?volunteerId=3
        /// <summary>
        /// Get the collection of volunteer activites by ID of the volunteer.
        /// </summary>
        /// <param name="volunteerId">ID of volunteer to get the activities for.</param>
        /// <returns>IQueryable of volunteer activities for the specified volunteer.</returns>
        [ResponseType(typeof(IQueryable<VolunteerIntent>))]
        public async Task<IHttpActionResult> GetVolunteerIntentByVolunteer(int volunteerId)
        {
            var volunteerActivities = await db.VolunteerActivities.Where(p => p.VolunteerId == volunteerId).ToListAsync();
            if (volunteerActivities == null || volunteerActivities.Count == 0)
            {
                return NotFound();
            }

            return Ok(volunteerActivities);
        }

        // GET: api/VolunteerActivities?eventId=2
        /// <summary>
        /// Get the collection of volunteer activities by ID of the event.
        /// </summary>
        /// <param name="eventId">ID of the event to get the activities for.</param>
        /// <returns>IQueryable of volunteer activities for the specified event.</returns>
        [ResponseType(typeof(IQueryable<VolunteerIntent>))]
        public async Task<IHttpActionResult> GetVolunteerIntentByEvent(int eventId)
        {
            var volunteerActivities = await db.VolunteerActivities.Where(p => p.EventId == eventId).ToListAsync();
            if (volunteerActivities == null || volunteerActivities.Count == 0)
            {
                return NotFound();
            }

            return Ok(volunteerActivities);
        }

        // PUT: api/VolunteerActivities/5
        /// <summary>
        /// Update a volunteer activity entry by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer activity entry to update.</param>
        /// <param name="volunteerActivity">Updated volunteer activity entry to save.</param>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVolunteerActivity(int id, VolunteerActivity volunteerActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteerActivity.Id)
            {
                return BadRequest();
            }

            db.Entry(volunteerActivity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerActivityExists(id))
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

        // POST: api/VolunteerActivities
        /// <summary>
        /// Add a new volunteer activity entry.
        /// </summary>
        /// <param name="volunteerActivity">Volunteer activity entry to add.</param>
        /// <returns>Volunteer activity data that was added.</returns>
        [ResponseType(typeof(VolunteerActivity))]
        public async Task<IHttpActionResult> PostVolunteerActivity(VolunteerActivity volunteerActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VolunteerActivities.Add(volunteerActivity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = volunteerActivity.Id }, volunteerActivity);
        }

        // DELETE: api/VolunteerActivities/5
        /// <summary>
        /// Delete a volunteer activity entry by ID.
        /// </summary>
        /// <param name="id">ID of the volunteer activity entry to delete.</param>
        /// <returns>Volunteer activity data that was deleted.</returns>
        [ResponseType(typeof(VolunteerActivity))]
        public async Task<IHttpActionResult> DeleteVolunteerActivity(int id)
        {
            VolunteerActivity volunteerActivity = await db.VolunteerActivities.FindAsync(id);
            if (volunteerActivity == null)
            {
                return NotFound();
            }

            db.VolunteerActivities.Remove(volunteerActivity);
            await db.SaveChangesAsync();

            return Ok(volunteerActivity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VolunteerActivityExists(int id)
        {
            return db.VolunteerActivities.Count(e => e.Id == id) > 0;
        }
    }
}