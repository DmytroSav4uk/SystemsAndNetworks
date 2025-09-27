static void SortData()
{
    string path = @"C:\Users\Dmytro\RiderProjects\SystemsAndNetworks\Shared\data.dat";

    if (!File.Exists(path))
    {
        Console.WriteLine("Файл не знайдено: " + path);
        return;
    }
    try
    {
        string content = File.ReadAllText(path);
        int[] numbers = content
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            for (int j = 0; j < numbers.Length - i - 1; j++)
            {
                if (numbers[j] > numbers[j + 1])
                {
                   
                    int temp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;
                    
                    File.WriteAllText(path, string.Join(" ", numbers));
                    
                    Thread.Sleep(1000);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка при сортуванні: " + ex.Message);
    }
}
SortData();