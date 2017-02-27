using RestSharp;

namespace EnterpriseClientSample.Console
{
    /// <summary>
    /// Client class for invoking entity API 
    /// </summary>
    internal class EntityClient
    {
        /// <summary>
        /// The rest client
        /// </summary>
        private RestClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        internal EntityClient(string baseUri)
        {
            _client = new RestClient(baseUri);
        }

        /// <summary>
        /// Gets the specified entity in the specified drawer.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerId">The drawer identifier.</param>
        /// <param name="entityID">The entity identifier.</param>
        /// <returns>Entity as a JSON object</returns>
        internal IRestResponse Get(string token, int drawerID, int entityID)
        {
            var request = new RestRequest("/content/v1/entity/{drawer}/{id}", Method.GET);
            request.AddUrlSegment("drawer", drawerID.ToString());
            request.AddUrlSegment("id", entityID.ToString());

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="entity">The entity identifier.</param>
        /// <returns>Newly created Entity object.</returns>
        internal IRestResponse Insert(string token, int drawerID, string entity)
        {
            var request = new RestRequest("/content/v1/entity/{drawer}", Method.POST);
            request.AddUrlSegment("drawer", drawerID.ToString());
            request.AddParameter("application/json", entity, ParameterType.RequestBody);

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="entityID">The entity identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Empty response.</returns>
        internal IRestResponse Update(string token, int drawerID, int entityID, string entity)
        {
            var request = new RestRequest("/content/v1/entity/{drawer}/{id}", Method.PUT);
            request.AddUrlSegment("drawer", drawerID.ToString());
            request.AddUrlSegment("id", entityID.ToString());
            request.AddParameter("application/json", entity, ParameterType.RequestBody);

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="entityID">The entity identifier.</param>
        /// <returns>Empty resposne.</returns>
        internal IRestResponse Delete(string token, int drawerID, int entityID)
        {
            var request = new RestRequest("/content/v1/entity/{drawer}/{id}", Method.DELETE);
            request.AddUrlSegment("drawer", drawerID.ToString());
            request.AddUrlSegment("id", entityID.ToString());

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }
    }
}
