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
using System.Web.Http.ModelBinding;
using System.Web.OData;
using System.Web.Http.OData.Routing;
using CourseWorkMT2.DAL;

namespace CourseWorkMT2.API.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using CourseWorkMT2.DAL;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Territory>("Territories");
    builder.EntitySet<Region>("Regions"); 
    builder.EntitySet<Employee>("Employees"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TerritoriesController : ODataController
    {
        private NorthWindContext db = new NorthWindContext();

        // GET: odata/Territories
        [EnableQuery]
        public IQueryable<Territory> GetTerritories()
        {
            return db.Territories;
        }

        // GET: odata/Territories(5)
        [EnableQuery]
        public SingleResult<Territory> GetTerritory([FromODataUri] string key)
        {
            return SingleResult.Create(db.Territories.Where(territory => territory.TerritoryID == key));
        }

        // PUT: odata/Territories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<Territory> patch)
        {
            //Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Territory territory = await db.Territories.FindAsync(key);
            if (territory == null)
            {
                return NotFound();
            }

            patch.Put(territory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerritoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(territory);
        }

        // POST: odata/Territories
        public async Task<IHttpActionResult> Post(Territory territory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Territories.Add(territory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TerritoryExists(territory.TerritoryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(territory);
        }

        // PATCH: odata/Territories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Territory> patch)
        {
            //Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Territory territory = await db.Territories.FindAsync(key);
            if (territory == null)
            {
                return NotFound();
            }

            patch.Patch(territory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerritoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(territory);
        }

        // DELETE: odata/Territories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            Territory territory = await db.Territories.FindAsync(key);
            if (territory == null)
            {
                return NotFound();
            }

            db.Territories.Remove(territory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Territories(5)/Region
        [EnableQuery]
        public SingleResult<Region> GetRegion([FromODataUri] string key)
        {
            return SingleResult.Create(db.Territories.Where(m => m.TerritoryID == key).Select(m => m.Region));
        }

        // GET: odata/Territories(5)/Employees
        [EnableQuery]
        public IQueryable<Employee> GetEmployees([FromODataUri] string key)
        {
            return db.Territories.Where(m => m.TerritoryID == key).SelectMany(m => m.Employees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerritoryExists(string key)
        {
            return db.Territories.Count(e => e.TerritoryID == key) > 0;
        }
    }
}
