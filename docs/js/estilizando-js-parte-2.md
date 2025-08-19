# Estilizando JS - Parte 2

Nessa parte, vamos olhar com carinho sobre o padrão de escrita `camelCase` e um pouco mais sobre
como as `funções` limpas e polidas ajudam na compreensão do código.

## Escrevendo no estilo camelo?

Cada linguagem adota um estilo de escrita. No JavaScript, a convenção mais comum é o **camelCase**:
os nomes de variáveis e funções começam com letra minúscula e, quando compostos por mais de uma
palavra, cada nova palavra começa com letra maiúscula, facilitando a leitura.

```js
// Exemplos
const casaDeAposta;

let numerosDaMegaSena;

function geraDinheiroInfinitoSemSerPegoPelaReceita(){}
```

Para constantes imutáveis ou valores globais bem definidos, utiliza-se a convenção **UPPER_CASE**
(também chamada de **Screaming Snake Case**). Esse padrão é comum no JavaScript e em diversas outras
linguagens para destacar esse tipo de valor.

```js
const NUMERO_DO_PI = 3.14;
const API_URL = "https://thiagocaja.dev/api/";
```

## Funções enriquecem o código

Vou contar um segredo: `funções` são `métodos` disfarçados.

Chamamos de função quando o código é declarado de forma independente, sem estar associado a nenhum
objeto específico:

```js
// Função independente.
function soma(valor1, valor2) {
  return valor1 + valor2;
}

// Chamando a função.
const resultado = soma(3, 5);
console.log(resultado); // 8
```

Já quando essa mesma função está associada a um objeto ou classe, ela passa a ser chamada de
**método**:

```js
// calculadora.js
function soma(valor1, valor2) {
  return valor1 + valor2;
}

function subtrai(valor1, valor2) {
  return valor1 - valor2;
}

const calculadora = {
  soma,
  subtrai,
};

export default calculadora;

// Utilizando os métodos do objeto calculadora.
const resultadoDaSoma = calculadora.soma(1, 1); // 2
```

> [!NOTE]  
> **Função** → código independente, não associado a nenhum objeto ou classe.
>
> **Método** → função que está associada a um objeto ou classe.

Aqui alguns exemplos de recomendações de escrita de funções:

```js
// Exemplo 1
// ❌ Código confuso, difícil de ler. As variáveis não são descritivas
// e não há espaçamento.
function s(v1, v2) {
  return v1 + v2;
}
function sub(v1, v2) {
  return v1 - v2;
}

const calc1 = { s, sub }; // o que é "s"? e o que é "sub"?
const r1 = calc1.s(2, 3);
console.log(r1); // 5

// ✅ Código organizado, com nomes descritivos e espaçamento
// facilitando a leitura.
function soma(valor1, valor2) {
  return valor1 + valor2;
}

function subtrai(valor1, valor2) {
  return valor1 - valor2;
}

const calculadora = {
  soma,
  subtrai,
};

const resultadoDaSoma = calculadora.soma(2, 3);
console.log(resultadoDaSoma); // 5

// Exemplo 2
// ❌ Código confuso, inline, sem espaçamento e parâmetros genéricos.
const calculadora2 = {
  soma: function (a, b) {
    return a + b;
  },
  subtrai: function (a, b) {
    return a - b;
  },
};

const r2 = calculadora2.soma(2, 3);
console.log(r2); // 5

// ✅ Código organizado, nomes claros e funções legíveis
const calculadora = {
  soma: function (valor1, valor2) {
    return valor1 + valor2;
  },
  subtrai: function (valor1, valor2) {
    return valor1 - valor2;
  },
};

const resultado = calculadora.soma(2, 3);
console.log(resultado); // 5

// Exemplo 3
// ❌ Código confuso, nomes mistos e inconsistentes
function somar(a, b) {
  return a + b;
}
const sub = function (x, y) {
  return x - y;
};

const calc3 = { somar, sub };
const r3 = calc3.somar(2, 3);
console.log(r3); // 5

// ✅ Código organizado, nomes consistentes e legíveis
function soma(valor1, valor2) {
  return valor1 + valor2;
}

function subtrai(valor1, valor2) {
  return valor1 - valor2;
}

const calculadora = { soma, subtrai };

const resultado = calculadora.soma(2, 3);
console.log(resultado); // 5
```

> [!NOTE]  
> Todo **código é uma representação** de algo que precisamos manipular. Procure trabalhar de forma
> organizada, separando os **contextos**, colocando cada **bloco de código** em seus respectivos
> arquivos e classes.

[Parte 3](estilizando-js-parte-3.md) pra quem quer uma convenção de como nomear as coisas.
