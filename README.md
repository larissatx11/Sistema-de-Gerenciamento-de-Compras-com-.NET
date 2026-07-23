<h1 align="center">Sistema de Gerenciamento de Compras</h1>

<p align="center">
  Aplicação web para gerenciamento de usuários, lojas, produtos e compras, desenvolvida com ASP.NET Core MVC.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white" alt=".NET 8">
  <img src="https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet&logoColor=white" alt="ASP.NET Core MVC">
  <img src="https://img.shields.io/badge/Entity%20Framework-Core-512BD4" alt="Entity Framework Core">
  <img src="https://img.shields.io/badge/Docker-suportado-2496ED?logo=docker&logoColor=white" alt="Docker">
  <img src="https://img.shields.io/badge/status-projeto%20acadêmico-blue" alt="Status">
</p>

## Sobre o projeto

Este projeto é uma aplicação web desenvolvida para praticar os principais conceitos do ecossistema .NET, incluindo:

- arquitetura MVC;
- criação de aplicações com ASP.NET Core;
- operações de CRUD;
- Entity Framework Core;
- relacionamentos entre entidades;
- padrão Repository;
- injeção de dependências;
- autenticação básica por sessão;
- validação de formulários;
- conteinerização com Docker.

Cada usuário pode cadastrar e gerenciar suas próprias lojas, produtos e compras.

## Funcionalidades

### Usuários

- criação de conta;
- login por e-mail e senha;
- sessão individual;
- consulta dos dados do perfil;
- edição de nome, e-mail e senha;
- exclusão da conta;
- logout.

### Lojas

- cadastro de lojas;
- listagem das lojas do usuário;
- edição de nome e endereço;
- exclusão de lojas.

### Produtos

- cadastro de produtos;
- associação do produto a uma loja;
- definição de nome e preço;
- listagem dos produtos do usuário;
- edição e exclusão.

### Compras

- registro de compras;
- associação da compra a um produto;
- visualização da loja relacionada ao produto;
- listagem das compras do usuário;
- edição e exclusão.

## Demonstração

### Login e página inicial

| Login | Página inicial |
|---|---|
| ![Tela de login](./Capturas%20TD4/Tela_Login.PNG) | ![Página inicial](./Capturas%20TD4/Pagina%20Inicial.PNG) |

### Operações de cadastro

| Cadastro de loja | Cadastro de produto |
|---|---|
| ![Cadastro de loja](./Capturas%20TD4/Create_Loja.PNG) | ![Cadastro de produto](./Capturas%20TD4/Create_Produto.PNG) |

| Cadastro de compra | Perfil do usuário |
|---|---|
| ![Cadastro de compra](./Capturas%20TD4/Create_Compra.PNG) | ![Perfil do usuário](./Capturas%20TD4/Index_Usuario.PNG) |

<details>
<summary><strong>Ver mais capturas da aplicação</strong></summary>

### Lojas

![Listagem de lojas](./Capturas%20TD4/Index_Lojas.PNG)

![Edição de loja](./Capturas%20TD4/Edit_Loja.PNG)

![Exclusão de loja](./Capturas%20TD4/Delete_Loja.PNG)

### Produtos

![Listagem de produtos](./Capturas%20TD4/Index_Produto.PNG)

![Edição de produto](./Capturas%20TD4/Edit_Produto.PNG)

![Exclusão de produto](./Capturas%20TD4/Delete_Produto.PNG)

### Compras

![Listagem de compras](./Capturas%20TD4/Index_Compra.PNG)

![Edição de compra](./Capturas%20TD4/Edit_Compra.PNG)

![Exclusão de compra](./Capturas%20TD4/Delete_Compra.PNG)

### Usuário

![Edição de usuário](./Capturas%20TD4/Edit_Usuario.PNG)

![Exclusão de usuário](./Capturas%20TD4/Delete_Usuario.PNG)

</details>

## Tecnologias

- C#;
- .NET 8;
- ASP.NET Core MVC;
- Razor Views;
- Entity Framework Core;
- Entity Framework Core InMemory;
- Bootstrap;
- HTML5;
- CSS3;
- JavaScript;
- Newtonsoft.Json;
- Docker.

## Arquitetura

O projeto utiliza o padrão Model–View–Controller e uma camada de repositórios para acesso aos dados.

```mermaid
flowchart LR
    U[Usuário] --> V[Views Razor]
    V --> C[Controllers]
    C --> R[Repositórios]
    R --> E[Entity Framework Core]
    E --> D[(Banco em memória)]
    C --> S[Sessão HTTP]
```

### Camadas

- **Models:** representam usuários, lojas, produtos e compras;
- **Views:** páginas Razor apresentadas ao usuário;
- **Controllers:** recebem requisições e coordenam as operações;
- **Repositórios:** concentram as operações de acesso aos dados;
- **Data:** configura o contexto do Entity Framework Core;
- **Helper:** gerencia a sessão do usuário.

## Modelo de dados

```mermaid
erDiagram
    USUARIO ||--o{ LOJA : possui
    USUARIO ||--o{ PRODUTO : cadastra
    USUARIO ||--o{ COMPRA : registra
    LOJA ||--o{ PRODUTO : oferece
    PRODUTO ||--o{ COMPRA : participa

    USUARIO {
        int Id
        string Nome
        string Email
        string Senha
    }

    LOJA {
        int Id
        string Nome
        string Endereco
        int UsuarioId
    }

    PRODUTO {
        int Id
        string Nome
        double Preco
        int LojaId
        int UsuarioId
    }

    COMPRA {
        int Id
        int ProdutoId
        int UsuarioId
    }
```

## Estrutura do projeto

```text
.
├── Capturas TD4/                 # Imagens da aplicação
├── WebApplication1/
│   ├── Controllers/              # Controllers MVC
│   ├── Data/
│   │   ├── Map/                  # Configuração das entidades
│   │   └── BancoContext.cs       # Contexto do Entity Framework
│   ├── Helper/                   # Gerenciamento de sessão
│   ├── Models/                   # Entidades e modelos
│   ├── Repositorio/              # Interfaces e repositórios
│   ├── ViewComponents/           # Componentes reutilizáveis
│   ├── Views/                    # Páginas Razor
│   ├── wwwroot/                  # CSS, JavaScript e imagens
│   ├── Dockerfile
│   ├── Program.cs
│   └── WebApplication1.csproj
├── .dockerignore
├── WebApplication1.sln
└── README.md
```

## Como executar

### Pré-requisitos

Para executar localmente, instale:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0);
- Git;
- um editor como Visual Studio, Visual Studio Code ou Rider.

Não é necessário instalar um servidor de banco de dados, pois a aplicação utiliza o Entity Framework Core InMemory.

### Instalação

Clone o repositório:

```bash
git clone https://github.com/larissatx11/Sistema-de-Gerenciamento-de-Compras-com-.NET.git
```

Entre na pasta da aplicação:

```bash
cd Sistema-de-Gerenciamento-de-Compras-com-.NET/WebApplication1
```

Restaure as dependências:

```bash
dotnet restore
```

Execute o projeto:

```bash
dotnet run
```

A aplicação ficará disponível em:

```text
http://localhost:5070
```

A rota inicial apresenta a página de login.

## Executando com Docker

### Pré-requisitos

- Docker Desktop ou Docker Engine.

Na raiz do repositório, gere a imagem:

```bash
docker build -f WebApplication1/Dockerfile -t gerenciamento-compras .
```

Execute o container:

```bash
docker run --rm -p 8000:8000 gerenciamento-compras
```

Abra no navegador:

```text
http://localhost:8000
```

## Fluxo de utilização

Para testar todas as funcionalidades:

1. crie uma conta;
2. faça login;
3. cadastre uma loja;
4. cadastre um produto associado à loja;
5. registre uma compra associada ao produto;
6. edite ou remova os registros;
7. consulte ou atualize seu perfil;
8. finalize a sessão usando a opção de logout.

## Limitações conhecidas

Este projeto possui finalidade acadêmica e ainda não está preparado para produção.

- o banco de dados é volátil;
- senhas são armazenadas sem hash;
- o objeto completo do usuário é serializado na sessão;
- algumas exceções internas são apresentadas ao usuário;
- não existem testes automatizados;
- não há migrations para um banco persistente;

## Aprendizados

O projeto demonstra conhecimentos em:

- desenvolvimento web com ASP.NET Core MVC;
- criação de CRUDs relacionados;
- modelagem de entidades;
- uso do Entity Framework Core;
- padrão Repository;
- injeção de dependências;
- controle de sessão;
- validação com Data Annotations;
- criação de views Razor;
- execução em containers Docker.

## Autoria

Desenvolvido por [Larissa Teixeira](https://github.com/larissatx11).
