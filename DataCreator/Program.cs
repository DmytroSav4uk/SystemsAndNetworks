static void randomNumbersArr()
{
    var array = new int[30];
    Random rnd = new Random();

    for (int i = 0; i < array.Length; i++)
    {
        array[i] = rnd.Next(10, 100);
    }
    
    File.WriteAllText(@"C:\Users\Dmytro\RiderProjects\SystemsAndNetworks\Shared\data.dat", string.Join(" ", array));
}


randomNumbersArr();