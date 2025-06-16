# 🎓 StudentReportCardAPI

This is an ASP.NET Core Web API project that provides detailed report cards for students using Entity Framework Core and LINQ. You can retrieve full academic performance for students by submitting a list of their IDs.

---

## ✅ Features

- Returns report cards for multiple students by IDs.
- Each report card includes:
  - Student Name
  - Class Name
  - Section Name
  - Academic Year
  - Exams (with details):
    - Exam ID and Name
    - Subjects with:
      - Subject ID and Name
      - Marks Obtained
      - Maximum Marks
    - Total Marks and Percentage per Exam

---

## 🛠️ Technologies Used

- ASP.NET Core Web API
- Entity Framework Core (In-Memory Provider)
- LINQ
- Swagger for API documentation
- .NET 7 / .NET 8 / .NET 9 Compatible

---

## 🚀 Getting Started

### Step 1: Clone the Repository

```bash
git clone https://github.com/your-username/StudentReportCardAPI.git
cd StudentReportCardAPI


### Step 2: Restore & Build

'''bash
dotnet restore
dotnet build

### Step 3: Run the API
dotnet run

### Visit the API UI here:

http://localhost:5204/swagger

