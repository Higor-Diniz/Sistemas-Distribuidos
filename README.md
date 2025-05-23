# Projeto BlogAPI - Sistemas Distrbuídos

## Introdução

Seja muito bem-vindo(a) ao projeto de API Blog. O presente projeto foca em uma solução backend de uma API simples, mas completa, utilizando ferramentas como .NET 8, Entity Framework Core, ASP.NET Core e JwtBearer.
A API permite que os usuários possam registrar uma conta, fazer login, e criar Post em determinadas categorias 'pré geradas' pelo sistema.

## Funções do Sistema

- Registrar e Autenticar usuários;
- Visualizar as categorias dentro do sistema;
- Realizar operação de CRUD (Create, Read, Update and Delete) de postagens;
- Controle de autorização ao criar postagens acerca do usuário que criou.

## Tecnologias Utilizadas

- **ASP.NET Core:** O ASP.NET Core é uma ferramenta multiplataforma bastante utilizada na construção de aplicações moderna e conectada à internet.

- **Entity Framework:** Entity Framework (EF) is an ORM (Object-Relational Mapping) para .NET o qual simplifica o acesso e mapeamento de dados com objeto de banco de dados.

- **Identity Framework:** ASP.NET Core Identity is a membership system that supports user authentication, authorization, and management.

- **MySQL:** O MySQL é um sistema de gerenciamento de banco de dados (SGBD) o qual utiliza a linguagem SQL, sendo um dos SGBDs mais populares atualmente.

- **Swagger:** Swagger é uma ferramenta utilizada para visualizar, construir e documentar APIs RESTful, possindo uma interface amigável para testar e implementar em sistemas.

- **AutoMappper:** Uma biblioteca que realiza o mapeamento de propriedades de um objeto para outro, assim, evitando possíveis erros de mapeamentos.

## Arquitetura do Projeto

```maths
    BlogAPI/
    ├── Controllers/
    ├── Data/
    │── DTOs/
    │ ├── Auth/
    │ ├── Categories/
    │ ├── Posts/
    ├── Entities/
    │── Mapping/
    ├── Migrations/
    ├── Properties/
    ├── Services/
    │ ├── Interfaces/
```

- **Controllers:** Contém os controladores responsáveis por receber requisições HTTP, tratar rotas e delegar ações às camadas de serviço;
- **Data:** Contém a classe de contexto (DbContext) que gerencia a conexão com o banco de dados e configura as entidades;
- **DTOs:** Define os Data Transfer Objects, usados para enviar e receber dados da API. Subpastas indicam organização por funcionalidade;
- **Entities:** Contém as entidades de domínio, ou seja, as classes que representam as tabelas do banco;
- **Mapping:** Define as configurações específicas de mapeamento entre entidades e banco (via Fluent API do EF Core);
- **Migrations:** Contém os arquivos de migração gerados pelo Entity Framework, usados para criar/alterar o esquema do banco de dados;
- **Properties:** Contém arquivos de configuração do projeto, como launchSettings.json, usado para definir como a aplicação roda localmente;
- **Services:** Implementa a lógica de negócio. Os controladores chamam os serviços para executar as ações.

## Getting Started

### Prerequisites

- .NET 8 SDK
- MySQL Server e MySQL Workbrench - 8.0.42
- Visual Studio (2022) ou VS Code
- EntityFrameworkCore - 8.0.1
- ASP.NET CORE - 8.0.1

### Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/higor-diniz/BlogAPI.git
   ```

2. Configure a string de conexão do banco de dados em appsettings.json:

   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*",
     "ConnectionStrings": {
       "ApplicationDbContext": "InsiraSuaStringDeConexaoAqui"
     }
   }
   ```

3. Realize os comandos abaixo para Buildar e Rodar o projeto:

   ```bash
   dotnet build
   ```

   ```bash
   dotnet run
   ```

## API Endpoints

### Usuários

<table>
<thead>
<tr>
<th>Método</th>
<th>Endpoint</th>
<th>Descrição</th>
</tr>
</thead>
<tbody>
<tr>
<td>POST</td>
<td>/api/v1/auth/register</td>
<td>Registra um novo usuário</td>
</tr>
<tr>
<td>POST</td>
<td>/api/v1/auth/login</td>
<td>Realiza o login do usuário</td>
</tr>
</tbody>
</table>

### Categorias

<table>
<thead>
<tr>
<th>Método</th>
<th>Endpoint</th>
<th>Descrição</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/v1/categories</td>
<td>Obtém todas as categorias</td>
</tr>
</tbody>
</table>

### Posts

<table>
<thead>
<tr>
<th>Método</th>
<th>Endpoint</th>
<th>Descrição</th>
</tr>
</thead>
<tbody>
<tr>
<td>POST</td>
<td>/api/v1/posts</td>
<td>Cadastra um novo Post</td>
</tr>
<tr>
<td>GET</td>
<td>/api/v1/posts</td>
<td>Obtém todos os Posts</td>
</tr>
<tr>
<td>GET</td>
<td>/api/v1/posts/{id}</td>
<td>Obtém um Post pelo 'id'</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/v1/posts/{id}</td>
<td>Altera um Post</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/v1/posts/{id}</td>
<td>Deleta um Post</td>
</tr>
</tbody>
</table>

## Agradecimentos

- Este projeto foi desenvolvido seguindo como base um projeto de Blog do <a href="https://github.com/denizciMert/BlogAPI" rel="nofollow">Mert Denizci - GitHub</a>.