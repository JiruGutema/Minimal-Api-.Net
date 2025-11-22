# Minimal-Api-.Net
## Bookstore
- The main difference between minimal api and controller based api is that in minimal api we don't have controllers, instead we define the endpoints directly in the Methods, but in the controller based api's are defined in the attribute routing in the controllers.
- minimal APIs require very little code, which is one of their main advantages.
- Here in this project, we are going to create a minimal API using .Net 9.0. 
- We are going to create a bookstore api
- The article I used to create this project is [here](https://www.freecodecamp.org/news/create-a-minimal-api-in-net-core-handbook/)

**Folder Structure**

    AppContext: Contains the database context and related configurations.
    Configurations: Holds Entity Framework Core configurations and seed data for the database.
    Contracts: Contains Data Transfer Objects (DTOs) used in our application.
    Endpoints: Where we define and configure our minimal API endpoints.
    Exceptions: Contains custom exception classes used in the project.
    Extensions: Holds extension methods that we will use throughout the project.
    Models: Contains business logic models.
    Services: Contains service classes that implement business logic.
    Interfaces: Holds interface definitions used to map our services.
**The best way to learn is by doing it yourself, so I recommend you to follow the article and create the project on your own machine.**
