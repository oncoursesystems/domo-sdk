using OnCourse.Domo.DataSets;
using OnCourse.Domo.Groups;
using OnCourse.Domo.Pages;
using OnCourse.Domo.Users;

namespace OnCourse.Domo
{
    public class DomoClient
    {
        public DomoClient(IDomoConfig config)
        {
            Groups = new GroupClient(config);
            Users = new UserClient(config);
            DataSets = new DataSetClient(config);
            Pages = new PageClient(config);
        }

        public IGroupClient Groups { get; set; }
        public IUserClient Users { get; set; }
        public IDataSetClient DataSets { get; set; }
        public IPageClient Pages { get; set; }
    }
}