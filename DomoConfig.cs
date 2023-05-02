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
        None = 0,
        Data = 1,
        User = 2,
        Dashboard = 4,
        Audit = 8,
        Workflow = 16,
        Account = 32,
        All = Data | User | Dashboard | Audit | Workflow | Account
    }
}