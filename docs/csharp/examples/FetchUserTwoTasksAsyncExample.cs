// Exemplo: duas tasks rodando ao mesmo tempo.
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
