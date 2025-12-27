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


# 📦 Frontend

O frontend é construído com **Angular 20** + **Ionic**, utilizando **componentes standalone**, **services**, **guards** e **interceptors** para autenticação e controle de acesso.

### Estrutura
src/app
├── core
│ ├── guards
│ │ ├── auth.guard.ts # Protege rotas para usuários autenticados
│ │ └── role.guard.ts # Protege rotas por função (Admin, Gestor)
│ ├── interceptors
│ │ └── auth.interceptor.ts # Adiciona token Bearer nas requisições HTTP
│ ├── services
│ │ ├── auth.service.ts # Login, logout e verificação de autenticação
│ │ └── users.service.ts # CRUD de usuários
│ └── models
│ ├── user.model.ts # Modelo de usuário
│ └── login.model.ts # Modelo de login
│
├── pages
│ ├── home
│ │ ├── home.page.ts # Página inicial (boas-vindas)
│ │ └── home.page.html
│ ├── login
│ │ ├── login.component.ts # Página de login (standalone)
│ │ └── login.component.html
│ ├── users
│ │ ├── users.component.ts # Listagem de usuários com modal de edição (Admin)
│ │ ├── users.component.html
│ │ └── user-edit/
│ │ ├── user-edit.component.ts # Modal de edição de usuário
│ │ └── user-edit.component.html
│ └── register-user
│ ├── register-user.component.ts # Página de registro de usuários
│ └── register-user.component.html
│
├── services
│ ├── auth-storage.service.ts # Armazenamento do token no localStorage
│ └── users.service.ts # CRUD de usuários (inclui register)
│
├── app-routing.module.ts # Rotas da aplicação
└── app.module.ts # Módulo principal (apenas para bootstrap do Angular)


### Fluxo de autenticação

1. **Login**
   - Usuário informa e-mail e senha.
   - Token JWT é salvo no `AuthStorage`.
   - Usuário é redirecionado para `/users` ou `/home`.

2. **Guards**
   - `auth.guard.ts`: garante que apenas usuários logados acessem certas rotas.
   - `role.guard.ts`: permite acesso baseado em função (`Admin` ou `Gestor`).

3. **Interceptor**
   - `auth.interceptor.ts`: adiciona o header `Authorization: Bearer <token>` em todas as requisições HTTP.

### Páginas principais

- **Home**: Página inicial com mensagem de boas-vindas.  
- **Login**: Entrada de usuário para autenticação.  
- **Users**: Listagem de usuários com opção de editar (somente Admin).  
- **Register User**: Permite cadastrar novos usuários, valida senha e confirma senha.

### Observações

- Componentes standalone permitem imports diretos de módulos como `FormsModule` e `IonicModule`.  
- Validações de senha no registro garantem mínimo de 8 caracteres, 1 letra maiúscula e 1 número.  
- Modal de edição só aparece para usuários Admin.



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

    
