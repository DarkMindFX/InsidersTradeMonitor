using GraphQL.Client.Http;
using ITM.Service.GraphQL;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using Xunit;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Types;

namespace ITM.Test.Service.GraphQL
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

            var graphQLOptions = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("http://localhost/graphql", UriKind.Absolute),
            };

            var graphClient = new GraphQLHttpClient(graphQLOptions, serializer);

            var query = new GraphQLHttpRequest
            {
                Query = queryContent
            };

            var response = await graphClient.SendQueryAsync<ListGraphType<ITM.Service.GraphQL.Types.Entity>>(query);

        }
    }
}