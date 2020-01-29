using System;

namespace registration_app.Models
{
   
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            created_by = "system";
            created_date = DateTime.Now;
        }
        private string created_by { get; set; }
        private DateTime created_date { get; set; }
        protected string updated_by { get; set; }
        protected DateTime updated_date { get; set; }

    }
}