using Coursework.DataApp;
using Coursework.Testing.Fixtures;
using Coursework.ValidationsModels.Extensions;

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Testing.Systems.Helpers
{
    public class TestValidationExtensions
    {
        private readonly EmployeeFixture fixture;
        public TestValidationExtensions()
        {
            fixture = new EmployeeFixture();
        }

        [Fact]
        public void Get_EmptyEmail_Return_False()
        {
            var email = string.Empty;

            var expected = false;

            var result = ValidationExtensions.EmailValidation(email);

            result.Should().Be(expected);
        }

        [Fact]
        public void Get_20_RandomEmails_Return_True()
        {
            var emails = fixture.GetListOfRandomEmployees(20).Select(x => x.email);

            var expected = true;

            bool result = true;
            foreach (var email in emails)
            {
                result = ValidationExtensions.EmailValidation(email);
                if(!result)
                    break;
            }

            result.Should().Be(expected);
        }

        [Fact]
        public void Get_50_LengthEmail_Return_False()
        {
            var email = "makklaud32434kljfdlfgfdgfdfdfdksj@mail.ru";

            var expected = false;

            bool result = ValidationExtensions.EmailValidation(email);

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("makklaud")]
        [InlineData("makklaud@")]
        [InlineData("@mail")]
        [InlineData("makklaud2mail.ru")]
        [InlineData("makklaud.ru")]
        public void Get_5_IncorrectVariants_Return_False(string incorrectEmail)
        {
            var email = incorrectEmail;

            var expected = false;

            bool result = ValidationExtensions.EmailValidation(email);

            result.Should().Be(expected);
        }

        [Fact]
        public void Get_20_RandomPhones_Return_True()
        {
            var phones = fixture.GetListOfRandomEmployees(20).Select(x => x.phone);

            var expected = true;

            bool result = true;
            foreach (var phone in phones)
            {
                result = ValidationExtensions.PhoneValidation(phone);
                if (!result)
                    break;
            }

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("+78398734435")]
        public void Get_5_IncorrectPhones_Return_True(string incorrectPhone)
        {
            var phone = incorrectPhone;

            var expected = false;

            bool result = ValidationExtensions.PhoneValidation(phone);

            result.Should().Be(expected);
        }


        [Fact]
        public void Get_EmptyEmployee_Return_False()
        {
            Employee employee = new Employee();

            var expected = false;

            var result = ValidationExtensions.EmployeeValidation(employee);

            result.Should().Be(expected);
        }


        [Fact]
        public void Get_20_RandomEmployees_Return_True()
        {
            var employees = fixture.GetListOfRandomEmployees(20);

            var expected = true;

            bool result = true;
            foreach (var employee in employees)
            {
                result = ValidationExtensions.EmployeeValidation(employee);
                if (!result)
                    break;
            }       

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("/")]
        [InlineData("      f")]
        [InlineData(" .  ")]
        [InlineData("   ")]
        [InlineData(".       .")]
        public void Get_5_IncorrectProductsNames_Return_False(string productName)
        {
            var expected = false;

            bool result = ValidationExtensions.ProductNameValidation(productName);

            result.Should().Be(expected);
        }

        [Fact]
        public void Get_10_Length_Word_Return_True()
        {
            var word = "hello ten";
            var min = 2;
            var max = 13;

            var expected = true;

            var result = word.FieldValidation(min, max);

            result.Should().Be(expected);
        }

        [Fact]
        public void Get_Null_Word_Return_False()
        {
            string word = null;
            var min = 2;
            var max = 13;

            var expected = false;

            var result = word.FieldValidation(min, max);

            result.Should().Be(expected);
        }
    }
}
