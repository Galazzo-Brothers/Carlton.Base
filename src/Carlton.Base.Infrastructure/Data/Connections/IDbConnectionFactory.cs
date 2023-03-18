using System.Data;

namespace Carlton.Base.Infrastructure.Data;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}
