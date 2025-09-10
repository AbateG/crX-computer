# Azure Deployment Guide

## Azure Resources
- App Service
- SQL Database
- Key Vault

## Steps
1. Provision Azure App Service and SQL Database.
2. Set up Key Vault for secrets (SMTP, JWT, API keys).
3. Configure deployment credentials and environment variables.
4. Set up GitHub Actions for CI/CD (see `.github/workflows/`).
5. Push to `main` branch to trigger deployment.
6. Monitor deployment in Azure Portal.

## Notes
- Ensure connection strings and secrets are stored securely in Key Vault.
- Update `appsettings.Production.json` for production settings.
