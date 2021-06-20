using Hyms.Api.IntegrationTests.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hyms.Api.IntegrationTests.Expenses
{
    [Collection("ApiCollection")]
    public class DeleteShould
    {
        private readonly ApiServer _server;
        private readonly HttpClient _client;

        public DeleteShould(ApiServer server)
        {
            _server = server;
            _client = server.Client;
        }

        [Fact]
        public async Task DeleteExistingItem()
        {
            var item = await new PostShould(_server).PostShouldCreateNewExpense();

            var response = await _client.DeleteAsync(new Uri($"api/Expenses/{item.Id}", UriKind.Relative));
            response.EnsureSuccessStatusCode();
        }
    }
}
