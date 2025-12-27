## Checklist de Configuração do Ambiente

Este arquivo lista os passos para preparar o ambiente em Windows, Linux e macOS, incluindo backend, frontend e ferramentas auxiliares.

## 1. Instalar Git

* Baixar e instalar [Git](https://git-scm.com/)

```bash
git --version
```

## 2. Node.js e NPM

* Instalar Node.js 20+ [Node.js Download](https://nodejs.org/)
* Verificar versões:

```bash
node -v
npm -v
```

* Opcional: usar NVM para gerenciar versões do Node

```bash
# Linux/macOS
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.6/install.sh | bash
source ~/.bashrc
nvm install 20
nvm use 20

# Windows (NVM-Windows)
https://github.com/coreybutler/nvm-windows
```

## 3. Ionic CLI

```bash
npm install -g @ionic/cli
ionic --version
```

## 4. Angular CLI (opcional)

```bash
npm install -g @angular/cli
ng version
```

## 5. Docker (opcional)

* Instalar Docker Desktop: [https://www.docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop)
* Verificar instalação:

```bash
docker --version
docker-compose --version
```

## 6. .NET SDK 8+ (Backend C#)

* Instalar .NET SDK: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
* Verificar instalação:

```bash
dotnet --version
```

## 7. Clonar repositório

```bash
git clone https://github.com/seuusuario/grandpneu.git
cd grandpneu
```

## 8. Configurações adicionais

* Backend: editar `appsettings.json` com conexão do banco.
* Frontend: API URL em `users.service.ts` e `auth.service.ts` (`http://localhost:5106`).
* Habilitar CORS no backend para `http://localhost:8100`.
