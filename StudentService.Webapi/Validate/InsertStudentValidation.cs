using System.Text.RegularExpressions;
using Commons;
using FluentValidation;
using StudentService.Domain;
using StudentService.Domain.RequestDtos;

namespace StudentService.Webapi.Validate;

public class InsertStudentValidation : AbstractValidator<InsertStudentsDto>
{
    public InsertStudentValidation()
    {
		RuleFor(x => x.GradeName).NotEmpty().WithMessage("年级不能为空!");
		RuleFor(x => x.GradeName).Must(gradeName => gradeName.EndsWith("年级")).WithMessage("必须以xx年级结尾!");
		RuleFor(x => x.GradeName).Must(ValidateRule.ValidateGradeName).WithMessage("年级范围必须在1-10,例如:1年级、10年级");

		RuleFor(x => x.SectionName).NotEmpty().WithMessage("班级不能为空!");
		RuleFor(x => x.SectionName).Must(gradeName => gradeName.EndsWith("班级")).WithMessage("必须以xx班级结尾!");
		RuleFor(x => x.SectionName).Must(ValidateRule.ValidateSectionName).WithMessage("班级范围必须在1-20,例如:1班、20班");

		RuleForEach(x => x.Students).NotNull().WithMessage("学生不可为空!");
		RuleForEach(x => x.Students).ChildRules(student => {
			student.RuleFor(x => x.Name).NotEmpty().WithMessage("学生的姓名不可为空!");
			student.RuleFor(x => x.Birthday).NotEmpty().WithMessage("学生出生日期不能为空!");
			student.RuleFor(x => x.Birthday.ToString()).Must(ValidateRule.ValidateBirthday).WithMessage("学生出生日期不能为空!");
			student.RuleFor(x => x.Gender).NotEmpty().WithMessage("学生性别不可为空!");
		});
		
	}
}
