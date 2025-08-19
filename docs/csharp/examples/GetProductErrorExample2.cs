// Executa no console
await RunAsync();

// Função simulando problema de negócio (produto não encontrado)
async Task<Product> GetProduct(int id)
{
  await Task.Delay(10); // Simula operação assíncrona

  if (id != 1)
    throw new NotFoundException($"Produto ID: {id} não encontrado"); // problema de negócio

  return new Product { Id = 1, Name = "Produto Exemplo" }; // sucesso
}

// Função principal simulando falha técnica (ex: consulta ao banco)
async Task RunAsync()
{
  try
  {
    var product = await GetProduct(2); // lança NotFoundException
    Console.WriteLine("Sucesso: " + product.Name);
  }
  catch (Exception err)
  {
    if (err is NotFoundException)
    {
      Console.WriteLine("Erro de negócio: " + err.Message);
    }
    else
    {
      // captura qualquer outro erro técnico e lança InternalServerError
      throw new InternalServerError("Falha técnica ao buscar produto", err);
    }
  }
}

// Classes de erro didáticas
class NotFoundException : Exception
{
  public NotFoundException(string message) : base(message) { }
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
