using Nest;

namespace StudentMgmtSys.Common
{
    public class CommonCredentials
    {
        public static readonly string CosmosURL = Environment.GetEnvironmentVariable("cosmos-url");
        public static readonly string DataBaseName = Environment.GetEnvironmentVariable("database-name");
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("container-name");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("auth-token");

    }
}