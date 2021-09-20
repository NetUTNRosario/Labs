using AngleSharp;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class IntegrationTestWebAfterAuthentication : IClassFixture<WebApplicationFactory<Web.Startup>>
    {
        private readonly HttpClient _client;

        public IntegrationTestWebAfterAuthentication(WebApplicationFactory<Web.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task VisitRootPage_ShouldRenderTwoMateriaCardsAndTheFirstOneMustHaveCertainCardSubtitle()
        {
            // Arrange
            var response = await _client.GetAsync("/");
            /// Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(Configuration.Default);

            // Act
            var pageContent = await response.Content.ReadAsStringAsync();
            var parsedDocument = context.GetService<IHtmlParser>().ParseDocument(pageContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(actual: parsedDocument.QuerySelectorAll(".card").Length, expected: 2);
            Assert.Contains(actualString: parsedDocument.QuerySelectorAll(".card-subtitle")[0].TextContent
                            , expectedSubstring: "Horas Semanales: 4");
        }

        [Fact]
        public async Task VisitCreateMateria_ShouldRedirectToLoginForm()
        {
            await ProtectedActionRedirectsToLoginTest(inputUrl: "/Materia/Create", redirectedUrl: "http://localhost/Account/Login?ReturnUrl=%2FMateria%2FCreate");
        }

        [Fact]
        public async Task VisitEditMateria_ShouldRedirectToLoginForm()
        {
            await ProtectedActionRedirectsToLoginTest(inputUrl: "/Materia/Edit/1", redirectedUrl: "http://localhost/Account/Login?ReturnUrl=%2FMateria%2FEdit%2F1");
        }

        [Fact]
        public async Task VisitDeleteMateria_ShouldRedirectToLoginForm()
        {
            await ProtectedActionRedirectsToLoginTest(inputUrl: "/Materia/Delete/1", redirectedUrl: "http://localhost/Account/Login?ReturnUrl=%2FMateria%2FDelete%2F1");
        }

        private async Task ProtectedActionRedirectsToLoginTest(string inputUrl, string redirectedUrl)
        {
            // Act
            var response = await _client.GetAsync(inputUrl);
            var pageContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(actual: response.RequestMessage.RequestUri.AbsoluteUri, expected: redirectedUrl);
            Assert.Contains("Password", pageContent);
        }
    }
}
