using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudentReportCardAPI.Data;
using StudentReportCardAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("StudentDB"));

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student Report Card API",
        Version = "v1"
    });
});

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var class1 = new Class { Id = 1, Name = "10", Section = "A", AcademicYear = "2024-2025" };
    var student1 = new Student { Id = 1, Name = "John Doe", Class = class1 };
    var student2 = new Student { Id = 2, Name = "Jane Smith", Class = class1 };

    var math = new Subject { Id = 1, Name = "Math" };
    var sci = new Subject { Id = 2, Name = "Science" };

    var exam1 = new Exam { Id = 1, Name = "Midterm" };
    var exam2 = new Exam { Id = 2, Name = "Final" };

    var es1 = new ExamSubject { Id = 1, Exam = exam1, Subject = math, MaxMarks = 100 };
    var es2 = new ExamSubject { Id = 2, Exam = exam1, Subject = sci, MaxMarks = 100 };
    var es3 = new ExamSubject { Id = 3, Exam = exam2, Subject = math, MaxMarks = 100 };
    var es4 = new ExamSubject { Id = 4, Exam = exam2, Subject = sci, MaxMarks = 100 };

    var marks = new List<StudentMark>
    {
        new() { Student = student1, ExamSubject = es1, MarksObtained = 78 },
        new() { Student = student1, ExamSubject = es2, MarksObtained = 88 },
        new() { Student = student1, ExamSubject = es3, MarksObtained = 90 },
        new() { Student = student1, ExamSubject = es4, MarksObtained = 85 },

        new() { Student = student2, ExamSubject = es1, MarksObtained = 67 },
        new() { Student = student2, ExamSubject = es2, MarksObtained = 75 },
        new() { Student = student2, ExamSubject = es3, MarksObtained = 80 },
        new() { Student = student2, ExamSubject = es4, MarksObtained = 82 },
    };

    db.Classes.Add(class1);
    db.Students.AddRange(student1, student2);
    db.Subjects.AddRange(math, sci);
    db.Exams.AddRange(exam1, exam2);
    db.ExamSubjects.AddRange(es1, es2, es3, es4);
    db.StudentMarks.AddRange(marks);
    db.SaveChanges();
}

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Report Card API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
