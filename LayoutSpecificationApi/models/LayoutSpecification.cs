namespace LayoutSpecificationApi;

public class LayoutSpecification
{
    public Guid LayoutSpecificationId { get; private set; }
    public User Owner { get; private set; }
    
    public SpecificationStatus Status { get; private set; }
    public IReadOnlyCollection<SpecificationAccess> AccessRights => _accessRights.AsReadOnly();

    private readonly List<SpecificationAccess> _accessRights;
}