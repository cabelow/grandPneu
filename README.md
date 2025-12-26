# Estrutura do Projeto

Este projeto segue a **Clean Architecture** e utiliza **ASP.NET Core** no backend, com autenticação e regras de negócio implementadas.

---

## 📦 Backend

- **Tecnologia:** ASP.NET Core
- **Arquitetura:** Clean Architecture
- **Funcionalidades:**
  - Autenticação JWT
  - Cadastro e login de usuários
  - Regras de negócio e controle de permissões
  - CRUD de usuários


**Endpoints principais:**

| Método | Endpoint      | Acesso                  | Descrição                       |
|--------|---------------|------------------------|---------------------------------|
| POST   | /users/register | Público                | Cria novo usuário com role      |
| POST   | /users/login    | Público                | Autentica usuário e retorna JWT |
| GET    | /users          | Roles 1 e 2           | Lista todos os usuários         |
| PUT    | /users          | Role 1                | Atualiza dados do usuário       |
| GET    | /health         | Público                | Retorna status da API           |


## 🐳 Infraestrutura

- **Docker**
  - `Dockerfile` para backend
  - `docker-compose.yml` para orquestração
- **Banco de dados**
  - Configuração inicial do DB
  - Conexão via variáveis de ambiente (`.env`)
- **Variáveis de ambiente**
  - JWT_KEY, JWT_ISSUER, JWT_AUDIENCE
  - DB_HOST, DB_PORT, DB_USER, DB_PASS, DB_NAME

---



**Diagrama Mermaid:**
```mermaid
classDiagram
    User <|-- Admin
    User <|-- Gestor
    User <|-- User

    class User {
        +Guid Id
        +string Name
        +string Email
        +string PasswordHash
        +UserRole Role
    }

    class Admin
    class Gestor
    class User

    

**Estrutura de pastas Mermaid:**
```mermaid

flowchart TB
    A[GrandPneu.sln] --> B[GrandPneu.Api]
    A --> C[GrandPneu.Application]
    A --> D[GrandPneu.Domain]
    A --> E[GrandPneu.Infrastructure]
    A --> F[docs]

    B --> B1[Controllers]
    B --> B2[DTOs]
    B --> B3[Helpers]

    C --> C1[Services]

    E --> E1[Data / DbContext / Migrations]

