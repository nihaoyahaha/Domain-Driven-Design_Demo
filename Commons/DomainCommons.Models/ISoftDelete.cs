namespace Commons.DomainCommons.Models;

public interface ISoftDelete
{
    bool IsDeleted { get; }
    void SoftDelete();
}
