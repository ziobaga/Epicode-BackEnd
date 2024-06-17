

using System.Runtime.InteropServices;

class Program
{
    public static void Main (string[] args)
    {
        Atleta a = new Atleta();
        a.Nome = "Mario";
        a.Cognome = "Rossi";
        a.Età = 25;
        a.Sport = "Calcio";
        a.Descriviti();

        Dipendente b = new Dipendente();
        b.Nome = "Marco";
        b.Cognome = "Carta";
        b.Età = 40;
        b.Lavoro = "Cuoco";
        b.Descriviti();

        Animale c = new Animale();
        c.Nome = "Sparky";
        c.Specie = "Cane";
        c.Età = 12;
        c.Descriviti();

        Veicolo d = new Veicolo();
        d.Marca = "Alfa Romeo";
        d.Anno = 2014;
        d.Descriviti();

    }

}
