using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Hyms.Api.Model.Login;
using Hyms.Api.Model.Users;
using Hyms.Api.IntegrationTests.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Hyms.Api.IntegrationTests.Common
{
    public class ApiServer : IDisposable
    {
        public const string Username = "admin";
        public const string Password = "admin";

        private IConfigurationRoot _config;

        public ApiServer()
        {
            _config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = GetAuthenticatedClient(Username, Password);
        }

        public HttpClient GetAuthenticatedClient(string username, string password)
        {
            var client = Server.CreateClient();
            var response = client.PostAsync("/api/Login/Authenticate",
                          new JsonContent(new LoginModel { Password = password, Username = username })).Result;

            response.EnsureSuccessStatusCode();

            var data = JsonConvert.DeserializeObject<UserWithTokenModel>(response.Content.ReadAsStringAsync().Result);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + data.Token);
            return client;
        }

        public HttpClient Client { get; private set; }

        public TestServer Server { get; private set; }

        public void Dispose()
        {
            if(Client != null)
            {
                Client.Dispose();
                Client = null;
            }

            if(Server != null)
            {
                Server.Dispose();
                Client = null;
            }
        }

    }
}
