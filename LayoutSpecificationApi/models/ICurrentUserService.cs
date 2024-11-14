namespace LayoutSpecificationApi;

public interface ICurrentUserService
{
    Guid UserId();
}

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId()
    {
        return Guid.NewGuid();
    }
}