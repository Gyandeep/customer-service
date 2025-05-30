# 🧾 Customer Service API – Local Development Guide

## 📋 Prerequisites

Ensure the following tools are installed on your system:

- [Minikube](https://minikube.sigs.k8s.io/docs/start/)
- [kubectl](https://kubernetes.io/docs/tasks/tools/)
- [Docker](https://www.docker.com/products/docker-desktop/)
- [.NET SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Postman](https://www.postman.com/downloads/)

---

## 🚀 Local Setup

### 🛠️ Backend (Customer Service API)

1. **Clone the Repository**
   ```cmd
   git clone https://github.com/Gyandeep/customer-service.git
   cd customer-service/src
   ```

2. **Build the Docker Image**
   > Ensure Docker is running.
   ```cmd
   docker build -t customerservice:v1 -f Dockerfile .
   ```

3. **Start Minikube**
   ```cmd
   minikube start
   ```

4. **Load the Docker Image into Minikube**
   ```cmd
   minikube image load customerservice:v1
   ```

5. **Deploy the Service**
   ```cmd
   kubectl apply -f deployment/kubernetes.deployment.yaml
   kubectl apply -f deployment/kubernetes.service.yaml
   ```

6. **Access the Service**
   ```cmd
   minikube service customerservice
   ```
   This will open the service in your default browser. Copy the full URL or tunnel port from the address bar.

---

### 🧪 Client (Postman)

1. **Import Environment**
   - Open Postman.
   - Import the environment file:
     ```
     ~\customer-service\client\customerservice.postman_environment.json
     ```

2. **Import Collection**
   - Import the collection file:
     ```
     ~\customer-service\client\customerservice.postman_collection.json
     ```

3. **Set the Port in Environment**
   - Go to the **Postman Environments** tab.
   - Select the `customerservice` environment.
   - Set the `port` variable to the tunnel port from the Minikube service URL.

4. **Test the API**
   - Create Customer: `POST /api/Customer`
   - Get Customer: `GET /api/Customer`
   - Update Customer: `PUT /api/Customer`
   - Delete Customer: `DELETE /api/Customer`

---

## 🧠 Architecture Note

> The API uses **in-memory caching** via `MemoryCache` to simulate data persistence.  
> This layer is injected into the `CustomerData` component and is designed for easy extension to other storage solutions like:
- Azure Cosmos DB
- AWS DynamoDB
- ADO.NET or Entity Framework

---

## ✅ Example Workflow

1. Start services via Minikube.
2. Open Postman and set up environment.
3. Create a customer using the `POST /api/Customer` request.
4. Use the returned `id` to run GET, PUT, or DELETE operations.

---