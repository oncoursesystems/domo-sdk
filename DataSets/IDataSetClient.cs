using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnCourse.Domo.Sdk.DataSets
{
    public interface IDataSetClient
    {
        Task<DataSet> CreateDataSetAsync(DataSet dataSet);
        Task<Policy> CreatePolicyAsync(string dataSetId, Policy policy);
        Task<bool> DeleteDataSetAsync(string dataSetId);
        Task<bool> DeletePolicyAsync(long pdpId, string dataSetId);
        Task<string> ExportDataAsync(string dataSetId, bool includeHeader, string fileName);
        Task<bool> ImportDataAsync(string dataSetId, string data);
        Task<IEnumerable<DataSet>> ListDataSetsAsync(string sort, long limit = 50, long offset = 0);
        Task<IEnumerable<Policy>> ListPoliciesAsync(string dataSetId);
        Task<DataSet> RetrieveDataSetAsync(string dataSetId);
        Task<Policy> RetrievePolicyAsync(long pdpId, string dataSetId);
        Task<DataSet> UpdateDataSetAsync(string dataSetId, DataSet dataSet);
        Task<Policy> UpdatePolicyAsync(long pdpId, string dataSetId, Policy policy);
    }
}