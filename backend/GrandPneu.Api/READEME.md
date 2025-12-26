📂 Estrutura de projetos (GrandPneu)
Projeto	Conteúdo / Função	Dependências principais
GrandPneu.Api	Projeto web/API. Contém Program.cs, configuração do pipeline (Swagger, HTTPS, middlewares, endpoints), controllers. É o projeto de startup.	Microsoft.AspNetCore.*, Microsoft.EntityFrameworkCore, Npgsql, Swashbuckle.AspNetCore
GrandPneu.Application	Camada de aplicação. Contém serviços, casos de uso, lógica de orquestração de entidades. Não acessa banco diretamente.	Referência a GrandPneu.Domain e, se necessário, GrandPneu.Infrastructure
GrandPneu.Domain	Camada de domínio. Contém entidades (User, UserRole), enums e regras de negócio puras. Não depende de EF nem de API externas.	Geralmente nenhuma dependência externa, para manter o domínio puro
GrandPneu.Infrastructure	Camada de infraestrutura. Contém DbContext, migrations, repositórios, integração com banco e outros serviços externos.	Microsoft.EntityFrameworkCore, Npgsql, Microsoft.Extensions.Configuration.*




📦 Gerenciamento de Pacotes no .NET
1️⃣ Adicionar pacotes (equivalente ao pip install <pacote> ou npm install <pacote>)
# Adiciona um pacote ao projeto atual
dotnet add package <NomeDoPacote>

# Adiciona um pacote com versão específica
dotnet add package <NomeDoPacote> --version <Versão>


Exemplo:

dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.0
dotnet add package DotNetEnv --version 3.1.0

2️⃣ Remover pacotes
# Remove um pacote do projeto
dotnet remove package <NomeDoPacote>

3️⃣ Atualizar pacotes
# Atualiza todos os pacotes para a última versão compatível
dotnet list package --outdated
dotnet add package <NomeDoPacote> --version <NovaVersão>

4️⃣ Listar pacotes
# Lista todos os pacotes instalados no projeto
dotnet list package

# Lista pacotes que estão desatualizados
dotnet list package --outdated

5️⃣ Restaurar pacotes (como pip install -r requirements.txt)
# Restaura todas as dependências do projeto
dotnet restore

6️⃣ Gerenciamento de ferramentas globais
# Instala uma ferramenta global (CLI)
dotnet tool install --global <NomeDaFerramenta>

# Atualiza uma ferramenta global
dotnet tool update --global <NomeDaFerramenta>

# Remove uma ferramenta global
dotnet tool uninstall --global <NomeDaFerramenta>

# Lista ferramentas globais instaladas
dotnet tool list --global

7️⃣ Dicas úteis

Cada projeto (.csproj) é como um requirements.txt próprio.

Para garantir consistência em todos os projetos de uma solução, use dotnet restore na raiz da solução (.sln).

Para ver todas as dependências com versão de todos os projetos de uma solução:

dotnet list <Projeto>.csproj package
