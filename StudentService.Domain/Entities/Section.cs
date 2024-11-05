
namespace StudentService.Domain.Entities;

public class Section
{
    public int SectionId { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
    
    public int GradeId { get; set; }
	public Grade Grade { get; set; }
	private Section() { }

    public Section(string name) => Name = name;

    public bool IsExistStudentByStudentId(int studentId) => Students.Any(x => x.StudentId == studentId);

    public bool IsExistStudentByStudentName(string studentName) => Students.Any(x => x.Name == studentName);

    public int RemoveStudent(int studentId) => Students.RemoveAll(x=>x.StudentId == studentId);

    public int RemoveStudents(int[] studentIds) => Students.RemoveAll(x=> studentIds.Contains(x.StudentId));

    public List<Student> GetStudents() => Students;

    public Student FindStudentByStudentId(int studentId) => Students.Single(x => x.StudentId == studentId);

    public Student FindStudentByStudentName(string studentName) => Students.Single(x => x.Name == studentName);

    public int StudentsCount() => Students.Count();

    public int StudentsCountByGender(Gender gender) => Students.Count(x => x.Gender == gender);

}
