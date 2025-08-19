// Executa no console.
Run();

// Método principal para testar no console.
static void Run()
{
  try
  {
    var product1 = GetProduct(1);
    Console.WriteLine("Sucesso: " + product1.Name);

    var product2 = GetProduct(2); // id 2 não existe e vai lançar um erro
    Console.WriteLine("Sucesso: " + product2.Name);
  }
  catch (Exception error)
  {
    Console.WriteLine("Erro capturado: " + error.Message);
  }

  // Método que retorna Product
  static Product GetProduct(int id)
  {
    if (id != 1)
    {
      throw new Exception("Produto não encontrado"); // simula falha
    }
    return new Product { Id = 1, Name = "Produto Exemplo" }; // sucesso -> produto com id 1
  }
}

// Classe Product
class Product
{
  public int Id { get; set; }
  public string Name { get; set; }
}
