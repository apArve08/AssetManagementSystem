 IT Asset Management Tool - Complete Setup Instructions

- .NET 6.0 or later SDK
- Node.js 16.x or later
- PostgreSQL database
- VS Code

Backend Setup

1. Create the Backend Project

bash
Create project directory
mkdir ITAssetManagement
cd ITAssetManagement

Create .NET Web API project
dotnet new webapi -n ITAssetManagement.API
cd ITAssetManagement.API

Add required packages
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package ClosedXML


2. Copy Backend Files

Copy all the backend files provided above into their respective folders:

- Program.cs
- appsettings.json (update the connection string with your PostgreSQL details)
- Models/ folder with all model files
- Data/AppDbContext.cs
- Services/ folder with all service files
- Controllers/ folder with all controller files

3. Database Setup

bash
Create and apply migrations
dotnet ef migrations add InitialCreate
dotnet ef database update


4. Run the Backend

bash
dotnet run


The API will start at https://localhost:7001 (or check the console for the actual port)

Frontend Setup

1. Create the Frontend Project

bash
Go back to main directory
cd ../..

Create Nuxt 3 project
npx nuxi@latest init ITAssetManagement.Frontend
cd ITAssetManagement.Frontend

Install dependencies
npm install

Install additional packages
npm install @pinia/nuxt pinia @nuxtjs/tailwindcss dayjs chart.js vue-chartjs axios vue3-toastify


2. Copy Frontend Files

Copy all the frontend files provided above into their respective folders:

- nuxt.config.ts
- app.vue
- stores/auth.js
- plugins/api.js
- middleware/auth.js
- layouts/default.vue
- components/ folder with all component files
- pages/ folder with all page files

3. Update API URL

In nuxt.config.ts, update the apiBase to match your backend URL:

typescript
runtimeConfig: {
  public: {
    apiBase: 'https://localhost:7001/api' // Update this
  }
}


4. Run the Frontend

bash
npm run dev


The frontend will start at http://localhost:3000

Usage

Default Login Credentials

- Admin: Username: admin, Password: admin123
- User: Username: user, Password: user123

Features Available

1. Dashboard - View asset statistics and charts
1. Asset Management - CRUD operations on assets
1. Search - Search assets by various fields
1. Export - Export asset list to Excel
1. Activity Log - Track all asset-related activities
1. Role-based Access - Admin can delete assets, users have read/update access

Troubleshooting

Common Issues

1. CORS Issues
- Make sure the backend has CORS configured properly in Program.cs
- The frontend should be running on http://localhost:3000
1. Database Connection
- Verify PostgreSQL is running
- Check connection string in appsettings.json
- Ensure database user has proper permissions
1. Authentication Issues
- Check if JWT token is being sent in requests
- Verify JWT configuration in appsettings.json
1. Port Conflicts
- Backend: Change port in Properties/launchSettings.json
- Frontend: Change port with npm run dev -- --port 3001

Project Structure Summary

Backend

- Models: Asset, User, ActivityLog, DTOs
- Controllers: Auth, Assets, Export
- Services: AuthService, ExportService
- Db: Entity Framework Core with PostgreSQL

Frontend

- Pages: Login, Dashboard, Assets (list/detail)
- Components: NavigationBar, AssetList, AssetForm, DashboardCharts
- State Management: Pinia (auth store)
- API client: Axios with interceptors
- UI: Tailwind CSS

Security Notes

- In production, hash passwords properly (use BCrypt or similar)
- Use environment variables for sensitive configuration
- Implement proper SSL/TLS certificates
- Add rate limiting and request validation
- Implement proper logging and monitoring
