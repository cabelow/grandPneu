# 🎮 Playbook de Desenvolvimento - Projeto GrandPneu

Este documento contém os comandos e fluxos essenciais para rodar, desenvolver e manter o projeto.

---

## 🛠️ Pré-requisitos
Antes de começar, certifique-se de ter instalado:
- [.NET SDK 7.0+](https://dotnet.microsoft.com/download)
- [Node.js & NPM](https://nodejs.org/)
- [Ionic CLI](https://ionicframework.com/docs/intro/cli) (`npm install -g @ionic/cli`)
- [Docker](https://www.docker.com/)

---

## 🖥️ Backend (C# / .NET)

### Comandos de Inicialização
* **Restaurar dependências:** `dotnet restore`
* **Compilar projeto:** `dotnet build`
* **Executar aplicação:** `dotnet run`
* **Hot Reload:** `dotnet watch run`

### Banco de Dados (Entity Framework Core)
> ⚠️ **Atenção:** Sempre crie uma migration ao alterar as classes de modelo e atualize o banco antes de rodar a API.

* **Criar Migration:** `dotnet ef migrations add NomeDaMigration`
* **Atualizar Banco:** `dotnet ef database update`
* **Remover última Migration:** `dotnet ef migrations remove`
* **Listar todas:** `dotnet ef migrations list`

---

## 📱 Frontend (Ionic / Angular)

### Comandos de Inicialização
* **Instalar dependências:** `npm install`
* **Rodar em desenvolvimento:** `ionic serve`
* **Build de produção:** `ionic build --prod`

### Gerador de Código (Scaffolding)
| Tipo | Comando |
| :--- | :--- |
| **Página** | `ionic generate page NomeDaPagina` |
| **Componente** | `ionic generate component NomeDoComponente` |
| **Serviço** | `ionic generate service NomeDoServico` |
| **Guard** | `ionic generate guard NomeDoGuard` |

### Mobile (Capacitor)
* **Android:** `ionic capacitor run android`
* **iOS:** `ionic capacitor run ios`

---

## 🐳 Infraestrutura (Docker & PostgreSQL)

### Subir banco via terminal (Sem Compose)
```bash
docker run --name grandpneu-db -e POSTGRES_USER=usuario -e POSTGRES_PASSWORD=senha123 -e POSTGRES_DB=grandpneu -p 5432:5432 -d postgres:15