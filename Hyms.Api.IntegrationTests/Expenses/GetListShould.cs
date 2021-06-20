using FluentAssertions;
using Hyms.Api.IntegrationTests.Common;
using Hyms.Api.Model.Common;
using Hyms.Api.Model.Expenses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hyms.Api.IntegrationTests.Expenses
{
    [Collection("ApiCollection")]

    public class GetListShould
    {
        private readonly ApiServer _server;
        private readonly HttpClient _client;

        public GetListShould(ApiServer server)
        {
            _server = server;
            _client = server.Client;
        }
     
        [Fact]
        public async Task ReturnAnyList()
        {
            var items = await Get(_client);
            items.Should().NotBeNull();
        }

        #region Extension methods 
        public static async Task<DataResult<ExpenseModel>> Get(HttpClient client)
        {
            var response = await client.GetAsync($"api/Expenses");
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<DataResult<ExpenseModel>>(responseText);
            return items;
        }

        #endregion
    }
}
