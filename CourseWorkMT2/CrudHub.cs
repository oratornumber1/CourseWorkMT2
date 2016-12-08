using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using CourseWorkMT2.DAL;

namespace CourseWorkMT2
{
    public class CrudHub : Hub
    {
        NorthWindContext db = new NorthWindContext();
    

        #region categories
        public void GetCategories()
        {
            var categories = db.Categories.ToList();
            Clients.Caller.retrieveCategories(categories);
        }

        public void AddCategory(string name, string description, byte[] picture)
        {
            var category = new Category { CategoryName = name, Description = description, Picture = picture};
            db.Categories.Add(category);
            db.SaveChanges();
            Clients.All.retrieveNewCategory(category);
        }

        public void UpdateCategory(int id, string name, string description, byte[] picture)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                Clients.Caller.retrieveError("Запись не найдена");
                return;
            }

            category.CategoryName = name;
            category.Description = description;
            category.Picture = picture;

            db.SaveChanges();
            Clients.All.retrieveUpdatedCategory(category);
        }

        public void DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                Clients.Caller.retrieveError("Запись не найдена");
                return;
            }

            db.Entry(category).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            Clients.All.retrieveDeletedCategory(id);
        }

        #endregion

        #region regions
        public void GetRegions()
        {
            var regions = db.Regions.ToList();
            Clients.Caller.retrieveRegions(regions);
        }

        public void AddRegion(string description)
        {
            var region = new Region {RegionDescription = description};
            db.Regions.Add(region);
            db.SaveChanges();
            Clients.All.retrieveNewRegion(region);
        }

        public void UpdateRegion(int id, string description)
        {
            var region = db.Regions.Find(id);
            if (region == null)
            {
                Clients.Caller.retrieveError("Запись не найдена");
                return;
            }

            region.RegionDescription = description;

            db.SaveChanges();
            Clients.All.retrieveUpdatedRegion(region);
        }

        public void DeleteRegion(int id)
        {
            var region = db.Regions.Find(id);
            if (region == null)
            {
                Clients.Caller.retrieveError("Запись не найдена");
                return;
            }

            db.Entry(region).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            Clients.All.retrieveDeletedRegion(id);
        }
        #endregion

        #region territories
        public void GetTerritories()
        {
            var territories = db.Territories.ToList();
            Clients.Caller.retrieveTerritories(territories);
        }

        public void AddTerritory(string description, int regionId)
        {
            var territory = new Territory { TerritoryDescription = description, RegionID = regionId };
            db.Territories.Add(territory);
            db.SaveChanges();
            Clients.All.retrieveNewTerritory(territory);
        }

        public void UpdateTerritory(int id, string description, int regionId)
        {
            var territory = db.Territories.Find(id);
            if (territory == null)
            {
                Clients.Caller.retrieveError("Запись не найдена");
                return;
            }

            territory.TerritoryDescription = description;
            territory.RegionID = regionId;

            db.SaveChanges();
            Clients.All.retrieveUpdatedTerritory(territory);
        }

        public void DeleteTerritory(int id)
        {
            var territory = db.Territories.Find(id);
            if (territory == null)
            {
                Clients.Caller.retrieveError("Запись не найдена");
                return;
            }

            db.Entry(territory).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            Clients.All.retrieveDeletedTerritory(id);
        }
        #endregion

    }
}