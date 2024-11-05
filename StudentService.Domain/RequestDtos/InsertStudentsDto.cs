using StudentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Domain.RequestDtos
{
	public record InsertStudentsDto(string GradeName,string SectionName,List<StudentDto> Students);
}
