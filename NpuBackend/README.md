# NPU backend

**Local installation**

You can reproduce the environment locally (except bucket)
with the Docker compose, just before fill the `.env` -
you can just make a copy from `.env.example` and fill the remaining values:

```
DB_HOST=npu.postgres
DB_PORT=5432
DB_USER=admin
DB_PASSWORD=admin
DB_NAME=npu
BLOB_STORAGE_CONNECTION_STRING=...
AZURE_STORAGE_CONTAINER_NAME=images
AZURE_STORAGE_BASE_URL=...
```
Then, if the Blob container exists and correctly referenced, you can simply run:
```shell
docker compose up --build -d
```

**Azure deployment**

This option has an ARM template, which can reproduce all the infrastructure with one deploy.
You should have an Azure CLI installed and have an `azure-deploy.parameters.json` 
in the `Infrastructure/ARM` folder - you can just make a copy from `Infrastructure/ARM/azure-deploy.parameters.example.json`.
Then, you need to run such commands:

```shell
# Create resource group
az group create --name npu-resource-group --location westeurope

# Create group resources
az deployment group create --resource-group npu-resource-group --template-file Infrastructure/ARM/azure-deploy.json --parameters Infrastructure/ARM/azure-deploy.parameters.json

# Once single server deployed, create database
az postgres db create --resource-group npu-resource-group --server-name npu-pgserver-west-eu --name npu

# Create Blob container (can be done in the portal as well)
az storage container create -n mystoragecontainer

# Then, you can publish and deploy the REST API (substitute with your path to release)
az webapp deploy --resource-group npu-resource-group --name npu-api --src-path <path_to_release> --type zip
```

Notes: 

1. Webapp can not be created at first time, because it should be created once AppService is created.
To control which service should be created under resource group, you can switch the parameter in the `azure-deploy.parameters.json`.
2. You will need to allow Azure resources access the database at [Azure portal](https://portal.azure.com):
   1. Azure Database for PostgreSQL single server
   2. YOUR_SERVER_NAME
   3. Settings
   4. Connection security
   5. Set `Allow access to Azure services` to `Yes`
