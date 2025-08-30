# Guia rápido para nomear as coisas

A principal diferença entre **JS** e **C#** está no uso de **camelCase** e **PascalCase** para
nomeação. Embora as convenções variem, ambas as linguagens seguem o padrão de **PascalCase** para
nomear **classes**, o que serve como um ótimo exemplo de consenso entre elas.

## ✅ Nomeando classes e propriedades

| **Categoria**       | **Prefixos/Keywords**                                                                      | **Exemplos**                                                                                                                |
| ------------------- | ------------------------------------------------------------------------------------------ | --------------------------------------------------------------------------------------------------------------------------- |
| **Classes**         | Nomes de classes em **PascalCase**, geralmente representando objetos ou entidades.         | `User`, `ProductService`, `OrderManager`, `CarFactory`                                                                      |
| **Propriedades**    | Nomes em **camelCase**, representando dados ou estados de uma classe.                      | `userName`, `isValid`, `totalAmount`, `lastUpdatedDate`                                                                     |
| **Constantes**      | Nomes em **UPPERCASE_SNAKE_CASE**, representando valores imutáveis.                        | `MAX_ATTEMPTS`, `DEFAULT_TIMEOUT`, `PI`                                                                                     |
| **Enumeradores**    | Nomes em **PascalCase**, com sufixo `Enum` opcional, representando um conjunto de valores. | `OrderStatus`, `UserRoleEnum`, `LogLevelEnum`                                                                               |
| **Coleções/Arrays** | Nome no plural e em **camelCase**, representando um conjunto de objetos ou dados.          | `usersList`, `userProfiles`, `productsList`, `orderItems`, `cartItems`, `completedOrders`, `ordersArray`, `employeeRecords` |

> [!NOTE]
>
> Alguns projetos e equipes podem utilizar **prefixos** e **sufixos** para facilitar a distinção da
> representação de uma estrutura. Se há muitas camadas, facilita o entendimento.

<details>
<summary>Guia MVC e Patterns <strong>JavaScript e C#:</strong></summary>
<br>
<p>Siga o contexto do seu projeto, optando por <strong>camel ou Pascal.</strong></p>

| **Categoria**        | **Prefixos/Keywords**                                                                   | **Exemplos**                                                          | **Comentário**                                                                                        |
| -------------------- | --------------------------------------------------------------------------------------- | --------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------- |
| **Model**            | Nomes em **PascalCase**, com sufixo `Model`, representando dados ou entidades.          | `UserModel`, `ProductModel`, `OrderModel`, `ProductEntity`            | Usado em padrões como **MVC** (Model-View-Controller).                                                |
| **View**             | Nomes em **PascalCase**, geralmente representando a camada de apresentação (UI).        | `UserView`, `ProductView`, `OrderView`                                | No padrão **MVC**, a **View** é responsável pela apresentação dos dados ao usuário.                   |
| **Controller**       | Nomes em **PascalCase**, geralmente com sufixo `Controller`.                            | `UserController`, `OrderController`, `ProductController`              | No padrão **MVC**, o controlador gerencia a interação entre a visão e o modelo.                       |
| **Service**          | Nomes de classes com sufixo `Service`, representando lógica de negócios.                | `UserService`, `EmailService`, `PaymentService`                       | Representa uma camada de serviço que contém lógica de negócios.                                       |
| **Repository/Store** | Sufixo `Repository` ou `Store`, representando acesso a dados ou banco de dados.         | `UserRepository`, `ProductRepository`, `OrderRepository`, `UserStore` | Segue o padrão **Repository** ou **Store** para abstração de acesso ao armazenamento de dados.        |
| **Interface**        | Prefixo `I` (opcional), seguido do nome da interface em **PascalCase**.                 | `IUserRepository`, `ICommandHandler`, `IService`                      | O uso do prefixo `I` é opcional em JavaScript, mas pode ser útil em projetos grandes.                 |
| **Factory**          | Nomes com sufixo `Factory`, representando a criação de objetos ou instâncias.           | `UserFactory`, `OrderFactory`, `ProductFactory`                       | Utilizado para encapsular a criação de objetos, especialmente quando envolve lógica de inicialização. |
| **Singleton**        | Prefixo `Singleton` ou nome da classe com o sufixo `Singleton`.                         | `DatabaseSingleton`, `LoggerSingleton`, `CacheSingleton`              | Representa um objeto que deve ter apenas uma instância ao longo do ciclo de vida da aplicação.        |
| **Adapter**          | Sufixo `Adapter`, representando a adaptação de interfaces externas para o sistema.      | `ApiAdapter`, `PaymentAdapter`, `EmailServiceAdapter`                 | Padrão **Adapter** para permitir que sistemas diferentes se comuniquem de forma transparente.         |
| **Facade**           | Prefixo `Facade`, representando uma interface simplificada para um subsistema complexo. | `DatabaseFacade`, `CacheFacade`, `PaymentGatewayFacade`               | Padrão **Facade** para simplificar interfaces complexas de subsistemas.                               |
| **Enum**             | Nomes em **PascalCase**, com sufixo `Enum`, representando valores fixos.                | `OrderStatusEnum`, `UserRoleEnum`, `LogLevelEnum`                     | Para representar um conjunto fixo de valores possíveis.                                               |

</details>

Se você está trabalhando com **React**, tem uma referência aqui também.

<details>
<summary>Guia pra quem é do <strong>React</strong>, seguindo um estilo de programação mais <strong>funcional:</strong></summary>
<br>
<p>Siga o contexto do seu projeto.</p>

| **Categoria**                     | **Prefixos/Keywords**                                                             | **Exemplos**                                                | **Comentário**                                                                                                                      |
| --------------------------------- | --------------------------------------------------------------------------------- | ----------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------- |
| **Componentes**                   | **PascalCase**, nomes de componentes React                                        | `Counter`, `UserProfile`, `ProductCard`, `App`              | Componentes React devem ser nomeados com **PascalCase** para seguir a convenção e fácil distinção.                                  |
| **Funções e Métodos**             | **camelCase**, funções internas e manipuladores de evento                         | `handleClick`, `setState`, `updateData`, `toggleVisibility` | Funções e métodos dentro de componentes devem ser nomeados em **camelCase**.                                                        |
| **Props**                         | **camelCase**, nomes de propriedades passadas para componentes                    | `userName`, `profilePicture`, `isLoggedIn`                  | Propriedades (props) devem seguir a convenção **camelCase** para consistência e legibilidade.                                       |
| **Estado (State)**                | **camelCase**, nomes de variáveis de estado em componentes                        | `count`, `userData`, `isModalOpen`, `isActive`              | Variáveis de estado devem ser nomeadas com **camelCase** para manter o padrão.                                                      |
| **Classes CSS**                   | **camelCase** ou **BEM (Block Element Modifier)**                                 | `buttonPrimary`, `navItemActive`, `modalOpen`               | Usar **camelCase** ou **BEM** para evitar conflitos e facilitar o entendimento das classes CSS.                                     |
| **Hooks**                         | **camelCase**, nomes de hooks personalizados                                      | `useUserData`, `useAuth`, `useLocalStorage`                 | Hooks personalizados devem ser nomeados em **camelCase**, com o prefixo `use` para indicar sua natureza.                            |
| **Componentes de Classe**         | **PascalCase**, para classes que estendem `React.Component` ou `Component`        | `UserProfilePage`, `ProductList`, `LoginForm`               | Mesmo para componentes de classe, o nome deve ser em **PascalCase**.                                                                |
| **Event Handlers**                | Prefixo **handle**, seguido de verbo no **camelCase**                             | `handleSubmit`, `handleChange`, `handleClick`               | Para eventos, prefira usar o prefixo **handle** seguido de um verbo, em **camelCase**.                                              |
| **Props de Evento**               | Prefixo **on**, seguido de evento no **camelCase**                                | `onClick`, `onSubmit`, `onChange`, `onMouseEnter`           | Props que representam manipuladores de eventos devem seguir o prefixo **on** em **camelCase**.                                      |
| **Componentes de Layout**         | **PascalCase**, se forem usados para layout ou agrupamento de conteúdo            | `Header`, `Footer`, `Sidebar`, `NavigationBar`              | Componentes de layout (estrutura) seguem a mesma regra de **PascalCase**.                                                           |
| **Constantes e Valores Fixos**    | **UPPERCASE_SNAKE_CASE**, para valores imutáveis                                  | `MAX_RETRIES`, `DEFAULT_TIMEOUT`, `API_URL`                 | Constantes devem ser sempre em **UPPERCASE_SNAKE_CASE** para fácil identificação.                                                   |
| **Componentes de Formulário**     | **PascalCase**, com sufixos como `Form` ou `Field` para componentes de formulário | `LoginForm`, `SearchField`, `UserProfileForm`               | Componentes relacionados a formulários devem seguir **PascalCase** e indicar claramente sua função.                                 |
| **Estado Global (Redux/Context)** | **camelCase** para estado global, **PascalCase** para reducers e actions          | `userData`, `setAuthToken`, `addItemToCart`, `UserReducer`  | O estado global (gerido por Redux ou Context) deve usar **camelCase** para as variáveis, e **PascalCase** para as ações e reducers. |
| **Componentes de UI**             | **PascalCase**, com sufixos como `Button`, `Input`, `Card`, `Modal`               | `PrimaryButton`, `TextInput`, `ProductCard`, `UserModal`    | Componentes de UI devem ser nomeados com **PascalCase** para a consistência de nomenclatura.                                        |

### Dicas Extras:

- **Evite nomes genéricos** como `Button1`, `Component2`. Seja específico no nome de componentes,
  como `PrimaryButton` ou `UserProfile`.
- **Use Hooks corretamente**: sempre que possível, crie hooks personalizados para reutilizar lógica
  de forma modular.

- **Props e Estado**: mantenha o nome das props e estado consistente com o nome das variáveis que
  elas representam. Por exemplo, se você tem uma prop que representa a visibilidade de um modal, use
  algo como `isModalOpen`.

- **Nomes de Arquivos**: Use **PascalCase** para arquivos que exportam componentes React (ex.:
  `UserProfile.js`) e **camelCase** para arquivos de funções utilitárias (ex.: `handleApiCall.js`).

- **Evite classes CSS genéricas**: tente sempre usar um nome específico para cada classe, e prefira
  o uso do **BEM** (se não for usar CSS Modules) para clareza, como `modal__header` e `modal__body`.

</details>

## ✅ Nomeando métodos

Fica a tabela de referência em **JavaScript**. Para **C-Sharp**, use **PascalCase**.

| Categoria                    | Prefixos/Keywords                                                                                                                           | Exemplos                                                                                                                                                             |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Boolean / Predicate**      | `is`, `has`, `can`, `should`                                                                                                                | `isValid`, `hasPermission`, `canExecute`, `shouldRetry`                                                                                                              |
| **CRUD**                     | `create`, `get/set`, `update`, `delete/remove`, `find/fetch`, `load/save`                                                                   | `createUser`, `getConfig`, `updateProfile`, `deleteUser`, `fetchPosts`, `saveData`                                                                                   |
| **Validadores / Checadores** | `validate`, `check`                                                                                                                         | `validateEmail`, `checkPermission`                                                                                                                                   |
| **Calculadores**             | `calculate`, `compute`, `evaluate`                                                                                                          | `calculateTax`, `computeHash`, `evaluateExpression`                                                                                                                  |
| **Conversores**              | `to`, `parse`, `map`                                                                                                                        | `toString`, `parseDate`, `mapDtoToEntity`                                                                                                                            |
| **Builders / Factories**     | `build`, `create`, `generate`                                                                                                               | `buildReport`, `createInstance`, `generateToken`                                                                                                                     |
| **Eventos**                  | `on` ou `trigger`                                                                                                                           | `onUserLogin`, `triggerUpdate`, `onDataReceived`                                                                                                                     |
| **Outros Verbos Comuns**     | `open/close`, `start/stop`, `enable/disable`, `send/receive`, `add/remove`, `attach/detach`, `subscribe/unsubscribe`, `initialize/shutdown` | `openConnection`, `startService`, `enableFeature`, `sendMessage`, `addItem`, `attachObserver`, `subscribeEvent`, `initializeApp`                                     |
| **Testes (BDD / Fluent)**    | `ensure`, `assert`, `expect`, `should`, `given/when/then`, `with`, `verify`, `mock/stub/spyOn`                                              | `ensureUserExists`, `assertEquals`, `expectError`, `shouldFailWhenInvalid`, `givenUser`, `whenLogin`, `thenSuccess`, `withName("John")`, `verifyCall`, `mockService` |

Vantagens do Uso de Convenções de Nomenclatura de Métodos:

- **Consistência**: Seguir convenções predefinidas ajuda a manter a consistência no código. Isso
  facilita a leitura e a manutenção, pois outros desenvolvedores (ou até mesmo você no futuro) podem
  rapidamente entender o propósito de uma função apenas pelo nome.

- **Clareza**: Utilizando prefixos comuns como get, set, validate, calculate, fica muito mais claro
  o que o método faz, sem precisar olhar sua implementação. A clareza no nome reduz a necessidade de
  comentários explicativos.

- **Intenção**: As convenções refletem a intenção do código. Por exemplo, métodos começando com is
  ou has indicam que eles retornam um valor booleano, enquanto métodos com create, save, ou load
  indicam operações de persistência.

- **Facilidade de Refatoração**: Em projetos grandes, renomear métodos de maneira consistente de
  acordo com padrões ajuda a evitar confusão e torna o refatoramento do código mais simples.

- **Documentação Implícita**: Seguir essas convenções torna a documentação do código mais intuitiva.
  Um novo membro da equipe pode começar a ler o código e entender a função dos métodos sem precisar
  de uma explicação detalhada.

## 🚫 Naming Anti-Patterns (Evitar Sempre)

| **Anti-Padrão**             | **Descrição**                                                                            | **Consequências**                                                                                       | **Como Evitar**                                                                   |
| --------------------------- | ---------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| **God Object**              | Um objeto que tem muitas responsabilidades ou métodos, concentrando toda a lógica.       | Difícil de manter, testar e evoluir. O código fica propenso a erros e acoplamento alto.                 | Divida o código em objetos menores e com responsabilidades bem definidas.         |
| **Spaghetti Code**          | Código sem estrutura clara, com muitas dependências e lógica dispersa.                   | Difícil de entender, modificar ou escalar. O código se torna confuso e frágil.                          | Use boas práticas de design, como MVC, e mantenha o código modular e coeso.       |
| **Copy-Paste Programming**  | Reutilização de código por cópia e colagem, sem abstração.                               | Dificuldade de manutenção e risco de introdução de erros em múltiplos locais.                           | Crie funções ou classes reutilizáveis e mantenha a lógica centralizada.           |
| **Magic Numbers / Strings** | Uso de valores literais diretamente no código sem explicação ou contexto.                | Dificuldade de manutenção e entendimento.                                                               | Use constantes ou variáveis nomeadas que expliquem o significado do valor.        |
| **Hardcoding**              | Configurações ou valores diretamente no código, sem flexibilidade.                       | Difícil de configurar ou alterar em diferentes ambientes ou para diferentes usuários.                   | Externalize configurações para arquivos de configuração ou variáveis de ambiente. |
| **Shotgun Surgery**         | Modificar múltiplos arquivos ou classes para fazer uma pequena alteração.                | Aumento do risco de introdução de bugs e dificuldade de manutenção.                                     | Divida o código em módulos e aplique o princípio de responsabilidade única.       |
| **Anemic Domain Model**     | Modelo de domínio sem comportamento, apenas dados (POJOs).                               | Falta de encapsulamento e lógica de negócios nos modelos, tornando a aplicação mais difícil de evoluir. | Incorpore comportamentos diretamente nos modelos de domínio.                      |
| **Over-engineering**        | Criar soluções excessivamente complexas para problemas simples.                          | Complica a manutenção e aumenta o tempo de desenvolvimento desnecessariamente.                          | Adote a abordagem de "simples, mas eficaz". Utilize o design simples e direto.    |
| **Singleton Abuse**         | Uso excessivo de Singletons para gerenciar o estado global.                              | Dificulta a testabilidade e cria acoplamento desnecessário.                                             | Evite Singletons sempre que possível e prefira injeção de dependência.            |
| **Premature Optimization**  | Tentar otimizar o código antes de ter certeza de que há um problema real de performance. | Atraso no desenvolvimento e complexidade desnecessária.                                                 | Foque primeiro em uma solução funcional, e só otimize quando necessário.          |

## Referências

### JavaScript (JS)

- [JavaScript Naming Conventions - MDN](https://developer.mozilla.org/en-US/docs/MDN/Contribute/Style_guidelines/JavaScript_style_guide#naming_conventions)
- [JavaScript Code Style - Airbnb](https://github.com/airbnb/javascript#naming-conventions)
- [React Reference](https://react.dev/reference/react)

### C-Sharp (C#)

- [C# Naming Conventions - Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)
- [C# Coding Standards - Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/naming-conventions)
- [C# Best Practices - freeCodeCamp](https://www.freecodecamp.org/news/coding-best-practices-in-c-sharp/)
