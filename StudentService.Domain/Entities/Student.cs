
namespace StudentService.Domain.Entities;

public class Student
{
    public int StudentId{ get; set;}

    public string Name { get; set; }
    
    public DateTime Birthday { get; set; }

    public Gender Gender { get; set; }

	public Section Section { get; set; }
    
    public int SectionId{ get; set;}
    
    private Student() { }

    public Student(string name, DateTime birthday,Gender gender )
    {
        Name = name;
        Birthday = birthday;
        Gender = gender;
        
    }

    public void ChangeSection(int sectionId) => SectionId = sectionId;

}
