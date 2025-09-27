static void randomNumbersArr()
{
    var array = new int[30];
    Random rnd = new Random();

    for (int i = 0; i < array.Length; i++)
    {
        array[i] = rnd.Next(10, 100);
    }

    using (var fs = new FileStream("../../../../Shared/data.dat", FileMode.Create, FileAccess.Write))
    using (var bw = new BinaryWriter(fs))
    {
        foreach (var num in array)
        {
            bw.Write(num); 
        }
    }
}

randomNumbersArr();