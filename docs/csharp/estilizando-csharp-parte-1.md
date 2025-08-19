# Estilizando C# - Parte 1

Pra quem já trabalhou com **Java** e **VB**, partir pro **Csharp** é uma ótima escolha. O **C#** é
uma linguagem robusta e fortemente tipada, ou seja, garante segurança de tipos em tempo de
compilação, evitando muitos erros comuns de programação.

Custos com licenças para empresas normalmente não são um grande problema, mas pra quem está
começando ou não tem recursos, foi sempre um empecilho.

Desde que [.NET Core](https://devblogs.microsoft.com/dotnet/net-core-is-open-source/) virou open
source, o **C#** passou a ser adotado por muitos Devs, ocupando espaço em startups, cloud e
servidores Linux.

Veja a evolução e história
[aqui](https://learn.microsoft.com/pt-br/dotnet/csharp/whats-new/csharp-version-history#c-version-10-1).

```csharp
// checando a versão instalada do SDK no terminal
dotnet --version // Saída: 8.0.118
```

## Declaração de variáveis

Se você passou pela documentação onde falo sobre [JavaScript](../js/estilizando-js-parte-1.md), vai
se lembrar do polemico `var`. No **C#**, `var` é usado para inferência de tipo estático: o
compilador deduz o tipo da variável a partir do valor atribuído, mas o tipo continua fixo em tempo
de compilação.

> [!NOTE]  
> Ao utilizar `var`, o compilador fica responsável por identificar qual é o tipo da variável.

```csharp
using System;

class Program
{
  static void Main()
  {
    // ❌ Uso de var deixa o código confuso
    var x = 10;
    var y = "teste";
    var z = new object();

    // Quem está lendo o código não sabe facilmente que tipos são esses.
    Console.WriteLine($"{x}, {y}, {z}");

    // ✅ var usado quando o tipo é óbvio
    var idade = 36; // claramente int
    var nome = "Thiago"; // claramente string
    var lista = new List<string>(); // tipo óbvio pela criação

    Console.WriteLine($"{idade}, {nome}, {lista.Count}");
  }
}
```

Exemplos de atribuição e escopo:

```csharp
using System;

class Program
{
  static void Main()
  {
    // Exemplo 1 - Re-declaração não é permitida.
    int numero = 10;
    // int numero = 20; // ❌ ERRO de compilação: já existe 'numero'
    numero = 20; // ✅ precisa reatribuir
    Console.WriteLine(numero); // 20


    // Exemplo 2 - Escopo de bloco respeitado.
    if (true)
    {
        int numeroDentroDoIf = 50;
        Console.WriteLine(numeroDentroDoIf); // 50
    }
    // Console.WriteLine(numeroDentroDoIf); // ❌ ERRO: não existe fora do bloco


    // Exemplo 3 - Escopo de função (aqui simulado com método).
    Teste();
    // Console.WriteLine(numeroDaFuncao); // ❌ ERRO: não existe fora do método


    // Exemplo 4 - Variável global acidental não existe em C#
    OutroTeste();
    // Console.WriteLine(numeroAcidental); // ❌ ERRO: não existe no escopo global
  }

  static void Teste()
  {
    int numeroDaFuncao = 100;
    Console.WriteLine(numeroDaFuncao); // 100
  }

  static void OutroTeste()
  {
    int numeroAcidental = 500; // precisa declarar sempre
    Console.WriteLine(numeroAcidental); // 500
  }
}
```

Além do **var**, temos outros
[tipos internos](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/builtin-types/built-in-types)
da linguagem, abaixo vou relacionar os mais usados:

| Categoria              | Tipo    | Valor padrão | Observações                               |
| ---------------------- | ------- | ------------ | ----------------------------------------- |
| **Inteiros**           | int     | 0            | 32 bits, -2.147.483.648 a 2.147.483.647   |
|                        | long    | 0            | 64 bits, grandes números inteiros         |
| **Ponto flutuante**    | double  | 0            | 64 bits, padrão para literais decimais    |
|                        | decimal | 0            | 128 bits, ideal para dinheiro, sufixo `m` |
| **Caractere / Texto**  | char    | '\0'         | Um único caractere Unicode                |
|                        | string  | null         | Sequência de caracteres                   |
| **Booleano**           | bool    | false        | true/false                                |
| **Objeto geral**       | object  | null         | Base de todos os tipos                    |
| **Inferência de tipo** | var     | N/A          | Tipo deduzido pelo compilador             |

### Use o `var` com critério

No **C#**, use **var** quando o tipo for óbvio pelo valor atribuído ou para simplificar tipos
longos. Se o tipo não ficar claro de cara, prefira **declarar explicitamente** (int, string, bool)
para deixar o código mais legível e seguro.

```csharp
using System;
using System.Collections.Generic;

class Program
{
  static void Main()
  {
    // Inteiros.
    int idade = 36; // 32 bits
    long populacao = 7800000000; // 64 bits

    // Ponto flutuante.
    double temperatura = 36.6; // padrão para literais decimais
    decimal preco = 19.99m; // ideal para dinheiro

    // Caractere / Texto.
    char letra = 'A'; // um único caractere Unicode
    string nome = "Thiago"; // sequência de caracteres

    // Booleano.
    bool ligado = true; // true/false

    // Objeto geral.
    object qualquerCoisa = 42; // pode armazenar qualquer tipo

    // Inferência de tipo
    var quantidade = 100; // o compilador deduz int
    var mensagem = "Olá Mundo"; // o compilador deduz string

    // Exibindo todos os valores
    Console.WriteLine($"Idade: {idade}");
    Console.WriteLine($"População: {populacao}");
    Console.WriteLine($"Temperatura: {temperatura}");
    Console.WriteLine($"Preço: {preco}");
    Console.WriteLine($"Letra: {letra}");
    Console.WriteLine($"Nome: {nome}");
    Console.WriteLine($"Ligado: {ligado}");
    Console.WriteLine($"Objeto: {qualquerCoisa}");
    Console.WriteLine($"Quantidade: {quantidade}");
    Console.WriteLine($"Mensagem: {mensagem}");
  }
}
```

### Gerando ID's únicos com GUID

Fica aqui a menção ao **Guid (Globally Unique Identifier)**, que é um **tipo nativo do .NET**, mas
não é um tipo primitivo do **C#** como `int` ou `bool`. Ele está definido na biblioteca **System
(System.Guid)** e representa um identificador único global de 128 bits.

```csharp
using System;

class Program
{
  static void Main()
  {
    Guid id = Guid.NewGuid(); // cria um novo GUID
    Console.WriteLine(id);
  }
}
```

> [!NOTE]  
> Para executar alguns exemplos diretamente no terminal, execute o comando:  
> **dotnet tool install -g dotnet-script**
>
> Agora você pode executar arquivos **.cs** diretamente com o comando **dotnet-script**.  
> Ex: **dotnet-script GuidExample.cs**

```csharp
// GuidExample.cs
// Cria e exibe um GUID.
Guid uuid = Guid.NewGuid();
Console.WriteLine(uuid); // Saída: 9dcd2f65-a9a6-4c45-84d3-4b86a6e98abc
```

Vale um resumo até aqui:

- **C# como evolução de outras linguagens**: Linguagem robusta e fortemente tipada, segura em tempo
  de compilação, popular em startups, cloud e Linux após o .NET Core ser open source.

- **Declaração de variáveis e escopo**: `var` permite inferência de tipo estático; reatribuição é
  permitida, mas redeclaração e escopo fora do bloco/método geram erro.

- **Tipos internos mais usados**: Inteiros (`int`, `long`), ponto flutuante (`double`, `decimal`),
  caracteres/texto (`char`, `string`), booleanos (`bool`), objeto geral (`object`) e inferência
  (`var`).

- **Boas práticas com `var`**: Usar apenas quando o tipo é óbvio ou simplifica tipos longos; caso
  contrário, declarar explicitamente aumenta legibilidade e segurança.

- **GUIDs e execução de scripts**: `Guid` gera IDs únicos globais; arquivos `.cs` podem ser
  executados diretamente no terminal usando `dotnet-script`.

Foi um bom começo até aqui. Pra não estender muito, fica o link pra
[parte 2](estilizando-csharp-parte-2.md).
