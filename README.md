# 🏦 FraudSys — Sistema de Gestão de Limite para Transações PIX

> Desafio técnico — Desenvolvedor Full Stack (.NET + DynamoDB)  
> Banco KRT (BTG Pactual) — Fraude & Limite de Transações PIX  

## 📋 Descrição do Projeto

O **FraudSys** é um sistema desenvolvido para gerenciar e controlar os **limites de transações PIX** de clientes, garantindo maior segurança e prevenção de fraudes.  

O sistema permite:
- Cadastrar limites por cliente (CPF, agência e conta);
- Consultar, atualizar e remover limites existentes;
- Processar transações PIX em tempo real;
- Validar se a transação está dentro do limite disponível e atualizar o saldo.

## ⚙️ Tecnologias Utilizadas

| Camada | Tecnologia |
|---------|-------------|
| **Backend** | .NET 8 (ASP.NET Core Web API) |
| **Banco de Dados** | AWS DynamoDB (ou DynamoDB Local) |
| **Arquitetura** | MVC + Repository + Service |
| **Padrões de Código** | Clean Code, SOLID |
| **Documentação** | Swagger / OpenAPI |
| **Testes (opcional)** | xUnit |

## 🧠 Arquitetura do Projeto

```
AppEstudoAPI/
├── Controllers/
│   ├── LimitsController.cs      → CRUD de limites PIX
│   └── PixController.cs         → Processamento de transações PIX
├── Models/
│   ├── AccountLimit.cs          → Entidade de limite de conta
│   └── PixTransaction.cs        → Entidade de transação PIX
├── Services/
│   ├── LimitService.cs          → Regras de negócio para limites
│   └── PixService.cs            → Regras de negócio para PIX
├── Repositories/
│   └── LimitRepository.cs       → Acesso ao DynamoDB
├── appsettings.json             → Configurações AWS
└── Program.cs                   → Configuração da aplicação
```

## 🚀 Como Executar o Projeto

### 🔹 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [AWS CLI](https://aws.amazon.com/cli/)
- Conta **AWS Free Tier** configurada  
  *(ou DynamoDB Local via Docker, veja abaixo)*

### 🔹 Configurar as Credenciais da AWS

```bash
aws configure
```

Insira suas credenciais IAM:
```
AWS Access Key ID [None]: SEU_ACCESS_KEY
AWS Secret Access Key [None]: SUA_SECRET_KEY
Default region name [None]: us-east-1
Default output format [None]: json
```

### 🔹 Criar a Tabela no DynamoDB

```bash
aws dynamodb create-table     --table-name AccountLimits     --attribute-definitions AttributeName=Id,AttributeType=S     --key-schema AttributeName=Id,KeyType=HASH     --provisioned-throughput ReadCapacityUnits=5,WriteCapacityUnits=5     --region us-east-1
```

### 🔹 Configurar o `appsettings.json`

#### 💻 Para usar **AWS real**
```json
{
  "AWS": {
    "Region": "us-east-1"
  }
}
```

#### 🧱 Para usar **DynamoDB Local**
```json
{
  "AWS": {
    "ServiceURL": "http://localhost:8000",
    "Region": "us-east-1"
  }
}
```

### 🔹 Rodar o projeto

```bash
dotnet run
```

Ou via Visual Studio → **F5**

### 🔹 Acessar o Swagger

```
https://localhost:5001/swagger
```

Endpoints disponíveis:

| Método | Rota | Descrição |
|--------|------|------------|
| `POST` | `/api/limits` | Cadastra novo limite PIX |
| `GET` | `/api/limits/{cpf}/{agency}/{account}` | Busca limite existente |
| `PUT` | `/api/limits` | Atualiza limite de uma conta |
| `DELETE` | `/api/limits/{cpf}/{agency}/{account}` | Remove limite cadastrado |
| `POST` | `/api/pix` | Processa uma transação PIX |

## 💡 Exemplo de Uso

### Criar Limite
**POST** `/api/limits`
```json
{
  "cpf": "12345678900",
  "agency": "0001",
  "account": "12345-6",
  "limit": 1500
}
```

### Fazer uma Transação PIX
**POST** `/api/pix`
```json
{
  "cpf": "12345678900",
  "agency": "0001",
  "account": "12345-6",
  "amount": 500
}
```

**Resposta (Aprovado):**
```json
{
  "status": "Aprovado",
  "mensagem": "Transação aprovada.",
  "limiteRestante": 1000
}
```

**Resposta (Negado):**
```json
{
  "status": "Negado",
  "mensagem": "Transação negada: limite insuficiente.",
  "limiteAtual": 300
}
```

## 🧩 Boas Práticas Adotadas

- ✅ **Clean Code** — nomes claros, sem duplicações.  
- ✅ **SOLID** — separação entre Controller, Service e Repository.  
- ✅ **Injeção de dependência** (Dependency Injection).  
- ✅ **Arquitetura MVC**.  
- ✅ **Documentação automática via Swagger**.  
- ✅ **Código pronto para testes unitários**.

## 🧪 Testes (opcional)

```bash
dotnet test
```

## 🧾 Licença

Este projeto foi desenvolvido apenas para fins de avaliação técnica no **Processo Seletivo BTG Pactual**.  
Uso livre apenas para fins educacionais e demonstrativos.

## ✨ Autor

**Tarcísio Coelho**  
Desenvolvedor Full Stack (.NET / Node / AWS)  
📧 [seu-email@exemplo.com]  
🔗 [github.com/seuusuario](https://github.com/seuusuario)
