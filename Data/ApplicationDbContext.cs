using Microsoft.EntityFrameworkCore;
using StudentReportCardAPI.Models;

namespace StudentReportCardAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Exam> Exams => Set<Exam>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<ExamSubject> ExamSubjects => Set<ExamSubject>();
    public DbSet<StudentMark> StudentMarks => Set<StudentMark>();
}
