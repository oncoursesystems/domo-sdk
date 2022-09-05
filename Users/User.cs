using System;
using System.Collections.Generic;
using Domo.Groups;

namespace Domo.Users
{
    public class User : DomoModel
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Title { get; set; }
        public string AlternateEmail { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Timezone { get; set; }
        public string Image { get; set; }
        public DateTime Locale { get; set; }
        //public string EmployeeNumber { get; set; }
        public List<DomoModel> Groups { get; set; }
    }
}