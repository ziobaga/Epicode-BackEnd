public class ContoCorrente
{
    public string Name { get; set; }
    public string SurName { get; set; }

    private decimal _saldo = 0;
    public decimal Saldo 
    { 
        get { return _saldo; } 
        set { _saldo = value; } 
    }

    private bool _contoAperto = false;

    public bool ContoAperto
    {
        get { return _contoAperto; }
        set { _contoAperto = value; }
    }
    public void Via()
    {
        Console.WriteLine("Seleziona un opzione");
        Console.WriteLine("1. APRI NUOVO CONTO CORRENTE");
        Console.WriteLine("2. EFFETTUA UN VERSAMENTO");
        Console.WriteLine("3. EFFETTUA UN PRELEVAMENTO");
        


      
        int scelta = int.Parse(Console.ReadLine());
        switch (scelta)
        {
            case 1: 
                Console.WriteLine("Hai scelto: APRI NUOVO CONTO CORRENTE");
                ApriConto();
                break;
            case 2: 
                Console.WriteLine("Hai scelto: EFFETTUA UN VERSAMENTO");
                EffettuaVersamento();
                break;
            case 3: 
                Console.WriteLine("Hai scelto: EFFETTUA UN PRELEVAMENTO");
                EffettuaPrelevamento();
                break;
           
        }
    }

    private void ApriConto()
    {
        Console.WriteLine("Inserisci Nome");
        string Nome = Console.ReadLine();

        Console.WriteLine("Inserisci Cognome");
        string Cognome = Console.ReadLine();

        ContoCorrente conto = new ContoCorrente();
        Name = Nome;
        SurName = Cognome;
        Saldo = 0;
        ContoAperto = true;

        Console.WriteLine($"Conto corrente nr. 2555411 intestato a: {Name} {SurName} con saldo {Saldo} è stato aperto correttamente");
        Via();

    }

    private void EffettuaPrelevamento()
    {
        if (_contoAperto == false)
            
        {
            Console.WriteLine("E' necessario aprire un conto prima di effettuare un prelevamento");
        }
        else
        {
            Console.WriteLine("Inserisci l'importo da prelevare: ");
            decimal importoPrelevato = decimal.Parse(Console.ReadLine());
            Saldo += importoPrelevato;
            Console.WriteLine($"Hai prelevato {_saldo.ToString("N")}");

        }

    }

    private void EffettuaVersamento()
    {
        if (_contoAperto == false)
        {
            Console.WriteLine("E' necessario aprire un conto prima di effettuare un versamento");
        }
        else
        {
            Console.WriteLine("Inserisci l'importo da versare: ");
            decimal importoVersato = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Versamento effettuato");
            _saldo += importoVersato;
            Console.WriteLine($"Nuovo saldo del CC odierno: {_saldo.ToString("N")}");
        }

    }

    public static void Main(string[] args)
    {
        ContoCorrente Conto = new ContoCorrente();
        Conto.Via();
    }
}