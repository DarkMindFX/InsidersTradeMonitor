using GraphQL.Client.Http;
using ITM.Service.GraphQL;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using Xunit;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Common.Response;
using ITM.Service.GraphQL.Types;
using GraphQL.Types;

namespace ITM.Test.Service.GraphQLTests
{
    public class EntitiesTest : E2ETestBase, IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public EntitiesTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("Queries\\Entities\\000.GetAll\\Query_AllFields.json")]
        public async void Entities_GetAll(string queryPath)
        {
            string queryContent = base.LoadQuery(queryPath);

            var serializer = new NewtonsoftJsonSerializer();

            var client = _factory.CreateClient();

            var graphQLOptions = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("https://localhost:7014/graphql", UriKind.Absolute),
            };

            var graphClient = new GraphQLHttpClient(graphQLOptions, serializer, client);

            var query = new GraphQLHttpRequest
            {
                Query = queryContent
            };

            var response = await graphClient.SendQueryAsync<ListGraphType<Entity>>(query);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            
                
            
        }
    }
}