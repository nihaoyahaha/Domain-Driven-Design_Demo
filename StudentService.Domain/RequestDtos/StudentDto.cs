using StudentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Domain;

public record StudentDto(string Name,DateTime Birthday,Gender Gender);

