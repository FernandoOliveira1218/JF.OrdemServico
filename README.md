````markdown
# JF.OrdemServico - Instruções para rodar a aplicação

## Requisitos

- Docker e Docker Compose instalados  
  [Download Docker Desktop](https://www.docker.com/products/docker-desktop)  
- (Opcional) .NET SDK instalado para rodar comandos localmente (caso queira rodar migrations manualmente)  
  [Download .NET SDK](https://dotnet.microsoft.com/en-us/download)

---

## Passos para rodar a aplicação

### 1. Clone este repositório

```bash
git clone https://github.com/FernandoOliveira1218/JF.OrdemServico.git
cd JF.OrdemServico
````

### 2. Suba todos os containers (banco, RabbitMQ, API e Worker)

```bash
docker-compose up -d --build
```

Esse comando irá:

* Subir os containers do PostgreSQL, MongoDB e RabbitMQ
* Construir e subir a API e o Worker
* Criar uma rede Docker para comunicação entre os containers

---

Claro! Aqui está o texto revisado, com ajustes de clareza, gramática e consistência no estilo:

---

### 3. Execute as migrations do banco de dados

Como os containers da API e do Worker possuem apenas o **runtime** do .NET, as migrations precisam ser aplicadas manualmente.

#### ✅ Se você possui o .NET SDK instalado localmente:

```bash
cd src/Services/JF.OrdemServico.API
dotnet ef database update
```

Esse comando irá criar as tabelas necessárias no banco de dados PostgreSQL executado dentro do container.

---

#### 🐳 Caso deseje aplicar as migrations via Docker (sem SDK instalado):

Execute o comando abaixo a partir da raiz do projeto:

```bash
docker run --rm -it -v ${PWD}:/src -w /src/src/Services/JF.OrdemServico.API mcr.microsoft.com/dotnet/sdk:9.0 bash -c "dotnet tool install --global dotnet-ef --version 9.0.5 && export PATH=\$PATH:/root/.dotnet/tools && dotnet restore && dotnet ef database update"
```

Esse comando cria um container temporário com o .NET SDK, instala a ferramenta `dotnet-ef`, restaura os pacotes e aplica as migrations no banco PostgreSQL configurado.

---

### 4. Verifique se a aplicação está rodando

* Acesse a API Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)
* Acesse a interface administrativa do RabbitMQ: [http://localhost:15672](http://localhost:15672)
  Usuário: `admin`
  Senha: `admin`

---

## Como testar

* Use os endpoints da API para criar e finalizar chamados.
* O Worker consome eventos da fila `chamado.finalizado` e salva os dados no MongoDB.
* MongoDB pode ser acessado via Compass na porta `27017`.

---

## Comandos úteis

* Para parar e remover os containers:

```bash
docker-compose down
```

* Para ver os logs da API:

```bash
docker logs -f ordemservico_api_prod
```

---

**Obrigado por utilizar o JF.OrdemServico!**

```
