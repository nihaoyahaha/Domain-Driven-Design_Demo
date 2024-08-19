using Commons;

namespace StudentService.Domain.Entities;

/// <summary>
/// 年级
/// </summary>
public class Grade
{
    /// <summary>
    /// 年级编号
    /// </summary>
    public string GradeId{get;init;}

    /// <summary>
    /// 年级名称
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// 班级
    /// </summary>
    public List<Section> Sections { get; private set; }=new List<Section>();

    private Grade()
    {
        
    }

    public Grade(string name)
    {
        Name = name;
        GradeId =HashHelper.ComputeSha256Hash(name).Substring(0,8);
    }

    /// <summary>
    /// 添加班级
    /// </summary>
    /// <param name="section"></param>
    public void AddSection(Section section)
    {
        if (!Sections.Contains(section))
        {
            Sections.Add(section);
        }
    }

    /// <summary>
    /// 删除班级
    /// </summary>
    public void RemoveSection(Section section)
    {
        if(section.Students.Count > 0)
        {
            throw new ArgumentException("不可删除该班级，班级内还有学生");
        }
        Sections.Remove(section);
    }

    /// <summary>
    /// 获取班级
    /// </summary>
    /// <param name="sectionName"></param>
    /// <returns></returns>
    public Section? FindSectionByName(string sectionName) => Sections.FirstOrDefault(s=>s.Name == sectionName && s.GradeId ==GradeId);
    
    /// <summary>
    /// 该年级学生总数
    /// </summary>
    /// <returns></returns>
    public int GetStudentsCount() => Sections.SelectMany(x=>x.Students).ToList().Count();


}
