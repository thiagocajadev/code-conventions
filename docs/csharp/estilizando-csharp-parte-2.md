# Estilizando JS - Parte 2

Nessa parte, vamos olhar com carinho sobre as convenções de nomes em Csharp e como `métodos` limpos
e polidos ajudam na compreensão do código.

## Convenções de Nomes em C#

No **C#**, usamos diferentes estilos de escrita **(Case)** dependendo do tipo de elemento no código.

### PascalCase

- **O que é:** Cada palavra começa com letra maiúscula.
- **Usado para:** Classes, structs, enums, métodos e propriedades públicas.

```csharp
// ❌ Nomes com letras minúsculas e abreviações.
class calculadora { }
void geraDinheiro() { }
public int idade { get; set; }

// ✅ Use PascalCase.
class Calculadora { }
void GeraDinheiro() { }
public int Idade { get; set; }
```

### camelCase

- **O que é:** A primeira palavra começa com letra minúscula; palavras seguintes começam com
  maiúscula.
- **Usado para:** Variáveis locais, parâmetros de métodos e campos privados.

```csharp
// ❌ nomes com letras maiúsculas no início ou abreviações int
NumerosDaMegaSena = 6;
var CasaDeAposta = "Cuidado com a tia BET";
void Soma(Valor1, Valor2) { }

// ✅ Use camelCase
int numerosDaMegaSena = 6;
var casaDeAposta = "Cuidado com a tia BET";
```

### UPPER_CASE

- **O que é:** Todas as letras maiúsculas, palavras separadas por underline `_`.
- **Usado para:** Constantes globais ou valores imutáveis (em algumas equipes, não é o padrão
  oficial C#).

```csharp
// ⚠️ Usando PascalCase para constantes globais pode confundir
const double NumeroDoPi = 3.14;

// ✅ UPPER_CASE (opcional)
const double NUMERO_DO_PI = 3.14;
const string API_URL ="https://thiagocaja.dev/api/";
```

Tabela para referência rápida:

| Elemento                   | Case                     | Exemplo ✅          |
| -------------------------- | ------------------------ | ------------------- |
| Classe / Struct / Enum     | PascalCase               | Calculadora         |
| Método / Propriedade       | PascalCase               | GeraDinheiro, Idade |
| Variável local / parâmetro | camelCase                | numerosDaMegaSena   |
| Constante                  | PascalCase ou UPPER_CASE | NUMERO_DO_PI        |

## Proteção de variáveis por visibilidade

No **C#**, a visibilidade controla quem pode acessar ou modificar uma variável, propriedade ou
método.

- **public** → acessível de qualquer lugar, dentro ou fora da classe.
- **protected** → acessível apenas dentro da classe e suas subclasses.
- **private** → acessível apenas dentro da própria classe (padrão para campos).
- **const** → valor imutável, sempre público ou privado conforme declarado, e não pode ser alterado.

> [!IMPORTANT]  
> Por padrão, um **elemento** com a **visibilidade não declarada** é definido como **private**
> (privado).
>
> As únicas variações podem ser para elementos de uma
> [**struct**](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/builtin-types/struct)
> ou
> [**interface**](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/interface).

Tabelinha de visibilidade e exemplos:

| Modificador | Acesso                          | Exemplo                 |
| ----------- | ------------------------------- | ----------------------- |
| public      | Qualquer lugar                  | public int Idade;       |
| protected   | Dentro da classe e subclasses   | protected string Nome;  |
| private     | Apenas dentro da própria classe | private double saldo;   |
| const       | Valor constante e imutável      | const double PI = 3.14; |

```csharp
class Pessoa
{
  int idade; // private por padrão
  string nome; // private por padrão
  public bool ativo; // definido com acesso público. Ex: Pessoa.ativo

  void Falar() { } // private por padrão
}
```

Em resumo:

- Use **private** para campos internos da classe.
- Use **protected** para permitir herança.
- Use **public** para expor propriedades e métodos.
- Use **const** para valores fixos que nunca mudam.

## Projetos modernos **ASP.NET MVC**

A abordagem mais usual para declarar variáveis, propriedades e métodos segue boas práticas de
**encapsulamento** e **visibilidade clara**.

```csharp
// Convenções Modernas MVC em C# (Campos, Propriedades, Constantes e Métodos)

// Campos privados
// Sempre `private`, geralmente com `camelCase` e,
// às vezes, prefixo `_` para distinguir de propriedades públicas.
private readonly int _idade;
private string nome;

// Propriedades públicas
// Usadas para expor dados do modelo ou classe.
// `PascalCase` e `public` para que o binding do MVC funcione corretamente.
public int Idade { get; set; }
public string Nome { get; set; }

// Constantes
// `const` ou `static readonly` para valores imutáveis.
// Pode ser `private` se interno à classe, ou `public` se usado globalmente.
private const double PI = 3.14;
public static readonly string ApiUrl = "https://api.meusite.com/";

// Métodos
// `public` para ações que precisam ser acessíveis (ex: métodos de Controllers).
// `private` ou `protected` para lógica interna, `helpers` ou métodos de apoio.
// `PascalCase` para todos os métodos.
public IActionResult Index() { /* ação do Controller */ }
private void CalcularDesconto() { /* lógica interna */ }
```

E claro que tem que ter uma tabelinha de referência pro MVC aqui:

| Elemento        | Case       | Visibilidade   | Observação                                            |
| --------------- | ---------- | -------------- | ----------------------------------------------------- |
| Campos privados | camelCase  | private        | Encapsula dados internos da classe                    |
| Propriedades    | PascalCase | public         | Usadas para binding com Views e Models                |
| Constantes      | PascalCase | private/public | Valores fixos, const ou static readonly               |
| Métodos         | PascalCase | public/private | Public para ações de Controller, private para helpers |

## Métodos enriquecem o código

No **C#**, tudo é método. Mesmo **funções “independentes”** precisam estar dentro de uma **classe**
ou **struct**. Quando usamos **métodos static**, podemos pensar neles como funções independentes,
mas tecnicamente ainda são métodos da classe.

```csharp
// Função (método estático independente).
class Program
{
    // Método estático que age como função independente
    public static int Soma(int valor1, int valor2)
    {
        return valor1 + valor2;
    }

    static void Main()
    {
        int resultado = Soma(3, 5);
        Console.WriteLine(resultado); // 8
    }
}

// Método associado a um objeto ou classe
class Calculadora
{
    public int Soma(int valor1, int valor2)
    {
        return valor1 + valor2;
    }

    public int Subtrai(int valor1, int valor2)
    {
        return valor1 - valor2;
    }
}

class Program
{
    static void Main()
    {
        var calculadora = new Calculadora();
        int resultadoDaSoma = calculadora.Soma(1, 1); // 2
        Console.WriteLine(resultadoDaSoma);
    }
}
```

Aqui alguns exemplos de recomendações de escrita de métodos:

```csharp
// Exemplo 1
// ❌ Código confuso, difícil de ler. Nomes curtos e sem espaçamento.
class Calc1
{
    public int S(int v1, int v2) => v1 + v2;
    public int Sub(int v1, int v2) => v1 - v2;
}

var calc1 = new Calc1();
int r1 = calc1.S(2, 3);
Console.WriteLine(r1); // 5

// ✅ Código organizado, com nomes descritivos e legíveis.
class Calculadora
{
    public int Soma(int valor1, int valor2) {
      int resultado = valor1 + valor2;
      return resultado;
    }

    public int Subtrai(int valor1, int valor2) {
      int resultado = valor1 - valor2;
      return resultado;
    }
}

var calculadora = new Calculadora();
int resultadoDaSoma = calculadora.Soma(2, 3);
Console.WriteLine(resultadoDaSoma); // 5
```

```csharp
// Exemplo 2
// ❌ Código confuso, inline e sem nomes claros.
class Calculadora2
{
    public int Soma(int a, int b) => a + b;
    public int Subtrai(int a, int b) => a - b;
}

var calc2 = new Calculadora2();
int r2 = calc2.Soma(2, 3);
Console.WriteLine(r2); // 5

// ✅ Código organizado, nomes claros e funções legíveis.
class Calculadora
{
    public int Soma(int valor1, int valor2)
    {
        return valor1 + valor2;
    }

    public int Subtrai(int valor1, int valor2)
    {
        return valor1 - valor2;
    }
}

var calculadora = new Calculadora();
int resultado = calculadora.Soma(2, 3);
Console.WriteLine(resultado); // 5
```

```csharp
// Exemplo 3
// ❌ Código confuso, nomes inconsistentes.
class Calc3
{
    public int Somar(int a, int b) => a + b;
    public int Sub(int x, int y) => x - y;
}

var calc3 = new Calc3();
int r3 = calc3.Somar(2, 3);
Console.WriteLine(r3); // 5

// ✅ Código organizado, nomes consistentes e legíveis.
class Calculadora
{
    public int Soma(int valor1, int valor2) => valor1 + valor2;
    public int Subtrai(int valor1, int valor2) => valor1 - valor2;
}

var calculadora = new Calculadora();
int resultado = calculadora.Soma(2, 3);
Console.WriteLine(resultado); // 5
```

[Parte 3](estilizando-csharp-parte-3.md) com algumas dicas pra usar nomes em inglês e intro para
métodos assíncronos.
