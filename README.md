# Desafio Blog API

## 📚 Descrição

O **Desafio Blog API** é um sistema de gerenciamento de postagens de blog com autenticação e controle de acesso baseado em **Identity**. Os usuários podem criar, visualizar, atualizar e excluir postagens, incluindo a possibilidade de associar imagens às publicações.

---

## 💻 Tecnologias Utilizadas

### Backend
- **.NET 8**: Framework utilizado para desenvolvimento da aplicação.
- **Entity Framework Core**: Para mapeamento objeto-relacional (ORM) e gerenciamento do banco de dados.
- **Microsoft Identity**: Para autenticação e gerenciamento de usuários.
- **SQL Server**: Banco de dados utilizado.
- **Swashbuckle (Swagger)**: Documentação e teste interativo da API.
- **AutoMapper**: Para facilitar a conversão de objetos.
- **FluentValidation**: Para validação de dados de entrada.
- **Moq e xUnit**: Ferramentas de teste unitário.

---

## 📋 Requisitos Funcionais

1. **Cadastro e Login de Usuários:**
   - Os usuários podem se registrar e fazer login no sistema.
   - Autenticação baseada em **JWT**.

2. **Gerenciamento de Postagens:**
   - Criar, visualizar, atualizar e excluir postagens.
   - Adicionar uma URL de imagem às postagens.

3. **Relacionamento entre Usuários e Postagens:**
   - Cada postagem pertence a um usuário autenticado.

4. **Respostas Customizadas:**
   - Todas as respostas da API são padronizadas com um middleware customizado.

5. **Documentação Interativa:**
   - A API conta com documentação no formato Swagger, com suporte para múltiplas versões.

---

## 🛠 Requisitos Técnicos

1. **Requisitos de Ambiente:**
   - .NET SDK 8.0 instalado.
   - SQL Server configurado com a connection string.
   - Ferramenta de gerenciamento de dependências (NuGet).

2. **Configuração do Projeto:**
   - A aplicação é dividida em camadas: `Presentation`, `Application`, `Core`, `Adapters` e `Shared`.
   - O serviço de autenticação utiliza o **Microsoft Identity** para gerenciar usuários.
   - Banco de dados configurado via **EF Core** com migrações.

3. **Configuração de Autenticação:**
   - JWT é utilizado para autenticação e autorização.
   - As chaves e configurações de JWT estão no `appsettings.json`.

4. **Documentação e Testes:**
   - Testes unitários cobrem os principais serviços e regras de negócio.
   - Swagger implementado para fácil exploração dos endpoints.

---

## 🚀 Como Executar

### Passos:

1. Clone este repositório:
   ```bash
   git clone https://github.com/Winilson/BlogDasafioLuza.git
