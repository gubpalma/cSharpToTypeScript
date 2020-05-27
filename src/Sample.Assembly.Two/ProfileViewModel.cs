using System;
using System.Collections.Generic;
using Sample.Assembly.One;
using Typescript.Modeller;

namespace Sample.Assembly.Two
{
    [TypeScriptViewModel]
    public class ProfileViewModel
    {
        public Guid? ApplicantId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public virtual int ApplicantStatus { get; set; }

        public virtual CarViewModel Car { get; set; }

        public virtual UnmarkedViewModel Unmarked { get; set; }

        public virtual ICollection<AddressViewModel> Addresses { get; set; }
    }
}