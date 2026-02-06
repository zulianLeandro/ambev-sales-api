# Ambev Developer Evaluation - Sales API

Este projeto consiste em uma API robusta de gerenciamento de vendas, desenvolvida como parte da avaliação técnica para a Ambev. 
A solução aplica conceitos avançados de **Clean Architecture**, **DDD (Domain Driven Design)** e **CQRS**, focando em alta testabilidade e desacoplamento.

## 🚀 Diferenciais Implementados (Plus)

Para elevar o nível da entrega, implementei os seguintes diferenciais solicitados:
* **Domain Events**: Disparo de eventos de domínio (`SaleCreatedEvent` e `SaleCancelledEvent`) via Mediator para ações desacopladas.
* **Event Handlers**: Reações que realizam logs estruturados das operações de venda, simulando uma futura integração com mensageria (RabbitMQ).
* **63 Testes Unitários**: Cobertura completa de regras de negócio e Handlers, garantindo a integridade do sistema.

## 🏗️ Decisões de Arquitetura

1.  **Regras de Desconto Progressivo**: Centralizadas nas entidades de domínio, garantindo que descontos de 10% (4+ itens) e 20% (10-20 itens) sejam aplicados de forma consistente.
2.  **Persistência Relacional**: Uso de PostgreSQL para garantir transações ACID, fundamentais para dados financeiros.
3.  **External Identities**: Uso de identidades externas para Clientes e Filiais, seguindo o padrão de denormalização solicitado no enunciado.

## 🛠️ Como Executar o Projeto

### Pré-requisitos
* Docker e Docker Compose instalados.

### Execução via Docker (Recomendado)
1.  Navegue até a pasta do backend:
    ```bash
    cd template/backend
    ```
2.  Suba os containers:
    ```bash
    docker-compose up -d --build
    ```
3.  Acesse a documentação Swagger em: `http://localhost:5000/swagger/index.html`

### Execução de Testes
Para validar as regras de negócio:
1.  Na pasta raiz da solução(\template\backend), execute:
    ```bash
    dotnet test tests/Ambev.DeveloperEvaluation.Unit/Ambev.DeveloperEvaluation.Unit.csproj
    ```

## 📬 Estrutura do Projeto

* **src/Application**: Handlers de comando, consultas e eventos (Loggers).
* **src/Domain**: Entidades, Eventos de Domínio e Regras de Negócio.
* **src/Infrastructure**: Persistência de dados (ORM/Migrations) e Repositórios.
* **tests/Unit**: Suíte de testes automatizados organizada por contexto.

---
*Desenvolvido com foco em excelência técnica para o desafio OMNIA - AMBEV.*