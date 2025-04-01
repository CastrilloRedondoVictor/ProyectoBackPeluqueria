using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NugetProyectoBackPeluqueria.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackPeluqueria.Services
{
    public class ServicePeluqueria
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;
        private IHttpContextAccessor HttpContextAccessor;

        public ServicePeluqueria(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.UrlApi = configuration.GetValue<string>
            ("ApiUrls:ApiProyectoBackPeluqueria");
            this.Header = new
            MediaTypeWithQualityHeaderValue("application/json");
            HttpContextAccessor = httpContextAccessor;
        }


        public string GetToken()
        {
            // Verifica si el token existe en los claims
            var token = HttpContextAccessor.HttpContext.User.FindFirst(x => x.Type == "Token")?.Value;

            // Si el token es null o vacío, puedes imprimirlo para depuración
            Console.WriteLine($"Token: {token}");

            return token;
        }



        public async Task<T> CallApiAsync<T>(string request, HttpMethod method, object data = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(Header);

                // Obtener el token y añadirlo a los headers
                string token = GetToken();
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                HttpRequestMessage httpRequest = new HttpRequestMessage(method, request);

                if (data != null)
                {
                    string json = JsonConvert.SerializeObject(data);
                    httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = await client.SendAsync(httpRequest);

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

        public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
        {
            var response = await CallApiAsync<bool>("api/management/UpdateUsuario", HttpMethod.Put, usuario);
            return response;
        }

        public async Task<Usuario> FindUsuario(int id)
        {
            var response = await CallApiAsync<Usuario>($"api/management/GetPerfil/{id}", HttpMethod.Get);
            return response;
        }






        public async Task<Reserva> FindReservaAsync(int id)
        {
            var response = await CallApiAsync<Reserva>($"api/reservas/FindReserva/{id}", HttpMethod.Get);
            return response;
        }

        public async Task<Reserva> GetProximaReservaUsuarioAsync(int id)
        {
            var response = await CallApiAsync<Reserva>($"api/reservas/GetProximaReservaUsuario/{id}", HttpMethod.Get);
            return response;
        }

        public async Task<string> GetServicioReservaAsync(int id)
        {
            var response = await CallApiAsync<string>($"api/reservas/GetServicioReserva/{id}", HttpMethod.Get);
            return response;
        }

        public async Task<(int diasAgregados, int diasExistentes)> AgregarDisponibilidadRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var response = await CallApiAsync<(int diasAgregados, int diasExistentes)>($"api/reservas/AgregarDisponibilidadRango?fechaInicio={fechaInicio}&fechaFin={fechaFin}", HttpMethod.Post);
            return response;
        }

        public async Task<List<(DateTime FechaInicio, DateTime FechaFin, string Servicio, int ReservaId)>> ObtenerCitasConHoras()
        {
            var response = await CallApiAsync<List<(DateTime FechaInicio, DateTime FechaFin, string Servicio, int ReservaId)>>("api/reservas/ObtenerCitasConHoras", HttpMethod.Get);
            return response;
        }

        public async Task<List<DateTime>> ObtenerDiasDisponibles()
        {
            var response = await CallApiAsync<List<DateTime>>("api/reservas/ObtenerDiasDisponibles", HttpMethod.Get);
            return response;
        }
    }
}
