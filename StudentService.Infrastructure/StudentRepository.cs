using Commons.CustomException;
using Microsoft.EntityFrameworkCore;
using StudentService.Domain;
using StudentService.Domain.Entities;

namespace StudentService.Infrastructure;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _dbCtx;

    public StudentRepository(StudentDbContext dbCtx)
    {
        _dbCtx = dbCtx;
    }

    public async Task AddGradeAsync(Grade grade)
    {
        await _dbCtx.grades.AddAsync(grade);
    }

    public async Task AddSectionAsync(Section section)
    {
        await _dbCtx.sections.AddAsync(section);
    }

    public async Task<List<Student>> FindBySectionAsync(string sectionName, string gradeName)
    {
        Grade? grade = await FindGradeByNameAsync(gradeName);
        if (grade == null) throw new GradeNotFoundException($"不存在{gradeName}!");
        Section? section = await FindSectionByNameAsync(sectionName,grade.GradeId);
        if (section == null) throw new SectionNotFoundException($"{gradeName}{sectionName}不存在!");
        return section.Students;
    }

    public async Task<Student?> FindByStudentIdAsync(string studentId)
    {
        return await _dbCtx.FindAsync<Student>(studentId);
    }

    public async Task<Grade?> FindGradeByNameAsync(string gradeName)
    {
        return await _dbCtx.grades.Include(s=>s.Sections).FirstOrDefaultAsync(x => x.Name == gradeName);
    }

    public async Task<Section?> FindSectionByNameAsync(string sectionName, string gradeId)
    {
        return await _dbCtx.sections.Include(s=>s.Students).FirstOrDefaultAsync(s => s.Name == sectionName && s.GradeId == gradeId);
    }

    public async Task<bool> IsExistStudent(string studentID, string sectionName, string gradeName)
    {
        Grade? grade = await FindGradeByNameAsync(gradeName);
        if (grade == null) throw new GradeNotFoundException($"{gradeName}不存在!");
        Section? section = await FindSectionByNameAsync(sectionName,grade.GradeId);
        if (section == null) throw new SectionNotFoundException("{gradeName}{sectionName}不存在!");
        return await _dbCtx.students.AnyAsync( x => x.StudentId == studentID && x.SectionId == section.SectionId && x.Grade ==grade) ;
    }

    public async Task RemoveStudentAsync(string studentId)
    {
        Student? student =await FindByStudentIdAsync(studentId);
        if(student !=null)
        {
            _dbCtx.students.Remove(student);     
        }    
    }

}
