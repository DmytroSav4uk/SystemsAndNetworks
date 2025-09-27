static void SortDataReversed()
{
    string path = "../../../../Shared/data.dat";
    using (Mutex mutex = new Mutex(false, "Global\\SharedDataMutex"))
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не знайдено: " + path);
            return;
        }

        try
        {
            mutex.WaitOne(); 

            int[] numbers;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                int length = (int)(fs.Length / sizeof(int));
                numbers = new int[length];
                for (int i = 0; i < length; i++)
                    numbers[i] = br.ReadInt32();
            }

           
            for (int i = 1; i < numbers.Length; i++)
            {
                int key = numbers[i];
                int j = i - 1;

                while (j >= 0 && numbers[j] < key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;

                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    using (var bw = new BinaryWriter(fs))
                    {
                        foreach (var num in numbers)
                            bw.Write(num);
                    }
                    Thread.Sleep(1000);
                }

                numbers[j + 1] = key;

                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                using (var bw = new BinaryWriter(fs))
                {
                    foreach (var num in numbers)
                        bw.Write(num);
                }
                Thread.Sleep(1000);
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}

SortDataReversed();