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
            var countries = new List<CountryProfile> {
                new Models.CountryProfile() { Name = "Ethiopia", Description = "An East African nation with population about 100 million" },
                new Models.CountryProfile() { Name = "Eritrea", Description = "An East African nation with population about 10 million" } };
            countries.ForEach(s => context.CountryProfiles.Add(s));
            context.SaveChanges();

            // Add regional states
            var regions = new List<RegionProfile> {
                new Models.RegionProfile() { Name = "Tigray", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Afar", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Amhara", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Oromia", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Somale", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Southern NNP", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Gambela", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Benishangul Gumuz", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Hareri", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Addis Abeba", Description = "" , CountryProfileId = 1},
                new Models.RegionProfile() { Name = "Dire Dawa", Description = "" , CountryProfileId = 1},
            };
            regions.ForEach(s => context.RegionProfiles.Add(s));
            context.SaveChanges();

            // Add Regions...
            // ...

            // Add Woredas...
            // ...

        }
    }
}