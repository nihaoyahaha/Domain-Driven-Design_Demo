using Microsoft.AspNetCore.Identity;

namespace StudentService.Domain;

public class Role :IdentityRole<Guid>
{
	public Role() => Id = Guid.NewGuid();
}
