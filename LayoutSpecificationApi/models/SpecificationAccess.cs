namespace LayoutSpecificationApi;

public class SpecificationAccess
{
    public Guid SpecificationAccessId { get; private set; }
    public LayoutSpecification LayoutSpecification { get; private set; }
    public SpecificationAccessType AccessType { get; set; }
    public User User { get; private set; }
    public User GrantedBy { get; private set; }
    public DateTime GrantedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public SpecificationAccess() { }

    public static SpecificationAccess Grant(
        Guid layoutSpecification,
        Guid userId,
        SpecificationAccessType accessType,
        Guid grantedBy)
    {
        return new SpecificationAccess
        {
             
        };
    }

    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
    }
}



