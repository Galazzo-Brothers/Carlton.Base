namespace Carlton.Core.Utilities.Data;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}
