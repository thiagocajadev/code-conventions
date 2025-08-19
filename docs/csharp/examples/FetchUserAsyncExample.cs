// Simula uma operação assíncrona.
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
