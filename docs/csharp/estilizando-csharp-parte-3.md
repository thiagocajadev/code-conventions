# Estilizando C# - Parte 3

Nessa parte, padronizamos a declaração de variáveis e métodos`em inglês`, além de explicar o uso de
`async/await` para tarefas assíncronas.

## Convenção para nomear variáveis e métodos

Dicas na hora de escolher nomes para variáveis e métodos:

- Usar **ação ou verbo no presente** para métodos/funções que executam algo.
- Evitar "ToX" (como `ToSum`) — é raro e desnecessário.
- Em inglês, nomes curtos e diretos funcionam bem: `Sum()`, `GetUser()`.
- Em português, nomes muito descritivos como `RetornaOUsuario()` podem ficar longos e confusos.
- Usar inglês ajuda a **manter os nomes curtos, claros e consistentes**, evitando ambiguidade.
- Em inglês a ligação entre palavras já é implícita no **PascalCase**.

Exemplos:

| Língua       | Nome do método                 | Nome da variável                         |
| ------------ | ------------------------------ | ---------------------------------------- |
| ❌ Português | `Somar()`, `RetornaOUsuario()` | `TotalEAtualizado`, `ListaDeIDsUsuarios` |
| ✅ Inglês    | `Sum()`, `GetUser()`           | `TotalUpdated`, `UserIdList()`           |

> [!NOTE]  
> Declarações em inglês tornam o código mais curto, consistente e compreensível por desenvolvedores
> de diferentes idiomas.

## Síncrono e Assíncrono

Em várias linguagens de programação, algumas operações são **síncronas** e precisam ser concluídas
imediatamente, enquanto outras são **assíncronas** e podem aguardar um processamento antes de
devolver uma resposta.

No **C#**, usamos as palavras reservadas **async** e **await**:

> [!IMPORTANT]  
> **async** -> define o método como assíncrono, permitindo que ele utilize **await**.
>
> **await** -> pausa a execução do método até que a **Task** seja concluída, garantindo que o
> resultado esteja disponível antes de continuar.

No **C#**, o equivalente a uma `Promise` do JavaScript é uma `Task` ou `Task<T>`.

- `Task` → representa uma operação **assíncrona** que **não retorna valor**.
- `Task<T>` → representa uma operação **assíncrona** que **retorna um valor do tipo T**(como a
  `Promise` que resolve um valor).

Exemplo simples:

```csharp
// FetchUserAsyncExample.cs
int DelayInMilliseconds = 3000;

// chama a tarefa e pausa a execução de forma não bloqueante.
await FetchUserAsync(DelayInMilliseconds);

static async Task FetchUserAsync(int delayInMilliseconds)
{
  Console.WriteLine($"Buscando o usuário em {delayInMilliseconds / 1000} segundos...");

  var user = await Task.Run(async () =>
  {
      await Task.Delay(delayInMilliseconds); // espera 3 segundos
      return new { Id = 1, Nome = "Thiago" }; // retorna um objeto anônimo
  });

  Console.WriteLine($"Usuário encontrado -> Id: {user.Id}, Nome: {user.Nome}");
}
```

`async/await `não significa automaticamente que irá executar algo na aplicação “em paralelo”. Usar
`async` apenas **marca um método como assíncrono** e permite **pausar a execução de forma não
bloqueante**, mantendo a lógica linear se você usar `await`.

Uma pequena variação no exemplo:

```csharp
// FetchUserAsyncExample.cs
int DelayInMilliseconds = 3000;

var task = FetchUserAsync(DelayInMilliseconds); // inicia a Task

Console.WriteLine("Escrevendo uma linha aqui de forma síncrona.");

// Aguarda a conclusão da task de forma assíncrona.
// A execução deste método pausa aqui, mas a aplicação não fica travada.
await task;

static async Task FetchUserAsync(int delayInMilliseconds)
{
  Console.WriteLine($"Buscando o usuário em {delayInMilliseconds / 1000} segundos...");

  var user = await Task.Run(async () =>
    {
      await Task.Delay(delayInMilliseconds); // espera 3 segundos
      return new { Id = 1, Nome = "Thiago" }; // retorna um objeto anônimo
    });

  Console.WriteLine($"Usuário encontrado -> Id: {user.Id}, Nome: {user.Nome}");
}
```

Exemplo com 2 tarefas rodando ao mesmo tempo:

```csharp
// FetchUserTwoTasksAsyncExample.cs
int delay1 = 2000;
int delay2 = 4000;

var task1 = FetchUserAsync(delay1, "Usuário A");
var task2 = FetchUserAsync(delay2, "Usuário B");

Console.WriteLine("Ambas as buscas começaram ao mesmo tempo...");

// Aguarda as duas terminarem (sem travar o programa).
await Task.WhenAll(task1, task2);

Console.WriteLine("As duas buscas foram concluídas!");

// Método assíncrono que simula busca.
static async Task FetchUserAsync(int delayInMilliseconds, string nome)
{
    Console.WriteLine($"Buscando {nome}, vai demorar {delayInMilliseconds / 1000} segundos...");
    await Task.Delay(delayInMilliseconds); // simula operação assíncrona
    Console.WriteLine($"{nome} carregado!");
}
```

### E o que é esse `<T>`?

Em C#, quando você vê algo como `Task<T>` ou `List<T>`, o `<T>` indica um tipo genérico. Como a
linguagem é **fortemente tipada**, você precisa especificar qual tipo será usado. A letra **T** é
apenas uma **convenção**, que significa `Type` ou **tipo genérico**, mas você poderia usar qualquer
letra ou nome (Task<Usuario>, List<int>, etc.).

```csharp
// Task que retorna um inteiro.
Task<int> GetNumberAsync();

// Task que retorna um objeto usuário.
Task<Usuario> GetUserAsync();
```

Exemplo mais sofisticado, usando um tipo genérico, escolhendo qual tipo será retornado na **chamada
do método**:

```csharp
// FetchUserGenericAsyncExample.cs
// Variável de delay
int delayInMilliseconds = 3000;

// Busca o usuário. É passado um <User> na chamada do método.
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
```

Pra mim é sempre uma confusão quando há coisas genéricas no código. Sempre **opto pelo mais
simples**, mas o uso de tipos genéricos tem como **principal beneficio** a **diminuição de código
redundante**. Vamos simplificar, analisando a parte mais difícil do exemplo:

```csharp
// Método original
static async Task<T> FetchAsync<T>(int delayInMilliseconds) where T : new(){...}

// Pode ser escrito assim pra facilitar, fugindo da convenção.
static async Task<TipoGenerico> FetchAsync<TipoGenerico>(int delayInMilliseconds) where TipoGenerico : new(){...}
```

Destrinchando o método
`static async Task<T> FetchAsync<T>(int delayInMilliseconds) where T : new()`, vamos entender cada
pedacinho dele:

| Parte                       | Significado                                                                                                                                                |
| --------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `static`                    | Indica que o método pertence à **classe**, não a uma instância específica. Você pode chamar o método sem criar um objeto da classe.                        |
| `async`                     | Marca o método como **assíncrono**, permitindo usar `await` dentro dele. Isso faz com que ele **retorne imediatamente uma Task**, sem bloquear o programa. |
| `Task<T>`                   | Tipo de retorno do método. `Task<T>` significa que o método é assíncrono e **irá devolver um valor do tipo T** no futuro, quando terminar a execução.      |
| `FetchAsync<T>`             | Nome do método. O `<T>` indica que ele é **genérico**, ou seja, o tipo T será definido **na hora da chamada do método**.                                   |
| `(int delayInMilliseconds)` | Parâmetro que define o tempo de espera (simula uma operação assíncrona).                                                                                   |
| `where T : new()`           | Restrição de tipo genérico. Garante que `T` tenha um **construtor sem parâmetros**, permitindo criar uma nova instância com `new T()`.                     |

### Quando devo criar um método assíncrono?

Sempre que a operação não for **instantânea** ou depender de serviços externos, crie um método
assíncrono para:

- Chamar uma API.
- Acessar um banco de dados.
- Ler ou escrever arquivos.
- Operações com timers ou delays.

> [!NOTE]  
> A ideia é que qualquer operação que retorne uma `Task` (ou `Task<T>`) deve estar dentro de um
> método **async**, para poder usar **await** e manter o código legível.

```csharp
// Exemplo buscando dados de uma API e convertendo para string.
using System.Net.Http;

static async Task FetchDataAsync()
{
  try
  {
      using var http = new HttpClient();
      var response = await http.GetStringAsync("https://thiagocaja.dev/api/users");
      Console.WriteLine(response);
  }
  catch (Exception ex)
  {
      Console.WriteLine($"Erro ao buscar dados: {ex.Message}");
  }
}

await FetchDataAsync();
```

Explicando o código:

- await `http.GetStringAsync(...)` espera a resposta da API.
- O retorno já é uma string contendo o `JSON`.
- `try/catch` captura qualquer erro na operação assíncrona.

Antes do **async/await**, usava-se **callbacks** e **ContinueWith()** em Tasks, que eram difíceis de
ler e escalar:

```csharp
// ❌ Exemplo com ContinueWith (verboso e confuso em cadeias maiores).
static Task<string> WaitSecondsThen(int seconds)
{
  return Task.Run(async () =>
  {
      await Task.Delay(seconds * 1000);
      return $"esperou {seconds}s";
  });
}

Console.WriteLine("Início");

WaitSecondsThen(3).ContinueWith(task =>
{
  Console.WriteLine(task.Result);
  Console.WriteLine("Fim");
});
```

Agora uma versão moderna:

```csharp
// ❌ Exemplo com operação síncrona simulada (trava a aplicação).
static string WaitSecondsSync(int seconds)
{
    var end = DateTime.Now.AddSeconds(seconds);
    while (DateTime.Now < end)
    {
        // trava a thread principal
    }
    return "feito!";
}

Console.WriteLine("Início");
var result = WaitSecondsSync(3); // trava a aplicação por 3 segundos
Console.WriteLine(result);
Console.WriteLine("Fim");

// ✅ Operação assíncrona usando async/await.
static async Task<string> WaitSecondsAsync(int seconds)
{
    await Task.Delay(seconds * 1000);
    return "feito!";
}

Console.WriteLine("Início");
var asyncResult = await WaitSecondsAsync(3); // não trava a aplicação
Console.WriteLine(asyncResult);
Console.WriteLine("Fim");
```

Vantagens da operação assíncrona:

- `await WaitSecondsAsync(3)` espera a **Task** terminar sem travar a aplicação.
- O código fica linear e legível, sem **ContinueWith** aninhados.
- Outras operações podem rodar enquanto a **Task** está pendente.

### Diferença entre `await Task.Delay` e `await http.GetStringAsync`

`await Task.Delay(...)` é usado para simular ou criar manualmente uma operação assíncrona. Exemplo
de uso: esperar alguns segundos antes de continuar.

```csharp
// Esse código não depende de serviços externos, apenas pausa de forma assíncrona.
await Task.Delay(3000); // espera 3 segundos
```

`await http.GetStringAsync(...)` é usado para operações que acessam recursos externos, como APIs ou
servidores. Esse método já retorna uma **Task**, então podemos usar `await` diretamente.

```csharp
// Esse código depende de um recurso externo,
// pode demorar algum tempo para processar e retornar os dados reais.
using var http = new HttpClient();
var response = await http.GetStringAsync("https://api.exemplo.com/users");

Console.WriteLine(response);
```

### Diferença entre JSON e Objeto C#

JSON é apenas um **texto estruturado no formato { "chave": "valor" }**. Já em **C#**, manipulamos
**objetos fortemente tipados**.

```csharp
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
```

Por aqui acabou. Mais dicas para código estruturado na [parte 4](estilizando-csharp-parte-4.md).
