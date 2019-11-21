using System;
using System.Collections.Generic;
using OnCourse.Domo.Sdk.Groups;

namespace OnCourse.Domo.Sdk.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
        public List<GroupBase> Groups { get; set; }
    }
}