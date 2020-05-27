using System;
using TypeScript.Modeller;

namespace Sample.Assembly.Two
{
    [TypeScriptViewModel]
    public class AddressViewModel
    {
        public Guid ApplicantAddressId { get; set; }

        public Guid ApplicantId { get; set; }

        public int AddressType { get; set; }

        public string UnitNumber { get; set; }

        public string StreetNumber { get; set; }

        public string Street { get; set; }

        public string Suburb { get; set; }

        public string State { get; set; }

        public string StateCode { get; set; }

        public string PostCode { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public virtual ProfileViewModel Profile { get; set; }
    }
}