internal class Program
{
    public static void Main(string[]args)
    {
        Persona a = new Persona("Mario", "Rossi", 30);

        Console.WriteLine(a.getNome());
        Console.WriteLine(a.getCognome());
        Console.WriteLine(a.getEta());
        a.getDettagli();
    }
}