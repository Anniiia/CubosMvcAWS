using CubosMvcAWS.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CubosMvcAWS.Services
{
    public class ServiceCubos
    {
        private MediaTypeWithQualityHeaderValue header;

        private string UrlApi;
        public ServiceCubos(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrl:ApiCubos");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }

        }

        public async Task<List<Cubo>> GetCubosAsync()
        {
            string request = "api/cubos";
            List<Cubo> data = await this.CallApiAsync<List<Cubo>>(request);
            return data;
        }

        public async Task<List<string>> GetMarcasAsync()
        {
            string request = "api/cubos/getmarcas";
            List<string> data = await this.CallApiAsync<List<string>>(request);
            return data;
        }

        public async Task<List<Cubo>> GetCubosMarcaAsync(string marca)
        {
            string request = "api/cubos/find/" + marca;
            List<Cubo> data =  await this.CallApiAsync<List<Cubo>>(request);
            return data;
        }

        public async Task<Cubo> FindCubo(int id)
        {
            string request = "api/cubos/" + id;
            Cubo data =await this.CallApiAsync<Cubo>(request);
            return data;
        }

        public async Task CreateCuboAsync(Cubo cubo)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/cubos";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(cubo);
                StringContent content =
                    new StringContent(json, this.header);
                HttpResponseMessage response =
                    await client.PostAsync(this.UrlApi + request
                    , content);
            }
        }

        public async Task UpdateCuboAsync(Cubo cubo)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(cubo);
                StringContent content =
                    new StringContent(json, this.header);
                HttpResponseMessage response =
                    await client.PutAsync(this.UrlApi + request
                    , content);
            }
        }

        public async Task DeleteCuboAsync(int idcubo)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/cubos/" + idcubo;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.DeleteAsync(this.UrlApi + request);
            }
        }

    }
}