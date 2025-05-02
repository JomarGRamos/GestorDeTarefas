# GestorDeTarefas

É uma aplicação de gestão de tarefas, onde você pode adicionar, editar, excluir e listar tarefas. Esta aplicação foi construída com **.NET 8.0** e utiliza **SQL Server** para persistência de dados. 

O projeto utiliza os seguintes recursos:

#### AutoMapper:

Usado para realizar o mapeamento entre objetos de tipos diferentes, como entre entidades do domínio e os DTOs.

#### Entity Framework Core:

Para persistência de dados com o banco de dados. Está sendo utilizado para realizar operações de CRUD (Create, Read, Update, Delete) e o gerenciamento de conexões com o banco de dados (SQL Server).

#### XUnit:

Para a realização de testes unitários na aplicação.


#### Swagger:

Para documentação da API, permitindo uma interface gráfica interativa para testar os endpoints da aplicação.

#### FluentValidation:

Para a validação de objetos, melhorando a legibilidade do código e facilitando a manutenção do código.

#### Logs

Para monitoramento e diagnóstico de possíveis erros na aplicação.

## Requisitos

Antes de executar o projeto, certifique-se de que você tem as seguintes ferramentas instaladas:

- [Docker](https://www.docker.com/get-started)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet)
- [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (caso não use Docker para rodar o banco de dados)

## Rodando a aplicação localmente

### 1. Clonar o repositório

Primeiro, clone o repositório para o seu ambiente local:
git clone https://github.com/JomarGRamos/GestorDeTarefas.git

### 2. Configurar o banco de dados
Crie um banco de dados no seu SQL Server (pode ser o SQL Server Express) e configure a connection string no arquivo appsettings.json:

```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP\\SQLEXPRESS;Database=GestorDeTarefasDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
Atenção: Substitua DESKTOP\\SQLEXPRESS pelo nome do seu servidor SQL.

Na pasta ScriptsBanco, existe um arquivo para criação da tabela "Tarefas".

### 3. Restaurar pacotes e compilar o projeto
Após configurar a connection string, restaure os pacotes NuGet e compile o projeto.

### 4. Rodar a aplicação
Agora, você pode rodar a aplicação localmente.

### 5. Acessar a API
Para testar a API, você pode acessar o Swagger na seguinte URL:
```bash
https://localhost:44312/swagger
```
