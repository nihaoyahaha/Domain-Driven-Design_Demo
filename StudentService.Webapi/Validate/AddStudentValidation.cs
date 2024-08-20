using System.Text.RegularExpressions;
using FluentValidation;
using StudentService.Domain;

namespace StudentService.Webapi.Validate;

public class AddStudentValidation : AbstractValidator<AddStudentReq>
{
    public AddStudentValidation()
    {
        RuleFor(x => x.StudentName).NotEmpty().WithMessage("学生姓名不能为空!");
        RuleFor(x => x.StudentName).Length(1, 10).WithMessage("学生姓名长度范围在1-10个字符以内");

        RuleFor(x => x.Birthday).NotEmpty().WithMessage("学生出生日期不能为空!");
        RuleFor(x => x.Birthday.ToString()).Must(ValidateBirthday).WithMessage("请输入正确的日期格式");

        RuleFor(x => x.GradeName).NotEmpty().WithMessage("年级不能为空!");
        RuleFor(x => x.GradeName).Must(gradeName => gradeName.EndsWith("年级")).WithMessage("必须以xx年级结尾!");
        RuleFor(x => x.GradeName).Must(ValidateGradeName).WithMessage("年级范围必须在1-10,例如:1年级、10年级");

        RuleFor(x => x.SectionName).NotEmpty().WithMessage("班级不能为空!");
        RuleFor(x => x.SectionName).Must(SectionName => SectionName.EndsWith("班")).WithMessage("必须以xx班结尾!");
        RuleFor(x => x.SectionName).Must(ValidateSectionName).WithMessage("班级范围必须在1-20,例如:1班、20班");
    }
    private bool ValidateBirthday(string strDate)
    {
        DateTime date;
        return DateTime.TryParse(strDate, out date);
    }

    private bool ValidateGradeName(string gradeName)
    {
        string gradeNum = gradeName.Substring(0, gradeName.Length - 2);
        string pattern = @"^(?:[1-9]|1[0-9]|10)$";
        return Regex.IsMatch(gradeName, pattern);
    }
    private bool ValidateSectionName(string sectionName)
    {
        string sectionNum = sectionName.Substring(0, sectionName.Length - 1);
        string pattern = @"^(?:[1-9]|1[0-9]|20)$";
        var match = Regex.IsMatch(sectionNum, pattern);
        return match;
    }
}
