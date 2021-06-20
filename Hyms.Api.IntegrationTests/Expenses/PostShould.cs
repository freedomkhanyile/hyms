using FluentAssertions;
using Hyms.Api.IntegrationTests.Common;
using Hyms.Api.IntegrationTests.Helpers;
using Hyms.Api.Model.Expenses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hyms.Api.IntegrationTests.Expenses
{
    [Collection("ApiCollection")]

    public class PostShould
    {
        private readonly ApiServer _server;
        private readonly HttpClientWrapper _client;
        private Random _random;
        
        public PostShould(ApiServer server)
        {
            _server = server;
            _client = new HttpClientWrapper(_server.Client);
            _random = new Random();
        }

        [Fact]
        public async Task<ExpenseModel> PostShouldCreateNewExpense()
        {
            var request = new CreateExpenseModel()
            {
                Amount = _random.Next(),
                Comment = _random.Next().ToString(),
                Date = DateTime.Now.AddMinutes(10),
                Description = _random.Next().ToString()
            };

            var createdItem = await _client.PostAsync<ExpenseModel>("api/Expenses", request);

            createdItem.Id.Should().BeGreaterThan(0);
            createdItem.Amount.Should().Be(request.Amount);
            createdItem.Comment.Should().Be(request.Comment);
            createdItem.Date.Should().Be(request.Date);
            createdItem.Description.Should().Be(request.Description);
            createdItem.Username.Should().Be("admin admin"); // created by our default admin user see ApiServer.cs

            return createdItem;
        }
    }
}
