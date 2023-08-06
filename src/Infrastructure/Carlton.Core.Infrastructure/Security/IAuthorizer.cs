namespace Carlton.Core.Infrastructure.Security;

public interface IAuthorizer
{
    bool IsAuthorized(object instance);
}
