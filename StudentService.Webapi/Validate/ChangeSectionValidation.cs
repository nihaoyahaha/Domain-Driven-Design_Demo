using Commons;
using FluentValidation;
using StudentService.Domain;

namespace StudentService.Webapi.Validate;

public class ChangeSectionValidation:AbstractValidator<ChangeSectionReq>
{
    public ChangeSectionValidation()
    {
        RuleFor(x=>x.StudentId).NotEmpty().WithMessage("学号不能为空!");
        RuleFor(x=>x.StudentId).Length(8).WithMessage("学号必须是8位");
       
        RuleFor(x => x.SectionName).NotEmpty().WithMessage("班级不能为空!");
        RuleFor(x => x.SectionName).Must(SectionName => SectionName.EndsWith("班")).WithMessage("必须以xx班结尾!");
        RuleFor(x => x.SectionName).Must(ValidateRule.ValidateSectionName).WithMessage("班级范围必须在1-20,例如:1班、20班");

        RuleFor(x => x.GradeName).NotEmpty().WithMessage("年级不能为空!");
        RuleFor(x => x.GradeName).Must(gradeName => gradeName.EndsWith("年级")).WithMessage("必须以xx年级结尾!");
        RuleFor(x => x.GradeName).Must(ValidateRule.ValidateGradeName).WithMessage("年级范围必须在1-10,例如:1年级、10年级");
    }
}
