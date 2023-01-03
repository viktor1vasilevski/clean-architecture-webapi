# Clean Architecture ASP.NET Core Web API 3.1

## Project Use Case

This is a WebAPI Project, implemented with Clean Architecture pattern. The code is very clean and it is very manageable. 
You can add your own models, validations, logic, etc. You can also create MVC project on top of it, or just 
use it as a api, deployed locally or publish on some server and use some front-end framework to show the data.

## Installation

1.  Download the code - Clone the repository or download the zip file

2.  Change the connection string - In the WebAPI project in the ```appsettings.json``` file enter your server name 
    and name of the database(I was working on SQL Server 2014 Management Studio).

3.  Go into the Package Manager Console and type: ```Add-Migration "Init```"
    <br />
    <br />
    ***!IMPORTANT - Make sure that your WebAPI is Set as Startup Project, and in the Package Manager Console your Default project is                                      CleanArchitectureWebAPI.Infrastructure.Data selected.***
    - this will create folder Migrations in CleanArchitectureWebAPI.Infrastructure.Data with the migration. 
      
4.  When this is done, just type in the Package Manager Console: ```Update-Database```
    - this will create the database with you database name with all the ASP.NET Identity tables and the models.
    
5.  Run the project
    - this will build the project and it will seed the database with some data, just something to work with. 
      (maybe you have to close the SQL Server 2014 Management Studio and start it again)
    - in the WebAPI project it will also be created folder Log with two files. One .json file and other .txt.
      These are for detail logging information.
    
6.  Next step is that you have to create "Admin" to take full experience of the API. 
    - because the API works with roles "Admin" and "User", you have to go first to create roles "Admin" and "User" in dbo.AspNetRoles.
    - you can use this set of commands in SQL Server 2014 Management Studio to create the roles "Admin" and "User".

    ```sql
    USE [yourDatabaseName]
    GO
    INSERT INTO dbo.AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp)
    VALUES (1, 'Admin', 'ADMIN', null)
    INSERT INTO dbo.AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp)
    VALUES (2, 'User', 'USER', null)
    ```
    
7.  Next step is to register some user <br />
    - ***from this point on, your api/project should be running all the time during the register, login and CRUD operations***<br />
    - ***I hosted the api/project locally, on my computer***
    - you can use Postman or Fiddler for registration, or some other program, and provide this json format in the body with your own values.
      (I used Postman)
    - The method is ```Post``` and the url will be ```localhost:port/api/account/register```<br />
      
    ```json
    {
       "Username": "YourUsername",
       "Email": "YourEmail@yahoo.com",
       "Password": "YourPassword@123",
       "ConfirmPassword": "YourPassword@123"
    }
     ```
    - after you get registered, automatically, this user will have the role "User". 
       Go in the SQL Server 2014 Management Studio, your database, table AspNetUserRoles and notice that the RoleId is already 2 for that one user. 
       Just edit the table and change it manually to 1 and press TAB,
       or write this command in SQL
       
    ```sql
    USE [yourDatabaseName]
    GO
    UPDATE [dbo].[AspNetUserRoles] SET RoleId = 1 
    WHERE UserId = 'userId' --here enter the user ID
    ```   
    This user, from then on will be with role of "Admin".

8.  Next step is to Sign In some user
    - url: ```localhost/port/api/account/signin```, and this method is also ```Post``` and you should provide this json format into the body
    ```json
    {
        "Username": "YourUsername",
        "Password": "YourPassword@123"
    }
      ```
    the logging user will be successful and you will recive a JWT Token,<br />
    which you must use for Authorization with the prefix Bearer : YourToken
    ```
    In Postman just select the Auth, and from the dropdown menu select Bearer Token
    ```
    
    Now the commands Post, Create, Edit and Delete will be available, otherwise you will get the:
    ```js 
    401 Unauthorized
    ```
    *(for Get and GetById you dont need no Authorization)*
    <br />
    <br />
    Every other registered user in the future will be with the role "User". To change it to "Admin" just do the step 7, part 5. 
    (in the project there is no request that is authorized by "User", so you can use this below some other Http request).<br />
    ```C#
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    ``` 
## Principle, Patterns and external libraries used.

1. Clean (Onion) Architecture Project with Domain Driven Design and SOLID principles.
2. ASP.NET Core Dependency Injection
3. Repository Pattern
4. Unit Of Work Pattern
5. Automapper
6. Logging with Serilog
7. In Memory Caching
8. Response Caching
9. Swagger
10. JWT Token
11. Unit Testing with Moq

## ToDO

1. There is no business logic, since this is only a concept.
2. The enities don't have foreign keys or any common table

## Notes
1. This project is build somewhere in the beginning of 2021 and I was just starting to apply for a job. Now, that I changed few comanies and I gained experience working on real projects, and think about the architecure of the project, I can see that the database, or the models, are now well structured. 
Because the api is build for a kind of web shop page, ultimatly what you want is to by a product. Well, now as the architecture is build, it is possible but the solution is not good. Say you have build a front-end project with Angular and you have cart section, and in that cart you have one soap and one balm as a products.
Now the next step is to have separate controller in the back-end that handles this logic. In this controller you have to have all the controllers because the you don't know the user what it will pick, soap, balm or oil. And for every item, you have to check in each controller if that id is in that table of the database. Because on the back-end, as the user checkouts, the user want to buy two items, and you will get two ids, nothing more. Those ids don't tell what is it. Is it a soap or balm. And image if you have hundred products. For instance hand soaps. scissors, shampoos and what not, the project will be difficult to manage and in this controller where the buying is made, you will end up with hundred servers, or instances from the tables of the database, and when the user wants to by just one product, you have to search in all the tables just to find that one product which the user wants it to buy.

   The solution for this problem is to have one table, named Products, and in this table you will have the columns like, Name, Brand, Edition, Unit Price and so on.
Or even better you will have another table Categories, and in that table you will save the categories like, beard category, hair category, hands category, and in this
Products table you will have CategoryId, respectively for every product. This way all the products are in one table and your search will be only in this table, that's it. This project can be even more improved, for instance you can write the logic for creating generic unit of work and generic repositories, you can have onther table Product Details where you will store the details for each product, you can insert column in Product table named ProductDetailsJson where all the details from the product you will store as a json file and on the front end you will handle this object, and so on. Hope this helps a little bit.

2. The solution folder Domian, should be named Domain. And also the name of the Class Library from that folder should be named CleanArchitectureWebAPI.Dom**ai**n, not CleanArchitectureWebAPI.Dom**ia**n.

