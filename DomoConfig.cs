using System;

namespace Domo
{
    public class DomoConfig : IDomoConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public Uri ApiHost { get; set; } = new Uri("https://api.domo.com/");
        public DomoAuthScope Scope { get; set; }
    }

    [Flags]
    public enum DomoAuthScope
    {
        None,

        /// <summary>
        /// Import and export data
        /// </summary>
        Data,

        /// <summary>
        /// Create/Edit Users and Groups
        User,

    }
}