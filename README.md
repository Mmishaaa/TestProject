# 📚Warehouse

> ### **_Manage your Warehouse easily_**
>
> ## :clipboard: Prerequisites

- Git - [Download & Install Git](https://git-scm.com/downloads).
- Docker - [Download & Install Docker](https://www.docker.com/products/docker-desktop/).
- ## 📦 How to install Warehouse

- You should be in **[develop](https://github.com/Mmishaaa/TestProject/tree/develop)** branch to run application locally
- You should be in **[docker](https://github.com/Mmishaaa/TestProject/tree/docker)** branch to run application in docker container

You can clone this repository:

```
https://github.com/Mmishaaa/TestProject.git
```

Or download it by clicking the green "Code" button and then "Download ZIP". Open it in your IDE.
- ## Locally
Before starting app localally you need to set up database connection string in appsettings.json file

Creating and seeding database(Package Manager Console):
```
update-database
```
- ## Docker container
Use  **[link](https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-8.0)** 
to be sure you have generated certificate and configure local machine

<<<<<<< docker
1)To do it run in powershell(In the commands, replace CREDENTIAL_PLACEHOLDER with your password.)
=======
1) TO do it run in powershell(In the commands, replace CREDENTIAL_PLACEHOLDER with your password.)
>>>>>>> master
```
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx"  -p CREDENTIAL_PLACEHOLDER
dotnet dev-certs https --trust
```
Then check if the password specified in the docker compose file(ASPNETCORE_Kestrel__Certificates__Default__Password) matches the password used for the certificate.

<<<<<<< docker
Then check the connectionString in appsettings.json, 
=======
2) Then check the connectionString in appsettings.json, 
>>>>>>> master
```
  the password should be the same as SA_PASSWORD in docker-compose for mssql service
  server should be mssql_db(service name for db container in docker-compose)
```
<<<<<<< docker
1) Be sure that docker desktop is running on your machine

2) Run docker container using powershell
=======
3) Be sure that docker desktop is running on your machine

4) Run docker container using powershell
>>>>>>> master
```
docker-compose up
docker-compose up -d (to run in detached mode)
```
Or you can start by clicking docker-compose button in visual studio

<<<<<<< docker
3)go to [https://localhost:8080/swagger/index.html](https://localhost:8080/swagger/index.html)
=======
5)go to [https://localhost:8080/swagger/index.html](https://localhost:8080/swagger/index.html)
>>>>>>> master

