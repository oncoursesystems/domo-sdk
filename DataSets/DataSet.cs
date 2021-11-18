using System.Collections.Generic;

namespace OnCourse.Domo.Sdk.DataSets
{
    public class DataSet
    {
        public string Id { get; set; }  // this is a string guid for DataSets
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DomoModel> Owner { get; set; }
        public int Columns { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string DataCurrentAt { get; set; }
        public DataSetSchema Schema { get; set; }
        public bool PdpEnabled { get; set; }
        public List<Policy> Policies { get; set; }
        public int Rows { get; set; }
    }

    public class DataSetSchema
    {
        public List<DataSetSchemaColumn> Columns { get; set; }
    }

    public class DataSetSchemaColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}