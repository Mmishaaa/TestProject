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
- # Docker compose

1) create .env file(u may use .env.example)
2) Run docker container using powershell
```
docker-compose up
docker-compose up -d (to run in detached mode)
```
- # K8S
- ## Set up mssql db for tinderService
1) Create mssql persistent volume claim
```
kubectl apply -f  .\K8S\TinderMssqlDb\local-pvc.yml
```
2) Creste mssql instance
```
kubectl apply -f  .\K8S\TinderMssqlDb\mssql-plat-depl.yml
```
- ## Set up postgres db for fusionAuth
1) Create postgres persistent volume claim
```
kubectl apply -f  .\K8S\FusionAuthPostgresDb\local-pvc.postgress.yml
```
2) Create postgres instance
```
kubectl apply -f  .\K8S\FusionAuthPostgresDb\postgres-plat-depl.yml
```
- ## Set up mongo db for subscriptionService
1) Create mongo persistent volume claim
```
kubectl apply -f  .\K8S\SubscriptionMongoDb\local-pvc.mongo.yml
```
2) Create mongo instance
```
kubectl apply -f  .\K8S\SubscriptionMongoDb\mongo-plat-depl.yml
```
- ## Set up tinder service
1) Create tinder node port(if you don't want to access service via localhost you may skip this step)
```
kubectl apply -f  .\K8S\TinderService\tinder-np-srv.yml
```
2) Create tinder deployment
```
kubectl apply -f  .\K8S\TinderService\tinder-depl.yml
```
- ## Set up subscription service
1) Create subscription node port(if you don't want to access service via localhost you may skip this step)
```
kubectl apply -f  .\K8S\SubscriptionService\subscription-np-srv.yml
```
2) Create subscription deployment
```
kubectl apply -f  .\K8S\SubscriptionService\subscription-depl.yml
```
- ## Set up notification service
1) Create notification node port(if you don't want to access service via localhost you may skip this step)
```
kubectl apply -f  .\K8S\NotificationService\notification-np-srv.yml
```
2) Create notification deployment
```
kubectl apply -f  .\K8S\NotificationService\notification-depl.yml
```
- ## Set up graphql service
1) Create graphql node port(if you don't want to access service via localhost you may skip this step)
```
kubectl apply -f  .\K8S\GraphqlService\graphql-np-srv.yml
```
2) Create graphql deployment
```
kubectl apply -f  .\K8S\GraphqlService\graphql-depl.yml
```
- ## Adding an API Gateway
1) Add new host configuration
```
cd C:\Windows\System32\drivers\etc\
open file hosts
add <127.0.0.1 acme.com> above # Added by Docker Desktop line
```
2) Create Nginx service
```
kubectl apply -f  .\K8S\Nginx\ingress-srv.yml
```
- ## Set up FusionAuth service 
1) Add a chart repository
```
helm repo add fusionauth https://fusionauth.github.io/charts
```
2) List chart repositories
```
List chart repositories
```
3) Output from listing chart repositories
```
NAME      	URL
fusionauth	https://fusionauth.github.io/charts
```
4) Search chart repositories
```
helm search repo fusionauth
```
5) Output from search
```
NAME                 	CHART VERSION	APP VERSION	DESCRIPTION
fusionauth/fusionauth	0.10.5       	1.30.1     	Helm chart for fusionauth
```
6) Install the FusionAuth chart
```
helm install my-release fusionauth/fusionauth -f values.yaml
```
7) Example output
```
NAME: my-release
LAST DEPLOYED: Sun Oct 10 19:23:41 2021
NAMESPACE: default
STATUS: deployed
REVISION: 1
NOTES:
1. Get the application URL by running these commands:
  export SVC_NAME=$(kubectl get svc --namespace default -l "app.kubernetes.io/name=fusionauth,app.kubernetes.io/instance=my-release" -o jsonpath="{.items[0].metadata.name}")
  echo "Visit http://127.0.0.1:9011 to use your application"
  kubectl port-forward svc/$SVC_NAME 9011:9011
```
8) Get a list of deployments running on the cluster
```
kubectl get deployments -o wide
```
9) Output
```
NAME                    READY   UP-TO-DATE   AVAILABLE   AGE     CONTAINERS   IMAGES                             SELECTOR
my-release-fusionauth   1/1     1            1           4m16s   fusionauth   fusionauth/fusionauth-app:1.30.1   app.kubernetes.io/instance=my-release,app.kubernetes.io/name=fusionauth
```
10) Setup port-forwarding proxy
```
 kubectl port-forward svc/my-release-fusionauth 9011:9011
```
11) Navigate to http://localhost:9011 and you will land on the FusionAuth Setup Wizard.
- # How to access services
1) Api gateway endpoint
```
http://acme.com
``` 
2) To access specific service from localhost
```
kubectl get service
```
  [] Find services with NodePort TYPE and you can acces them via http://localhost:3xxxx
  [] Find services with LoadBalancer TYPE and you can acces them via http://localhost:port, use the first port(by default, one of these: 27017/1433/5432/15672)
- # Commands sheet
  
1) Get all pods
```
kubectl get pods
```
2)  Get all services
```
kubectl get service
```
3) Get all deployments
```
kubectl get deployments
```
4) Get all persistent volume claims
```
kubectl get pvc
```
5) Get all namespaces
```
kubectl get namespace
```
6) Get services from specific namespace
```
kubectl get service --namespace=<namespace_name>
```
