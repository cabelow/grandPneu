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


## Para testar via Swagger:
- **link:** http://localhost:5106/swagger/index.html
- **API:** /users/login

API: /users/login

## Dados de Logins:

- **Admin:** admin@grandpneus.com.br
- **Gestor:** gestor@grandpneus.com.br
- **User:** user@grandpneus.com.br
- **Senha padrão:** GranPneu@1234

## Pegar o token e salvar no Autorize do Swagger.

- **token ex:** "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AZ3JhbmRwbmV1cy5jb20uYnIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImlkIjoiOGQ2NTIwNTgtMzRlYS00NWVmLTlmNDAtOGYyMTM4MTFiMTVhIiwiZXhwIjoxNzY2ODU4MTQ2LCJpc3MiOiJHcmFuZFBuZXVBUEkiLCJhdWQiOiJHcmFuZFBuZXVDbGllbnQifQ.6keaSJ8_h8i4R1112B8BKn4j3xF4XRHcqScTxCBH90Y"


## 📦 Frontend

## Estrutura
src/app
├── core
│   ├── guards
│   │   ├── auth.guard.ts
│   │   └── role.guard.ts
│   ├── interceptors
│   │   └── auth.interceptor.ts
│   ├── services
│   │   ├── auth.service.ts
│   │   └── user.service.ts
│   └── models
│       ├── user.model.ts
│       └── login.model.ts
│
├── pages
│   ├── home
│   │   ├── home.page.ts
│   │   └── home.page.html
│   ├── login
│   │   ├── login.component.ts
│   │   └── login.component.html
│   └── users
│       ├── users.component.ts
│       └── users.component.html
├── services
│   ├── auth.service.ts
│   ├── auth-storage.service.ts
│   └── users.service.ts
│
├── app-routing.module.ts
└── app.module.ts



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

    
