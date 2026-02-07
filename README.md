# Ambev Developer Evaluation - Sales API

Este projeto é uma implementação de alta performance para o gerenciamento de vendas, desenvolvida como parte da avaliação técnica para a **Ambev**. A solução utiliza **.NET 8** e foi desenhada seguindo os princípios da **Clean Architecture**, garantindo que as regras de negócio sejam independentes de frameworks, bancos de dados ou agentes externos.

## 🏛️ Arquitetura e Decisões Técnicas (Decision Log)

Para este desafio, apliquei padrões de nível Enterprise para garantir escalabilidade e manutenibilidade:

* **CQRS & MediatR**: Separação clara entre fluxos de leitura e escrita, otimizando o processamento de comandos e consultas.
* **Domain-Driven Design (DDD)**: O domínio é o coração da aplicação. As entidades `Sale` e `SaleItem` funcionam como **Aggregate Roots**, encapsulando regras de negócio e protegendo a consistência do estado.
* **Domain Events**: Disparo de eventos (`SaleCreatedEvent`, `SaleCancelledEvent`) para reações desacopladas e extensibilidade do sistema.
* **Soft Delete (Cancelamento Lógico)**: Implementação da coluna `IsCancelled` para preservar a integridade referencial e o histórico de auditoria financeira.
* **FluentValidation**: Camada de validação robusta que impede dados inconsistentes antes de atingirem a camada de Application.

## 🚀 Diferenciais Implementados (Plus)

* **Resiliência no Docker**: Uso de `Health Checks` e dependências condicionais para garantir que a API aguarde a prontidão total do PostgreSQL antes de iniciar.
* **Auto-Migration**: Sincronização automática do schema do banco de dados no startup, eliminando a necessidade de comandos manuais.
* **Observabilidade**: Logs estruturados via `ILogger` para rastreabilidade total de eventos e transações no console do container.
* **64 Testes Unitários**: Cobertura completa de regras de desconto progressivo e fluxos de Handlers.

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
    cd template/backend
    ```

2.  Suba os containers(Caso nao tenha feito):
    ```bash
    docker-compose up -d --build
    ```

3.  Execução de Testes   
    ```bash
    dotnet test tests/Ambev.DeveloperEvaluation.Unit/Ambev.DeveloperEvaluation.Unit.csproj
    ```
### Acesso ao Banco de Dados (Opcional)
Se desejar validar os dados via dBeaver ou similar:
* **Host**: `localhost`
* **Porta**: `5433` (Mapeada para evitar conflitos com instâncias locais)
* **User/Pass**: `developer` / `ev@luAt10n`

## 📬 Estrutura do Projeto

* **src/Application**: Handlers de comando, consultas e eventos (Loggers).
* **src/Domain**: Entidades, Eventos de Domínio e Regras de Negócio.
* **src/Infrastructure**: Persistência de dados (ORM/Migrations) e Repositórios.
* **tests/Unit**: Suíte de testes automatizados organizada por contexto.

---
*Desenvolvido com foco em excelência técnica para o desafio OMNIA - AMBEV.*