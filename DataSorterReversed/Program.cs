using System;
using System.IO;
using System.Threading;

namespace DataSorter
{
    class DataSorterReversed
    {
        static Mutex mutex = new Mutex(false, "Global\\SharedDataMutex");

        static void Main()
        {
            Thread sortingThread = new Thread(SortDataReversed) { IsBackground = true };
            sortingThread.Start();

            Console.WriteLine("Reversed sorting started. Press Enter to exit...");
            Console.ReadLine();
        }

        static void SortDataReversed()
        {
            string path = "../../../../Shared/data.dat";

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                return;
            }

            int[] numbers;

            mutex.WaitOne();
            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var br = new BinaryReader(fs);
                int length = (int)(fs.Length / sizeof(int));
                numbers = new int[length];
                for (int i = 0; i < length; i++)
                    numbers[i] = br.ReadInt32();
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            Console.WriteLine("Initial array:");
            PrintArray(numbers);

            for (int i = 1; i < numbers.Length; i++)
            {
                int key = numbers[i];
                int j = i - 1;

                while (j >= 0 && numbers[j] < key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;

                    WriteArrayToFile(numbers, path);
                    // PrintArray(numbers);

                    Thread.Sleep(1000);
                }

                numbers[j + 1] = key;

                WriteArrayToFile(numbers, path);
                PrintArray(numbers);

                Thread.Sleep(1000);
            }

            Console.WriteLine("Reversed sorting completed.");
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
