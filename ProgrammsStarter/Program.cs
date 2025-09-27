using System.Diagnostics;

class ProgrammsStarter
{
    static void Main()
    {
        RunProjectAndWait("DataCreator");
      
        
        
      
        RunProject("DataSorterReversed");
        RunProject("DataSorter");
        
        
        RunProject("DataVisualiser");
    }

    static void RunProject(string projectName)
    {
        string? exePath = FindExePath(projectName);
        if (exePath != null)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Path.GetFullPath(exePath),
                UseShellExecute = true
            });
            Console.WriteLine($"{projectName} started.");
        }
        else
        {
            Console.WriteLine($"Exe not found for {projectName}");
        }
    }

    static void RunProjectAndWait(string projectName)
    {
        string? exePath = FindExePath(projectName);
        if (exePath != null)
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = Path.GetFullPath(exePath),
                UseShellExecute = true
            });
            process?.WaitForExit();
            Console.WriteLine($"{projectName} finished.");
        }
        else
        {
            Console.WriteLine($"Exe not found for {projectName}");
        }
    }

    static string? FindExePath(string projectName)
    {
        string basePath = Path.Combine("..", "..", "..", "..");
        string[] frameworks = { "net9.0", "net9.0-windows" };

        foreach (var fw in frameworks)
        {
            string path = Path.Combine(basePath, projectName, "bin", "Debug", fw, $"{projectName}.exe");
            if (File.Exists(path))
                return path;
        }

        return null;
    }
}