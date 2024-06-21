
    public class Contribuente
    {
        // Variabile statica per memorizzare la scelta del menu
        private static int scelta;
        // Lista statica per memorizzare tutti i contribuenti
        private static List<Contribuente> listaContribuenti = new List<Contribuente>();

        // Proprietà pubbliche per i dettagli del contribuente
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public string Sesso { get; set; }
        public string ComuneResidenza { get; set; }
        public double RedditoAnnuale { get; set; }
        public double ImpostaDovuta { get; set; }

        // Metodo per creare un nuovo contribuente attraverso input utente
        public static Contribuente MenuContribuente()
        {
            // Richiesta dei dati del contribuente
            Console.WriteLine("Inserisci il nome:");
            string inputNome = Console.ReadLine();
            Console.WriteLine("Inserisci il cognome:");
            string inputCognome = Console.ReadLine();
            Console.WriteLine("Inserisci la data di nascita:");
            string inputData = Console.ReadLine();
            Console.WriteLine("Inserisci il codice fiscale:");
            string inputCodice = Console.ReadLine();
            Console.WriteLine("Inserisci il sesso:");
            string inputSesso = Console.ReadLine();
            Console.WriteLine("Inserisci il comune di residenza:");
            string inputResidenza = Console.ReadLine();
            Console.WriteLine("Inserisci il reddito annuale:");
            string inputReddito = Console.ReadLine();

            // Verifico che l'input per il reddito sia un numero valido
            double reddito;
            if (!double.TryParse(inputReddito, out reddito))
            {
                Console.WriteLine("Input non valido. Assicurati di inserire un valore numerico.");
                return null; // Ritorna null se l'input non è valido
            }

            // Creo un nuovo contribuente con i dati inseriti
            Contribuente nuovoContribuente = new Contribuente
            {
                Nome = inputNome,
                Cognome = inputCognome,
                DataNascita = inputData,
                CodiceFiscale = inputCodice,
                Sesso = inputSesso,
                ComuneResidenza = inputResidenza,
                RedditoAnnuale = reddito
            };

            return nuovoContribuente; // Ritorna il nuovo contribuente
        }

        // Metodo per visualizzare il menu e gestire le scelte dell'utente
        public static void MenuImposta()
        {
            while (true) // Ciclo infinito per mantenere il menu attivo
            {
                // Stampa del menu
                Console.WriteLine();
                Console.WriteLine("=========================================");
                Console.WriteLine("                 M E N U                 ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1) Inserimento di una nuova dichiarazione di un contribuente");
                Console.WriteLine("2) La lista completa di tutti i contribuenti che sono stati analizzati.");
                Console.WriteLine("3) Esci dal programma");
                Console.WriteLine();
                Console.WriteLine("Scegli un opzione:");

                // Verifica che l'input dell'utente sia un numero valido
                if (int.TryParse(Console.ReadLine(), out scelta))
                {
                    switch (scelta) // Gestisce le scelte del menu
                    {
                        case 1:
                            Console.WriteLine();
                            Console.WriteLine("Opzione 1");
                            Contribuente nuovoContribuente = MenuContribuente();
                            if (nuovoContribuente != null)
                            {
                                // Calcolo l'imposta e aggiungo il contribuente alla lista
                                CalcolaImposta(nuovoContribuente);
                                listaContribuenti.Add(nuovoContribuente);
                                Console.WriteLine();
                                Console.WriteLine("Contribuente aggiunto alla lista.");
                            }
                            break;
                        case 2:
                            Console.WriteLine();
                            Console.WriteLine("Opzione 2");
                            // Visualizzo la lista dei contribuenti
                            VisualizzaListaContribuenti();
                            break;
                        case 3:
                            Console.WriteLine();
                            Console.WriteLine("Opzione 3: Uscita dal programma.");
                            Console.WriteLine();
                            return; // Esce dal programma
                        default:
                            Console.WriteLine();
                            Console.WriteLine("Opzione non valida, riprova");
                            Console.WriteLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opzione non valida, riprova");
                }
            }
        }

        // Metodo per calcolare l'imposta in base al reddito annuale
        public static void CalcolaImposta(Contribuente contribuente)
        {
            double reddito = contribuente.RedditoAnnuale;

            // Calcolo dell'imposta basato sulle fasce di reddito
            if (reddito <= 15000)
            {
                contribuente.ImpostaDovuta = reddito * 0.23;
            }
            else if (reddito <= 28000)
            {
                contribuente.ImpostaDovuta = 3450 + (reddito - 15000) * 0.27;
            }
            else if (reddito <= 55000)
            {
                contribuente.ImpostaDovuta = 6960 + (reddito - 28000) * 0.38;
            }
            else if (reddito <= 75000)
            {
                contribuente.ImpostaDovuta = 17220 + (reddito - 55000) * 0.41;
            }
            else
            {
                contribuente.ImpostaDovuta = 25420 + (reddito - 75000) * 0.43;
            }

            // Stampa dei dettagli del calcolo dell'imposta
            Console.WriteLine();
            Console.WriteLine($"CALCOLO DELL’IMPOSTA DA VERSARE:");
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
            Console.WriteLine($"nato il {contribuente.DataNascita} ({contribuente.Sesso}),");
            Console.WriteLine($"residente in {contribuente.ComuneResidenza},");
            Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
            Console.WriteLine($"Reddito dichiarato: EURO {contribuente.RedditoAnnuale}");
            Console.WriteLine($"IMPOSTA DA VERSARE: EURO {contribuente.ImpostaDovuta}");
        }

        // Metodo per visualizzare la lista completa dei contribuenti
        public static void VisualizzaListaContribuenti()
        {
            Console.WriteLine("Lista dei contribuenti:");
            // Itera su ogni contribuente nella lista e ne stampa i dettagli
            foreach (var contribuente in listaContribuenti)
            {
                Console.WriteLine();
                Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
                Console.WriteLine($"nato il {contribuente.DataNascita} ({contribuente.Sesso}),");
                Console.WriteLine($"residente in {contribuente.ComuneResidenza},");
                Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
                Console.WriteLine($"Reddito dichiarato: EURO {contribuente.RedditoAnnuale}");
                Console.WriteLine($"IMPOSTA DA VERSARE: EURO {contribuente.ImpostaDovuta}");
            }
            // Ho aggiunto questa funzione... avevo tempo =)
        }
    }

