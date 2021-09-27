using AngleSharp;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class IntegrationTestWebBeforeAuthentication : IClassFixture<WebApplicationFactory<Web.Startup>>
    {
        private readonly HttpClient _client;

        public IntegrationTestWebBeforeAuthentication(WebApplicationFactory<Web.Startup> factory)
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
        public async Task VisitCreateMateriaForm_ShouldDisplayThreeValidationSectionsAndTheSelectMustIncludeIngSistemasOption()
        {
            // Arrange
            var response = await _client.GetAsync("/Materia/Create");
            /// Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(Configuration.Default);

            // Act
            var pageContent = await response.Content.ReadAsStringAsync();
            var parsedDocument = context.GetService<IHtmlParser>().ParseDocument(pageContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(actual: parsedDocument.QuerySelectorAll("span[class='text-danger field-validation-valid']").Length, expected: 3);
            Assert.Contains(actualString: parsedDocument.QuerySelector("option[value='1']").TextContent
                            , expectedSubstring: "Ingeniería en Sistemas de Información:2008");
        }
    }
}
