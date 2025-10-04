namespace DataSorter
{
    class DataSorter
    {
        static Mutex mutex = new Mutex(false, "Global\\SharedDataMutex");

        static void Main()
        {
            Thread sortingThread = new Thread(SortData) { IsBackground = true };
            sortingThread.Start();

            Console.WriteLine("Sorting started. Press Enter to exit...");
            Console.ReadLine();
        }

        static void SortData()
        {
            string path = "../../../../Shared/data.dat";

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                return;
            }

            int[] numbers = ReadArrayFromFile(path);

            Console.WriteLine("Initial array:");
            PrintArray(numbers);

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = 0; j < numbers.Length - i - 1; j++)
                {
                    numbers = ReadArrayFromFile(path);

                    if (numbers[j] > numbers[j + 1])
                    {
                        (numbers[j], numbers[j + 1]) = (numbers[j + 1], numbers[j]);

                        WriteArrayToFile(numbers, path);
                        PrintArray(numbers);

                        Thread.Sleep(1000);
                    }
                }
            }

            Console.WriteLine("Sorting completed.");
        }

        static int[] ReadArrayFromFile(string path)
        {
            mutex.WaitOne();
            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var br = new BinaryReader(fs);
                int length = (int)(fs.Length / sizeof(int));
                int[] numbers = new int[length];
                for (int i = 0; i < length; i++)
                    numbers[i] = br.ReadInt32();
                return numbers;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        static void WriteArrayToFile(int[] numbers, string path)
        {
            mutex.WaitOne();
            try
            {
                using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                using var bw = new BinaryWriter(fs);
                foreach (var num in numbers)
                    bw.Write(num);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        static void PrintArray(int[] numbers)
        {
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}
