internal class Program
{
    static void Main(string[] args)
    {
        string[] nomi = { "Adriano", "Andrea", "Checco", "Flavio", "Stefano" };
        Console.WriteLine("Cerca un nome:");
        Cerca(nomi);
    }
    static void Cerca(string[] nomi)
    {
        string nome = Console.ReadLine();
        bool check = false;

        for (int i = 0; i < nomi.Length; i++)
        {
            if (nome == nomi[i])
            {
                check = true;
                Console.WriteLine("Il nome esiste");
                Cerca(nomi);
            }
        }

        if (!check)
        {
            Console.WriteLine("Il nome non esiste. Riprova.");
            Cerca(nomi);
        }
    }
}