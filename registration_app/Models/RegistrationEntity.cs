
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace registration_app.Models
{
    public class RegistrationEntity : BaseViewModel
    {
        private static RegistrationEntity instance;

        private RegistrationEntity()
        {
        }

        public static RegistrationEntity getInstance()
        {
            if (instance == null)
            {
                instance = new RegistrationEntity();
            }
            return instance;
        }
        public int id { get; set; }
        [Required, StringLength(25)]
        public string mobile_number { get; set; }

        [Required, StringLength(25)]
        public string first_name { get; set; }

        [Required, StringLength(25)]
        public string last_name { get; set; }

        public DateTime date_of_birth { get; set; }

        public string gender { get; set; }

        [Required, StringLength(25)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
    }
}