using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsaLomi.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? ParentPlaceId { get; set; }

        public virtual Place ParentPlace { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }

    public class Mahiber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }
        public virtual ICollection<MahiberPayment> MahiberPayments { get; set; }
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

        public virtual ICollection<MahiberPayment> MahiberPayments { get; set; }
    }

    public class MahiberPayment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MahiberId { get; set; }
        public int PaymentId { get; set; }
        public int PaymentNumber { get; set; }

        public virtual Mahiber Mahiber { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
    }

    public enum CurrencyTypes
    {
        Birr, Dollar, Pound, Euro
    }
    public class Donation
    {
        public int Id { get; set; }
        public int MahiberPaymentId { get; set; }
        public CurrencyTypes CurrencyType { get; set; }
        public int Amount { get; set; }

        public virtual MahiberPayment MahiberPayment { get; set; }
    }

    public class MahiberMember
    {
        public int Id { get; set; }
        public int MahiberId { get; set; }
        public string MemberId { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Mahiber Mahiber { get; set; }
        public virtual AspNetUser Member { get; set; }
    }

    public class AspNetUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    }

    public class SearchObject
    {
        public int Id { get; set; }
        public int SearchFor { get; set; }
        public int AndSearchFor { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Brievity { get; set; }
    }
}