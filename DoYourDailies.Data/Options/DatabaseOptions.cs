namespace DoYourDailies.Data.Options
{
    public class DatabaseOptions
    {
        public const string Section = "Database";
        public const string SQLite = "SQLite";

        public string Name { get; set; } = string.Empty;
        public bool Use { get; set; } = false;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
