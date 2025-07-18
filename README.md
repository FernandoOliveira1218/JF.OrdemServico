````markdown
# JF.OrdemServico

## Descrição
Projeto de gestão de ordens de serviço utilizando **.NET 9**, RabbitMQ, MongoDB, PostgreSQL, com arquitetura limpa (Clean Architecture).

## Estrutura
- API RESTful (.NET 9) - camada principal de domínio e aplicação
- Worker (.NET 9) - consumidor de mensagens RabbitMQ para gravação em MongoDB
- Banco de dados:
  - PostgreSQL: dados relacionais
  - MongoDB: logs / eventos armazenados
- Mensageria: RabbitMQ para comunicação assíncrona

## Como rodar com Docker

1. Subir containers:

```bash
docker-compose up -d
````

2. Verificar containers rodando:

```bash
docker ps
```

3. API disponível em: `http://localhost:5000`

4. RabbitMQ UI disponível em: `http://localhost:15672` (usuário: admin / senha: admin)

## Como testar

* Use endpoints da API para criar e finalizar chamados
* O worker consome eventos `chamado.finalizado` da fila e salva no MongoDB
* MongoDB pode ser acessado via Compass na porta `27017`

## Configurações

* Variáveis e conexões configuradas no `appsettings.json`
* RabbitMQ, MongoDB e PostgreSQL configurados via Docker Compose e injeção de dependências

---
