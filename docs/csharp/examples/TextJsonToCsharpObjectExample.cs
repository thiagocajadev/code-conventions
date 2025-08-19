using System.Text.Json;

// Texto JSON (string).
string jsonResponse = @"
{
  ""Id"": 1,
  ""Name"": ""Thiago""
}";

Console.WriteLine("Texto estruturado em JSON:");
Console.WriteLine(jsonResponse); // Saída: { "Id": 1, "Nome": "Thiago" }

// Convertendo Texto JSON -> Objeto C#.
User user = JsonSerializer.Deserialize<User>(jsonResponse)!;
Console.WriteLine("Documento JSON convertido para objeto C#.");
Console.WriteLine($"Acessando a propriedade 'user.Name': {user.Name}"); // Thiago

// Classe User no C# para representar o JSON.
class User
{
  public int Id { get; set; }
  public string Name { get; set; }
}
