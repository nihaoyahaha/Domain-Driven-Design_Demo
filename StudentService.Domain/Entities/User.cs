using Commons.DomainCommons.Models;
using Microsoft.AspNetCore.Identity;

namespace StudentService.Domain.Entities;

public class User : IdentityUser<Guid>, IHasCreationTime,IHasDeletionTime,ISoftDelete
{
    public DateTime CreationTime {get;init;}

    public DateTime? DeletionTime {get;private set;}

    public bool IsDeleted {get;private set;}

    public User(string userName):base(userName)
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }    

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletionTime = DateTime.Now;
    }

}
