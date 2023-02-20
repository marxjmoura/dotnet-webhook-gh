# .NET Webhook for GitHub

.NET Core webhook to listen for GitHub events.

[![CircleCI](https://circleci.com/gh/marxjmoura/dotnet-webhook-gh/tree/main.svg?style=shield)](https://circleci.com/gh/marxjmoura/dotnet-webhook-gh/tree/main)
[![codecov](https://codecov.io/gh/marxjmoura/dotnet-webhook-gh/branch/main/graph/badge.svg)](https://codecov.io/gh/marxjmoura/dotnet-webhook-gh)

## How to

Prerequisite: Download and install [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

### How to run the API locally:

`dotnet run --project src/DotnetWebhookGH.Api`

### How to run the automated tests:

1. Install dotnet-reportgenerator tool:  
`dotnet tool install --local dotnet-reportgenerator-globaltool --version 5.1.17`

2. Run the test tool:  
`tools/code-test.sh`

3. Read the code coverage report:  
`src/DotnetWebhookGH.Tests/coverage/report/index.html`

### How to debug:

1. Download and install [VS Code](https://code.visualstudio.com/download).
The easiest, lightest and multi-platform way to debug.

2. Install these two VS Code extensions:
   - [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp):
     For editing, highlighting, intellisense (autocomplete) and debugging.
   - [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client):
     To send request to API (same as Postman or Insomnia) using `*.http` files.

3. Press F5:  
Aftter build, VS Code should open your browser and launch the API
from the documentation page (`http://localhost:5000/docs/index.html`).
Have fun!

### How to deploy:

It's important you have some knowledge about these AWS services:
[CloudFormation](https://aws.amazon.com/cloudformation/),
[RDS for PostgreSQL](https://aws.amazon.com/rds/postgresql/),
[API Gateway](https://aws.amazon.com/api-gateway/),
[Lambda](https://aws.amazon.com/lambda/).

1. You need an [AWS account](https://aws.amazon.com/account/) and
the [AWS CLI](https://aws.amazon.com/cli/) installed and configured on your computer.

2. Run the tool `tools/deploy-database.sh` to create the PostgreSQL database.

3. Run the tool `tools/deploy-api.sh` to deploy the API.

## Architecture

<a href="https://raw.githubusercontent.com/marxjmoura/dotnet-webhook-gh/main/diagrams/architecture.svg">
  <img src="https://raw.githubusercontent.com/marxjmoura/dotnet-webhook-gh/main/diagrams/architecture.svg" alt="Architecture diagram" width="320">
</a>

_[click on the image to enlarge]_

## Data modeling - PostgreSQL

<a href="https://raw.githubusercontent.com/marxjmoura/dotnet-webhook-gh/main/diagrams/der.svg">
  <img src="https://raw.githubusercontent.com/marxjmoura/dotnet-webhook-gh/main/diagrams/der.svg" alt="DER" width="320">
</a>

_[click on the image to enlarge]_
