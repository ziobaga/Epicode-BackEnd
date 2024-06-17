public class Atleta
{
    public string nome;
    public string cognome;
    public int età;
    public string sport;

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

    public string Sport
    {
        get { return sport; }
        set { sport = value; }
    }

    public void Descriviti()
    {
        Console.WriteLine("Sono " + nome + " " + cognome + " ho " + età + " anni e pratico " + sport);
    }
}