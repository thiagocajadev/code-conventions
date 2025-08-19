// Variável de delay
int delayInMilliseconds = 3000;

// Busca o usuário
var user = await FetchAsync<User>(delayInMilliseconds);
Console.WriteLine($"Id: {user.Id}, Nome: {user.Nome}");

// Método assíncrono genérico
// "T" é um tipo genérico. Isso significa que não
// é preciso definir de antemão qual tipo o método vai retornar;
// você decide na hora de chamar o método.
static async Task<T> FetchAsync<T>(int delayInMilliseconds) where T : new()
{
  Console.WriteLine($"Buscando o resultado em {delayInMilliseconds / 1000} segundos...");

  await Task.Delay(delayInMilliseconds); // espera o tempo informado

  // Retorna uma nova instância de T.
  // Ex: Se na chamada do método foi definido um User, vai voltar um User.
  // Se foi definido um Product, vai retornar um Product
  return new T();
}

// Classe User
class User
{
  public int Id { get; set; }
  public string Nome { get; set; }
}
