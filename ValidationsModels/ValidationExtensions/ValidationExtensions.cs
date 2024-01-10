using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Coursework.ValidationsModels.ValidationExtensions
{
    public static class ValidationExtensions
    {

        public static bool NameValidation(string name) =>
            !String.IsNullOrEmpty(name) 
            && name.Trim().Length > 3 
            && name.Trim().Length <= 20;

        public static bool PhoneValidation(string phoneNumber) =>
            !String.IsNullOrEmpty(phoneNumber) 
            && phoneNumber.Trim().Length >= 16 
            && phoneNumber.Trim().Length <= 20;

        public static bool EmployeeValidation(Employee employee) =>
            NameValidation(employee.firstname)
            && NameValidation(employee.lastname)
            && EmailValidation(employee.email)
            && PhoneValidation(employee.phone);

        public static bool EmailValidation(string email) =>
            new EmailAddressAttribute().IsValid(email)
            && email.Trim().Length <= 40;
    }
}
