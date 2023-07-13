# micro-cosmos-bus-bemol
Microsserviços em C#, o primeiro recebe um objeto no formato JSON via HTTP POST, armazena esse objeto no banco de dados não relacional Azure Cosmos DB e envia uma mensagem para uma fila do Azure Service Bus com as informações do objeto armazenado. O segundo deve consome a fila e processa os objetos do banco a partir dos dados que foram recebidos.

Documentação útil

• Azure Cosmos DB: https://docs.microsoft.com/en-us/azure/cosmos-db/
• Azure Service Bus: https://docs.microsoft.com/en-us/azure/service-bus/
• gRPC: https://grpc.io/docs/

• C# programming guide: https://docs.microsoft.com/en-
us/dotnet/csharp/programming-guide/

• .NET API documentation: https://docs.microsoft.com/en-us/dotnet/api/
• Documentação oficial do ASP.NET Core:
https://docs.microsoft.com/aspnet/core/mvc/controllers/filters?view=aspnetcore-5.0

• Artigo na plataforma Medium: https://medium.com/swlh/asp-net-core-filters-deep-
dive-c1d84468ef2e

Exemplos de tutoriais e amostras de código para o desenvolvimento de
aplicativos .Net:

• Tutorial de .NET com o Azure Cosmos DB: https://docs.microsoft.com/en-
us/azure/cosmos-db/create-sql-api-dotnet

• Tutorial de .NET com o Azure Service Bus: https://docs.microsoft.com/en-
us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues

• Tutorial de gRPC com .NET: https://docs.microsoft.com/en-
us/aspnet/core/grpc/?view=aspnetcore-6.0
