using StudentService.Domain.Entities;

namespace StudentService.Domain;

public class StudentDomainService
{
    private readonly IStudentRepository _repository;

    public StudentDomainService(IStudentRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 新增年级班级
    /// </summary>
    /// <param name="sectionName"></param>
    /// <param name="gradeName"></param>
    /// <returns></returns>
    public async Task AddGradeAndSectionAsync(string sectionName, string gradeName)
    {
        Grade? grade = await _repository.FindGradeByNameAsync(gradeName);
        if (grade == null)
        {
            grade = new Grade(gradeName);
            await _repository.AddGradeAsync(grade);
        }
        Section? section = await _repository.FindSectionByNameAsync(sectionName, grade.GradeId);
        if (section == null)
        {
            section = new Section(sectionName, grade);
            await _repository.AddSectionAsync(section);
        }
        grade.AddSection(section);
    }

    /// <summary>
    /// 班级添加新学生
    /// </summary>
    /// <param name="studentReq"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task SetionAddStudentAsync(AddStudentReq req)
    {
        Grade? grade = await _repository.FindGradeByNameAsync(req.GradeName);
        Section? section = await _repository.FindSectionByNameAsync(req.SectionName, grade.GradeId);
        Student student = new Student(req.StudentName, req.Birthday, section, grade);
        section.AddStudent(student);
    }

    /// <summary>
    /// 学生是否在班级中
    /// </summary>
    /// <param name="studentName"></param>
    /// <param name="sectionName"></param>
    /// <param name="gradeName"></param>
    /// <returns></returns>
    public async Task<bool> SectionExistStudentAsync(QueryStudentReq req)
    {
        return await _repository.IsExistStudent(req.StudentId, req.SectionName, req.GradeName);
    }

    /// <summary>
    /// 学生更换班级
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="sectionName"></param>
    /// <param name="gradeName"></param>
    /// <returns></returns>
    public async Task StudentChangeSectionAsync(ChangeSectionReq req)
    {
        Student student = await FindStudentByIdAsync(req.StudentId);
        Grade grade = await FindGradeByNameAsync(req.GradeName);
        Section section = grade.FindSectionByName(req.SectionName);
        student.ChangeSection(section);
    }

    /// <summary>
    /// 学生升年级
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="gradeName"></param>
    /// <returns></returns>
    public async Task StudentUpGrade(string studentId, string sectionName, string gradeName)
    {
        Student student = await FindStudentByIdAsync(studentId);
        Grade grade = await FindGradeByNameAsync(gradeName);
        Section section = grade.FindSectionByName(sectionName);
        student.ChangeSection(section);
        student.ChangeGrade(grade);
    }

    /// <summary>
    /// 根据学号查找学生
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task<Student> FindStudentByIdAsync(string studentId) => await _repository.FindByStudentIdAsync(studentId);

    /// <summary>
    /// 根据名称查找年级
    /// </summary>
    /// <param name="gradeName"></param>
    /// <returns></returns>
    public async Task<Grade> FindGradeByNameAsync(string gradeName) => await _repository.FindGradeByNameAsync(gradeName);

}
