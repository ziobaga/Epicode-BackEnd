public class Dipendente
{
    public string nome;
    public string cognome;
    public int età;
    public string lavoro;

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public string Cognome
    {
        get { return cognome; }
        set { cognome = value; }
    }

    public int Età
    {
        get { return età; }
        set { età = value; }
    }

    public string Lavoro
    {
        get { return lavoro; }
        set { lavoro = value; }
    }

    public void Descriviti()
    {
        Console.WriteLine("Sono " + nome + " " + cognome + " ho " + età + " anni e faccio il " + lavoro);
    }
}