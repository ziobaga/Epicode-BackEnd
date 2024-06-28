using esercizio4.Classi;

namespace esercizio4.Service
{
    public interface iPrenotazioneService
    {
        public void PrenotaBiglietto(Addetto addetto);

        public List<Addetto> addetto { get; set; }
    }
}
