using System;
using System.Collections.Generic;

namespace Domo.Pages
{
    public class Page : DomoModel
    {
        public long ParentId { get; set; }
        public long OwnerId { get; set; }
        public bool Locked { get; set; }
        public long CollectionIds { get; set; }
        public long CardIds { get; set; }
        public IDictionary<string, string> Children { get; set; }
        public Visibility Visibility { get; set; }
    }

    public class Visibility
    {
        public IEnumerable<int> UserIds { get; set; }
        public IEnumerable<int> GroupIds { get; set; }
    }
}