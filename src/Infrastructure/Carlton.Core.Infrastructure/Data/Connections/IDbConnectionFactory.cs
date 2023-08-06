namespace Carlton.Core.Infrastructure.Data;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}
