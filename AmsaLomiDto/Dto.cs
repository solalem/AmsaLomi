using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmsaLomiDto
{
    public class CountryProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RegionProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryProfileId { get; set; }
        public string CountryProfile { get; set; }
    }

    public class ZoneProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RegionProfileId { get; set; }
        public string RegionProfile { get; set; }
    }

    public class WoredaProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ZoneProfileId { get; set; }
        public string ZoneProfile { get; set; }

    }

    public class Mahiber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WoredaProfileId { get; set; }
        public string WoredaProfile { get; set; }
    }

    public enum PaymentTypes
    {
        SMS, BankAccount, MobileBanking
    }
    public class Payment
    {
        public int Id { get; set; }
        public PaymentTypes Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
    }

    public enum CurrencyTypes
    {
        Birr, Dollar, Pound, Euro
    }
    public class Donation
    {
        public int Id { get; set; }
        public int MahiberPaymentId { get; set; }
        public string MahiberPayment { get; set; }
        public CurrencyTypes CurrencyType { get; set; }
        public int Amount { get; set; }
    }
}
