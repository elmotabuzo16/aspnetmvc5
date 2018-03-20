using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        // overriding conventions
        //[Required(ErrorMessage = "Name is Required")]
        //[Required]  // no longer be nullable
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        //public MembershipType MembershipType { get; set; }

        //[Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        //[Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

    }
}