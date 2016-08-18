using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmsaLomi.Dto
{
    public class CountryProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static CountryProfile FromBusinessEntity(Models.CountryProfile entity)
        {
            return new CountryProfile { Id = entity.Id, Description = entity.Description, Name = entity.Name };
        }

        public Models.CountryProfile ToBusinessEntity()
        {
            return new Models.CountryProfile { Id = this.Id, Description = this.Description, Name = this.Name };
        }

        public static IEnumerable<CountryProfile> FromBusinessEntity(IEnumerable<Models.CountryProfile> list)
        {
            return (from s in list select CountryProfile.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.CountryProfile, CountryProfile>(FromBusinessEntity));
        }
    }

    public class RegionProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryProfileId { get; set; }
        public string CountryProfile { get; set; }

        public static RegionProfile FromBusinessEntity(Models.RegionProfile entity)
        {
            return new RegionProfile { Id = entity.Id, Description = entity.Description, Name = entity.Name, CountryProfile = entity.CountryProfile.Name, CountryProfileId = entity.CountryProfileId };
        }

        public Models.RegionProfile ToBusinessEntity()
        {
            return new Models.RegionProfile { Id = this.Id, Description = this.Description, Name = this.Name, CountryProfileId = this.CountryProfileId };
        }

        public static IEnumerable<RegionProfile> FromBusinessEntity(IEnumerable<Models.RegionProfile> list)
        {
            return (from s in list select RegionProfile.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.RegionProfile, RegionProfile>(FromBusinessEntity)); ;
        }
    }

    public class ZoneProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RegionProfileId { get; set; }
        public string RegionProfile { get; set; }

        public static ZoneProfile FromBusinessEntity(Models.ZoneProfile entity)
        {
            return new ZoneProfile { Id = entity.Id, Description = entity.Description, Name = entity.Name, RegionProfile = entity.RegionProfile.Name, RegionProfileId = entity.RegionProfileId };
        }

        public Models.ZoneProfile ToBusinessEntity()
        {
            return new Models.ZoneProfile { Id = this.Id, Description = this.Description, Name = this.Name, RegionProfileId = this.RegionProfileId };
        }

        public static IEnumerable<ZoneProfile> FromBusinessEntity(IEnumerable<Models.ZoneProfile> list)
        {
            return (from s in list select ZoneProfile.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.ZoneProfile, ZoneProfile>(FromBusinessEntity)); ;
        }
    }

    public class WoredaProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ZoneProfileId { get; set; }
        public string ZoneProfile { get; set; }

        public static WoredaProfile FromBusinessEntity(Models.WoredaProfile entity)
        {
            return new WoredaProfile { Id = entity.Id, Description = entity.Description, Name = entity.Name, ZoneProfile = entity.ZoneProfile.Name, ZoneProfileId = entity.ZoneProfileId };
        }

        public Models.WoredaProfile ToBusinessEntity()
        {
            return new Models.WoredaProfile { Id = this.Id, Description = this.Description, Name = this.Name, ZoneProfileId = this.ZoneProfileId };
        }

        public static IEnumerable<WoredaProfile> FromBusinessEntity(IEnumerable<Models.WoredaProfile> list)
        {
            return (from s in list select WoredaProfile.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.WoredaProfile, WoredaProfile>(FromBusinessEntity)); ;
        }
    }

    public class Mahiber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WoredaProfileId { get; set; }
        public string WoredaProfile { get; set; }

        public static Mahiber FromBusinessEntity(Models.Mahiber entity)
        {
            return new Mahiber { Id = entity.Id, Description = entity.Description, Name = entity.Name, WoredaProfile = entity.WoredaProfile.Name, WoredaProfileId = entity.WoredaProfileId };
        }

        public Models.Mahiber ToBusinessEntity()
        {
            return new Models.Mahiber { Id = this.Id, Description = this.Description, Name = this.Name, WoredaProfileId = this.WoredaProfileId };
        }

        public static IEnumerable<Mahiber> FromBusinessEntity(IEnumerable<Models.Mahiber> list)
        {
            return (from s in list select Mahiber.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.Mahiber, Mahiber>(FromBusinessEntity)); ;
        }
    }

    public class Payment
    {
        public int Id { get; set; }
        public Models.PaymentTypes Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static Payment FromBusinessEntity(Models.Payment entity)
        {
            return new Payment { Id = entity.Id, Description = entity.Description, Name = entity.Name, Type=entity.Type };
        }

        public Models.Payment ToBusinessEntity()
        {
            return new Models.Payment { Id = this.Id, Description = this.Description, Name = this.Name };
        }

        public static IEnumerable<Payment> FromBusinessEntity(IEnumerable<Models.Payment> list)
        {
            return (from s in list select Payment.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.Payment, Payment>(FromBusinessEntity)); ;
        }
    }

    public class MahiberPayment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MahiberId { get; set; }
        public int PaymentId { get; set; }
        public string Mahiber { get; set; }
        public string Payment { get; set; }
        public int PaymentNumber { get; set; }

        public static MahiberPayment FromBusinessEntity(Models.MahiberPayment entity)
        {
            return new MahiberPayment { Id = entity.Id, Description = entity.Description, Mahiber = entity.Mahiber.Name, MahiberId = entity.MahiberId, Payment = entity.Payment.Name, PaymentId=entity.PaymentId, PaymentNumber = entity.PaymentNumber };
        }

        public Models.MahiberPayment ToBusinessEntity()
        {
            return new Models.MahiberPayment { Id = this.Id, Description = this.Description, MahiberId = this.MahiberId, PaymentId = this.PaymentId, PaymentNumber = this.PaymentNumber };
        }

        public static IEnumerable<MahiberPayment> FromBusinessEntity(IEnumerable<Models.MahiberPayment> list)
        {
            return (from s in list select MahiberPayment.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.MahiberPayment, MahiberPayment>(FromBusinessEntity)); ;
        }
    }

    public class Donation
    {
        public int Id { get; set; }
        public int MahiberPaymentId { get; set; }
        public string MahiberPayment { get; set; }
        public Models.CurrencyTypes CurrencyType { get; set; }
        public int Amount { get; set; }

        public static Donation FromBusinessEntity(Models.Donation entity)
        {
            return new Donation { Id = entity.Id, Amount = entity.Amount, CurrencyType = entity.CurrencyType, MahiberPayment= entity.MahiberPayment.Description, MahiberPaymentId = entity.MahiberPaymentId };
        }

        public Models.Donation ToBusinessEntity()
        {
            return new Models.Donation { Id = this.Id, Amount = this.Amount, CurrencyType = this.CurrencyType, MahiberPaymentId = this.MahiberPaymentId };
        }

        public static IEnumerable<Donation> FromBusinessEntity(IEnumerable<Models.Donation> list)
        {
            return (from s in list select Donation.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.Donation, Donation>(FromBusinessEntity)); ;
        }
    }
}
