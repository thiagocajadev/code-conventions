# Estilizando C# - Parte 4

Nessa parte, veja como lidar com o tratamento de erros de forma mais profissional. Além disso, usar
a técnica do direct return deixa o código mais linear.

## Tratando Erros

Fluxos que podem gerar falhas técnicas vão dentro de um **try/catch**. O detalhe está em **lançar o
erro certo**. Quando for preciso entender a causa do problema, capturar e lançar o tipo certo de
erro deixa a manutenção mais rápida e clara.

```csharp
// Exemplo1 - Simulando operação que pode falhar.
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

  // Método que retorna Product.
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
```

Erros bem estruturados ajudam a separar o que é **problema de negócio** do que é **falha técnica**.
Por exemplo, se um **produto não for encontrado**, lançamos um `NotFoundError` sem precisar de
`try/catch` naquele ponto.

Agora, se a consulta ao banco falhar, usamos `try/catch` e lançamos um `InternalServerError` ou
outro erro específico.

```csharp
// Exemplo 2 - Simulando problema de negócio, passando um Id de Produto que não existe.
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

// Classe Product.
class Product
{
  public int Id { get; set; }
  public string Name { get; set; }
}
```

```csharp
// Exemplo 3 - Simulando uma falha técnica.
// Execute algumas vezes para simular intermitência.
try
{
  await RunAsync();
}
catch (InternalServerError ex)
{
  Console.WriteLine("InternalServerError: " + ex.Message);
}

// Função principal.
async Task RunAsync()
{
  try
  {
    var product = await GetProductAsync(1);
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

// Função simulando falha técnica e problema de negócio.
async Task<Product> GetProductAsync(int id)
{
  await Task.Delay(10); // Simula operação assíncrona

  if (id != 1)
    throw new NotFoundError("Produto não encontrado"); // problema de negócio

  if (new Random().NextDouble() < 0.5)
    throw new Exception("Conexão com o banco falhou"); // falha técnica 50%

  return new Product { Id = 1, Name = "Produto Exemplo" };
}

// Classes de erro didáticas.
class NotFoundError : Exception
{
  public NotFoundError(string message) : base(message) { }
}

class InternalServerError : Exception
{
  public InternalServerError(string message, Exception inner = null) : base(message, inner) { }
}

// Classe Produto.
class Product
{
  public int Id { get; set; }
  public string Name { get; set; }
}

/*
// Saída no console:
❯ dotnet-script GetProductErrorExample3.cs
Erro técnico capturado: Conexão com o banco falhou
  at Submission#0.<GetProduct>d__2.MoveNext() in code-conventions/docs/csharp/examples/GetProductErrorExample3.cs:line 43
--- End of stack trace from previous location ---
*/
```

Isso deixa claro para quem estiver lendo o código qual foi a **origem do problema** e facilita
tratar cada caso de forma adequada, sem confusão.

Guia rápido para lembrar:

Não use **try/catch** se:

- Só está encadeando chamadas e pode deixar o erro subir naturalmente.
- Vai capturar o erro só para logar e ignorar (isso mascara problemas).
- O erro já será tratado em um nível superior (duplicar try/catch só polui).

Use **try/catch** se:

- Precisa **capturar exceções externas/inesperadas** (ex.: falha no DB, rede, IO).
- Fluxo exige **mapear erro** → ação de negócio (ex.: se não achou produto, lançar NotFoundError).
- Você está em **fronteira de execução (ex.: controller HTTP)** onde o erro precisa ser
  tratado/logado antes de responder.

```csharp
// ❌ Tratamento de erro genérico e mal feito.
// Simulando uma abstração de chamada com Dapper
public async Task<Product?> FindOneById(int id)
{
  try
  {
    // Chamada com o SQL SERVER.
    var result = await database.QuerySingleAsync<Product>(
      query: "SELECT TOP 1 * FROM Products WHERE Id = @id;",
      parameters: new { id }
    );

    if (result == null)
    {
      throw new Exception("Produto não encontrado");
    }

    return result;
  }
  catch
  {
    Console.WriteLine("Deu erro em alguma coisa");
    return null;
  }
}
```

Desvantagens:

- Lança erro como string (sem tipo ou contexto).
- catch engole o erro e não propaga.
- Log inútil, não ajuda a diagnosticar.
- Retorno null oculta a causa real.

```js
// ✅ Tratamento de erro estruturado e claro.
public async Task<Product> FindOneByIdStructuredAsync(int id)
{
  try
  {
    // Query SQL e parâmetros
    string sql = "SELECT TOP 1 * FROM Products WHERE Id = @id;";
    var parameters = new { id };

    // Chamada abstrata ao database
    var result = await database.QuerySingleAsync<Product>(
      query: sql,
      parameters: parameters
    );

    if (result == null)
    {
      // Erro de negócio
      throw new NotFoundException(
        "O id informado não foi encontrado no sistema. " +
        "Verifique se o id foi digitado corretamente."
      );
    }

    return result;
  }
  catch (Exception error)
  {
    if (error is NotFoundException)
    {
      // Repassa erros de negócio sem mascarar
      throw;
    }

    // Captura exceções inesperadas (ex.: conexão DB caiu)
    throw new InternalServerError(
      "Falha ao consultar o banco de dados.",
      error
    );
  }
}
```

Vantagens:

- Usa classes de erro específicas (NotFoundError, DatabaseError).
- Não engole erros, propaga com contexto.
- Permite tratamento diferenciado na camada superior.

### Abstraindo Erros

É uma boa prática abstrair a lógica com tratamento de erros específicos, reaproveitando em toda a
aplicação.

```js
// ✅ Abstraindo erros em uma classe, padronizando o retorno para melhor entendimento e resolução.
// Classe base para exceções em C#.
public class BaseError : Exception
{
  public string Action { get; }
  public int StatusCode { get; }

  public BaseError(
    string message,
    string action = "Entre em contato com o suporte.",
    int statusCode = 500,
    Exception? innerException = null
  ) : base(message, innerException)
  {
    Action = action;
    StatusCode = statusCode;
  }

  // Método para representar o erro como objeto JSON
  public object ToJson()
  {
    return new
    {
      error = new
      {
        name = GetType().Name,
        message = Message,
        action = Action,
        status_code = StatusCode
      }
    };
  }
}

// Erro interno
public class InternalServerError : BaseError
{
  public InternalServerError(Exception? cause = null)
    : base(
        message: "Um erro interno não esperado aconteceu.",
        action: "Entre em contato com o suporte.",
        statusCode: 500,
        innerException: cause
      ) { }
}

// Recurso não encontrado
public class NotFoundError : BaseError
{
  public NotFoundError(
    string? message = null,
    string? action = null,
    Exception? cause = null
  ) : base(
      message: message ?? "Recurso não encontrado.",
      action: action ?? "Verifique se o recurso existe.",
      statusCode: 404,
      innerException: cause
    ) { }
}

// Erro de validação
public class ValidationError : BaseError
{
  public ValidationError(
    string? message = null,
    string? action = null,
    Exception? cause = null
  ) : base(
      message: message ?? "Os dados fornecidos são inválidos.",
      action: action ?? "Revise os dados de entrada.",
      statusCode: 400,
      innerException: cause
    ) { }
}
```

Com a lógica centralizada, podemos fazer toda parte de tratamento de erros de conexão com o banco de
dados dentro da classe **database**, por exemplo.

## Retornando Direto

O uso de `direct return` (ou retorno direto) é uma prática de programação que pode melhorar a
legibilidade e a eficiência do código. Essa abordagem consiste em **retornar um valor imediatamente
de uma função**. Os detalhes de implementação ficam encapsulados em um método auxiliar.

```csharp
// ❌ Sem direct return. Mais verboso, sem sufixo padrão e com variável auxiliar desnecessária
public async Task<Product> FindOneByIdVerbose(int id)
{
  Product? productFound = null;

  // Chamada abstrata ao database
  var results = await database.QuerySingleAsync<Product>(
    query:
      """
      SELECT TOP 1
        *
      FROM
        Products
      WHERE
        Id = @id;
      """,
    parameters: new { id }
  );

  if (results == null)
  {
    throw new NotFoundError(
      "O id informado não foi encontrado no sistema.",
      "Verifique se o id foi digitado corretamente."
    );
  }
  else
  {
    productFound = results;
  }

  return productFound;
}

```

Desvantagens:

- Usa variável auxiliar desnecessária (productFound), alocando como null.
- Estrutura condicional redundante (else depois de throw).
- O retorno fica “escondido” lá embaixo.

```js
// ✅ Com direct return. Abstração da lógica, retorno direto no topo do método

// Executando o método
var productFound = await FindOneByIdAsync(id);

// Detalhes de implementação
public async Task<Product> FindOneByIdAsync(int id)
{
  // Retorno direto, usando função auxiliar para encapsular detalhes da query
  var productFound = await RunSelectQueryAsync(id);
  return productFound;

  // Função auxiliar que executa a query e lança erro se não encontrar resultado
  async Task<Product> RunSelectQueryAsync(int productId)
  {
    var result = await database.RunSelectQueryAsync<Product>(
      query: """
        SELECT TOP 1 *
        FROM Products
        WHERE Id = @id;
      """,
      parameters: new { id = productId }
    );

    if (result == null)
    {
      throw new NotFoundError(
        "O id informado não foi encontrado no sistema.",
        "Verifique se o id foi digitado corretamente."
      );
    }

    return result;
  }
}
```

Vantagens:

- Menos código e menos variáveis inúteis.
- Fluxo de leitura mais claro.
- Facilita manutenção – menos linhas, menos chance de bug, mais fácil de entender de cara.

É isso ai! Bons estudos e bons códigos.
