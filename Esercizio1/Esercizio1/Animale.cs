public class Animale
{
    public string nome;
    public string specie;
    public int età;
    

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public string Specie
    {
        get { return specie; }
        set { specie = value; }
    }

    public int Età
    {
        get { return età; }
        set { età = value; }
    }

    public void Descriviti()
    {
        Console.WriteLine("Ciao sono " + nome + " " + "e sono un " + specie + " " + "di " + età) ;
    }

}