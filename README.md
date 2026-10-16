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

## Setup Instructions
### 1. Clone the Repository
''' git clone <your-github-link>
cd RaceDayAPI '''

### 2. Database Setup
- Ensure SQL Server is installed and running.
* Update appsettings.json with your connection string:
''' "ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=RaceDayDB;Trusted_Connection=True;"
} '''

### 3. Run the API

### 4. Swagger UI

## Authentication & Role-Based Access
- Passwords are hashed using BCrypt.
- JWT tokens issued on login include role claims.
- Role enforcement:
  - [Authorize(Roles="Organiser")] → Organiser-only endpoints.
  - [Authorize(Roles="Participant")] → Participant-only endpoints.
- Session maintained via JWT validation on each request.

  ## Implemented Endpoints
- Auth
  - POST /api/auth/register → Register new user
  - POST /api/auth/login → Login and receive JWT
 
- Users
  - GET /api/users/{id} → View profile
  - PUT /api/users/{id} → Update profile
 
- Events
  - POST /api/events → Create event (Organiser)
  - PUT /api/events/{id} → Edit event (Organiser)
  - DELETE /api/events/{id} → Delete event (Organiser)
  - GET /api/events → View events (All)
 
- Categories
  - POST /api/categories → Create category (Organiser)
  - GET /api/categories/{eventId} → View categories (All)
 
- Enrollments
  - POST /api/enrollments → Enroll in event (Participant)
  - GET /api/enrollments/{eventId} → View enrollments (Organiser)
 
- Results
  - POST /api/results → Capture result (Organiser)
  - GET /api/results/{userId} → View personal results (Participant)
 
## Unit Testing
- Framework: ** xUnit **
- Coverage:
  - Registration & login (success + failure).
  - Role enforcement (participant blocked from organiser endpoints).
  - Event CRUD (organiser only).
  - Enrollments (participant only).
  - Results (organiser captures, participant views).
- All tests pass locally and in CI/CD.

## CI/CD
- GitHub Actions workflow builds project and runs tests on every push.
- Workflow file: ''' .github/workflows/dotnet.yml '''

## Video Presentation
Unlisted YouTube link: https://youtu.be/your-video-link  
The video demonstrates:
- Running API in Visual Studio.
- Swagger UI with endpoints.
- Authentication and role enforcement.
- Unit tests executed live.
- Code structure explained.

