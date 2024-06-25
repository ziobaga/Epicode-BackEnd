namespace esercizio1
{
    public class Menu
    {
        public static List<MenuBody> MenuItems { get; } = new List<MenuBody>
        {
            new MenuBody { Nome = "Coca Cola", Prezzo = 2.50m },
            new MenuBody { Nome = "Insalata di pollo", Prezzo = 5.20m },
            new MenuBody { Nome = "Pizza Margherita", Prezzo = 10.00m },
            new MenuBody { Nome = "Pizza 4 formaggi", Prezzo = 12.50m },
            new MenuBody { Nome = "Pz patatine fritte", Prezzo = 3.50m },
            new MenuBody { Nome = "Insalata di riso", Prezzo = 8.00m },
            new MenuBody { Nome = "Frutta di stagione", Prezzo = 5.00m },
            new MenuBody { Nome = "Pizza fritta", Prezzo = 5.00m },
            new MenuBody { Nome = "Piadina vegetariana", Prezzo = 6.00m },
            new MenuBody { Nome = "Panino Hamburger", Prezzo = 7.90m }
        };
    }
}
