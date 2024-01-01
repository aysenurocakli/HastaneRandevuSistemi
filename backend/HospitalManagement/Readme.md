# Hospital Appointment System

This project is a Hospital Appointment System developed using ASP.NET Core 6 MVC, C#, SQL Server, and Entity Framework Core ORM. The system allows users to register, login, and make appointments with doctors.

## Project Structure

The project is structured into several key components:

- `Models`: This is where the data models are defined. For example, the [User](file:///Users/ozmekik/Documents/test/aysenur/Models/User.cs#1%2C14-1%2C14) model is defined in `Models/User.cs`.
- `Data`: This is where the database context is defined. The [ApplicationDbContext](file:///Users/ozmekik/Documents/test/aysenur/Data/ApplicationDbContext.cs#1%2C14-1%2C14) class in `Data/ApplicationDbContext.cs` is responsible for interacting with the database.
- `Controllers`: This is where the application logic is defined. For example, the [UserController](file:///Users/ozmekik/Documents/test/aysenur/Controllers/UserController.cs#3%2C14-3%2C14) in `Controllers/UserController.cs` handles user registration and login.
- `Startup.cs`: This is where the application services are configured.
- `appsettings.json`: This is where the application settings, such as the database connection string and JWT secret key, are stored.

## Key Features

- **User Registration**: Users can register for an account. The password is hashed using MD5 before being stored in the database. The user's role is set to "PATIENT" by default. See `Controllers/UserController.cs` for the implementation.
- **User Login**: Users can login with their username and password. Upon successful login, a JWT token is generated and returned to the user. See `Controllers/UserController.cs` for the implementation.

## Setup

1. Clone the repository.
2. Update the [DefaultConnection](file:///Users/ozmekik/Documents/test/aysenur/Startup.cs#4%2C65-4%2C65) string in `appsettings.json` with your SQL Server connection details.
3. Run the application.

## Future Work

This is a basic implementation of a Hospital Appointment System. Future work could include:

- Implementing the appointment booking functionality.
- Adding more user roles (e.g., ADMIN, DOCTOR).
- Implementing the admin panel.
- Adding multilingual support.
- Implementing the API service using LINQ.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)