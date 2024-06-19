# TimeTravelAgency - a project for a fictional travel agency
## Technologies: 
### Backend:
- ASP.NET Core + mvc
- DDD
- EF Core
- MS SQL Server
- Authentication and authorization (Cookie)
- Logging - NLog
- Unit testing - xUnit + Moq
### Frontend:
- Html/Css + Bootstrap
- jQuery/Ajax
### Data:
- Bogus: generating data for a database
- Midjourney AI: generated images
- ChatGpt: generated text

### Database
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/BD.png)

## More about the app
### Application Overview
On the main page there is a window with a parallax effect, introductory information about the "company", as well as links to other sections.

Due to the built-in grid system, Bootstrap design is adaptive for different devices.

All images + tour information are stored in the database as an array of bytes (and strings, respectively).

![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/overview.gif)
### User registration
Any user can view the information, however, to make and pay for an order, you must register.

The password hash is stored in the database(sha256).

![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/registration.gif)

![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/user%20authentication.gif)

### Authentication and authorization
Authentication and authorization occupy an important place in the application.

ASP.NET has built-in support for cookie-based authentication. Upon receipt of a request from a client containing authentication cookies, their validation, deserialization and initialization of the User property of the HttpContext object takes place. The asynchronous context method HttpContext.SignInAsync() is used to set cookies. The ClaimsPrincipal object (representing the user) contains a list of claims: user's Id, Login, and Role.

The key tool for authorization is the [Authorize] attribute, which prevents unauthorized access to certain methods (for example, "cart"). If an anonymous user tries to access such methods, they will be redirected to the Home/Index path.

There are 3 types of users in the application. Each of them has a different level of access to information and application functions:

User:
- Basic information of the site (Home page, Travel Guide, Tours, About us)
- The ability to change your profile data (full name, mail, phone, etc.)
- The ability to add tours to the cart
- The ability to create and pay for an order
  
Moderator:
- The same as the user
- Ability to add/remove/change tours

Administrator:
- The same as the moderator
- Access to all accounts
- Ability to delete/change accounts

In the interface, this is implemented through additional tabs and buttons for the corresponding roles.
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/authorization.gif)

### Access level "Administrator"
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/administrator.gif)

### Validation
All forms check the values specified by the user and display the errors found.
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/validation.gif)

### An example of using the application by a user
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/user%20creates%20order.gif)