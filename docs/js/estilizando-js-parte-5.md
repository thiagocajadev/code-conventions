# Estilizando JS - Parte 5

É uma boa ter referências sobre quais cenários ficam melhores com o uso de **controle de fluxos**,
direcionando a tomada de decisão no script.

## Tipos de Controle de Fluxo

Vou colocar o guia rápido abaixo:

| Nome        | Tipo        | Recomendação                                                                |
| ----------- | ----------- | --------------------------------------------------------------------------- |
| if / else   | Condicional | Usar quando há condições simples e claras.                                  |
| switch      | Condicional | Preferir em múltiplos casos para melhorar legibilidade.                     |
| for         | Iteração    | Usar em loops controlados por contador.                                     |
| for...of    | Iteração    | Usar para percorrer coleções iteráveis (arrays, strings, etc.).             |
| for...in    | Iteração    | Evitar em arrays; usar apenas para percorrer propriedades enumeráveis.      |
| while       | Iteração    | Usar quando não se sabe o número de repetições.                             |
| do...while  | Iteração    | Útil quando precisa executar pelo menos uma vez antes de checar a condição. |
| break       | Controle    | Usar para sair imediatamente de loops ou switch.                            |
| continue    | Controle    | Usar para pular a iteração atual e seguir para a próxima.                   |
| try...catch | Exceção     | Usar para tratar erros em tempo de execução.                                |
| throw       | Exceção     | Usar para lançar erros customizados.                                        |

Alguns códigos de exemplo vem a seguir, em uma abordagem didática.

### Condicionais - Nível 1

Como exemplo, vamos trabalhar com maioridade para obter a carteira de motorista.

```js
// Exemplo 1 - Uso simples de condicional com if/else.
// "Se for verdade faça, se não for verdade, faça outra coisa".
const idade = 20;

if (idade >= 18) {
  console.log("Você pode tirar a carteira de motorista.");
} else {
  console.log("Você ainda não pode tirar a carteira de motorista.");
}
```

```js
// Exemplo 2 - Forma mais curta de escrever if/else uso do operador ternário.
// Representação -> condição ? valorSeVerdadeiro : valorSeFalso.
const idade = 20;

// prettier-ignore
const mensagem = idade >= 18
  ? "Você pode obter a carteira de motorista."
  : "Você ainda não pode obter a carteira de motorista.";

console.log(mensagem); // "Você pode obter a carteira de motorista."
```

```js
// Exemplo 3 - Mais uma variação com ternário.
const idade = 20;
const podeDirigir = idade >= 18 ? "Sim" : "Não";

console.log(podeDirigir); // "Sim"
```

### Condicionais - Nível 2

Agora analisando exemplos com mais verificações:

```js
// Exemplo 1 - Uso de if/else encadeado.
// Quando temos mais de duas condições para verificar.
const idade = 20;

if (idade < 18) {
  console.log("Você ainda não pode iniciar o processo para habilitação.");
} else if (idade === 18) {
  console.log("Você pode obter a Permissão para Dirigir (PPD).");
} else {
  console.log("Você pode obter a Carteira Nacional de Habilitação (CNH).");
}
```

```js
// Exemplo 2 - Ternário encadeado.
const idade = 20;

// prettier-ignore
const mensagem = idade < 18
  ? "Você ainda não pode iniciar o processo para habilitação."
  : idade === 18
    ? "Você pode obter a Permissão para Dirigir (PPD)."
    : "Você pode obter a Carteira Nacional de Habilitação (CNH).";

console.log(mensagem);
```

```js
// Exemplo 3 - Clareza na lógica de verificação.
const ppd = "Você pode obter a Permissão para Dirigir (PPD).";
const cnh = "Você pode obter a Carteira Nacional de Habilitação (CNH).";
const menor = "Você ainda não pode iniciar o processo para habilitação.";

// Fica mais natural a leitura e entendimento da lógica.
const mensagem = idade < 18 ? menor : idade === 18 ? ppd : cnh;

console.log(mensagem);
```

```js
// Exemplo 4 - Uso de switch/case.
// Usamos switch(true) para permitir condições booleanas em cada case.
// Cada case testa uma condição (idade < 18, idade === 18).
// default funciona como o else, capturando todos os outros casos.
const idade = 20;

switch (true) {
  case idade < 18:
    console.log("Você ainda não pode iniciar o processo para habilitação.");
    break;
  case idade === 18:
    console.log("Você pode obter a Permissão para Dirigir (PPD).");
    break;
  default:
    console.log("Você pode obter a Carteira Nacional de Habilitação (CNH).");
    break;
}
```

```js
// Exemplo 5 - Uso de switch/case com um valor fixo.
const corDoFarol = "vermelho";

switch (corDoFarol) {
  case "vermelho":
    console.log("Pare!");
    break;
  case "amarelo":
    console.log("Atenção!");
    break;
  case "verde":
    console.log("Siga!");
    break;
  default:
    console.log("Sinal inválido");
}
```

> [!TIP]  
> Fique atento na hora de comparar igualdade entre valores e tipos.
>
> Comparando valores com `===` -> Verifica valor e tipo, ou seja, só retorna true se forem iguais e
> do mesmo tipo.
>
> Comparando valores com `==` -> Verifica valor apenas, realizando coerção de tipo automática se
> necessário.

```js
1 === "1"; // false (tipos diferentes)
1 == "1"; // true (coerção converte string para número)
```

Sempre que possível, use `===` para evitar surpresas com coerção de tipo. O `==` pode gerar
resultados inesperados, porque o JavaScript **tenta converter tipos automaticamente** antes de
comparar.

```js
0 == false; // true (coerção faz 0 virar false)
"" == false; // true (string vazia vira false)
null == undefined; // true
```

### Condicionais - Nível 3

Conforme vamos codificando, surge um objetivo na vida do programador: a necessidade de **reduzir
profundidade de código** e deixar o fluxo mais **linear/limpo**, evitando aninhamentos
desnecessários.

Pra deixar o código linear, temos algumas estratégias de boas práticas como:

- **Early Return** -> Sai cedo da função quando a condição é atendida, evitando else.
- **Guard Clauses** -> Condições que “protegendo” o fluxo principal, tratando exceções primeiro.
- **Lookup Table / Map Object** -> Usa objeto ou mapa para retornar resultados sem múltiplos if.
- **Circuit-Break** -> Usa operadores lógicos (&& / ||) para executar código condicionalmente sem
  else.

```js
// Exemplo 1 - Early Return.
// Aqui o `if` tem o retorno antecipado dentro da função (early return).
// Checagens extras não são feitas quando uma condição é atendida,
// reduzindo a necessidade de processamento.
const age = 17;

canDrive(age);

function canDrive(age) {
  if (age === 18) {
    console.log("Você ainda não pode obter a carteira de motorista.");
    return; // para a execução aqui e já retorna
  } else if (age < 18) {
    console.log("Você pode obter a Permissão para Dirigir (PPD).");
  } else {
    console.log("Você pode obter a Carteira Nacional de Habilitação (CNH).");
  }
}
```

```js
// Exemplo 2 - Guard Clauses.
// Combinado com o retorno antecipado, valida as exceções primeiro.
// Caso passe nas validações, segue o fluxo principal.
const age = 17;

canDrive(age);

function canDrive(age) {
  if (age < 18) {
    console.log("Você ainda não pode obter a carteira de motorista.");
    return; // guard clause
  }

  if (age === 18) {
    console.log("Você pode obter a Permissão para Dirigir (PPD).");
    return; // guard clause
  }

  // Fluxo principal. Veja que não precisou de else e ficou sem aninhamento.
  console.log("Você pode obter a Carteira Nacional de Habilitação (CNH).");
}
```

```js
// Exemplo 3 - Lookup Table com Objeto.
// A tabela de consulta é um conceito de mapeamento chave-valor para substituir múltiplos if/else.
// No JavaScript, podemos implementar com um objeto ou um Map.
// Cada chave representa uma condição e o valor é a mensagem correspondente.
// Uma função auxiliar decide qual chave usar baseado na idade.
const age = 17;

// Lookup Table mapeando condições para mensagens.
const messagesObject = {
  menor: "Você ainda não pode obter a carteira de motorista.",
  ppd: "Você pode obter a Permissão para Dirigir (PPD).",
  cnh: "Você pode obter a Carteira Nacional de Habilitação (CNH).",
};

// Função para obter a chave correta baseado na idade.
function getKey(age) {
  if (age < 18) return "menor";
  if (age === 18) return "ppd";
  return "cnh";
}

// Usa a Lookup Table para imprimir a mensagem.
console.log(messagesObject[getKey(age)]);
```

```js
// Exemplo 4 - Lookup Table com Map.
// Cada chave representa uma condição e o valor é a mensagem correspondente.
// Uma função auxiliar decide qual chave usar baseado na idade.
const age = 17;

// Lookup Table usando Map.
const messagesMap = new Map([
  ["menor", "Você ainda não pode obter a carteira de motorista."],
  ["ppd", "Você pode obter a Permissão para Dirigir (PPD)."],
  ["cnh", "Você pode obter a Carteira Nacional de Habilitação (CNH)."],
]);

// Função para obter a chave correta baseado na idade.
function getKey(age) {
  if (age < 18) return "menor";
  if (age === 18) return "ppd";
  return "cnh";
}

// Consulta a Lookup Table usando Map.
console.log(messagesMap.get(getKey(age)));
```

```js
// Exemplo 5 - Lookup Table com Map - Versão inline.
const age = 17;

// Cria o Map diretamente com chaves e mensagens.
const messagesMap = new Map([
  ["menor", "Você ainda não pode obter a carteira de motorista."],
  ["ppd", "Você pode obter a Permissão para Dirigir (PPD)."],
  ["cnh", "Você pode obter a Carteira Nacional de Habilitação (CNH)."],
]);

// Determina a chave inline e já busca no Map.
console.log(messagesMap.get(age < 18 ? "menor" : age === 18 ? "ppd" : "cnh"));
```

```js
// Exemplo 6 - Circuit-Break.
// Executa cada condição de forma independente usando curto-circuito.
// (condição) && ação → se a condição for verdadeira, a ação é executada.
// Cada linha é independente, funcionando como um mini “curto-circuito",
// interrompendo o fluxo de execução daquela linha se a condição for falsa.
const age = 17;

// prettier-ignore
(age < 18) && console.log("Você ainda não pode obter a carteira de motorista.");

// prettier-ignore
(age === 18) && console.log("Você pode obter a Permissão para Dirigir (PPD).");

// prettier-ignore
(age > 18) && console.log("Você pode obter a Carteira Nacional de Habilitação (CNH).");
```

Minha preferência pessoal é por usar **Guard Clauses**. Organizar a validação e trazer retorno das
exceções no topo, torna mais simples tratar o comportamento da aplicação.

### Iterações - Nível 1

**Iterar** significa percorrer elementos de uma coleção ou repetir um bloco de código várias vezes,
passo a passo. **Iterar -> andar pelos elementos**.

Use o `for` em **loops controlados por contador**, quando você sabe **quantas vezes quer repetir** o
bloco de código.

```js
// Exemplo 1 - Incrementa valores. Números de 1 a 5.
// Por convenção o nome da variável é a letra `i`.
for (let i = 1; i <= 5; i++) {
  // i vai de 1 a 5
  console.log(i);
}
```

```js
// Exemplo 2 - Decrementa valores. Números de 5 a 1.
for (let i = 5; i >= 1; i--) {
  // i vai de 5 a 1
  console.log(i);
}
```

Use o `while` quando a execução do loop depender de **uma condição que deve ser verificada antes**
de cada iteração.

```js
// Exemplo 3 - Incrementa valores usando while. Números de 1 a 5.
let i = 1;

while (i <= 5) {
  console.log(i);
  i++; // incrementa o contador
}
```

Use o `do...while` quando você quiser que o bloco de código seja executado **pelo menos uma vez**,
mesmo que a condição seja falsa inicialmente.

```js
// Exemplo 4 - Incrementa valores usando do...while. Números de 1 a 5.
let j = 1;

do {
  console.log(j);
  j++; // incrementa o contador
} while (j <= 5);
```

### Iterações - Nível 2

Temos 2 variações do **for** pra fazer loop (laço) e percorrer sobre as **chaves (índices)** e
**valores diretos**.

- ⚠️ **for...in** -> Percorre as **chaves** ou índices de objetos e arrays.

- ✅ **for...of** -> Percorre os **valores** diretamente de arrays, strings ou qualquer iterável.

```js
// Exemplo 1 - for...in com objeto.
const user = { name: "Thiago", age: 72 };

for (const key in user) {
  console.log(key, "->", user[key]);
}
// Saída:
// name -> Thiago
// age -> 72
```

```js
// Exemplo 2 - for...in com array.
const numbers = [10, 20, 30];

for (const index in numbers) {
  console.log(index, "->", numbers[index]);
}
// Saída:
// 0 -> 10
// 1 -> 20
// 2 -> 30
```

Geralmente, **`for...of`** é mais **performático** que **`for...in`** para **arrays**. O `for...in`
percorre todas as propriedades **enumeráveis**, incluindo as herdadas do protótipo, o que adiciona
**overhead (processamento extra)**.

```js
// Exemplo 3 - for...of com array.
const numbers = [10, 20, 30];

for (const value of numbers) {
  console.log(value);
}
// Saída:
// 10
// 20
// 30
```

```js
// Exemplo 4 - for...of com string, verificando cada letra.
for (const letter of "JS") {
  console.log(letter);
}
// Saída:
// J
// S
```

```js
// Exemplo 5 - for...of com Map, iterando sobre os pares [chave, valor].
const capitals = new Map([
  ["Brasil", "Brasília"],
  ["França", "Paris"],
  ["Japão", "Tóquio"],
]);

for (const [country, capital] of capitals) {
  console.log(`${country} -> ${capital}`);
}
// Saída:
// Brasil -> Brasília
// França -> Paris
// Japão -> Tóquio
```

```js
// Exemplo 6 - for...of com Set. Percorre cada valor do Set, já que não existem índices.
const numbers = new Set([10, 20, 30]);

for (const num of numbers) {
  console.log(num);
}
// Saída:
// 10
// 20
// 30
```

Em JavaScript, `for...of` funciona de forma muito parecida com o **for-each** de outras linguagens
(Java, C#, Python, etc.), porque percorre os **valores do iterável diretamente**, sem verificar
propriedades do protótipo.

```js
// Arrays possuem a função forEach, que anda por cada elemento do array.
const numbers = [10, 20, 30];

// Percorre cada valor do array e executa a função callback.
numbers.forEach((value, index) => {
  console.log(`Índice: ${index}, Valor: ${value}`);
});
```

> [!WARNING]  
> **Observação sobre objetos:**  
> Para percorrer objetos, `for...in` percorre diretamente todas as propriedades enumeráveis,
> incluindo as herdadas do protótipo, o que pode gerar comportamento inesperado.
>
> Por isso, uma abordagem mais clara e segura é usar `Object.keys(obj)` ou `Object.entries(obj)`,
> que transformam o objeto em **arrays de chaves** ou **pares [chave, valor]**.
>
> Depois disso, você percorre com `for...of`, garantindo que só iterará sobre os elementos que
> deseja, melhorando legibilidade e performance.

```js
// Exemplo 7 - Iterando objeto com Object.keys.
const user = { name: "Thiago", age: 72 };

// Object.keys retorna um array com as chaves.
for (const key of Object.keys(user)) {
  console.log(key, "->", user[key]);
}
// Saída:
// name -> Thiago
// age -> 72
```

```js
// Exemplo 8 - Iterando objeto com Object.entries.
// Object.entries retorna um array de [chave, valor].
for (const [key, value] of Object.entries(user)) {
  console.log(key, "->", value);
}
// Saída:
// name -> Thiago
// age -> 72
```

**Resumindo**:

- Para **arrays e iteráveis** → prefira **`for...of`**.
- Para **objetos** → use métodos como **`Object.keys()`** / **`Object.entries()`** para maior
  performance e clareza, e apenas como última alternativa use **`for...in`**.

### Iterações - Nível 3

Aprofundando entre equilíbrio e performance, veja esse
[benchmark testando 1M de elementos:](https://medium.com/@robinviktorsson/comparing-the-performance-of-different-loop-techniques-in-javascript-typescript-1540c62d1f97)

| Loop / Método  | Tempo de Execução |
| -------------- | ----------------- |
| for-loop       | 26.905 ms         |
| while-loop     | 20.662 ms         |
| for-of-loop    | 128.958 ms        |
| forEach-method | 200.12 ms         |
| map-method     | 338.333 ms        |
| reduce-method  | 124.49 ms         |
| for-in-loop    | 3.553 s           |

> [!NOTE]
>
> `for` e `while` são mais rápidos para loops simples sobre arrays.
>
> `for-of`, `forEach`, `map` e `reduce` têm overhead extra por funções internas e abstrações.
>
> `for-in` é **extremamente lento para arrays**, porque percorre todas as propriedades enumeráveis,
> incluindo as herdadas.

Minha preferência sempre será por um **código claro e fácil de manter**, tendo em vista **baixa
latência, segurança e alta performance**.

```js
// Combinando os exemplos até agora.
// Array com lista de idades dos candidatos a tirar a CNH.
const ageList = [17, 18, 20, -5, "vinte"];
canGetLicenseDriver(ageList);

// Função que valida uma lista de idades e retorna quem pode dirigir.
function canGetLicenseDriver(ageList) {
  // Guard clause: sai cedo se não houver dados.
  if (!Array.isArray(ageList) || ageList.length === 0) {
    console.log("Nenhuma idade fornecida.");
    return;
  }

  for (const age of ageList) {
    // Ignora valores inválidos.
    if (typeof age !== "number" || age < 0) continue;

    if (age < 18) {
      console.log(`Idade ${age}: ainda não pode dirigir.`);
    } else if (age === 18) {
      console.log(`Idade ${age}: pode obter a PPD.`);
    } else {
      console.log(`Idade ${age}: pode obter a CNH.`);
    }
  }
}

// Saída:
// Idade 17: ainda não pode dirigir.
// Idade 18: pode obter a PPD.
// Idade 20: pode obter a CNH.
```

```js
// Mesmo exemplo mais enxuto, com mais elementos no vetor (array).
const ageList = [17, 18, 20, -5, "vinte", "Thiago", 2];
canGetLicenseDriver(ageList);

function canGetLicenseDriver(ageList) {
  if (!Array.isArray(ageList) || ageList.length === 0) {
    return console.log("Nenhuma idade fornecida.");
  }

  for (const age of ageList) {
    if (typeof age !== "number" || age < 0) continue;

    // prettier-ignore
    const message = age < 18
        ? `Idade ${age}: ainda não pode dirigir.`
        : age === 18
          ? `Idade ${age}: pode obter a PPD.`
          : `Idade ${age}: pode obter a CNH.`;

    console.log(message);
  }
}

// Saída:
// Idade 17: ainda não pode dirigir.
// Idade 18: pode obter a PPD.
// Idade 20: pode obter a CNH.
// Idade 2: ainda não pode dirigir.
```

```js
// Lista de candidatos como objetos.
const candidatesList = [
  { id: 1, name: "João", age: 17 },
  { id: 2, name: "Maria", age: 18 },
  { id: 3, name: "Pedro", age: 20 },
  { id: 4, name: "Ana", age: -5 },
  { id: 5, name: "Thiago", age: "vinte" },
  { id: 6, name: "Lucas", age: 2 },
];

canGetLicenseDriver(candidatesList);

function canGetLicenseDriver(candidatesList) {
  // Guard clause: sai cedo se não houver dados.
  if (!Array.isArray(candidatesList) || candidatesList.length === 0) {
    return console.log("Nenhum candidato fornecido.");
  }

  // Desestrutura cada objeto da lista para obter id, name e age.
  for (const { id, name, age } of candidatesList) {
    // Ignora candidatos com idade inválida.
    if (typeof age !== "number" || age < 0) continue;

    // prettier-ignore
    // Mensagem com ternário para simplificar if/else.
    const message = age < 18
        ? `Candidato ${name} (ID: ${id}, idade: ${age}) ainda não pode dirigir.`
        : age === 18
          ? `Candidato ${name} (ID: ${id}, idade: ${age}) pode obter a PPD.`
          : `Candidato ${name} (ID: ${id}, idade: ${age}) pode obter a CNH.`;

    console.log(message);
  }
}

// Saída:
// Candidato João (ID: 1, idade: 17) ainda não pode dirigir.
// Candidato Maria (ID: 2, idade: 18) pode obter a PPD.
// Candidato Pedro (ID: 3, idade: 20) pode obter a CNH.
// Candidato Lucas (ID: 6, idade: 2) ainda não pode dirigir.
```

```js
// Mesmo exemplo acima, visando menos redundância e clareza.
// Lista de candidatos como objetos.
const candidatesList = [
  { id: 1, name: "João", age: 17 },
  { id: 2, name: "Maria", age: 18 },
  { id: 3, name: "Pedro", age: 20 },
  { id: 4, name: "Ana", age: -5 },
  { id: 5, name: "Thiago", age: "vinte" },
  { id: 6, name: "Lucas", age: 2 },
];

canGetLicenseDriver(candidatesList);

function canGetLicenseDriver(candidatesList) {
  if (!Array.isArray(candidatesList) || candidatesList.length === 0) {
    return console.log("Nenhum candidato fornecido.");
  }

  // Mensagens auxiliares.
  const messageTooYoung = "ainda não pode dirigir";
  const messageProvisoryLicense = "pode obter a PPD";
  const messageDriverLicense = "pode obter a CNH";

  for (const { id, name, age } of candidatesList) {
    if (typeof age !== "number" || age < 0) continue;

    // prettier-ignore
    // Escolhe a mensagem baseado na idade.
    const status = age < 18
        ? messageTooYoung
        : age === 18
          ? messageProvisoryLicense
          : messageDriverLicense;

    const message = `Candidato ${name} (ID: ${id}, idade: ${age}) ${status}.`;

    console.log(message);
  }
}

// Saída:
// Candidato João (ID: 1, idade: 17) ainda não pode dirigir.
// Candidato Maria (ID: 2, idade: 18) pode obter a PPD.
// Candidato Pedro (ID: 3, idade: 20) pode obter a CNH.
// Candidato Lucas (ID: 6, idade: 2) ainda não pode dirigir.
```

Pra finalizar, um exemplo bem abstraído entre uma **requisição** e **resposta** HTTP, simulando um
**POST** contra um **ENDPOINT**, enviando uma lista de candidatos em texto no formato JSON,
retornando quem pode obter a habilitação de motorista.

Fluxo de Processo: Requisição -> Processamento -> Resposta.

```js
// Exemplo de JSON enviado no body da requisição POST para https://api.example.com/api/v1/drivers/license
// {
//   "candidates": [
//     { "id": 1, "name": "João", "age": 17 },
//     { "id": 2, "name": "Maria", "age": 18 },
//     { "id": 3, "name": "Pedro", "age": 20 },
//     { "id": 4, "name": "Ana", "age": -5 },
//     { "id": 5, "name": "Thiago", "age": "vinte" },
//     { "id": 6, "name": "Lucas", "age": 2 }
//   ]
// }

// Requisição.
const jsonDocumentRequestBody = `
{
  "candidates": [
    { "id": 1, "name": "João", "age": 17 },
    { "id": 2, "name": "Maria", "age": 18 },
    { "id": 3, "name": "Pedro", "age": 20 },
    { "id": 4, "name": "Ana", "age": -5 },
    { "id": 5, "name": "Thiago", "age": "vinte" },
    { "id": 6, "name": "Lucas", "age": 2 }
  ]
}`;

// Converte JSON string para objeto JavaScript.
const request = JSON.parse(jsonDocumentRequestBody);

// Resultado da conversão.
// const request = {
//   candidates: [
//     { id: 1, name: "João", age: 17 },
//     { id: 2, name: "Maria", age: 18 },
//     { id: 3, name: "Pedro", age: 20 },
//     { id: 4, name: "Ana", age: -5 },
//     { id: 5, name: "Thiago", age: "vinte" },
//     { id: 6, name: "Lucas", age: 2 },
//   ],
// };

// Processamento.
canGetLicenseDriver(request);

// Função simulando endpoint da API.
function canGetLicenseDriver(request) {
  const { candidates } = request;

  if (!Array.isArray(candidates) || candidates.length === 0) {
    return { message: "Nenhum candidato fornecido." };
  }

  // Variáveis auxiliares.
  const messageTooYoung = "ainda não pode dirigir";
  const messageProvisoryLicense = "pode obter a PPD";
  const messageDriverLicense = "pode obter a CNH";

  const result = [];

  for (const { id, name, age } of candidates) {
    if (typeof age !== "number" || age < 0) continue;

    // prettier-ignore
    const status = age < 18
      ? messageTooYoung 
      : age === 18
        ? messageProvisoryLicense
        : messageDriverLicense;

    result.push({
      id,
      name,
      age,
      status,
    });
  }

  // Resposta.
  // Converte o resultado do processamento em JSON texto com indentação.
  const response = JSON.stringify({ candidates: result }, null, 2);

  // {
  //   "candidates": [
  //     {
  //       "id": 1,
  //       "name": "João",
  //       "age": 17,
  //       "status": "ainda não pode dirigir"
  //     },
  //     {
  //       "id": 2,
  //       "name": "Maria",
  //       "age": 18,
  //       "status": "pode obter a PPD"
  //     },
  //     {
  //       "id": 3,
  //       "name": "Pedro",
  //       "age": 20,
  //       "status": "pode obter a CNH"
  //     },
  //     {
  //       "id": 6,
  //       "name": "Lucas",
  //       "age": 2,
  //       "status": "ainda não pode dirigir"
  //     }
  //   ]
  // }

  // Exibe no console o retorno em objeto JavaScript.
  console.log({ candidates: result });

  // Retorna texto JSON, com escapes no console.
  return response;
}
```

Com isso, concluímos essa "pequena" saga. 😅

Bons estudos e bons códigos!
