namespace CollegeMgmtSystem.common
{
    public class CommonCredentials
    {
        public static readonly string CosmosURL = Environment.GetEnvironmentVariable("cosmos-url");
        public static readonly string DataBaseName = Environment.GetEnvironmentVariable("database-name");
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("container-name");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("auth-token");

        public static readonly string CollegeDocumentType = "college";
        public static readonly string DepartmentDocumentType = "department";
        public static readonly string StudentDocumentType = "student";
    }
}
