internal class Program
{
    static void Main(string[] args)
    {
        int x = 0;
        Console.WriteLine("Specifica dimensione array");
        x = int.Parse(Console.ReadLine());
        int[] listanumeri = new int[x];

        int i;
        int somma = 0;

        for (i = 0; i < listanumeri.Length; i++)
        {
            Console.WriteLine("Inserisci numero");
            listanumeri[i] = int.Parse(Console.ReadLine());
        }
        for (i = 0; i < listanumeri.Length; i++)
        {
            somma += listanumeri[i];
        }
        Console.WriteLine($"\nLa somma è: {somma}\n");
        Console.WriteLine($"La media è: {somma / x}");
        Console.ReadLine();
    }
}