using System.Collections.Generic;

namespace OnCourse.Domo.DataSets
{
    public class Policy : DomoModel
    {
        public string Type { get; set; }
        public List<PolicyFilter> Filters { get; set; }
        public List<int> Users { get; set; } = new List<int>();
        public List<int> Groups { get; set; } = new List<int>();
    }

    public class PolicyFilter
    {
        public string Column { get; set; }
        public bool Not { get; set; }
        public string Operator { get; set; }
        public List<string> Values { get; set; }
    }
}