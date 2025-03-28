using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NugetProyectoBackPeluqueria.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ProyectoBackPeluqueria.Services
{
    public class ServicePeluqueria
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServicePeluqueria(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>
            ("ApiUrls:ApiProyectoBackPeluqueria");
            this.Header = new
            MediaTypeWithQualityHeaderValue("application/json");
        }


        public async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(Header);
                var response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<(string token, Usuario user)> LoginAsync(string email, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/auth/login";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                LoginModel model = new LoginModel
                {
                    Email = email,
                    Password = password
                };

                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject keys = JObject.Parse(data);

                    string token = keys.GetValue("authToken")?.ToString();
                    Usuario usuario = keys["user"]?.ToObject<Usuario>();

                    return (token, usuario);
                }
                else
                {
                    return (null, null);
                }
            }
        }


        public async Task<bool> RegisterAsync(Usuario usuario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/auth/register"; // Ruta de la API
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(usuario);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(request, content);

                return response.IsSuccessStatusCode; // Devuelve `true` si se registró correctamente
            }
        }

    }
}
