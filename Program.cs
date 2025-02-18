namespace cdealy_na_easy;

class Program
{
    static void Main(string[] args)
    {
        string readContent = File.ReadAllText(@"C:\vs code\cdealy_na_easy\name.txt");
        Console.WriteLine(readContent);
    }
}
