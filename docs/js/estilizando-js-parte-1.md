# Estilizando JS - Parte 1

JavaScript é uma das linguagens mais [populares](https://survey.stackoverflow.co/2025/technology/)
para web. Ela não para de evoluir e vem sendo melhorada a cada ano pra acompanhar o mercado.

As definições do JS vem do ECMAScript. Veja a evolução e história
[aqui](https://webreference.com/javascript/basics/versions/).

## Declaração de variáveis

Não é só no futebol que o `var` gera polêmica 😅. No JS ele veio primeiro e, antes do **ES6**, era a
única forma de declarar variáveis. Seu **escopo é de função, não de bloco,** o que pode causar
comportamentos inesperados fora de funções.

Abaixo exemplos que causam problemas:

> Se quiser testar de forma simples, abra o navegador e entre no DevTools apertando F12. Vá no
> console, copie e cole os exemplos e aperte ENTER pra ver o resultado.

```js
// 1 - Re-declaração sem aviso.
var numero = 10;
var numero = 20; // ❌ sobrescreveu o valor sem aviso
console.log(numero); // 20

// 2- Escopo de bloco ignorado.
if (true) {
  var numeroDentroDoIf = 50; // deveria ficar "dentro" do if
}
console.log(numeroDentroDoIf); // 50, mesmo fora do bloco

// 3-  Escopo de função vs global.
function teste() {
  var numeroDaFuncao = 100; // só existe dentro da função
  console.log(numeroDaFuncao); // 100
}
teste();
console.log(typeof numeroDaFuncao); // undefined, não existe fora da função

// 4 - Variável global acidental.
function outroTeste() {
  numeroAcidental = 500; // ❌ sem var/let/const → vira global
}
outroTeste();
console.log(numeroAcidental); // 500, agora existe globalmente!
```

Quem sofreu, sofreu. Agora não vamos mais passar por isso. Abaixo os tipos de declaração, do escopo
mais aberto ao mais restrito.

| Tipo de variável | Declaração            | Escopo                 | Reatribuição | Pode redeclarar              |
| ---------------- | --------------------- | ---------------------- | ------------ | ---------------------------- |
| Global acidental | `variavel = 10`       | Global (objeto global) | Sim          | Sim                          |
| `var`            | `var variavel = 20`   | Função                 | Sim          | Sim (dentro da mesma função) |
| `let`            | `let variavel = 30`   | Bloco                  | Sim          | Não                          |
| `const`          | `const variavel = 40` | Bloco                  | Não          | Não                          |

### Use const e let

O padrão atual usa apenas **const** e **let**. Tá em dúvida? Use **const** pra tudo! E quando
precisar reatribuir algum valor, ai você usa **let**.

- **const**: imutável, escopo de bloco, mais seguro.
- **let**: mutável, escopo de bloco, use só quando necessário.

```js
// 1 - Usando const (valor não muda)
const nome = "Thiago";
console.log("Nome:", nome);

// Tentativa de mudar const → erro
// nome = "Ronaldo"; // ❌ TypeError: Assignment to constant variable.

// 2 - Usando let (valor pode mudar)
let idade = 77;
console.log("Idade inicial:", idade);

// Atualizando a variável let
idade += 1;
console.log("Idade atualizada:", idade);

// Exemplo em loop com let
for (let i = 0; i < 3; i++) {
  console.log("Contador:", i);
}
```

Usando **const** você **protege a referência da variável**, impedindo que ela seja reatribuída. No
caso de um array (vetor) ou objeto, não é possível redeclarar o mesmo, podendo apenas alterar os
elementos dentro dele.

```js
// Usando const com número (imutável).
const numero = 10;
// numero = 20; // ❌ erro! não dá para reatribuir

// Usando const com array (referência protegida, mas conteúdo mutável).
const lista = [1, 2, 3];

// Podemos alterar elementos do array.
lista.push(4); // ✅ funciona
lista[0] = 100; // ✅ funciona
console.log(lista); // [100, 2, 3, 4]

// Mas não podemos reatribuir a variável lista.
// lista = []; // ❌ TypeError

// Usando let com array (referência e conteúdo mutáveis).
let lista2 = [10, 20, 30];
lista2.push(40); // ✅ funciona
lista2 = []; // ✅ também funciona
console.log(lista2); // []
```

Exemplo com um objeto:

```js
// Declarando o objeto com const.
const carro = {
  marca: "Nissan",
  modelo: "Kicks",
  ano: 2023,
};

console.log("Carro inicial:", carro);

// Podemos alterar propriedades do objeto.
carro.modelo = "Versa"; // ✅ altera a propriedade
carro.ano = 2025; // ✅ altera a propriedade
console.log("Carro atualizado:", carro);

// Podemos adicionar novas propriedades
carro.cor = "prata"; // ✅ funciona
console.log("Carro com cor:", carro);

// Mas não podemos reatribuir o objeto inteiro
// carro = {};
// ❌ TypeError: Assignment to constant variable.
```

Resumindo:

> const deixa o código mais seguro, evitando que alguém troque a variável inteira por engano. Para
> objetos/arrays, ainda é possível alterar o conteúdo interno, então cuidado se precisar de
> imutabilidade total.

Tudo ok até então. Pra não estender muito, fica o link pra [parte 2](estilizando-js-parte-2.md).
