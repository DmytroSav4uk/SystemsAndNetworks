using System.Diagnostics;

class ProgrammsStarter
{
    static void Main()
    {
        RunProject("DataCreator");
        RunProject("DataSorter");
        
        RunProject("DataSorterReversed");
        
        RunProject("DataVisualiser");
    }

    static void RunProject(string projectName)
    {
        string basePath = Path.Combine("..", "..", "..", ".."); 
        string[] frameworks = { "net9.0", "net9.0-windows" };

        foreach (var fw in frameworks)
        {
            string path = Path.Combine(basePath, projectName, "bin", "Debug", fw, $"{projectName}.exe");
            if (File.Exists(path))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.GetFullPath(path),
                    UseShellExecute = true
                });
                Console.WriteLine($"{projectName} запущено ({fw}).");
                return;
            }
        }

        Console.WriteLine($"Не знайдено exe для {projectName}");
    }
}