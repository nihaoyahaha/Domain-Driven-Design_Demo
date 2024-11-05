using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentService.Domain.Entities;

namespace StudentService.Domain;

public interface IStudentRepository
{
	Task AddGradeAsync(Grade grade);
	Task<List<Grade>> FindAllGradesAndSectionsAsync();

	Task<bool> IsExistGradeByGradeNameAsync(string gradeName);

	Task<bool> IsExistSectionByGradeNameAndSectionNameAsync(string gradeName,string sectionName);

	Task<dynamic> FindGradeByGradeNameAsync(string Name);

	Task<Section> FindSectionByGradeNameAndSectionNameAsync(int gradeId, string sectionName);
	void DetectChanges();

}
