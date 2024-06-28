namespace esercizio4.Classi
{
    public class Sala : EntityBase
    {
        public enum name
        {
            NORD,
            SUD,
            EST,
            OVEST
        }

        public int capacity { get; set; } = 120;
    }
}
