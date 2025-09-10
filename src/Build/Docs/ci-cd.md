# CI/CD Pipeline (GitHub Actions)

## Overview
Automate build, test, and deployment using GitHub Actions.

## Example Workflow
- Trigger: Push to `main` or PR
- Steps:
  1. Checkout code
  2. Set up .NET and Node.js
  3. Restore dependencies
  4. Build and test backend and frontend
  5. Run integration and E2E tests
  6. Deploy to Azure App Service

## Example: `.github/workflows/ci-cd.yml`
```yaml
name: CI/CD
on:
  push:
    branches: [ main ]
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      - name: Setup Node.js
        uses: actions/setup-node@v4
        with:
          node-version: '20.x'
      - run: dotnet restore
      - run: dotnet build --no-restore
      - run: dotnet test
      - run: npm install --prefix src/WebApp
      - run: npm run build --prefix src/WebApp
      - run: npx cypress run --project src/Tests/E2E
      - name: Deploy to Azure
        uses: azure/webapps-deploy@v3
        with:
          app-name: ${{ secrets.AZURE_WEBAPP_NAME }}
          publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
          package: src/Api
```
