using OnCourse.Domo.Sdk.DataSets;
using OnCourse.Domo.Sdk.Groups;
using OnCourse.Domo.Sdk.Users;

namespace OnCourse.Domo.Sdk
{
    public class DomoClient
    {
        private IDomoConfig _config { get; set; }

        public DomoClient(IDomoConfig config)
        {
            _config = config;
            Groups = new GroupClient(_config);
            Users = new UserClient(_config);
            DataSets = new DataSetClient(_config);
        }

        public IGroupClient Groups { get; set; }
        public IUserClient Users { get; set; }
        public IDataSetClient DataSets { get; set; }
    }
}