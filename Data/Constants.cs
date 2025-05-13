namespace todo_maui_reposetory.Data
{
    public static class Constants
    {
        public const string DatabaseFilename = "TodoDb.db3";

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}