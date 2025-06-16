namespace StudentReportCardAPI.Models;

public class Exam
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<ExamSubject> Subjects { get; set; }
}
