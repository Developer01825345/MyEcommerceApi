# MyEcommerceApi
This is just a simple web API using repository pattern in .Net. 

✅ Project Architecture:

MyEcommerceApi.Api (Web API project - Presentation Layer)
	-> Controllers
		-> ProductController.cs
	-> Program.cs
	-> appsettings.json
	
MyEcommerceApi.Domain (Domain Models, DTOs, Interfaces)
	-> Models
		-> Domain
			-> Product.cs
		-> Dto
			-> CreateProductDto.cs
	-> Interfaces
		-> IProductRepository.cs
		-> IProductService.cs
	
MyEcommerceApi.Infrastructure (Database, Repositories, Services)
	-> Repositories
		-> ProductRepository.cs
	-> Services
		-> ProductService.cs
	-> Migrations
	
MyEcommerceApi.Tests (Unit Tests - May be later, once I learn it)

========================================================================================================================================================================

✅ Architecture Breakdown:
1. MyEcommerceApi.Api (Presentation Layer)
	Controllers: This folder holds the Web API controllers, like ProductController.cs etc., which will handle HTTP requests and interact with the service layer.

	Program.cs: Where you configure services and middleware.

	appsettings.json: For storing app-level configurations, like database connection strings or other settings.

2. MyEcommerceApi.Domain (Domain Layer)
	Models/Domain: Contains the core domain models, such as Product.cs, which represents the business entities.

	Models/Dto: The DTOs (Data Transfer Objects) like CreateProductDto.cs help structure the data that will be transferred between layers (e.g., controller ↔ service).

	Interfaces: The interfaces (IProductRepository.cs, IProductService.cs) that define the operations for the repository and service layers. This follows Dependency Inversion Principle.

3. MyEcommerceApi.Infrastructure (Infrastructure Layer)
	Repositories: Contains the implementation of the repository pattern, such as ProductRepository.cs etc., which handles data access (EF Core).

	Services: Contains the service layer (ProductService.cs), where business logic and rules are applied.

	Migrations: For handling database migrations (if using EF Core).

4. MyEcommerceApi.Tests (Unit Tests)
	@ToDo: May be later

✅ Why This Architecture Works:
	Separation of Concerns: Each layer has its responsibility — controllers, services, repositories, domain models — which makes the app maintainable and testable.

	Scalability: As you add new features, you can extend each layer without modifying existing code. For instance, if you want to add more business logic, you can add more services.

	Testability: With interfaces and a clear separation, we can easily write unit tests for controllers, services, and repositories. 
				 In future, I will mock services in the controllers.

	Dependency Injection (DI): This pattern makes it easy to inject dependencies into classes, which is a common practice in modern .NET development.
