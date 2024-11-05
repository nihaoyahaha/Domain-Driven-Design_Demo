
namespace StudentService.Domain.Entities;

public class Grade
{
    public int GradeId{ get; set;}

    public string Name { get; set; }

	public List<Section> Sections { get; set; } = new List<Section>();

    private Grade() { }

    public Grade(string name) => Name = name;

    public void AddSection(Section section) => Sections.Add(section); 

    public void AddSections(List<Section> sections) => Sections.AddRange(sections);
    
    public void RemoveSection(int sectionId) => Sections.RemoveAll(x=>x.SectionId == sectionId);

    public void RemoveSections(int[] sectionIds) => Sections.RemoveAll(x=> sectionIds.Contains(x.SectionId));

    public Section FindSectionById(int sectionId) => Sections.Single(s=> s.SectionId == sectionId);

    public List<Section> GetSections() => Sections;

    public bool IsExistSectionBySectionId(int sectionId) => Sections.Any(x => x.SectionId == sectionId);

    public bool IsExistSectionBySectionName(string sectionName) => Sections.Any(x => x.Name == sectionName);

    public bool IsExistStudentBySectionIdAndStudentId(int sectionId, int studentId) =>
        Sections.Single(x => x.SectionId == sectionId).IsExistStudentByStudentId(studentId);

	public bool IsExistStudentBySectionIdAndStudentName(int sectionId, string studentName) =>
	    Sections.Single(x => x.SectionId == sectionId).IsExistStudentByStudentName(studentName);

	public List<Student>? GetStudentsBySectionId(int sectionId) => 
        Sections.Single(x => x.SectionId == sectionId).Students;

	public int StudentCountBySectionId(int sectionId) => 
        Sections.Single(x=>x.SectionId == sectionId).Students.Count();

    public int StudentsCount() =>
        Sections.SelectMany(x => x.Students).Count();

}
