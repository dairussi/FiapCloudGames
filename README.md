# Fiap Cloud Games API

API REST para gest�o de usu�rios, jogos, promo��es e compras de jogos, com autentica��o via JWT. Projeto em .NET 8 e ASP.NET Core, utilizando Entity Framework Core com SQL Server.

## Tecnologias
- .NET 8 / C# 12
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- Autentica��o JWT (Bearer)
- Swagger (OpenAPI)

## Arquitetura
- `FiapCloudGames.Domain`: entidades, value objects, ports (interfaces de dom�nio)
- `FiapCloudGames.Application`: casos de uso (commands/queries), DTOs e mapeadores
- `FiapCloudGames.Infraestructure`: persist�ncia (EF Core), reposit�rios, servi�os externos, middleware
- `FiapCloudGames.API`: controllers e configura��o de pipeline

Padr�es utilizados: CQRS (queries/commands), Ports & Adapters.

## Pr�-requisitos
- .NET SDK 8.0+
- SQL Server (local ou remoto)

## Configura��o
1. Configure a conex�o com o banco e a chave JWT em `appsettings.json` do projeto `FiapCloudGames`:

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

2. Aplique as migra��es (se necess�rio):
- via CLI: `dotnet ef database update` (na pasta do projeto `FiapCloudGames`)
- ou deixe o EF criar o banco na primeira execu��o, caso j� esteja configurado

> Observa��o: o Swagger � habilitado apenas em `Development`. Garanta a vari�vel `ASPNETCORE_ENVIRONMENT=Development` ao executar localmente.

## Execu��o
- Via CLI:
  - `dotnet build`
  - `dotnet run --project FiapCloudGames/FiapCloudGames.csproj`
- Via Visual Studio/VS Code: execute o projeto `FiapCloudGames`

A API ficar� dispon�vel (por padr�o) em:
- Swagger: `https://localhost:xxxx/swagger`

## Autentica��o
- Endpoint de login: `POST /api/Auth/login`
- Payload:
```json
{
  "email": "usuario@dominio.com",
  "password": "senha"
}
```
- Resposta cont�m o token JWT. Use o bot�o "Authorize" no Swagger e informe `Bearer {token}`.

## Principais endpoints
- `AuthController` (`api/Auth`): login
- `UsersController` (`api/Users`): usu�rios (cria��o/edi��o, desativa��o, consulta por id e pagina��o)
- `GamesController` (`api/Games`): jogos (cria��o/edi��o, consulta por id e pagina��o)
- `PromotionsController` (`api/Promotions`): promo��es (cria��o/edi��o, consulta por id e pagina��o)
- `GamePurchasesController` (`api/GamePurchases`): compras por usu�rio

Detalhes completos e modelos de requisi��o/resposta est�o no Swagger.

## Estrutura de pastas (resumo)
- `FiapCloudGames/API/Controllers` � controllers da API
- `FiapCloudGames/Application` � casos de uso, DTOs e mapeadores
- `FiapCloudGames/Domain` � entidades, VOs e interfaces do dom�nio
- `FiapCloudGames/Infraestructure` � EF Core, reposit�rios, servi�os, middleware
- `FiapCloudGames/Program.cs` � configura��o do host, DI, autentica��o e Swagger
- `FiapCloudGames.Tests` � testes automatizados

## Testes
- Execute: `dotnet test`

## Desenvolvimento
- Contribui��es s�o bem-vindas via pull requests.
- Padr�es: manter separa��o por camadas, seguir handlers de `commands/queries` e reposit�rios por `ports`.

## Licen�a
Este projeto � disponibilizado para fins educacionais. Ajuste a licen�a conforme necess�rio para seu uso.
