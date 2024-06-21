internal class Programs
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string ConfermaPassword { get; set; }

    public bool IsLoggedIn { get; set; }


    private bool _isLoggedIn = false;

    public void Start()
    {
        Console.WriteLine("Benvenuto, " + Username + " ,scegli un opzione");
        Console.WriteLine("1. LOGIN");
        Console.WriteLine("2. LOGOUT");

        int scegli = int.Parse(Console.ReadLine());
        if (scegli ==1)
        {
            Login();
        }
        else
        {
             
        }
    }

    public void Login()
    {

        if (_isLoggedIn == false)
        {
            Console.WriteLine("Inserisci il tuo username");
            string name = Console.ReadLine();

            Console.WriteLine("Inserisci la tua password");
            string password = Console.ReadLine();

            Console.WriteLine("Conferma la tua password");
            string confermaPassword = Console.ReadLine();

            Start();
        }
        else
        {
            Console.WriteLine("I dati non sono corretti!");
        }
    }
    

   
}