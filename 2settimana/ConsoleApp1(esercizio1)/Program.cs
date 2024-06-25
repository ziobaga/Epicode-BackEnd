using esercizio1;
using System;

namespace ConsoleApp1_esercizio1_
{
    internal class Program
    {

        private List<MenuBody> ordine = new List<MenuBody>();
        private decimal totale = 0.0m;

        public void Start()
        {
            Console.WriteLine("Menù");
            int index = 1;
            foreach (var i in Menu.MenuItems)
            {
                Console.WriteLine($"{index}. {i.Nome} - {i.Prezzo} euro");
                index++;
            }
            Scegli();
        }

        private void Scegli()
        {
            Console.WriteLine("\nPremi 0 per confermare");
            Console.WriteLine("       OPPURE");
            Console.WriteLine("Scegli un elemento del menu inserendo il numero corrispondente");
            int scelta;
            bool isValidInput = int.TryParse(Console.ReadLine(), out scelta);

            if (isValidInput && scelta > 0 && scelta <= Menu.MenuItems.Count)
            {
                var selectedItem = Menu.MenuItems[scelta - 1];
                ordine.Add(selectedItem);
                totale += (decimal)selectedItem.Prezzo;
                Console.WriteLine($"Hai scelto: {selectedItem.Nome} - {selectedItem.Prezzo} euro");
                Scegli();
            }
            else if (scelta == 0)
            {
                Conto();
            }
            else
            {
                Console.WriteLine("Scelta non valida. Per favore inserisci un numero valido.");
                Scegli();
            }
        }

        private void Conto()
        {
            decimal costoFinale = totale + 3.00m;

            Console.WriteLine("\nOrdine completo:");
            foreach (var item in ordine)
            {
                Console.WriteLine($"{item.Nome} - {item.Prezzo} euro");
            }
            Console.WriteLine($"Costo totale: {totale} euro");
            Console.WriteLine($"Costo totale inclusivo di servizio(3 euro): {costoFinale} euro");
        }




        static void Main(string[] args)
        {

            Program program = new Program();
            program.Start();
        }
    }
}
