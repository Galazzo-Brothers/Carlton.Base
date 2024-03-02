using System.Data;
namespace Carlton.Core.Foundation.API.Data.Connections;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}
