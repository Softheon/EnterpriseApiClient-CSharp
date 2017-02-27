using RestSharp;

namespace EnterpriseClientSample.Console
{
    /// <summary>
    /// Client class for invoking entity template API 
    /// </summary>
    internal class EntityTemplateClient
    {
        /// <summary>
        /// The rest client
        /// </summary>
        private RestClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityTemplateClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base API URI.</param>
        internal EntityTemplateClient(string baseUri)
        {
            _client = new RestClient(baseUri);
        }

        /// <summary>
        /// Gets the specified Entity Template by type.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="type">The template type.</param>
        /// <returns>Entity Template as a JSON object</returns>
        internal IRestResponse Get(string token, int type)
        {
            var request = new RestRequest("/template/v1/ftl/{type}", Method.GET);
            request.AddUrlSegment("type", type.ToString());

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }

        /// <summary>
        /// Inserts the specified Entity Template.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="template">The template as json.</param>
        /// <returns>New Entity Template as a JSON object</returns>
        internal IRestResponse Insert(string token, string template)
        {
            var request = new RestRequest("/template/v1/ftl", Method.POST);
            request.AddParameter("application/json", template, ParameterType.RequestBody);

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }

        /// <summary>
        /// Deletes the specified Entity Template.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="template">The template as json.</param>
        /// <returns>Emtpy response</returns>
        internal IRestResponse Delete(string token, int type)
        {
            var request = new RestRequest("/template/v1/ftl/{type}", Method.DELETE);
            request.AddUrlSegment("type", type.ToString());            

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }
    }
}
