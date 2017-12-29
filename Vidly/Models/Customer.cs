﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool isSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        [ForeignKey("MembershipType")]
        public byte MembershipTypeId { get; set; }
        public DateTime DOB { get; set; } 
    }
}