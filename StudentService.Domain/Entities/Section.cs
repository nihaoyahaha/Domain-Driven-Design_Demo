using Commons;

namespace StudentService.Domain.Entities;

/// <summary>
/// 班级
/// </summary>
public class Section
{
    /// <summary>
    /// 班级Id
    /// </summary>
    public string SectionId { get; init; }
    public string Name { get; init; }
    public List<Student> Students { get; private set; } = new List<Student>();
    public Grade Grade { get; init; }
    public string GradeId { get; private set; }

    private Section()
    {
        
    }
    public Section(string name, Grade grade)
    {
        Name = name;
        Grade = grade;
        SectionId = HashHelper.ComputeSha256Hash($"{name}{grade.Name}").Substring(0,8);
    }

	/// <summary>
	/// 班级内增加学生
	/// </summary>
	/// <param name="student"></param>
	public void AddStudent(Student student) => Students.Add(student);

	/// <summary>
	/// 班级内删除学生
	/// </summary>
	/// <param name="student"></param>
	public void RemoveStudent(Student student) => Students.Remove(student);

	/// <summary>
	/// 获取该班级内的学生
	/// </summary>
	/// <returns></returns>
	public List<Student> GetStudents() => Students;
}
