using System.Collections.Generic;

namespace OnCourse.Domo.Pages
{
    public class Page : DomoModel
    {
        // public long? ParentId { get; set; }
        // public long OwnerId { get; set; }
        // public bool Locked { get; set; }
        // public IEnumerable<int> CollectionIds { get; set; } = new List<int>();
        public IEnumerable<int> CardIds { get; set; } = new List<int>();
        public IEnumerable<ChildPage> Children { get; set; } = new List<ChildPage>();
        public Visibility Visibility { get; set; } = new Visibility();
        public IEnumerable<PageOwner> Owners { get; set; } = new List<PageOwner>();
    }

    public class Visibility
    {
        // public IEnumerable<int> UserIds { get; set; } = new List<int>();
        public IEnumerable<int> GroupIds { get; set; } = new List<int>();
    }

    public class ChildPage : DomoModel
    {
        public IEnumerable<ChildPage> Children { get; set; }
    }

    public class PageOwner
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string DisplayName { get; set; }
    }
}
