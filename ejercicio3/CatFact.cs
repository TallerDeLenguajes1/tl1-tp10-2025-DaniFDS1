using System.Text.Json.Serialization;

// clase para un dato curioso individual
public class CatFact
{
    [JsonPropertyName("fact")]
    public string Fact { get; set; } = string.Empty;

    [JsonPropertyName("length")]
    public int Length { get; set; }
}

// clase para la respuesta de multiples datos
public class CatFactsResponse
{
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("data")]
    public List<CatFactData> Data { get; set; } = new List<CatFactData>();

    [JsonPropertyName("first_page_url")]
    public string FirstPageUrl { get; set; } = string.Empty;

    [JsonPropertyName("from")]
    public int From { get; set; }

    [JsonPropertyName("last_page")]
    public int LastPage { get; set; }

    [JsonPropertyName("last_page_url")]
    public string LastPageUrl { get; set; } = string.Empty;

    [JsonPropertyName("links")]
    public List<Link> Links { get; set; } = new List<Link>();

    [JsonPropertyName("next_page_url")]
    public string NextPageUrl { get; set; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }

    [JsonPropertyName("prev_page_url")]
    public string PrevPageUrl { get; set; } = string.Empty;

    [JsonPropertyName("to")]
    public int To { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}

// clase para cada dato en la lista
public class CatFactData
{
    [JsonPropertyName("fact")]
    public string Fact { get; set; } = string.Empty;

    [JsonPropertyName("length")]
    public int Length { get; set; }
}

// clase para los enlaces de navegacion
public class Link
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}