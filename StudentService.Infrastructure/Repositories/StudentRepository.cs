using Commons.CustomException;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentService.Domain;
using StudentService.Domain.Entities;
using System.Xml;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace StudentService.Infrastructure;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _dbCtx;

	public StudentRepository(StudentDbContext dbCtx) => _dbCtx = dbCtx;

	public async Task AddGradeAsync(Grade grade) => await _dbCtx.grades.AddAsync(grade);

	public async Task<List<Grade>> FindAllGradesAndSectionsAsync() => 
		await _dbCtx.grades.Include(x => x.Sections).ThenInclude(x=>x.Students). ToListAsync();

	public async Task<bool> IsExistGradeByGradeNameAsync(string gradeName) => 
		await _dbCtx.grades.AnyAsync(g=>g.Name == gradeName);

	public async Task<bool> IsExistSectionByGradeNameAndSectionNameAsync(string gradeName, string sectionName)
	{
		var grade = await _dbCtx.grades.SingleAsync(g => g.Name == gradeName);
		return await _dbCtx.sections.AnyAsync(section => section.GradeId == grade.GradeId && section.Name == sectionName);
	}

	public async Task<dynamic> FindGradeByGradeNameAsync(string Name) =>
		await _dbCtx.grades
		.Include(x=>x.Sections)
		.ThenInclude(x => x.Students)
		.Select(x => new { 
		    x.Name,
			Sections = x.Sections.Select(s=> new { 
				s.Name,
				Students = s.Students.Select(stu => new {
					stu.Name,
					stu.Birthday,
					Gender = stu.Gender == Gender.male ? "’j" : "—",
				})
			})})
		.Select(x=> new { x.Name, x.Sections })
		.SingleAsync(x=>x.Name == Name);

	public async Task<Domain.Entities.Section> FindSectionByGradeNameAndSectionNameAsync(int gradeId, string sectionName) =>
		await _dbCtx.sections.Include(x => x.Students).SingleAsync(x => x.GradeId == gradeId && x.Name == sectionName);

	public void DetectChanges() => _dbCtx.ChangeTracker.DetectChanges();

}
