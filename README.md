Project Setup Guide

Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js and npm](https://nodejs.org/en/download/)
- [Angular CLI](https://angular.io/cli)
- [Visual Studio Code](https://code.visualstudio.com/)

Setting up the API

1. Clone the Repository in Visual Studio

3. Build the Solution
   
5. Run the Api
   - Make sure to run with https
   - Ensure the API is running on `https://localhost:7076`.

Setting up the Frontend

1. Clone the Repository in VS Code

2. Open the Frontend in VS Code
   - Open VS Code.
   - Open the repository in VS Code

3. Install Dependencies
   Open the terminal in Visual Studio Code and run: npm install

4. Run the Angular Development Server
   - In the terminal now run: ng serve
   - The frontend should now be running on `http://localhost:4200`.

Accessing the Application

1. Open your browser and navigate to `http://localhost:4200`.
2. The frontend should be able to communicate with the API running on `https://localhost:7076`.

********************

The project is unfortunately not finished completely and some requirements are missing.

The current solution only has the ability to create a shipment, add bags to it (either letter or parcel) and if parcel bag then add parcels to it. Also there is the ability to delete a shipment that deletes all the bags and parcels connected to it.

(To add parcels to a bag, first save the bag and then the ability to add parcels will appear)
