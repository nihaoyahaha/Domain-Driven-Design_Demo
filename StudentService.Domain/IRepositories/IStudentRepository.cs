using StudentService.Domain.Entities;

namespace StudentService.Domain;

public interface IStudentRepository
{
    Task<bool> IsExistStudent(string studentID, string sectionName, string gradeName);
    Task<Student?> FindByStudentIdAsync(string studentId);
    Task<List<Student>> FindBySectionAsync(string sectionName,string gradeName);
    Task RemoveStudentAsync(string studentId);
    Task<Grade?> FindGradeByNameAsync(string gradeName);
    Task<Section?> FindSectionByNameAsync(string sectionName,string gradeId);
    Task AddGradeAsync(Grade grade);
    Task AddSectionAsync(Section section);
}
