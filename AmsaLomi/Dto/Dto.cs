using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmsaLomi.Dto
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentPlaceId { get; set; }
        public string ParentPlace { get; set; }

        public static Place FromBusinessEntity(Models.Place entity)
        {
            return new Place { Id = entity.Id, Description = entity.Description, Name = entity.Name, ParentPlace = entity.ParentPlace.Name, ParentPlaceId = entity.ParentPlaceId };
        }

        public Models.Place ToBusinessEntity()
        {
            return new Models.Place { Id = this.Id, Description = this.Description, Name = this.Name, ParentPlaceId = this.ParentPlaceId };
        }

        public static IEnumerable<Place> FromBusinessEntity(IEnumerable<Models.Place> list)
        {
            return (from s in list select Place.FromBusinessEntity(s)); ;
            //return list.ConvertAll(new Converter<Models.RegionProfile, RegionProfile>(FromBusinessEntity)); ;
        }
    }

    public class Mahiber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PlaceId { get; set; }
        public string Place { get; set; }

        public static Mahiber FromBusinessEntity(Models.Mahiber entity)
        {
            return new Mahiber { Id = entity.Id, Description = entity.Description, Name = entity.Name, Place = entity.Place.Name, PlaceId = entity.PlaceId };
        }

        public Models.Mahiber ToBusinessEntity()
        {
            return new Models.Mahiber { Id = this.Id, Description = this.Description, Name = this.Name, PlaceId = this.PlaceId };
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
