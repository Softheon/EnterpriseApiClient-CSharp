using RestSharp;

namespace EnterpriseClientSample.Console
{
    /// <summary>
    /// Client class for invoking folder API 
    /// </summary>
    internal class FolderClient
    {
        /// <summary>
        /// The rest client
        /// </summary>
        private RestClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        internal FolderClient(string baseUri)
        {
            _client = new RestClient(baseUri);
        }

        /// <summary>
        /// Gets the specified folder in the specified drawer.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerId">The drawer identifier.</param>
        /// <param name="folderID">The folder identifier.</param>
        /// <returns>Folder as a JSON object</returns>
        internal IRestResponse Get(string token, int drawerID, int folderID)
        {
            var request = new RestRequest("/content/v1/folder/{drawer}/{id}", Method.GET);
            request.AddUrlSegment("drawer", drawerID.ToString());
            request.AddUrlSegment("id", folderID.ToString());

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }

        /// <summary>
        /// Inserts the specified folder.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="folder">The folder identifier.</param>
        /// <returns>Newly created Folder object.</returns>
        internal IRestResponse Insert(string token, int drawerID, string folder)
        {
            var request = new RestRequest("/content/v1/folder/{drawer}", Method.POST);
            request.AddUrlSegment("drawer", drawerID.ToString());
            request.AddParameter("application/json", folder, ParameterType.RequestBody);

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }

        /// <summary>
        /// Updates the specified folder.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="folderID">The folder identifier.</param>
        /// <param name="folder">The folder.</param>
        /// <returns>Empty response.</returns>
        internal IRestResponse Update(string token, int drawerID, int folderID, string folder)
        {
            var request = new RestRequest("/content/v1/folder/{drawer}/{id}", Method.PUT);
            request.AddUrlSegment("drawer", drawerID.ToString());
            request.AddUrlSegment("id", folderID.ToString());
            request.AddParameter("application/json", folder, ParameterType.RequestBody);

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }

        /// <summary>
        /// Deletes the specified folder.
        /// </summary>
        /// <param name="token">The OAuth token.</param>
        /// <param name="drawerID">The drawer identifier.</param>
        /// <param name="folderID">The folder identifier.</param>
        /// <returns>Empty resposne.</returns>
        internal IRestResponse Delete(string token, int drawerID, int folderID)
        {
            var request = new RestRequest("/content/v1/folder/{drawer}/{id}", Method.DELETE);
            request.AddUrlSegment("drawer", drawerID.ToString());
            request.AddUrlSegment("id", folderID.ToString());

            // Add token
            request.AddHeader("authorization", string.Format("Bearer {0}", token));

            var response = _client.Execute(request);
            return response;
        }
    }
}
