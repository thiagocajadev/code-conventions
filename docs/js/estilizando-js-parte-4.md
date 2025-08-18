# Estilizando JS - Parte 4

Nessa parte, veja como lidar com o `tratamento de erros` de forma mais profissional. Além disso,
usar a técnica do `direct return` deixa o código mais linear.

## Tratando Erros

Fluxos que podem gerar falhas técnicas vão dentro de um **try/catch**. O detalhe está em **lançar o
erro certo**. Quando for preciso entender a causa do problema, capturar e lançar o tipo certo de
erro deixa a manutenção mais rápida e clara.

```js
// Função simulando operação que pode falhar.
async function getProduct(id) {
  if (id !== 1) {
    throw new Error("Produto não encontrado"); // simula falha
  }
  return { id: 1, name: "Produto Exemplo" }; // sucesso -> produto com id 1
}

// Função principal para testar no console.
async function run() {
  try {
    const product1 = await getProduct(1);
    console.log("Sucesso:", product1);

    const product2 = await getProduct(2); // id 2 não existe e vai lançar um erro
    console.log("Sucesso:", product2);
  } catch (error) {
    console.error("Erro capturado:", error.message);
  }
}

// Executa no console.
run();
```

Erros bem estruturados ajudam a separar o que é **problema de negócio** do que é **falha técnica**.
Por exemplo, se um **produto não for encontrado**, lançamos um `NotFoundError` sem precisar de
`try/catch` naquele ponto.

Agora, se a consulta ao banco falhar, usamos `try/catch` e lançamos um `InternalServerError` ou
outro erro específico.

```js
// Exemplo 1 - Simulando problema de negócio, passando um Id de Produto que não existe.

// Classes de erro didáticas.
class NotFoundError extends Error {}
class InternalServerError extends Error {}

// Função simulando problema de negócio (produto não encontrado).
async function getProduct(id) {
  if (id !== 1) {
    throw new NotFoundError(`"Produto ID: ${id} não encontrado"`); // problema de negócio
  }
  return { id: 1, name: "Produto Exemplo" }; // sucesso
}

// Função principal simulando falha técnica (ex: consulta ao banco).
async function run() {
  try {
    const product = await getProduct(2); // lança NotFoundError
    console.log("Sucesso:", product);
  } catch (err) {
    if (err instanceof NotFoundError) {
      console.warn("Erro de negócio:", err.message);
    } else {
      // captura qualquer outro erro técnico e lança InternalServerError.
      throw new InternalServerError("Falha técnica ao buscar produto");
    }
  }
}

// Executa no console.
run();

// Exemplo 2 - Simulando uma falha técnica.
class NotFoundError extends Error {}
class InternalServerError extends Error {}

async function getProduct(id) {
  if (id !== 1) throw new NotFoundError("Produto não encontrado"); // problema de negócio

  if (Math.random() < 0.5) throw new Error("Conexão com o banco falhou"); // falha técnica. 50% chance de erro

  return { id: 1, name: "Produto Exemplo" };
}

async function run() {
  try {
    const product = await getProduct(1);
    console.log("Sucesso:", product);
  } catch (error) {
    if (error instanceof NotFoundError) {
      console.warn("Erro de negócio:", error.message);
    } else {
      console.error("Erro técnico capturado:", error.message);
      console.error(error.stack);
      throw new InternalServerError("Falha técnica ao buscar produto");
    }
  }
}

// Execute esse trecho algumas vezes para simular intermitência.
run().catch((err) => console.error(error.name + ":", error.message));

// Saída no console.
/*
VM188:20 Erro técnico capturado: Conexão com o banco falhou
VM188:21 Error: Conexão com o banco falhou
  at getProduct (<anonymous>:7:34)
  ...
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

```js
// ❌ Tratamento de erro genérico e mal feito.
async function findOneById(id) {
  try {
    const results = await database.query({
      text: "SELECT * FROM products WHERE id = $1 LIMIT 1;",
      values: [id],
    });

    if (results.rowCount === 0) {
      throw "Produto não encontrado"; // Erro genérico, string solta
    }

    return results.rows[0];
  } catch (error) {
    console.log("Deu erro em alguma coisa"); // Engole o erro, sem detalhes
    return null; // Retorna valor confuso, sem contexto
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
async function findOneById(id) {
  try {
    const results = await database.query({
      text: "SELECT * FROM products WHERE id = $1 LIMIT 1;",
      values: [id],
    });

    if (results.rowCount === 0) {
      throw new NotFoundError({
        message: "O id informado não foi encontrado no sistema.",
        action: "Verifique se o id foi digitado corretamente.",
      });
    }

    return results.rows[0];
  } catch (error) {
    if (error instanceof NotFoundError) {
      // Repassa erros de negócio sem mascarar.
      throw error;
    }

    // Captura exceções inesperadas (ex: conexão DB caiu).
    throw new InternalServerError({
      message: "Falha ao consultar o banco de dados.",
      cause: error,
    });
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
// errors.js - Classe base.
export class BaseError extends Error {
  constructor({ name, message, action, statusCode, cause }) {
    super(message, { cause });
    this.name = name || "BaseError";
    this.action = action || "Entre em contato com o suporte.";
    this.statusCode = statusCode || 500;
    this.cause = cause;
  }

  toJSON() {
    return {
      error: {
        name: this.name,
        message: this.message,
        action: this.action,
        status_code: this.statusCode,
      },
    };
  }
}

// Erro interno.
export class InternalServerError extends BaseError {
  constructor({ cause } = {}) {
    super({
      name: "InternalServerError",
      message: "Um erro interno não esperado aconteceu.",
      action: "Entre em contato com o suporte.",
      statusCode: 500,
      cause,
    });
  }
}

// Não encontrado.
export class NotFoundError extends BaseError {
  constructor({ message, action, cause } = {}) {
    super({
      name: "NotFoundError",
      message: message || "Recurso não encontrado.",
      action: action || "Verifique se o recurso existe.",
      statusCode: 404,
      cause,
    });
  }
}

// Erro de validação.
export class ValidationError extends BaseError {
  constructor({ message, action, cause } = {}) {
    super({
      name: "ValidationError",
      message: message || "Os dados fornecidos são inválidos.",
      action: action || "Revise os dados de entrada.",
      statusCode: 400,
      cause,
    });
  }
}
```

Com a lógica centralizada, podemos fazer toda parte de tratamento de erros de conexão com o banco de
dados dentro da classe **database**, por exemplo.

## Retornando Direto

O uso de `direct return` (ou retorno direto) é uma prática de programação que pode melhorar a
legibilidade e a eficiência do código. Essa abordagem consiste em **retornar um valor imediatamente
de uma função**. Os detalhes de implementação ficam encapsulados em um método auxiliar.

```js
// ❌ Sem direct return. Mais verboso, else desnecessário e leitura dificultada.
async function findOneById(id) {
  let productFound = null;

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

  if (results.rowCount === 0) {
    throw new NotFoundError({
      message: "O id informado não foi encontrado no sistema.",
      action: "Verifique se o id foi digitado corretamente.",
    });
  } else {
    productFound = results.rows[0];
  }

  return productFound;
}
```

Desvantagens:

- Usa variável auxiliar desnecessária (productFound), alocando como null.
- Estrutura condicional redundante (else depois de throw).
- O retorno fica “escondido” lá embaixo.

```js
// ✅ Com direct return. Abstração da lógica, indicando no topo do código o retorno esperado.
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

Vantagens:

- Menos código e menos variáveis inúteis.
- Fluxo de leitura mais claro.
- Facilita manutenção – menos linhas, menos chance de bug, mais fácil de entender de cara.

É isso ai! Bons estudos e bons códigos.
