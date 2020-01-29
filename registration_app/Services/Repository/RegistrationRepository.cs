using registration_app.Models;
using registration_app.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace registration_app.Services
{
    public class RegistrationRepository : IRegistration
    {
        private examEntities db;
        public RegistrationRepository()
        {
            db = new examEntities();
        }

        public string Add(registration data)
        {
            string result = "ok";
            if (GetBy(data) != null)
            {
                result = "mobile phone and email already exist";
            }
            else
            {
                db.registrations.Add(data);
                db.SaveChanges();
            }

            return result;
        }

        public registration GetBy(registration data)
        {
            return db.registrations.Where(x => x.mobile_number.Equals(data.mobile_number) && x.email.Equals(data.email)).FirstOrDefault();
        }
    }
}