# Estilizando JS - Parte 3

Nessa parte, temos recomendações para trabalhar padronizando a declaração de variáveis e funções
`em ingles`. Falando mais um pouco sobre **funções** (**métodos**, pra não esquecer 😁) e a
serventia do `async/await`.

## Convenção para nomear variáveis e métodos

Dicas na hora de escolher nomes para variáveis e métodos:

- Usar **ação ou verbo no presente** para métodos/funções que executam algo.
- Evitar "toX" (como `toSum`) — é raro e desnecessário.
- Em inglês, nomes curtos e diretos funcionam bem: `sum()`, `getUser()`.
- Em português, nomes muito descritivos como `retornaOUsuario()` podem ficar longos e confusos.
- Usar inglês ajuda a **manter os nomes curtos, claros e consistentes**, evitando ambiguidade.
- Em inglês a ligação entre palavras já é implícita no **camelCase**.

Exemplos:

| Língua       | Nome do método                 | Nome da variável                         |
| ------------ | ------------------------------ | ---------------------------------------- |
| ❌ Português | `somar()`, `retornaOUsuario()` | `totalEAtualizado`, `listaDeIDsUsuarios` |
| ✅ Inglês    | `sum()`, `getUser()`           | `totalUpdated`, `userIdList()`           |

> Declarações em inglês tornam o código mais curto, consistente e compreensível por desenvolvedores
> de diferentes idiomas.

## Síncrono e Assíncrono

Em várias linguagens de programação, algumas operações são **síncronas** e precisam ser concluídas
imediatamente, enquanto outras são **assíncronas** e podem aguardar um processamento antes de
devolver uma resposta.

No JavaScript, usamos as palavras reservadas **async** e **await**:

- **async** define uma função como assíncrona, permitindo que ela utilize **await**.

- **await** pausa a execução da função até que a `Promise` seja resolvida, garantindo que o
  resultado esteja disponível antes de continuar.

```js
// Exemplo simples. Busca um usuário.
async function fetchUser() {
  // Simulando uma Promise, que é um código que retorna valor futuro.
  const user = await new Promise((resolve) => {
    setTimeout(() => resolve({ id: 1, nome: "Thiago" }), 1000);
  });

  console.log(user); // { id: 1, nome: "Thiago" }
}

// Chamada da função.
fetchUser();
```

Explicando o código:

1. **async function fetchUser()** → função assíncrona.
1. **await new Promise(...)** → espera 1 segundo até resolver e retorna o objeto.
1. **console.log(user)** → só executa depois que a Promise foi resolvida.

Com essa abordagem o código fica mais limpo e legível, facilita tratamento de erros com
**try/catch** e mantém a lógica **linear**.

### Quando devo criar uma função assíncrona?

Sempre que a operação não for **instantânea** ou dependa de serviços externos, crie um método
assíncrono para:

- Chamar uma API.
- Acessar um banco de dados.
- Ler ou escrever arquivos.
- Operações com timers ou delays.

> A ideia é que qualquer operação que retorne uma **Promise** deve estar dentro de uma função async,
> para poder usar await e manter o código legível.

```js
// Exemplo buscando os dados em uma API, retornando a resposta no formato JSON.
async function fetchData() {
  try {
    const response = await fetch("https://thiagocaja.dev/api/users");
    const json = await response.json();
    console.log(json);
  } catch (error) {
    console.error("Erro ao buscar dados:", error);
  }
}
```

Explicando o código:

1. **await fetch(...)** espera a resposta da API.
1. **await response.json()** transforma a resposta em objeto.
1. **try/catch** captura qualquer erro na operação assíncrona.

Se o código não trata corretamente fluxos que demandam uma resposta morosa, a aplicação trava. Veja
so exemplos abaixo:

Primeiro, um pouco de história, antes da chegada do
[async/await](https://css-tricks.com/using-es2017-async-functions/), usando **callbacks** e
**Promisses com .then()**:

```js
// ❌ Exemplo com callback (aninhado e confuso). Difícil de ler e escalar.
function waitSecondsCallback(seconds, callback) {
  setTimeout(() => {
    callback("feito!");
  }, seconds * 1000);
}

console.log("Início");
waitSecondsCallback(3, (result) => {
  console.log(result);
  console.log("Fim");
});

// ❌ Exemplo com Promise e .then (melhor que callback, mas ainda verboso).
// O código fica problemático conforme cresce a indentação,
// devido a várias operações encadeadas.
function waitSecondsThen(seconds) {
  return new Promise((resolve) => {
    setTimeout(() => resolve("feito!"), seconds * 1000);
  });
}

console.log("Início");
waitSecondsThen(3).then((result) => {
  console.log(result);
  console.log("Fim");
});

// ❌ Exemplo com Promise e vários .then encadeados.
// A leitura começa a ficar confusa quando há muitas etapas.
function waitSecondsThen(seconds) {
  return new Promise((resolve) => {
    setTimeout(() => resolve(`esperou ${seconds}s`), seconds * 1000);
  });
}

console.log("Início");

// Chamada do método confusa.
// A leitura ainda é aceitável, mas em casos maiores vira uma “escadinha” difícil de manter.
waitSecondsThen(1)
  .then((res1) => {
    console.log(res1);
    return waitSecondsThen(2);
  })
  .then((res2) => {
    console.log(res2);
    return waitSecondsThen(3);
  })
  .then((res3) => {
    console.log(res3);
    return waitSecondsThen(1);
  })
  .then((res4) => {
    console.log(res4);
    console.log("Fim");
  })
  .catch((err) => {
    console.error("Erro:", err);
  });
```

Agora uma versão mais moderna:

```js
// ❌ Operação síncrona simulada, travando a execução.
function waitSeconds(seconds) {
  const endWhile = Date.now() + seconds * 1000;
  while (Date.now() < endWhile) {
    // trava o thread (processo) principal
  }
  return "feito!";
}

console.log("Início");
const result = waitSeconds(3); // trava a aplicação por 3 segundos
console.log(result);
console.log("Fim");

// ✅ Operação assíncrona usando async/await.
function waitSecondsWithAsyncAwait(seconds) {
  return new Promise((resolve) => {
    setTimeout(() => resolve("feito!"), seconds * 1000);
  });
}

async function runCode() {
  console.log("Início");
  const result = await waitSecondsWithAsyncAwait(3); // não trava a aplicação
  console.log(result);
  console.log("Fim");
}

runCode();

// ✅ Como ficaria a chamada do método "waitSecondsThen", que usa vários ".then()".
// Com async/await, o mesmo código fica linear e limpo:
async function runWithoutNestedThen() {
  console.log("Início");
  // Chamada simples.
  console.log(await waitSecondsThen(1));
  console.log(await waitSecondsThen(2));
  console.log(await waitSecondsThen(3));
  console.log(await waitSecondsThen(1));
  console.log("Fim");
}

runWithoutNestedThen();
```

Vantagens da operação assíncrona:

- **await waitSecondsAsync(3)** espera a **Promise** resolver sem travar o thread principal.
- Código fica linear e legível, sem callbacks aninhados.
- Outras operações podem rodar enquanto a Promise está pendente.

Vou deixar um tópico para entender o uso de **await new Promise(...)** e **await fetch(...)**.

### Diferença entre `await new Promise` e `await fetch`

**`await new Promise(...)`**

Usado para **simular ou criar manualmente** uma operação assíncrona. Exemplo de uso: delay ou lógica
interna.

```js
await new Promise((resolve) => setTimeout(resolve, 3000)); // 3 segundos
```

Esse código não depende de serviços externos, mas precisa por alguma circunstancia aguardar pra ser
executado.

**`await fetch(...)`**

Usado para operações que **acessam recursos externos**, como APIs ou servidores. `fetch` já retorna
uma Promise, então podemos usar `await` diretamente.

```js
const response = await fetch("https://api.exemplo.com/users");
const data = await response.json();

console.log(data);
```

Esse código depende de um recurso externo, pode demorar algum tempo para processar e retornar os
dados reais.

Resumindo:

- `new Promise` → criamos a Promise manualmente (simulação ou lógica interna).
- `fetch` → Promise já existente (resultado de operação externa).

### Diferença entre JSON e Objeto JavaScript

**JSON** é o acrônimo de **JavaScript Object Notation** (Notação de Objeto JavaScript). É apenas uma
**convenção de notação**.

Portanto, **JSON não é um objeto JavaScript** — é apenas um texto usado para representar dados de
forma estruturada. Tecnicamente, não existe **“objeto JSON”**; o termo correto é **texto JSON** ou
**documento JSON**. Exemplificando:

```js
// Documento JSON (texto). Chaves sempre com aspas duplas.
{
  "id": 1,
  "name": "Thiago",
  "enabled": true
}

// Objeto JavaScript: chaves sem aspas podem ser usadas como propriedades,
// permitindo acessar os valores diretamente.
const user = {
  id: 1,
  name: "Thiago",
  enabled: true
};

console.log(user.name) // Thiago
```

> [!IMPORTANT]  
> APIs geralmente retornam dados em **JSON**, que é apenas **texto estruturado**. Para usar esses
> dados no código, convertemos o **texto JSON** em um **objeto JavaScript**, permitindo acessar os
> **valores** diretamente pelas suas **propriedades**.

Quando usamos o método `fetch()`, temos acesso ao `response.json()`. Ele lê o corpo da resposta no
formato **JSON** e faz a conversão para um **objeto JavaScript**. Para uma **string JSON local**
(por exemplo, recebida de um arquivo ou variável), usamos **JSON.parse()** para fazer a mesma
conversão.

```js
// ❌ Exemplo - 1
// JSON como texto puro (string). O texto não é manipulável como objeto.
const jsonResponse = '{ "id": 1, "nome": "Thiago" }';
console.log(jsonResponse);
// Saída: { "id": 1, "nome": "Thiago" }

// ✅ Convertendo a string JSON para objeto JavaScript. O objeto JS é manipulável.
const data = JSON.parse(jsonResponse);
console.log(data);
// Saída: { id: 1, nome: "Thiago" }

// ✅ Exemplo - 2
// JSON retornado de uma API com fetch(). O objeto JS é manipulável.
async function fetchUser() {
  const response = await fetch("https://api.exemplo.com/usuarios");
  // Lê a resposta em JSON (texto) e converte para objeto JS.
  const dataFromApi = await response.json();
  console.log(dataFromApi);
}

fetchUser();
// Saída: { id: 1, nome: "Thiago" }
```

Pra ficar fácil de lembrar:

- **JSON** -> texto (string) seguindo uma convenção { "chave": "valor" }.
- **Objeto JavaScript** -> estrutura de dados do próprio JavaScript { chave: "valor" }.

Por aqui acabou. Mais dicas para código estruturado na [parte 4](estilizando-js-parte-4.md).
