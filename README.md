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

**Estrutura sugerida:**

backend/
├── GrandPneu.Api/ # API REST
├── GrandPneu.Application/ # Regras de negócio / services
├── GrandPneu.Domain/ # Entidades e enums
├── GrandPneu.Infrastructure/ # DB, EF Core, migrations
└── backend.sln # Solution file


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
    User <|-- Cliente

    class User {
        +Guid Id
        +string Name
        +string Email
        +string PasswordHash
        +UserRole Role
    }

    class Admin
    class Gestor
    class Cliente

    
