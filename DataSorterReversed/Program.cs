static void SortDataReversed()
{
    string path = "../../../../Shared/data.dat";

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
        
        for (int i = 1; i < numbers.Length; i++)
        {
            int key = numbers[i];
            int j = i - 1;
            while (j >= 0 && numbers[j] < key)
            {
                numbers[j + 1] = numbers[j];
                j--;
                
                File.WriteAllText(path, string.Join(" ", numbers));
                Thread.Sleep(1000); 
            }
            numbers[j + 1] = key;
            
            File.WriteAllText(path, string.Join(" ", numbers));
            Thread.Sleep(1000);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка при сортуванні: " + ex.Message);
    }
}

SortDataReversed();