# .github/workflows/deploy-infra.yml
name: Deploy Azure Infrastructure

on:
  workflow_dispatch:

jobs:
  deploy-bicep:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout code
        uses: actions/checkout@v3

      - name: Login to Azure
        uses: azure/login@v1
        with:
          creds: ${{ secrets.AZURE_CREDENTIALS }}

      - name: Deploy Bicep Template
        run: |
          az deployment group create \
            --resource-group HorasExtrasRG \
            --name pruebaDespliegue${{ github.run_number }} \
            --template-file infra/main.bicep \
            --parameters environment=prod location=eastus \
            --output json