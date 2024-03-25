# Projeto FraudSyS com .net 8, DynamoDb-local e docker-compose

Este é um projeto criado com intuito de validar alguns conhecimentos em Solid, DDD, manipulação de banco noSql e clean code.
O projeto é basicamente um CRUD de accounts e realiza a transação de valores entre elas.

## Requisitos

Certifique-se de ter o seguinte instalado em seu sistema:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Como Executar

1. Clone este repositório:
   ```bash
   git clone https://github.com/gcamposl/fraud-sys.git
   ```
   
2. Navegue até o diretório do projeto e execute o comando abaixo para construir a imagem Docker:
   ```bash
   docker-compose build
   ```
    
3. Inicie o container:
   ```bash
   docker-compose up -d
   ```

4. Execute a API:
   ```bash
   cd Api/
   dotnet run
   ```

5. Tenha acesso ao Swagger da Api através da Url:
   ```bash
   https://localhost:33062/swagger
   ```

## Como Encerrar

1. Finalize o container:
   ```bash
   docker-compose down
   ```
