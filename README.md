# Clean-Architecture-WebAPI

This is a WebAPI, implemented with Clean Architecture pattern. The code is very clean and it is very manageable. 
You can add your own models, validations, etc, and you can actually create MVC project on top of it, or just 
use it as a api, deployed locally or publish on some server and use some front-end framework to show the data.

There are some steps that you have to do, to get working on you computer:

1.  Change the connection string - In the WebAPI project in the appsettings.json file enter your server name 
    and name of the database(I was working on SQL Server 2014 Management Studio).

2.  Go into the Package Manager Console and type: add-migration !IMPORTANT - Make sure that your WebAPI is Set as Startup Project, 
    and in the Package Manager Console your Default project is CleanArchitectureWebAPI.Infrastructure.Data selected.
    - this will create folder Migration in CleanArchitectureWebAPI.Infrastructure.Data with the migrations. 
      Then it will show all the tables in your database that are comming with the IdentityDbContext, plus the models in the Domain folder.
3.  When this is done just type in the Package Manager Console: update-database.
    - This will create the database with you database name with all the AspNet tables and the models.
    
    Because the API works with roles "Admin" and "User", you have to go first to create roles "Admin" and "User" in dbo.AspNetRoles 
    with some number Id. (because "User" is with Id 2, make "Admin" with Id 1) Then you have to get registered and automatically this user will 
    have the role "User". !IMPORTANT - just make sure that the username is all in lower case. To change this you have to go in dbo.AspNetUserRoles 
    and notice that the RoleId is already 2 for that one user. Just change it manually to 1 and press TAB. This user, from then on will be with the role of "Admin". 
    Every other registered user in the future will be with the role "User". To change it to "Admin" just do the previous step. 
    (in the project there is no request that is authorized by "User", so you can use this:
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")] <<<, below some other Http request).

This WebAPI uses JSON Web Token (JWT) for Authorization and Information Exchange. T
his WebAPI also uses swagger, so you can test it. Just write in the URL insted of {url}/api/soaps, or {url}/api/balms, just write {url}/swagger

Then just run the project on F5 and this will build the project and it will seed the database with some data, just something to work with. (maybe you have to close the SQL Server 2014 Management Studio and start it again)
So the project is very useful because it show how to implement Clean Architecture Design, Domain-Driven Design, Separation of Concerns, Basis of OOP, Validation and User Authentication and Authorization, Token, AutoMapper, Inversion Of Control, SOLID Principles and more.
