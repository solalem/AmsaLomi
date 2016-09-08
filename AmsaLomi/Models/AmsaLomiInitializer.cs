using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsaLomi.Models
{
    public class AmsaLomiInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<AmsaLomiContext>
    {
        protected override void Seed(AmsaLomiContext context)
        {
            // Add Ethiopia as a country
            var countries = new List<Place> {
                new Models.Place() { Name = "Ethiopia", Description = "An East African nation with population about 100 million" },
                new Models.Place() { Name = "Eritrea", Description = "An East African nation with population about 10 million" } };
            countries.ForEach(s => context.Places.Add(s));
            context.SaveChanges();

            // Add regional states
            var regions = new List<Place> {
                new Models.Place() { Name = "Tigray", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Afar", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Amhara", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Oromia", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Somale", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Southern NNP", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Gambela", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Benishangul Gumuz", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Hareri", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Addis Abeba", Description = "" , ParentPlaceId = 1},
                new Models.Place() { Name = "Dire Dawa", Description = "" , ParentPlaceId = 1},
            };
            regions.ForEach(s => context.Places.Add(s));
            context.SaveChanges();

            // Add Regions...
            // ...

            // Add Woredas...
            // ...

        }
    }
}