# Fur2FeathersAPI-project3
Haroldo Altamirano, Jonathan Bui, Abraham Bergerson


The API supports RESTful CRUD operations to 10 database tables. The API is open to extension through dotnet migrations. The API was first created through a database first scaffold and then decoupled from entity framework core. It has a repository for each controller and that repository is instantiated through dependency injection in startup.cs.

The update method does not support posting a new object; the update (PUT) method in each controller's respective action method (for example the Address controller's PutAddress action method) only updates and existing object. If an object is not found with the queried id then a 400 bad request code is returned as a response.

A swagger page of auto generated schemas and response codes for success, client error failure, and server failure for all the controllers' action methods are available at the baseURL/swagger/index.html

In order to launch the API and it's PostgreSQL database we recommend running a docker-compose up --build command at the directory where the docker-compose files are. Additionally, the kubernetes manifests are included and have been previously successfully been deployed to AWS. Currently an azure pipeline continually deploys master branch after commits to master branch. The pipeline builds the application, publishes the docker image to jhbui1's repository on Dockerhub and that image is deployed to AWS kubernetes service.
