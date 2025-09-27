class DataCreator
{
    static void Main()
    {
        string path = "../../../../Shared/data.dat";
        Random rnd = new Random();
        int[] numbers = new int[30];

        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = rnd.Next(10, 100);

        Mutex mutex = new Mutex(false, "Global\\SharedDataMutex");
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

        Console.WriteLine("File created.");
    }
}