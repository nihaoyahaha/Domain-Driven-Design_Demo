using FluentValidation;

namespace StudentService.Domain;

public record AddSectionReq(string SectionName, string GradeName);
