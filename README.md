# Disclaimer

This project is based on [ASP.NET Boilerplate](https://aspnetboilerplate.com/Pages/Documents).

# Requirements
- NET Core 3.1
- NodeJS v16
- MsSQL
- OpenWeatherMap API key

# Setup
This guide is mainly taken from [here](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular).

How to run backend:
- Open your solution in Visual Studio 2017 v15.3.5+ and build the solution.
- Select the 'Web.Host' project as the startup project.
- Check the connection string in the appsettings.json file of the Web.Host project, change it if you need to.
- Open the Package Manager Console and run an Update-Database command to create your database (ensure that the Default project is selected as .EntityFrameworkCore in the Package Manager Console window).
- Update APIKey value in appsettings.json
- Run the application. It will show swagger-ui if it is successful.

How to run frontend:
- Open a command prompt, navigate to the angular folder which contains the *.sln file and run 'npm install'
- After finished, run 'npm start'
- The web app is accessible from localhost:4200

# Demo
Added a weather checker feature to the web app:

- Add WeatherService including the unit tests in the backend.

The endpoints are /WeatherService/GetLocation and /WeatherService/GetCurrentWeather. This service doesn't need authorization. The location data is still hardcoded. For the weather information it uses Weather API from OpenWeatherMap.

- Add Weather Checker page in the frontend. 

The weather check page URL is localhost:4200/app/weather. This page doesn't need authorization.

