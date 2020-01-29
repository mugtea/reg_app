using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using registration_app.Controllers;
using registration_app.Services;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace registration_app.Tests.Controllers
{
    [TestClass]
    public class RegistrationControllerTest
    {
        RegistrationController controller;

        HttpConfiguration configuration;
        HttpRequestMessage request;

        public RegistrationControllerTest()
        {
            controller = new RegistrationController();
            configuration = new HttpConfiguration();
            request = new HttpRequestMessage();
            controller.Request = request;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;
        }

        [TestMethod]
        public void MobilePhoneAndEmailMustUnique()
        {
            // Arrange
            var randomNum = DateTime.Now.Millisecond;
            // Act
            var dummy = new registration()
            {
                date_of_birth = DateTime.Now,
                email = string.Concat(randomNum, "mukti@gmail.com"),
                first_name = "mukti",
                last_name = "wibowo",
                gender = "M",
                mobile_number = string.Concat("+6281626", randomNum)
            };

            var results = controller.Post(dummy);
            results = controller.Post(dummy);
            var response = results as OkNegotiatedContentResult<string>;
            Assert.AreEqual("mobile phone and email already exist", response.Content);
        }

        [TestMethod]
        public void PropertyRequired()
        {
            // Arrange
            var randomNum = DateTime.Now.Millisecond;
            // Act
            var dummy = new registration()
            {
                date_of_birth = DateTime.Now,
                email = "",
                first_name = "",
                last_name = "",
                gender = "M",
                mobile_number = ""
            };

            var results = ValidateModel(dummy);
            Assert.IsTrue(results.Any(v => v.ErrorMessage == "The email field is required."));
            Assert.IsTrue(results.Any(v => v.ErrorMessage == "The first_name field is required."));
            Assert.IsTrue(results.Any(v => v.ErrorMessage == "The last_name field is required."));
            Assert.IsTrue(results.Any(v => v.ErrorMessage == "The mobile_number field is required."));
        }

        [TestMethod]
        public void ValidEmail()
        {
            // Arrange
            var randomNum = DateTime.Now.Millisecond;
            // Act
            List<string> listemail = new List<string>() { "muktiwww@gmail.com", "mukti.www@gmail.com", "muktiwww@gmail.co.id" };

            foreach (var item in listemail)
            {
                var dummy = new registration() { email = item };
                var results = ValidateModel(dummy);
                Assert.IsFalse(results.Any(x => x.ErrorMessage.Contains("The field email must match the regular expression")));
            }
        }

        [TestMethod]
        public void InValidEmail()
        {
            // Arrange
            var randomNum = DateTime.Now.Millisecond;
            // Act
            List<string> listemail = new List<string>() { ".muktiwww@gmail.com", "mukti.wwwgmail.com", "muktiwww@@gmail.co.id", "muktiwww@@gmail", "@com" };

            foreach (var item in listemail)
            {
                var dummy = new registration() { email = item };
                var results = ValidateModel(dummy);
                Assert.IsTrue(results.Any(x => x.ErrorMessage.Contains("The field email must match the regular expression")));
            }
        }

        [TestMethod]
        public void ValidIndonesiaPhoneNumber()
        {
            List<string> listPhoneNumber = new List<string>() {
                "+62817737669",
                "62817737669",
                "0817737669",
                "6221995500",
                "021995500"};

            foreach (var item in listPhoneNumber)
            {
                var dummy = new registration() { mobile_number = item };
                var results = ValidateModel(dummy);
                Assert.IsFalse(results.Any(x => x.ErrorMessage.Contains("The field mobile_phone must match the regular expression")));
            }
        }

        [TestMethod]
        public void InValidIndonesiaPhoneNumber()
        {
            List<string> listPhoneNumber = new List<string>() {
                "+817737669",
                "817737669",
                "0017737669",
                "081 773 7669",
                "0817-737-669",
                "021-995500"};

            foreach (var item in listPhoneNumber)
            {
                var dummy = new registration() { mobile_number = item };
                var results = ValidateModel(dummy);
                Assert.IsTrue(results.Any(x => x.ErrorMessage.Contains("The field mobile_number must match the regular expression ")));
            }
        }
        private List<ValidationResult> ValidateModel<T>(T model)
        {
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, context, result, true);

            return result;
        }
    }
}
