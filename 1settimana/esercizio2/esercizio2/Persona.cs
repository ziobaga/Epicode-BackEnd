internal class Persona
{
    public string _nome;
    public string _cognome;
    public int _eta;

    public Persona (string nome, string cognome, int eta)
    {
        _nome = nome;
        _cognome = cognome;
        _eta = eta;
    }

    public string getNome()
    {
        return _nome;
    }

    public string getCognome()
    {
        return _cognome;
    }

    public int getEta()
    {
        return _eta;
    }

    public void getDettagli()
    {
        Console.WriteLine("Nome: " + _nome + " Cognome: " + _cognome + " Età: " + _eta);
    }

}