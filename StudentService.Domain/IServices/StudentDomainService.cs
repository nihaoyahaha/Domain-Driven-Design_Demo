using Commons.CustomException;
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
    public async Task AddGradeAndSectionAsync(AddSectionReq req)
    {
        Grade? grade = await _repository.FindGradeByNameAsync(req.GradeName);
        if (grade == null)
        {
            grade = new Grade(req.GradeName);
            await _repository.AddGradeAsync(grade);
        }
        Section? section = await _repository.FindSectionByNameAsync(req.SectionName, grade.GradeId);
        if (section == null)
        {
            section = new Section(req.SectionName, grade);
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
        if (grade == null) throw new GradeNotFoundException($"年级:{req.GradeName}不存在!");
        Section? section = await _repository.FindSectionByNameAsync(req.SectionName, grade.GradeId);
        if(section == null)  throw new SectionNotFoundException($"不存在{req.GradeName}{req.SectionName}!");
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
        Student? student = await FindStudentByIdAsync(req.StudentId);
        if (student == null) throw new StudentNotFoundException($"找不到学号为:{req.StudentId}的学生!");
        Grade? grade = await FindGradeByNameAsync(req.GradeName);
        if(grade == null) throw new GradeNotFoundException($"{req.GradeName}不存在!");
        Section? section = grade.FindSectionByName(req.SectionName);
        if (section == null) throw new SectionNotFoundException($"{req.GradeName}{req.SectionName}不存在!");
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
        Student? student = await FindStudentByIdAsync(studentId);
        if (student == null) throw new StudentNotFoundException($"找不到学号为:{studentId}的学生!");
        Grade? grade = await FindGradeByNameAsync(gradeName);
        if(grade == null) throw new GradeNotFoundException($"{gradeName}不存在!");
        Section? section = grade.FindSectionByName(sectionName);
        if (section == null) throw new SectionNotFoundException($"{gradeName}{sectionName}不存在!");
        student.ChangeSection(section);
        student.ChangeGrade(grade);
    }

    /// <summary>
    /// 根据学号查找学生
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task<Student?> FindStudentByIdAsync(string studentId) => await _repository.FindByStudentIdAsync(studentId);

    /// <summary>
    /// 根据名称查找年级
    /// </summary>
    /// <param name="gradeName"></param>
    /// <returns></returns>
    public async Task<Grade?> FindGradeByNameAsync(string gradeName) => await _repository.FindGradeByNameAsync(gradeName);

}
