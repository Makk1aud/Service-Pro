using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Coursework.ValidationsModels.Extensions
{
    public static class ValidationExtensions
    {

        public static bool NameValidation(string name) =>
            name.FieldValidation(3, 20);          


        public static bool PhoneValidation(string phoneNumber) =>
            phoneNumber.FieldValidation(16,20);           

        public static bool EmployeeValidation(Employee employee) =>
            NameValidation(employee.firstname)
            && NameValidation(employee.lastname)
            && EmailValidation(employee.email)
            && PhoneValidation(employee.phone);

        public static bool EmailValidation(string email) =>
            new EmailAddressAttribute().IsValid(email)
            && email.FieldValidation(max: 40);

        public static bool ProductNameValidation(string productName) =>
            productName.FieldValidation(3, 30)
            && productName.Where(x => char.IsLetterOrDigit(x)).Any();

        public static bool FieldValidation(this string item, int min = 0, int max = int.MaxValue) =>
            !String.IsNullOrEmpty(item)
            && item.Trim().Length >= min
            && item.Trim().Length <= max;
    }
}
