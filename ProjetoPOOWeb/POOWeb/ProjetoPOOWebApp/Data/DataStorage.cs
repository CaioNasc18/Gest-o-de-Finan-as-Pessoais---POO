using System.Text.Json;

public static class DataStorage
{
    private static readonly string BasePath =
        Path.Combine(AppContext.BaseDirectory, "Data");

    public static List<T> Load<T>(string fileName)
    {
        string path = Path.Combine(BasePath, fileName);

        if (!File.Exists(path))
            return new List<T>();

        string json = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(json))
            return new List<T>();

        return JsonSerializer.Deserialize<List<T>>(json)
               ?? new List<T>();
    }

    public static void Save<T>(string fileName, List<T> data)
    {
        string path = Path.Combine(BasePath, fileName);
        Directory.CreateDirectory(BasePath);

        string json = JsonSerializer.Serialize(data,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(path, json);
    }
}
