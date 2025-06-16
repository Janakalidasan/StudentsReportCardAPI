using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentReportCardAPI.Data;

namespace StudentReportCardAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportCardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReportCardController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> GetReportCards([FromBody] List<int> studentIds)
    {
        var result = await _context.Students
            .Where(s => studentIds.Contains(s.Id))
            .Include(s => s.Class)
            .Select(s => new
            {
                StudentId = s.Id,
                StudentName = s.Name,
                ClassName = s.Class.Name,
                SectionName = s.Class.Section,
                AcademicYear = s.Class.AcademicYear,
                Exams = _context.Exams.Select(exam => new
                {
                    ExamId = exam.Id,
                    ExamName = exam.Name,
                    Subjects = _context.ExamSubjects
                        .Where(es => es.ExamId == exam.Id)
                        .Select(es => new
                        {
                            SubjectId = es.Subject.Id,
                            SubjectName = es.Subject.Name,
                            MarksObtained = _context.StudentMarks
                                .Where(sm => sm.StudentId == s.Id && sm.ExamSubjectId == es.Id)
                                .Select(sm => sm.MarksObtained)
                                .FirstOrDefault(),
                            MaxMarks = es.MaxMarks
                        }).ToList(),
                    TotalObtained = _context.StudentMarks
                        .Where(sm => sm.StudentId == s.Id && sm.ExamSubject.ExamId == exam.Id)
                        .Sum(sm => sm.MarksObtained),
                    TotalMax = _context.ExamSubjects
                        .Where(es => es.ExamId == exam.Id)
                        .Sum(es => es.MaxMarks),
                }).ToList()
            })
            .ToListAsync();

        var report = result.Select(student => new
        {
            student.StudentId,
            student.StudentName,
            student.ClassName,
            student.SectionName,
            student.AcademicYear,
            Exams = student.Exams.Select(exam => new
            {
                exam.ExamId,
                exam.ExamName,
                exam.Subjects,
                exam.TotalObtained,
                exam.TotalMax,
                Percentage = exam.TotalMax > 0 ? (exam.TotalObtained * 100) / exam.TotalMax : 0
            }).ToList()
        });

        return Ok(report);
    }
}
