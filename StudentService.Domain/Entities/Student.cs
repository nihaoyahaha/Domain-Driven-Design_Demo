using Commons;
using Microsoft.Win32.SafeHandles;

namespace StudentService.Domain.Entities;

public class Student
{
    /// <summary>
    /// 学号
    /// </summary>
    public string StudentId{get;init;}

    public string Name { get; init; }
    
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime Birthday { get; init; }

    /// <summary>
    /// 班级
    /// </summary>
    public Section Section { get; private set; }
    
    /// <summary>
    /// 班级外键
    /// </summary>
    public string SectionId{get;private set;}

    /// <summary>
    /// 年级
    /// </summary>
    public Grade Grade { get; private set; }
    
    private Student(){

    }

    public Student(string name, DateTime birthday, Section section, Grade grade)
    {
        StudentId = Guid.NewGuid().ToString().Substring(0,8);
        Name = name;
        Birthday = birthday;
        Section = section;
        Grade = grade;
    }

    /// <summary>
    /// 更换班级
    /// </summary>
    /// <param name="section"></param>
    public void ChangeSection(Section section) => Section = section;

    /// <summary>
    /// 更换年级
    /// </summary>
    /// <param name="grade"></param>
    public void ChangeGrade(Grade grade) => Grade = grade;

}
