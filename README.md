# Table of Contents
1. [Overview](#overview)
2. [Demo](#demo)

<a id="overview"></a>
# .net Aspire 

[Offical Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview)

## What is Aspire

Aspire is a distributed application orchestration tool with an emphasis on Azure components and integrations. 

It thrives in local development but also allows you to deploy to Azure

## Why Use Aspire

Aspire has a lot of features but does really well on

- Orchestrating Microservices (distributed applications)
    - Start all of your services 
    - Startup order and references
- [Service Discovery](https://learn.microsoft.com/en-us/dotnet/aspire/service-discovery/overview)
    - Don't need to manage connection strings
    - Port overriding 
- [Existing Integrations](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/integrations-overview)
    - Azure integrations 
    - Node, java, go (more than just .net)
    - SQL, Mongo, Redis, etc
- Dashboard 
    - View and manage resources
    - Logs, metrics, configuration


## Where it Struggles 

Aspire still has shortcomings 

- All or nothing 
    - You want to run your entire ecosystem with Aspire. Don't try to run just one application.
- No Hot reloading 
    - At least I couldn't get it to work even though it said it reloaded??
    

## Deploying an Aspire Application

While Aspire's main purpose is to enhance local development, it can still be deployed to Azure 

You can follow these steps to deploy to Azure 
- [Install azd](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd?tabs=winget-windows%2Cbrew-mac%2Cscript-linux&pivots=os-windows)
- in terminal at host project
    - `azd init --from-code`
    - `azd auth login`
    - `azd up`
    - Select options 

> Deployment can take several minutes

While similar to K8, Aspire is not intended to replace it. There is another tool, [Aspir8](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/overview#deploy-to-kubernetes), for deploying an Aspire application to K8. 

For official deployment documentation, [click here](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/overview)


<a id="demo"></a>
# Demo

This reposity demonstrates how to use Aspire to launch a distibuted application. 

- a few asp.net apis
- sql server via dockerfile 
- redis cache
- next.js (react)

# Running AppHost

To run this project you must have .net 9 installed and have docker running. 

To start the app, run the AspireExample.AppHost project. 

> This will start all the apps listed above and open a dashboard. 

Click the url for frontend and a basic webpage will open showing random combos of NBA players and team names. 

# Deployment 
At root of repo, run these commands (must have [azd installed](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd?tabs=winget-windows%2Cbrew-mac%2Cscript-linux&pivots=os-windows))

- `azd init`
- `azd auth login`
- `azd up`
    - runs provision, package, and deploy under the hood 
- Select options 

> Deployment can take several minutes

For more info, view [how to deploy aspire to azure](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment)


# CI/CD
[Github Actions](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment-github-actions?tabs=windows&pivots=github-actions)