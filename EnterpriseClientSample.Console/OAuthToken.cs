using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace EnterpriseClientSample.Console
{
    /// <summary>
    /// Static class used to fetch new OAuthTokens
    /// </summary>
    /// <see cref="https://github.com/Softheon/AccessTokenRequest-CSharp"/>
    internal static class OAuthToken
    {
        /// <summary>
        /// Gets an Access Token from the given token endpoint URI using the given Client ID and Client Secret
        /// </summary>
        /// <param name="uri">The token endpoint URI.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="scope">The scope.</param>
        /// <returns>Bearer token as a string.</returns>
        internal static string Get(string uri, string clientId, string clientSecret, string scope)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(uri);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            string encodedClientCredentials = EncodeClientCredentials(clientId, clientSecret);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedClientCredentials);

            FormUrlEncodedContent encodedContent = EncodeFormContent(scope);

            CancellationToken cancellationToken = default(CancellationToken);

            HttpResponseMessage response = client.PostAsync(string.Empty, encodedContent, cancellationToken).Result;

           return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Encodes the Client ID and Client Secret as a Base 64 String.
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <returns>String</returns>
        private static string EncodeClientCredentials(string clientId, string clientSecret)
        {
            Encoding encoding = Encoding.UTF8;
            string clientCredentials = String.Format("{0}:{1}", clientId, clientSecret);
            return Convert.ToBase64String(encoding.GetBytes(clientCredentials));
        }

        /// <summary>
        /// Form URL Encodes the grant_type and scope.
        /// </summary>
        /// <param name="scope">Scope</param>
        /// <returns>FormUrlEncodedContent</returns>
        private static FormUrlEncodedContent EncodeFormContent(string scope)
        {
            Dictionary<string, string> content = new Dictionary<string, string> {
                { "grant_type", "client_credentials" },
                { "scope", scope }
            };

            return new FormUrlEncodedContent(content);
        }
    }
}
