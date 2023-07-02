### dotnet-rideShare REST API

## Overview

This project is crated to created for people to find passengers for their journeys.
When a user need to travel with his vehicle from one city to another, He may create a journey and other user can find and join the travel.

Application City identification has been made for 1000 to 500 rectangle map. First Id is upper left and increased.

## Features

- Secure user authentication system for registering, logging in, and updating user information.
- Users can add travels by departure date and location,destination location, passenger count and description details.
- Users also can disable his travels with notifying existing passengers.
- Passengers can search available travels.
- Passenger count to the travels are controlled by application.
- Available travels not just searched by city to city search if their path on other travels search results will show them anyway.

## Installation

1. Clone the repository to your local machine.
2. Install dependencies using `dotnet restore`.
3. Add `appsettings.json` and fill in the required settings.
4. Build project with `dotnet build`.
5. Start the server using `dotnet run`.
6. Open `http://localhost:7182` to view it in the browser.

## Usage

1. Register or login with the authentication system.
2. Create a travel with departure and destination place with a date.
3. There are two search one for point to point other looks alternative paths within the path.
4. Passenger can add multiple user on the travel.
5. Passenger and driver can cancel trip. (A notification will be sended.)

## Contributing

We welcome contributions from the open-source community. If you find any issues or bugs, please create an issue or pull request. Make sure to follow code standards and include useful tests.

## Resources

You may want to familiarize yourself with the following technologies/libraries:

- [dotnet 6.0](https://dotnet.microsoft.com/en-us/)
- [MySQL](https://www.mysql.com/)

## Feedback

If you have any feedback about the project, please let me know. I am always looking for ways to improve the user experience.
