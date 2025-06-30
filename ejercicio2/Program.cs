using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    
    static async Task Main(string[] args)
    {
        try
        {
            // peticion get a la api de usuarios
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users/");
            
            // verificar que la peticion fue exitosa
            response.EnsureSuccessStatusCode();
            
            // leer el contenido como string
            var jsonString = await response.Content.ReadAsStringAsync();
            
            // deserializar json a lista de usuarios
            var usuarios = JsonSerializer.Deserialize<List<Usuario>>(jsonString);
            
            if (usuarios != null && usuarios.Any())
            {
                // mostrar solo los primeros 5 usuarios
                var primerosUsuarios = usuarios.Take(5).ToList();
                
                Console.WriteLine("=== primeros 5 usuarios ===\n");
                
                foreach (var usuario in primerosUsuarios)
                {
                    Console.WriteLine($"nombre: {usuario.Name}");
                    Console.WriteLine($"email: {usuario.Email}");
                    Console.WriteLine($"domicilio: {usuario.Address.Street} {usuario.Address.Suite}, {usuario.Address.City} - {usuario.Address.Zipcode}");
                    Console.WriteLine(new string('-', 50));
                }
                
                // guardar todos los usuarios en archivo json
                var jsonCompleto = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync("usuarios.json", jsonCompleto);
                
                Console.WriteLine($"\n se guardaron {usuarios.Count} usuarios en usuarios.json");
            }
            else
            {
                Console.WriteLine(" no se encontraron usuarios");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($" error de conexion: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($" error al procesar json: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" error inesperado: {ex.Message}");
        }
        finally
        {
            client.Dispose();
        }
    }
}