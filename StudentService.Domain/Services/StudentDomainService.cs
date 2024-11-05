using Commons.CustomException;
using StudentService.Domain.Entities;
using StudentService.Domain.RequestDtos;

namespace StudentService.Domain;

public class StudentDomainService
{
    private readonly IStudentRepository _repository;

    public StudentDomainService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task InitGradeAndSection()
    {
        for (int i = 1; i <= 6; i++)
        {
            Grade grade = new($"{i}年级") ;
            for (int j = 1; j <= 4; j++)
            {
                Section section = new($"{j}班");
                grade.Sections.Add( section );
            }
            if (!await IsExistGradeByGradeNameAsync(grade.Name)) 
            {
				await _repository.AddGradeAsync(grade);
			}
        }
    }

    public async Task<bool> IsExistGradeByGradeNameAsync(string gradeName) => 
        await _repository.IsExistGradeByGradeNameAsync(gradeName);

    public async Task<List<Grade>> FindGradesAsync() => await _repository.FindAllGradesAndSectionsAsync();

    public async Task<dynamic> FindGradeByGradeNameAsync(string gradeName) => await _repository.FindGradeByGradeNameAsync(gradeName);

    public async Task AddStudentAsync(InsertStudentsDto dto)
    {
        if (dto.Students == null)
            throw new ArgumentNullException("学生不可为空!");
        if (await IsExistGradeByGradeNameAsync(dto.GradeName) == false)
            throw new GradeNotFoundException($"{dto.GradeName}不存在!");
        if (await _repository.IsExistSectionByGradeNameAndSectionNameAsync(dto.GradeName, dto.SectionName) == false)
            throw new SectionNotFoundException($"{dto.GradeName}-{dto.SectionName}不存在!");
        
        Grade grade = await _repository.FindGradeByGradeNameAsync(dto.GradeName);
        Section section = await _repository.FindSectionByGradeNameAndSectionNameAsync(grade.GradeId, dto.SectionName);

        dto.Students.ForEach(x => {
            Student student = new(x.Name,x.Birthday,x.Gender);
            section.Students.Add(student);
		});
        _repository.DetectChanges();
	}

    private async Task Select(Grade grade)
    { 
       
    }

}

/*
 {
  "gradeName": "1年级",
  "sectionName": "1班",
  "students": [
    {
      "name": "string",
      "birthday": "2024-11-02",
      "gender": 0
    }
  ]
}
 */
