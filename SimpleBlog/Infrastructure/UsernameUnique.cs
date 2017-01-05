using SimpleBlog.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Infrastructure
{
    public class UsernameUnique : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value.ToString();

            if (Database.UnitOfWork.Users.SingleOrDefault(u => u.Username == username) != null)
            {
                return new ValidationResult("The username must be unique.");
            }

            return ValidationResult.Success;
        }
    }
}