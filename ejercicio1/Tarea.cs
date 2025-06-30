using System.Text.Json.Serialization;

// clase que representa una tarea de la api
public class Tarea
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("completed")]
    public bool Completed { get; set; }
}