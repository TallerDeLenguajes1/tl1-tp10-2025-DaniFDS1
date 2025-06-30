using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    
    static async Task Main(string[] args)
    {
        Console.WriteLine("cat facts api - datos curiosos sobre gatos");
        Console.WriteLine("=".PadRight(50, '='));
        
        try
        {
            // obtener un dato curioso aleatorio
            await ObtenerDatoAleatorio();
            
            Console.WriteLine("\n" + "=".PadRight(50, '='));
            
            // obtener multiples datos curiosos
            await ObtenerMultiplesDatos();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error general: {ex.Message}");
        }
        finally
        {
            client.Dispose();
        }
    }
    
    static async Task ObtenerDatoAleatorio()
    {
        try
        {
            Console.WriteLine("\nobteniendo dato curioso aleatorio...");
            
            var response = await client.GetAsync("https://catfact.ninja/fact");
            response.EnsureSuccessStatusCode();
            
            var jsonString = await response.Content.ReadAsStringAsync();
            var catFact = JsonSerializer.Deserialize<CatFact>(jsonString);
            
            if (catFact != null)
            {
                Console.WriteLine($"\ndato curioso:");
                Console.WriteLine($"{catFact.Fact}");
                Console.WriteLine($"longitud: {catFact.Length} caracteres");
                
                // guardar dato individual
                var jsonIndividual = JsonSerializer.Serialize(catFact, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync("dato_aleatorio.json", jsonIndividual);
                Console.WriteLine("guardado en: dato_aleatorio.json");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"error de conexion: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"error al procesar json: {ex.Message}");
        }
    }
    
    static async Task ObtenerMultiplesDatos()
    {
        try
        {
            Console.WriteLine("\nobteniendo lista de datos curiosos...");
            
            var response = await client.GetAsync("https://catfact.ninja/facts?limit=10");
            response.EnsureSuccessStatusCode();
            
            var jsonString = await response.Content.ReadAsStringAsync();
            var factsResponse = JsonSerializer.Deserialize<CatFactsResponse>(jsonString);
            
            if (factsResponse != null && factsResponse.Data.Count > 0)
            {
                Console.WriteLine($"\n{factsResponse.Data.Count} datos curiosos:");
                
                for (int i = 0; i < factsResponse.Data.Count; i++)
                {
                    var fact = factsResponse.Data[i];
                    Console.WriteLine($"\n{i + 1:D2}. {fact.Fact}");
                    Console.WriteLine($"{fact.Length} caracteres");
                }
                
                // informacion de paginacion
                Console.WriteLine($"\ninformacion de paginacion:");
                Console.WriteLine($"   pagina actual: {factsResponse.CurrentPage}");
                Console.WriteLine($"   total de datos: {factsResponse.Total}");
                Console.WriteLine($"   datos por pagina: {factsResponse.PerPage}");
                Console.WriteLine($"   total de paginas: {factsResponse.LastPage}");
                
                // guardar datos completos
                var jsonCompleto = JsonSerializer.Serialize(factsResponse, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync("datos_curiosos_gatos.json", jsonCompleto);
                Console.WriteLine($"\ndatos guardados en: datos_curiosos_gatos.json");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"error de conexion: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"error al procesar json: {ex.Message}");
        }
    }
}