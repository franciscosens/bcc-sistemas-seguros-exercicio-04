# Desenvolvimento de Sistemas Seguros - Exercício 04

Fundação da Universidade Regional de Blumenau

Alunos: 
* Francisco Lucas Sens
* Patrick Krauss

[Enunciado](assets/L04-PathTraversal.pdf)

## Requisitos:

(NET Core 2.2)[https://dotnet.microsoft.com/download/dotnet-core/2.2]

## Como executar?

O banco de dados será criado automatimente pelo Entity Framework Core através das migrations.

* Definir no arquivo [appsettings.json](Web/appsettings.json) o seu usuário(Uid) e senha(Pwd) do MySQL

* Prompt Command:

  ```
    dotnet ef migrations add Init
  
    Update-Database
  ```
* Package Manager Console (Visual Studio)

  ```
    Add-Migration Init
  
    dotnet ef database update
  ```
