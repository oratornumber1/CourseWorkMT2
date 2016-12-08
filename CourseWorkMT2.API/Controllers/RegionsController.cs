﻿using System;
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
    builder.EntitySet<Region>("Regions");
    builder.EntitySet<Territory>("Territories"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RegionsController : ODataController
    {
        private NorthWindContext db = new NorthWindContext();

        // GET: odata/Regions
        [EnableQuery]
        public IQueryable<Region> GetRegions()
        {
            return db.Regions;
        }

        // GET: odata/Regions(5)
        [EnableQuery]
        public SingleResult<Region> GetRegion([FromODataUri] int key)
        {
            return SingleResult.Create(db.Regions.Where(region => region.RegionID == key));
        }

        // PUT: odata/Regions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Region> patch)
        {
            //Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Region region = await db.Regions.FindAsync(key);
            if (region == null)
            {
                return NotFound();
            }

            patch.Put(region);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(region);
        }

        // POST: odata/Regions
        public async Task<IHttpActionResult> Post(Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Regions.Add(region);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RegionExists(region.RegionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(region);
        }

        // PATCH: odata/Regions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Region> patch)
        {
            //Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Region region = await db.Regions.FindAsync(key);
            if (region == null)
            {
                return NotFound();
            }

            patch.Patch(region);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(region);
        }

        // DELETE: odata/Regions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Region region = await db.Regions.FindAsync(key);
            if (region == null)
            {
                return NotFound();
            }

            db.Regions.Remove(region);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Regions(5)/Territories
        [EnableQuery]
        public IQueryable<Territory> GetTerritories([FromODataUri] int key)
        {
            return db.Regions.Where(m => m.RegionID == key).SelectMany(m => m.Territories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegionExists(int key)
        {
            return db.Regions.Count(e => e.RegionID == key) > 0;
        }
    }
}
