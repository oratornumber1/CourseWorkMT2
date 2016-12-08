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
    builder.EntitySet<Employee>("Employees");
    builder.EntitySet<Order>("Orders"); 
    builder.EntitySet<Territory>("Territories"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmployeesController : ODataController
    {
        private NorthWindContext db = new NorthWindContext();

        // GET: odata/Employees
        [EnableQuery]
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        // GET: odata/Employees(5)
        [EnableQuery]
        public SingleResult<Employee> GetEmployee([FromODataUri] int key)
        {
            return SingleResult.Create(db.Employees.Where(employee => employee.EmployeeID == key));
        }

        // PUT: odata/Employees(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Employee> patch)
        {
            //Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = await db.Employees.FindAsync(key);
            if (employee == null)
            {
                return NotFound();
            }

            patch.Put(employee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employee);
        }

        // POST: odata/Employees
        public async Task<IHttpActionResult> Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            await db.SaveChangesAsync();

            return Created(employee);
        }

        // PATCH: odata/Employees(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Employee> patch)
        {
            //Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = await db.Employees.FindAsync(key);
            if (employee == null)
            {
                return NotFound();
            }

            patch.Patch(employee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employee);
        }

        // DELETE: odata/Employees(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Employee employee = await db.Employees.FindAsync(key);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Employees(5)/Employees1
        [EnableQuery]
        public IQueryable<Employee> GetEmployees1([FromODataUri] int key)
        {
            return db.Employees.Where(m => m.EmployeeID == key).SelectMany(m => m.Employees1);
        }

        // GET: odata/Employees(5)/Employee1
        [EnableQuery]
        public SingleResult<Employee> GetEmployee1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Employees.Where(m => m.EmployeeID == key).Select(m => m.Employee1));
        }

        // GET: odata/Employees(5)/Orders
        [EnableQuery]
        public IQueryable<Order> GetOrders([FromODataUri] int key)
        {
            return db.Employees.Where(m => m.EmployeeID == key).SelectMany(m => m.Orders);
        }

        // GET: odata/Employees(5)/Territories
        [EnableQuery]
        public IQueryable<Territory> GetTerritories([FromODataUri] int key)
        {
            return db.Employees.Where(m => m.EmployeeID == key).SelectMany(m => m.Territories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int key)
        {
            return db.Employees.Count(e => e.EmployeeID == key) > 0;
        }
    }
}
