# Code Conventions

Olá Dev! Nesse projeto quero documentar convenções e boas práticas para estilizar a escrita e
leitura de códigos **JavaScript** e **C#**.

Como no dia a dia as linguagens acabam sendo utilizadas em conjunto, vou aproveitar a ocasião para
comparar as semelhanças e diferenças entre ambas.

Aqui um pequeno spoiler em **JavaScript**:

```js
// Chamada de método de forma limpa, com a lógica organizada em abstração.
const productFound = await product.findOneById(id);

// Detalhes de implementação.
async function findOneById(id) {
  // Retorno direto no início do método, com a lógica separa em uma função auxiliar.
  const productFound = await runSelectQuery(id);
  return productFound;

  // Detalhes da implementação da função auxiliar.
  async function runSelectQuery(id) {
    const results = await database.query({
      text: `
        SELECT
          *
        FROM
          products
        WHERE
          id = $1
        LIMIT
          1
        ;`,
      values: [id],
    });

    // Lança exceção se não houver resultado.
    if (results.rowCount === 0) {
      throw new NotFoundError({
        message: "O id informado não foi encontrado no sistema.",
        action: "Verifique se o id foi digitado corretamente.",
      });
    }

    return results.rows[0];
  }
}
```

E o mesmo exemplo de código em **C sharp**:

```csharp
// Chamada de método de forma limpa, com a lógica organizada em abstração.
var productFound = await FindOneByIdAsync(id);

// Detalhes de implementação.
public async Task<Product> FindOneByIdAsync(Guid id)
{
  // Direct return, com variável intermediária para didática.
  var productFound = await RunSelectQueryAsync(id);
  return productFound;

  // Método auxiliar encapsulado.
  async Task<Product> RunSelectQueryAsync(Guid id)
  {
    var results = await database.QueryAsync<Product>(
      """
      SELECT
        *
      FROM
        products
      WHERE
        id = @id
      LIMIT
        1
      ;
      """,
      new { id }
    );

    if (!results.Any())
    {
      throw new NotFoundException(
        message: "O id informado não foi encontrado no sistema.",
        action:  "Verifique se o id foi digitado corretamente."
      );
    }

    return results.First();
  }
}
```

Já contei que eu curto um bolinho 🧁? Nada melhor do que seguir uma receita, passo a passo pra te
"convencer" sobre as minhas convenções.

[Estilizando JavaScript - Parte 1](docs/js/estilizando-js-parte-1.md)  
[Estilizando JavaScript - Parte 2](docs/js/estilizando-js-parte-2.md)  
[Estilizando JavaScript - Parte 3](docs/js/estilizando-js-parte-3.md)
[Estilizando JavaScript - Parte 4](docs/js/estilizando-js-parte-4.md)

## Referencias

[Airbnb JavaScript Code Style](https://github.com/airbnb/javascript?tab=readme-ov-file)  
[Mozilla JavaScript Code Style](https://developer.mozilla.org/en-US/docs/MDN/Writing_guidelines/Code_style_guide/JavaScript)  
[Google C# Code Style](https://google.github.io/styleguide/csharp-style.html)  
[Microsoft C# Code Style](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
