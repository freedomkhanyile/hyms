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
    public class PutShould
    {

        private readonly ApiServer _server;
        private readonly HttpClientWrapper _client;
        private Random _random;

        public PutShould(ApiServer server)
        {
            _server = server;
            _client = new HttpClientWrapper(_server.Client);
            _random = new Random();
        }

        [Fact]
        public async Task PutShouldUpdateExistingExpense()
        {
            var item = await new PostShould(_server).PostShouldCreateNewExpense();

            var request = new UpdateExpenseModel
            {
                Date = DateTime.Now,
                Description = _random.Next().ToString(),
                Amount = _random.Next(),
                Comment = _random.Next().ToString()
            };

            await _client.PutAsync<ExpenseModel>($"api/Expenses/{item.Id}", request);

            var updatedItem = await GetItemShould.GetById(_client.Client, item.Id);

            updatedItem.Date.Should().Be(request.Date);
            updatedItem.Description.Should().Be(request.Description);

            updatedItem.Amount.Should().Be(request.Amount);
            updatedItem.Comment.Should().Contain(request.Comment);
        }
    }
}
