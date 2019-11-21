using System.Collections.Generic;

namespace OnCourse.Domo.DataSets
{
    public class QueryDataSetResult
    {
        public string Datasource { get; set; }
        public List<string> Columns { get; set; }
        public List<QueryDataSetMetadata> Metadata { get; set; }
        public List<List<object>> Rows { get; set; }
        public int NumRows { get; set; }
        public int NumColumns { get; set; }
        public bool Fromcache { get; set; }
    }

    public class QueryDataSetMetadata
    {
        public string Type { get; set; }
        public string DatasourceId { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public int PeriodIndex { get; set; }
    }
}