using System.Collections.Generic;

namespace OnCourse.Domo.Groups
{
    public class Group : DomoModel
    {
        public bool Default { get; set; }
        public bool Active { get; set; }
        public string CreatorId { get; set; }
        public int MemberCount { get; set; }
        public List<int> UserIds { get; set; }
    }
}