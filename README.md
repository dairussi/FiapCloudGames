# Fiap Cloud Games API

API REST para gestão de usuários, jogos, promoções e compras de jogos, com autenticação via JWT. Projeto em .NET 8 e ASP.NET Core, utilizando Entity Framework Core com SQL Server.

## Tecnologias
- .NET 8 / C# 12
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- Autenticação JWT (Bearer)
- Swagger (OpenAPI)

## Arquitetura
- `FiapCloudGames.Domain`: entidades, value objects, ports (interfaces de domínio)
- `FiapCloudGames.Application`: casos de uso (commands/queries), DTOs e mapeadores
- `FiapCloudGames.Infraestructure`: persistência (EF Core), repositórios, serviços externos, middleware
- `FiapCloudGames.API`: controllers e configuração de pipeline

Padrões utilizados: CQRS (queries/commands), Ports & Adapters.

## Pré-requisitos
- .NET SDK 8.0+
- SQL Server (local ou remoto)

## Configuração
1. Configure a conexão com o banco e a chave JWT em `appsettings.json` do projeto `FiapCloudGames`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FiapCloudGames;User Id=sa;Password=SuaSenhaAqui;TrustServerCertificate=True"
  },
  "Jwt": {
    "SecretKey": "sua-chave-secreta-bem-grande-e-aleatoria"
  }
}
```

2. Aplique as migrações (se necessário):
- via CLI: `dotnet ef database update` (na pasta do projeto `FiapCloudGames`)
- ou deixe o EF criar o banco na primeira execução, caso já esteja configurado

> Observação: o Swagger é habilitado apenas em `Development`. Garanta a variável `ASPNETCORE_ENVIRONMENT=Development` ao executar localmente.

## Execução
- Via CLI:
  - `dotnet build`
  - `dotnet run --project FiapCloudGames/FiapCloudGames.csproj`
- Via Visual Studio/VS Code: execute o projeto `FiapCloudGames`

A API ficará disponível (por padrão) em:
- Swagger: `https://localhost:xxxx/swagger`

## Autenticação
- Endpoint de login: `POST /api/Auth/login`
- Payload:
```json
{
  "email": "usuario@dominio.com",
  "password": "senha"
}
```
- Resposta contém o token JWT. Use o botão "Authorize" no Swagger e informe `Bearer {token}`.

## Principais endpoints
- `AuthController` (`api/Auth`): login
- `UsersController` (`api/Users`): usuários (criação/edição, desativação, consulta por id e paginação)
- `GamesController` (`api/Games`): jogos (criação/edição, consulta por id e paginação)
- `PromotionsController` (`api/Promotions`): promoções (criação/edição, consulta por id e paginação)
- `GamePurchasesController` (`api/GamePurchases`): compras por usuário

Detalhes completos e modelos de requisição/resposta estão no Swagger.

## Estrutura de pastas (resumo)
- `FiapCloudGames/API/Controllers` – controllers da API
- `FiapCloudGames/Application` – casos de uso, DTOs e mapeadores
- `FiapCloudGames/Domain` – entidades, VOs e interfaces do domínio
- `FiapCloudGames/Infraestructure` – EF Core, repositórios, serviços, middleware
- `FiapCloudGames/Program.cs` – configuração do host, DI, autenticação e Swagger
- `FiapCloudGames.Tests` – testes automatizados

## Testes
- Execute: `dotnet test`

## Desenvolvimento
- Contribuições são bem-vindas via pull requests.
- Padrões: manter separação por camadas, seguir handlers de `commands/queries` e repositórios por `ports`.

## Licença
Este projeto é disponibilizado para fins educacionais. Ajuste a licença conforme necessário para seu uso.
