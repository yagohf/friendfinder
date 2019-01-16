# friendfinder
Repositório para armazenamento dos artefatos relacionados ao teste técnico solicitado pela empresa [**Cubo - Mentoring , Consultoria e Treinamentos**](http://www.cubotecnologia.com.br/).

Esse projeto consiste em uma aplicação que possibilita organizar visitas a amigos, baseado na distância em que eles se encontram da sua posição atual.

## Estrutura de pastas
- A pasta [Backend](./Backend) contém uma API desenvolvida em [ASP.NET Core 2.1](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-2.2), utilizando o [Swagger](https://swagger.io/) como ferramenta para documentar API's, e o [Entity Framework](https://docs.microsoft.com/pt-br/ef/core/) como ORM para acesso a um banco [SQL Server](https://docs.microsoft.com/pt-br/sql/sql-server/sql-server-technical-documentation?view=sql-server-2017). O framework de testes unitários utilizado foi o [MSTest](https://docs.microsoft.com/pt-br/dotnet/core/testing/unit-testing-with-mstest). A aplicação foi desenvolvida em camadas totalmente independentes, facilitando assim a elaboração de testes unitários (aproximadamente 80% da aplicação está coberta por testes unitários).
- A pasta [Database](./Database) contém os scripts que devem ser executados em uma base SQL Server para que a aplicação funcione. Os scripts já encontram-se na ordem correta de execução. Existe inclusive um script que cria um usuário para utilização em testes, já com uma massa de dados (Usuário: **cubo** / Senha: **123mudar**). Recomenda-se o uso da versão 2014 ou superior do SQL Server.
- A pasta [Frontend](./Frontend) contém uma aplicação cliente desenvolvida em [Angular 7](https://angular.io/).

## Ajustes necessários para executar o projeto
- Substituir a connection string no arquivo [appsettings.Development.json](./Backend/Yagohf.Cubo.FriendFinder.Api/appsettings.Development.json) pela conexão do seu banco de dados. Caso queira publicar a aplicação, é necessário alterar essas mesmas informações no arquivo [appsettings.json](./Backend/Yagohf.Cubo.FriendFinder.Api/appsettings.json).
- Substituir a URL da API no arquivo [environment.ts](./Frontend/friendfinder/src/environments/environment.ts) pela URL em que sua API encontra-se rodando.
- Substituir a API Key do Google Maps no arquivo [index.html](./Frontend/friendfinder/src/index.html) por uma Key válida. Para obter uma key [siga essas instruções](https://developers.google.com/maps/documentation/javascript/get-api-key).

## Demonstração
- Essa aplicação foi publicada na AWS para fins de demonstração. [Clique aqui para acessar](http://friendfinder-frontend.s3-website-us-east-1.amazonaws.com). Utilizar o usuário **cubo** e a senha **123mudar**.
