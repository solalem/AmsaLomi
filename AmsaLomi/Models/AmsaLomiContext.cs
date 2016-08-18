using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AmsaLomi.Models
{
    public class AmsaLomiContext : DbContext
    {
        public DbSet<CountryProfile> CountryProfiles { get; set; }
        public DbSet<RegionProfile> RegionProfiles { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<MahiberPayment> MahiberPayments { get; set; }
        public DbSet<Mahiber> Mahibers { get; set; }
        public DbSet<WoredaProfile> WoredaProfiles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ZoneProfile> ZoneProfiles { get; set; }

        public AmsaLomiContext() : base("AmsaLomiContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Table naes should be in sigular form
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}