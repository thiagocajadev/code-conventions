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
1 == "1"; // true  (coerção converte string para número)
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
// Exemplo 1 - Early Return
// Aqui o `if` tem o retorno antecipado dentro da função (early return).
// Checagens extras não são feitas quando uma condição é atendida,
// Reduzindo a necessidade de processamento.
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
// Exemplo 2 - Guard Clauses
// Combinado com o retorno antecipado, valida as exceções primeiro.
// Caso passe nas validações, segue o fluxo principal
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
// Exemplo 3 - Lookup Table com Objeto
// A tabela de consulta é um conceito de mapeamento chave-valor para substituir múltiplos if/else.
// No JavaScript, podemos implementar com um objeto ou um Map.
// Cada chave representa uma condição e o valor é a mensagem correspondente.
// Uma função auxiliar decide qual chave usar baseado na idade.
const age = 17;

// Lookup Table mapeando condições para mensagens
const mensagens = {
  menor: "Você ainda não pode obter a carteira de motorista.",
  ppd: "Você pode obter a Permissão para Dirigir (PPD).",
  cnh: "Você pode obter a Carteira Nacional de Habilitação (CNH).",
};

// Função para obter a chave correta baseado na idade
function getKey(age) {
  if (age < 18) return "menor";
  if (age === 18) return "ppd";
  return "cnh";
}

// Usa a Lookup Table para imprimir a mensagem
console.log(mensagens[getKey(age)]);
```

```js
// Exemplo 4 - Lookup Table com Map
// Cada chave representa uma condição e o valor é a mensagem correspondente.
// Uma função auxiliar decide qual chave usar baseado na idade.
const age = 17;

// Lookup Table usando Map
const mensagensMap = new Map([
  ["menor", "Você ainda não pode obter a carteira de motorista."],
  ["ppd", "Você pode obter a Permissão para Dirigir (PPD)."],
  ["cnh", "Você pode obter a Carteira Nacional de Habilitação (CNH)."],
]);

// Função para obter a chave correta baseado na idade
function getKey(age) {
  if (age < 18) return "menor";
  if (age === 18) return "ppd";
  return "cnh";
}

// Consulta a Lookup Table usando Map
console.log(mensagensMap.get(getKey(age)));
```

```js
// Exemplo 5 - Lookup Table com Map - Versão inline
const age = 17;

// Cria o Map diretamente com chaves e mensagens
const mensagensMap = new Map([
  ["menor", "Você ainda não pode obter a carteira de motorista."],
  ["ppd", "Você pode obter a Permissão para Dirigir (PPD)."],
  ["cnh", "Você pode obter a Carteira Nacional de Habilitação (CNH)."],
]);

// Determina a chave inline e já busca no Map
console.log(mensagensMap.get(age < 18 ? "menor" : age === 18 ? "ppd" : "cnh"));
```

```js
// Exemplo 6 - Circuit-Break
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

É isso ai! Bons estudos e bons códigos.
