using MvcSegundaPracticaMarcos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcSegundaPracticaMarcos.Services
{
    public class ServiceIA
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceIA(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiPreguntas");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> PreguntarAsync(string pregunta)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(this.header);

                var body = JsonConvert.SerializeObject(new PreguntaRequest { Pregunta = pregunta });
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(this.ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    RespuestaIA resultado = JsonConvert.DeserializeObject<RespuestaIA>(json);
                    return resultado.Respuesta;
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
        }
    }
}