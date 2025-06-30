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
            // peticion get a la api
            var response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos/");
            
            // deserializar json a lista de tareas
            var tareas = JsonSerializer.Deserialize<List<Tarea>>(response);
            
            if (tareas != null)
            {
                // mostrar tareas pendientes primero
                Console.WriteLine("=== tareas pendientes ===");
                var pendientes = tareas.Where(t => !t.Completed).ToList();
                foreach (var tarea in pendientes)
                {
                    Console.WriteLine($"id: {tarea.Id} - {tarea.Title} - estado: pendiente");
                }
                
                Console.WriteLine("\n=== tareas completadas ===");
                var completadas = tareas.Where(t => t.Completed).ToList();
                foreach (var tarea in completadas)
                {
                    Console.WriteLine($"id: {tarea.Id} - {tarea.Title} - estado: completada");
                }
                
                // serializar y guardar en archivo
                var json = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync("tareas.json", json);
                
                Console.WriteLine($"\n✅ se guardaron {tareas.Count} tareas en tareas.json");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" error: {ex.Message}");
        }
        finally
        {
            client.Dispose();
        }
    }
}