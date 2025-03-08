

# Some things to test 

check if more azd init/up options 

should show service discovery (have player api call team api?)
have frontend call info service which calls player api and team api


# Basics of Aspire 

[Offical Overview](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview)

Aspire is an __orchestration__ tool similar to K8 or docker compose. 

It thrives in local development but also allows you to deploy to [Azure Container Apps](https://learn.microsoft.com/en-us/azure/container-apps/overview)

> Aspire is not intended to replace K8. There is another tool [Aspir8](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/overview#deploy-to-kubernetes).

Some notes: 
- Ideal for a multi project environment (microservices). 
- Don't need to set urls (don't need to remember ports) [service discovery](https://learn.microsoft.com/en-us/dotnet/aspire/service-discovery/overview)
- Not limited to just .net. There are [existing integrations](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/integrations-overview)
- Doesn't require custom dockerfiles to deploy 
- Difficult to run single service without using aspire 
- Not much transparency on pricing

Nice features:
- Force services to depend on others
- Override ports
- 


# This Repository

This reposity uses Aspire to launch a distibuted application. 

- 2 asp.net apis
- sql server via dockerfile 
- next.js (react)

# Deployment 
At root of repo, run these commands (must have [azd installed](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd?tabs=winget-windows%2Cbrew-mac%2Cscript-linux&pivots=os-windows))

- `azd init`
- `azd auth login`
- `azd up`
- Select options 

> Deployment can take several minutes

For more info, view [how to deploy aspire to azure](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment)


# CI/CD
[Github Actions](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment-github-actions?tabs=windows&pivots=github-actions)