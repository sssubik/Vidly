using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == 1) {
                return ValidationResult.Success;
            }
            if (customer.DOB == null) {
                return new ValidationResult("Birthdate Is Required");
            }
            var age = DateTime.Today.Year - customer.DOB.Year;

            return (age > 18) ? ValidationResult.Success : 
                new ValidationResult("Customer Should be atleast 18 years old to go on Membership");
        }
    }
}