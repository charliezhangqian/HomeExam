using HomeExam.Controllers.Request;
using HomeExam.Controllers.Response;
using HomeExam.Core.Models;
using HomeExam.Test.Helper;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeExam.Test
{
    public class ContactsControllerIntergrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ContactsControllerIntergrationTest(WebApplicationFactory<Startup> webAppFactory)
        {
            var testWebAppFactory = Utilities.BuildWebAppFactory(webAppFactory);

            // Create an HttpClient to submit requests against the test host.
            _client = testWebAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task FilterByEmail()
        {
            var response = await _client.GetAsync("api/contacts?query=charlie&queryBy=email&page=1&pageSize=10");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<QueryResult<ContactResponse>>(responseString);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task GetNotFound()
        {
            var response = await _client.GetAsync("api/contacts/100");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetSuccess()
        {
            var response = await _client.GetAsync("api/contacts/2");
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProjectResponse>(responseString);
            Assert.Equal("david", result.Name);
        }

        [Fact]
        public async Task Post()
        {
            var request = new ContactRequest()
            {
                Name = "leo",
                Email = "leo@leo.com",
                Phone = "66666"
            };

            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/contacts", stringContent);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var contact = JsonConvert.DeserializeObject<ContactResponse>(responseString);
            Assert.Equal(4, contact.Id);
            Assert.Equal("leo", contact.Name);
        }

        [Fact]
        public async Task PostWithBadRequest()
        {
            var request = new ContactRequest()
            {
                Phone = "66666"
            };

            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/contacts", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutNotFound()
        {
            var request = new ContactRequest()
            {
                Name = "leo",
                Email = "leo@leo.com",
                Phone = "66666"
            };

            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/contacts/100", stringContent);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutSuccess()
        {
            var request = new ContactRequest()
            {
                Id = 3,
                Name = "susan lee",
                Email = "leo@leo.com",
                Phone = "4321"
            };

            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/contacts/3", stringContent);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var contact = JsonConvert.DeserializeObject<ContactResponse>(responseString);
            Assert.Equal("susan lee", contact.Name);
            Assert.Equal("4321", contact.Phone);
        }

        [Fact]
        public async Task DeleteNotFound()
        {
            var response = await _client.DeleteAsync("/api/contacts/100");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteSuccess()
        {
            var response = await _client.DeleteAsync("api/contacts/1");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("1", responseString);

            response = await _client.DeleteAsync("/api/contacts/1");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
