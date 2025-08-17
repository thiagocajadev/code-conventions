# Estilizando JS - Parte 3

Nessa parte, temos recomendações para trabalhar padronizando a declaração de variáveis e funções
`em ingles`. Falando mais um pouco sobre funções (métodos pra não esquecer 😁), pra que serve o
`async/wait` e o que é a boa prática de padronizar a implementação com `direct return`.

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
