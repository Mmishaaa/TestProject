# 📚Dating App

> ### **How to set up App**
>
> ## :clipboard: Prerequisites

- Git - [Download & Install Git](https://git-scm.com/downloads).
- kubectl - Command line tool that interacts with the Kubernetes API server and is useful for managing Kubernetes clusters. [installation documentation that corresponds to your platform.](https://kubernetes.io/docs/tasks/tools/)
- Docker - [Download & Install Docker](https://www.docker.com/products/docker-desktop/).
- helm - Package manager used for installing and managing Kubernetes applications - [Installing Helm.](https://helm.sh/docs/intro/install/)
- ## 📦 Getting started

You can clone this repository:

```
https://github.com/Finka95/KM
```
- ## Docker compose

1) create .env file(u may use .env.example)
2) Run docker container using powershell
```
docker-compose up
docker-compose up -d (to run in detached mode)
```
- ## K8S
- # Set up mssql db for tinderService
1) Create mssql persistent volume claim
```
kubectl apply -f  .\K8S\TinderMssqlDb\local-pvc.yml
```
2) Creste mssql instance
```
kubectl apply -f  .\K8S\TinderMssqlDb\mssql-plat-depl.yml
```
- # Set up postgres db for fusionAuth
1) Create postgres persistent volume claim
```
kubectl apply -f  .\K8S\FusionAuthPostgresDb\local-pvc.postgress.yml
```
2) Create postgres instance
```
kubectl apply -f  .\K8S\FusionAuthPostgresDb\postgres-plat-depl.yml
```
- # Set up mongo db for subscriptionService
1) Create mongo persistent volume claim
```
kubectl apply -f  .\K8S\SubscriptionMongoDb\local-pvc.mongo.yml
```
2) Create mongo instance
```
kubectl apply -f  .\K8S\SubscriptionMongoDb\mongo-plat-depl.yml
```
