using FluentAssertions;
using Hyms.Api.IntegrationTests.Common;
using Hyms.Api.Model.Expenses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hyms.Api.IntegrationTests.Expenses
{
    [Collection("ApiCollection")]
    public class GetItemShould
    {
        private readonly ApiServer _server;
        private readonly HttpClient _client;
        private Random _random;

        public GetItemShould(ApiServer server)
        {
            _server = server;
            _client = _server.Client;
            _random = new Random();
        }

        [Fact]
        public async Task GetItemShouldReturnItemById()
        {
            var item = await new PostShould(_server).PostShouldCreateNewExpense();

            var result = await GetById(_client, item.Id);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetItemShouldReturn404StatusIfItemNotFoundById()
        {
            var response = await _client.GetAsync(new Uri($"api/Expenses/-1", UriKind.Relative));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #region Extension methods 
        public static async Task<ExpenseModel> GetById(HttpClient client, int id)
        {
            var response = await client.GetAsync(new Uri($"api/Expenses/{id}", UriKind.Relative));

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpenseModel>(result);
        }
        #endregion
    }
}
