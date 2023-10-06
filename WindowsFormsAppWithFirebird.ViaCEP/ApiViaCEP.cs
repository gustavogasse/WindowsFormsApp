using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WindowsFormsAppWithFirebird.ViaCEP
{
    public class ApiViaCEP
    {
        public async Task<CepResponse> BuscaLogradouro(string cep)
        {
            string zipcode = String.Join("", System.Text.RegularExpressions.Regex.Split(cep, @"[^\d]"));
            string url = $"https://viacep.com.br/ws/{zipcode}/json/";

            try
            {
                if (string.IsNullOrEmpty(zipcode))
                    throw new Exception("Digite um CEP corretamente");

                var client = new HttpClient();
                var json = await client.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<CepResponse>(json);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Problema ao localizar o CEP.\n\nErro: {ex.Message}");
            }
         }
    }
}
