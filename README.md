# RaceDay RESTful API – Part 2

## System Overview
RaceDay is a web-based event management system designed 
for South Africa’s running, walking, and cycling community.
This API powers the platform, handling authentication, role-based access, 
event management, enrollments, and results.

## Roles
### Organiser
Register/login as organiser
Create, edit, delete events
Define categories for events
Capture participant results
View all enrollments for their events

### Participant
Register/login as participant
Browse events and categories
Enroll in events
View personal enrollments and results

## Clone the Repository
git clone <your-github-link>
cd RaceDayAPI

## Database Setup
- Ensure SQL Server is installed and running.
* Update appsettings.json with your connection string:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=RaceDayDB;Trusted_Connection=True;"
}

+ Run migrations:
dotnet ef database update

## Run the API

## Swagger UI
