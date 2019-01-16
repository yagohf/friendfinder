# friendfinder
Repositório para armazenamento dos artefatos relacionados ao teste técnico solicitado pela empresa **Cubo - Mentoring , Consultoria e Treinamentos**.

Esse projeto consiste em uma aplicação para que seja possível organizar visitas a amigos, baseado na distância em que eles se encontram da sua posição atual.

## Estrutura de pastas
- A pasta [Backend](./Backend) contém uma API desenvolvida em ASP.NET Core 2.1, utilizando o Swagger como ferramenta para documentar API's, e o Entity Framework como ORM para acesso a um banco SQL Server. O framework de testes unitários utilizado foi o MSTest. A aplicação foi desenvolvida em camadas totalmente independentes, facilitando assim a elaboração de testes unitários (aproximadamente 80% da aplicação está coberta por testes unitários).
- A pasta [Database](./Database) contém os scripts que devem ser executados em uma base SQL Server para que a aplicação funcione. Os scripts já encontram-se na ordem correta de execução. Existe inclusive um script que cria um usuário para utilização em testes, já com uma massa de dados (usuário **cubo** / senha **123mudar**). Recomenda-se o uso da versão 2014 ou superior do SQL Server.
- A pasta [Frontend](./Frontend) contém uma aplicação cliente desenvolvida em Angular 7.

## Ajustes necessários para executar o projeto
- Substituir a connection string no arquivo [appsettings.Development.json](./Backend/Yagohf.Cubo.FriendFinder.Api/appsettings.Development.json) pela conexão do seu banco de dados. Caso queira publicar a aplicação, é necessário alterar essas mesmas informações no arquivo [appsettings.json](./Backend/Yagohf.Cubo.FriendFinder.Api/appsettings.json).
- Substituir a URL da API no arquivo [environment.ts](./Frontend/friendfinder/src/environments/environment.ts) pela URL em que se encontra rodando (ou publicada) sua API.

## Demonstração
- Essa aplicação foi publicada na AWS para fins de demonstração. O endereço é XXX. Utilizar o usuário **cubo** e a senha **123mudar**.
