using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnCourse.Domo.DataSets
{
    public class DataSetClient : IDataSetClient
    {
        private readonly DomoHttpClient _domoHttpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public DataSetClient(IDomoConfig config)
        {
            _domoHttpClient = new DomoHttpClient(config);
            _serializerOptions = new JsonSerializerOptions()
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Retrieves the details of an existing DataSet.
        /// </summary>
        /// <param name="dataSetId">The ID of the DataSet</param>
        /// <returns>
        /// Returns a DataSet object if valid DataSet ID was provided. When requesting, if the DataSet ID 
        /// is related to a DataSet that has been deleted, a subset of the DataSet's information will be returned, 
        /// including a deleted property, which will be true. <see cref="Domo.DataSets.DataSet"/>
        /// </returns>
        public async Task<DataSet> RetrieveDataSetAsync(string dataSetId)
        {
            string uri = $"v1/datasets/{dataSetId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(uri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DataSet>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Creates a new DataSet in your Domo instance. Once the DataSet has been created, data can then be imported into the DataSet.
        /// </summary>
        /// <param name="dataSet">Properties and values for the dataset being created</param>
        /// <returns>Returns a DataSet object when successful. <see cref="Domo.DataSets.DataSet"/></returns>
        public async Task<DataSet> CreateDataSetAsync(DataSet dataSet)
        {
            string uri = $"v1/datasets";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(dataSet, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PostAsync(uri, content);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DataSet>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Updates the specified DataSet’s metadata by providing values to parameters passed.
        /// </summary>
        /// <param name="dataSetId">The ID of the DataSet.</param>
        /// <param name="dataSet">Domo User Info to update to.</param>
        /// <returns>Returns a full DataSet object. <see cref="Domo.DataSets.DataSet"/></returns>
        public async Task<DataSet> UpdateDataSetAsync(string dataSetId, DataSet dataSet)
        {
            string uri = $"v1/datasets/{dataSetId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(dataSet, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PutAsync(uri, content);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DataSet>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Permanently deletes a DataSet from your Domo instance. This can be done for all DataSets, not just those created through the API.
        /// </summary>
        /// <param name="dataSetId"></param>
        /// <returns>Returns a bool of whether or not the DataSet was successfully deleted</returns>
        public async Task<bool> DeleteDataSetAsync(string dataSetId)
        {
            string uri = $"v1/datasets/{dataSetId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Queries the data in an existing Domo DataSet
        /// </summary>
        /// <param name="dataSetId">The ID of the DataSet</param>
        /// <param name="query">The SQL query</param>
        /// <returns>Returns data from the DataSet based on your SQL query.</returns>
        public async Task<QueryDataSetResult> QueryDataSetAsync(string dataSetId, string query)
        {
            string uri = $"v1/datasets/query/execute/{dataSetId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var formContent = new
            {
                Sql = query
            };

            StringContent content = new StringContent(JsonSerializer.Serialize(formContent, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PostAsync(uri, content);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<QueryDataSetResult>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Get a list of all DataSets in your Domo instance.
        /// </summary>
        /// <param name="sort">The DataSet field to sort by. Fields prefixed with a negative sign reverses the sort (i.e. '-name' does a reverse sort by the name of the DataSets).</param>
        /// <param name="limit">The amount of DataSet to return in the list. The default is 50 and the maximum is 50..</param>
        /// <param name="offset">The offset of the DataSet ID to begin list of users within the response.</param>
        /// <returns>Returns all DataSet objects that meet argument criteria from original request. <see cref="Domo.DataSets.DataSet"/></returns>
        public async Task<IEnumerable<DataSet>> ListDataSetsAsync(string sort, long offset = 0, long limit = 50)
        {
            if (limit < 1 || limit > 50) throw new ArgumentOutOfRangeException("limit", $"List limit {limit} cannot be used. Use a limit value between 1 and 50");

            string uri = $"v1/datasets?limit={limit}&offset={offset}&sort={sort}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(uri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<DataSet>>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Import data into a DataSet in your Domo instance. This request will replace the data currently in the DataSet.
        /// NOTE: The only supported content type is currently CSV format.
        /// </summary>
        /// <param name="dataSetId">The ID of the DataSet to have data imported</param>
        /// <param name="data">The data to be imported in proper CSV format</param>
        /// <returns>Returns a bool of whether or not the data was successfully imported</returns>
        public async Task<bool> ImportDataAsync(string dataSetId, string data)
        {
            string uri = $"v1/datasets/{dataSetId}/data";
            _domoHttpClient.SetAcceptRequestHeaders("text/csv");

            StringContent content = new StringContent(data, Encoding.UTF8, "text/csv");
            var response = await _domoHttpClient.Client.PutAsync(uri, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Export data from a DataSet in your Domo instance.
        /// </summary>
        /// <param name="dataSetId">The ID of the DataSet to download data</param>
        /// <param name="includeHeader">Include table header</param>
        /// <param name="fileName">The filename of the exported csv</param>
        /// <returns>Returns a raw CSV in the response body or error for the outcome of data being exported into DataSet.</returns>
        public async Task<string> ExportDataAsync(string dataSetId, bool includeHeader, string fileName)
        {
            string uri = $"v1/datasets/{dataSetId}/data?includeHeader={includeHeader}&filename={fileName}";
            _domoHttpClient.SetAcceptRequestHeaders("text/csv");

            var response = await _domoHttpClient.Client.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Retrieve a policy from a DataSet within Domo. A DataSet is required for a PDP policy to exist.
        /// </summary>
        /// <param name="pdpId">The ID of the PDP policy to download data</param>
        /// <param name="dataSetId">The ID of the DataSet associated to the PDP policy</param>
        /// <returns>
        /// Returns a subset of the DataSet object specific to the data permission policy. <see cref="Domo.DataSets.Policy"/>
        /// </returns>
        public async Task<Policy> RetrievePolicyAsync(long pdpId, string dataSetId)
        {
            string uri = $"v1/datasets/{dataSetId}/policies/{pdpId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(uri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Policy>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Create a PDP policy for user and or group access to data within a DataSet.  Users and groups must exist before creating PDP policy.
        /// </summary>
        /// <param name="dataSetId">The ID of the DataSet associated to the PDP policy.</param>
        /// <param name="policy">Properties and values for the policy being created</param>
        /// <returns>Returns a subset of the DataSet object specific to the data permission policy. <see cref="Domo.DataSets.Policy"/></returns>
        public async Task<Policy> CreatePolicyAsync(string dataSetId, Policy policy)
        {
            string uri = $"v1/datasets/{dataSetId}/policies";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(policy, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PostAsync(uri, content);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Policy>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Update the specific PDP policy for a DataSet by providing values to parameters passed.
        /// </summary>
        /// <param name="pdpId">The ID of the PDP policy to download data.</param>
        /// <param name="dataSetId">The ID of the DataSet associated to the PDP policy.</param>
        /// <param name="policy">Domo Policy to update to.</param>
        /// <returns>Returns a full Policy object. <see cref="Domo.DataSets.Policy"/></returns>
        public async Task<Policy> UpdatePolicyAsync(long pdpId, string dataSetId, Policy policy)
        {
            string uri = $"v1/datasets/{dataSetId}/policies/{pdpId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(policy, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PutAsync(uri, content);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Policy>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Permanently deletes a PDP policy on a DataSet in your Domo instance.
        /// </summary>
        /// <param name="pdpId">The ID of the PDP policy to delete.</param>
        /// <param name="dataSetId">The ID of the DataSet associated to the PDP policy.</param>
        /// <returns>Returns a bool of whether or not the Policy was successfully deleted</returns>
        public async Task<bool> DeletePolicyAsync(long pdpId, string dataSetId)
        {
            string uri = $"v1/datasets/{dataSetId}/policies/{pdpId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// List the Personalized Data Permission (PDP) policies for a specified DataSet.
        /// </summary>
        /// <param name="dataSetId">The ID of the DataSet associated to the PDP policy.</param>
        /// <returns>Returns all PDP policies that are applied to the DataSet specified in request. <see cref="Domo.DataSets.Policy"/></returns>
        public async Task<IEnumerable<Policy>> ListPoliciesAsync(string dataSetId)
        {
            string uri = $"v1/datasets/{dataSetId}/policies";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(uri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Policy>>(stringResponse, _serializerOptions);
        }
    }
}