using Movies.Models.LoginRequest;
using System.Net.Http.Headers;

namespace Movies.ConstantFile
{
    public class Apiservice
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Apiservice(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:7205");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AuthenticateAsync(LoginRequest loginRequest)
        {

            var response = await _httpClient.PostAsJsonAsync("api/UserAuth/Login", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                // Authentication successful, extract JWT token from response
                var tokenresponse = await response.Content.ReadFromJsonAsync<TokenResponseTR>();
                // Store the token in session storage
                _httpContextAccessor.HttpContext.Session.SetString("JWTToken", tokenresponse.Token);
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<string> GetDataAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Movie/GetAll");

            // Get JWT token from session storage
            var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(jwtToken))
            {
                // Include JWT token in the authorization header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Handle error response
                return null;
            }
        }
    }
}
