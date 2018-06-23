using HomeExam.Controllers.Request;
using HomeExam.Controllers.Response;
using HomeExam.Core.Models;
using HomeExam.Test.Helper;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeExam.Test
{
    public class ProjectsControllerIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProjectsControllerIntegrationTest(WebApplicationFactory<Startup> webAppFactory)
        {
            var testWebAppFactory = Utilities.BuildWebAppFactory(webAppFactory);

            // Create an HttpClient to submit requests against the test host.
            _client = testWebAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task Filter()
        {
            var response = await _client.GetAsync("api/projects?page=1&pageSize=10");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<QueryResult<ProjectResponse>>(responseString);
            Assert.Equal(3, result.TotalCount);
        }

        [Fact]
        public async Task GetNotFound()
        {
            var response = await _client.GetAsync("api/projects/100");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetSuccess()
        {
            var response = await _client.GetAsync("api/projects/2");
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProjectResponse>(responseString);
            Assert.Equal("Project2", result.Name);
        }

        [Fact]
        public async Task Post()
        {
            var request = new ProjectRequest()
            {
                Name = "Project4",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Contacts = new List<int> { 1, 2 }
            };

            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/projects", stringContent);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var project = JsonConvert.DeserializeObject<ProjectResponse>(responseString);
            Assert.Equal(4, project.Id);
            Assert.Equal(2, project.Contacts.Count);
        }

        [Fact]
        public async Task PutNotFound()
        {
            var request = new ProjectRequest()
            {
                Id = 100,
                Name = "Project33",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Contacts = new List<int> { 1, 2 }
            };

            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/projects/100", stringContent);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutSuccess()
        {
            var request = new ProjectRequest()
            {
                Id = 3,
                Name = "Project33",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Contacts = new List<int> { 1, 2 }
            };

            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/projects/3", stringContent);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var project = JsonConvert.DeserializeObject<ProjectResponse>(responseString);
            Assert.Equal("Project33", project.Name);
            Assert.Equal(2, project.Contacts.Count);
        }

        [Fact]
        public async Task DeleteNotFound()
        {
            var response = await _client.DeleteAsync("/api/projects/100");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteSuccess()
        {
            var response = await _client.DeleteAsync("api/projects/1");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("1", responseString);

            response = await _client.DeleteAsync("/api/projects/1");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
