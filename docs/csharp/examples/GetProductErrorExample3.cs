// Execute algumas vezes para simular intermitência
try
{
  await RunAsync();
}
catch (InternalServerError ex)
{
  Console.WriteLine("InternalServerError: " + ex.Message);
}

// Função principal
async Task RunAsync()
{
  try
  {
    var product = await GetProduct(1);
    Console.WriteLine("Sucesso: " + product.Name);
  }
  catch (Exception error)
  {
    if (error is NotFoundError)
    {
      Console.WriteLine("Erro de negócio: " + error.Message);
    }
    else
    {
      Console.WriteLine("Erro técnico capturado: " + error.Message);
      Console.WriteLine(error.StackTrace);
      throw new InternalServerError("Falha técnica ao buscar produto", error);
    }
  }
}

// Função simulando falha técnica e problema de negócio
async Task<Product> GetProduct(int id)
{
  await Task.Delay(10); // Simula operação assíncrona

  if (id != 1)
    throw new NotFoundError("Produto não encontrado"); // problema de negócio

  if (new Random().NextDouble() < 0.5)
    throw new Exception("Conexão com o banco falhou"); // falha técnica 50%

  return new Product { Id = 1, Name = "Produto Exemplo" };
}

// Classes de erro didáticas
class NotFoundError : Exception
{
  public NotFoundError(string message) : base(message) { }
}

class InternalServerError : Exception
{
  public InternalServerError(string message, Exception inner = null) : base(message, inner) { }
}

// Classe Produto
class Product
{
  public int Id { get; set; }
  public string Name { get; set; }
}



