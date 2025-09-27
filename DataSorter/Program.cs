static void SortData()
{
    string path = "../../../../Shared/data.dat";

    if (!File.Exists(path))
    {
        Console.WriteLine("Файл не знайдено: " + path);
        return;
    }

    try
    {
        int[] numbers;

       
        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (var br = new BinaryReader(fs))
        {
            int length = (int)(fs.Length / sizeof(int)); 
            numbers = new int[length];
            for (int i = 0; i < length; i++)
            {
                numbers[i] = br.ReadInt32();
            }
        }

    
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            for (int j = 0; j < numbers.Length - i - 1; j++)
            {
                if (numbers[j] > numbers[j + 1])
                {
                    int temp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;

                    
                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    using (var bw = new BinaryWriter(fs))
                    {
                        foreach (var num in numbers)
                            bw.Write(num);
                    }

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