namespace StudentReportCardAPI.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int ClassId { get; set; }
    public Class Class { get; set; }
}
