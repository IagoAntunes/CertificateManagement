<!--
🌐 [English](#-english) | [Português](#-português)
-->

<p align="center">
  <a href="#-português">🇧🇷 Português</a>
  <br/><br/>
  <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white" alt=".NET Badge" />
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C# Badge" />
  <img src="https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white" alt="Angular Badge" />
  <img src="https://img.shields.io/badge/MongoDB-%2347A248.svg?style=for-the-badge&logo=mongodb&logoColor=white" alt="MongoDB Badge" />
  <img src="https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white" alt="RabbitMQ Badge" />
</p>

<img width="1080" height="758" src="https://github.com/user-attachments/assets/13103975-603a-49f3-9ce4-4457a746c0ab"/>

---


## 🇺🇸 English

### 🎬 Project Description

**CertificateManagement** is a full-stack web application designed for dynamic certificate generation and management. The system allows users to create personalized certificates, list them, and download the final image. A key feature is the session management system that provides a personalized experience without requiring user authentication, associating generated certificates with a unique browser session.

### 🛠️ Tools and Technologies Used

- **Backend:**
  - **.NET 8 / C#** 💻 — Core platform for the API
  - **ASP.NET Core** 🌐 — Framework for building the REST API
  - **MongoDB** 🍃 — NoSQL database for storing certificate data
  - **MassTransit** 🚌 — Abstraction for message-based communication
  - **RabbitMQ** 🐇 — Message broker for asynchronous tasks
  - **AutoMapper** 🔄 — For object-to-object mapping (Entities to DTOs)

- **Frontend:**
  - **Angular** 🅰️ — Framework for building the client-side user interface
  - **TypeScript** ⌨️ — Main language for the frontend
  - **HTML & SCSS** 🎨 — For structuring and styling the application
  - **PrimeNG** ✨ — UI component library for Angular

### 🏛️ Project Architecture

The project is structured following modern software design principles, separating backend and frontend responsibilities.

#### Backend Architecture (Clean Architecture)
The API follows the **Clean Architecture** principles, separating concerns into distinct layers:
- **Domain**: Contains the core business logic and entities (e.g., `CertificateEntity`), with no external dependencies.
- **Application**: Orchestrates the data flow and implements use cases (services), depending only on the Domain layer.
- **Infrastructure**: Handles external concerns like database access (MongoDB repository implementation), messaging, and file storage.
- **API**: The entry point of the application, responsible for exposing the REST endpoints and handling HTTP requests/responses. It connects all other layers through **Dependency Injection**.

#### Frontend Architecture
The frontend is built with Angular, following its standard architecture:
- **Components**: Reusable UI blocks with their own logic and templates (e.g., certificate list, creation form).
- **Services**: Encapsulate business logic, such as API communication (`CertificateService`) and session management (`SessionService`).
- **`localStorage`**: Used to persist a unique session ID, allowing for a "login-less" user experience.

### 🚀 How to Run the Project Locally

#### Backend (.NET API)
1. **Prerequisites:**
   - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - A running instance of [MongoDB](https://www.mongodb.com/try/download/community)
   - A running instance of [RabbitMQ](https://www.rabbitmq.com/download.html) (can be run with Docker)

2. **Clone the repository:**
   ```bash
   git clone https://github.com/IagoAntunes/CertificateManagement.git
   ```

3. **Configure the backend:**
   - Navigate to the `src/CertificateManagement.API` directory.
   - Update the `appsettings.json` file with your connection strings for MongoDB and RabbitMQ.

4. **Run the API:**
   ```bash
   cd src/CertificateManagement.API
   dotnet restore
   dotnet run
   ```
   The API will be available at `https://localhost:7015`.

#### Frontend (Angular)
1. **Prerequisites:**
   - [Node.js and npm](https://nodejs.org/en/) (LTS version recommended)
   - Angular CLI: `npm install -g @angular/cli`

2. **Navigate to the frontend folder:**
   ```bash
   cd src/CertificateManagement.Web
   ```

3. **Install dependencies:**
   ```bash
   npm install
   ```

4. **Run the application:**
   ```bash
   ng serve
   ```
   The application will be available at `http://localhost:4200`.

---

## 🇧🇷 Português

### 🎬 Descrição do Projeto

**CertificateManagement** é uma aplicação web full-stack para geração e gestão dinâmica de certificados. O sistema permite que os usuários criem certificados personalizados, listem-nos e façam o download da imagem final. Uma funcionalidade chave é o sistema de sessão que oferece uma experiência personalizada sem exigir autenticação do usuário, associando os certificados gerados a uma sessão única do navegador.

### 🛠️ Ferramentas e Tecnologias Utilizadas

- **Backend:**
  - **.NET 8 / C#** 💻 — Plataforma principal da API
  - **ASP.NET Core** 🌐 — Framework para construção da REST API
  - **MongoDB** 🍃 — Banco de dados NoSQL para armazenar os dados dos certificados
  - **MassTransit** 🚌 — Abstração para comunicação baseada em mensagens
  - **RabbitMQ** 🐇 — Message broker para tarefas assíncronas
  - **AutoMapper** 🔄 — Para mapeamento de objetos (Entidades para DTOs)

- **Frontend:**
  - **Angular** 🅰️ — Framework para construção da interface do cliente
  - **TypeScript** ⌨️ — Linguagem principal do frontend
  - **HTML & SCSS** 🎨 — Para estruturação e estilização da aplicação
  - **PrimeNG** ✨ — Biblioteca de componentes de UI para Angular

### 🏛️ Descrição da Arquitetura

O projeto é estruturado seguindo princípios modernos de design de software, separando as responsabilidades de backend e frontend.

#### Arquitetura do Backend (Clean Architecture)
A API segue os princípios da **Clean Architecture**, dividindo as responsabilidades em camadas distintas:
- **Domain**: Contém a lógica de negócio principal e as entidades (ex: `CertificateEntity`), sem dependências externas.
- **Application**: Orquestra o fluxo de dados e implementa os casos de uso (serviços), dependendo apenas da camada de Domínio.
- **Infrastructure**: Lida com preocupações externas, como acesso ao banco de dados (implementação do repositório MongoDB), mensageria e armazenamento de arquivos.
- **API**: Ponto de entrada da aplicação, responsável por expor os endpoints REST e lidar com requisições/respostas HTTP. Conecta todas as outras camadas através de **Injeção de Dependência**.

#### Arquitetura do Frontend
O frontend é construído com Angular, seguindo sua arquitetura padrão:
- **Components**: Blocos de UI reutilizáveis com sua própria lógica e templates (ex: lista de certificados, formulário de criação).
- **Services**: Encapsulam a lógica de negócio, como a comunicação com a API (`CertificateService`) e o gerenciamento de sessão (`SessionService`).
- **`localStorage`**: Utilizado para persistir um ID de sessão único, permitindo uma experiência de usuário "sem login".

### 🚀 Como Rodar o Projeto Localmente

#### Backend (.NET API)
1. **Pré-requisitos**:
   - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - Uma instância do [MongoDB](https://www.mongodb.com/try/download/community) em execução
   - Uma instância do [RabbitMQ](https://www.rabbitmq.com/download.html) em execução (pode ser via Docker)

2. **Clone o repositório:**
   ```bash
   git clone https://github.com/IagoAntunes/CertificateManagement.git
   ```

3. **Configure o backend**:
   - Navegue até o diretório `src/CertificateManagement.API`.
   - Atualize o arquivo `appsettings.json` com suas connection strings para o MongoDB e RabbitMQ.

4. **Execute a API**:
   ```bash
   cd src/CertificateManagement.API
   dotnet restore
   dotnet run
   ```
   A API estará disponível em `https://localhost:7015`.

#### Frontend (Angular)
1. **Pré-requisitos**:
   - [Node.js e npm](https://nodejs.org/en/) (versão LTS recomendada)
   - Angular CLI: `npm install -g @angular/cli`

2. **Navegue até a pasta do frontend**:
   ```bash
   cd src/CertificateManagement.Web
   ```

3. **Instale as dependências**:
   ```bash
   npm install
   ```

4. **Execute a aplicação**:
   ```bash
   ng serve
   ```
   A aplicação estará disponível em `http://localhost:4200`.

---
<p align="center">
  <a href="#-english">⬆️ Back to top</a>
</p>
