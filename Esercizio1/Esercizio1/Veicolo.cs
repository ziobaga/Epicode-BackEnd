public class Veicolo
{
    public string marca;
   
    public int anno;


    public string Marca
    {
        get { return marca; }
        set { marca = value; }
    }

    public int Anno
    {
        get { return anno; }
        set { anno = value; }
    }

 

    public void Descriviti()
    {
        Console.WriteLine("Ciao sono una " + marca + " " + "e sono dell'anno  " + anno);
    }
}