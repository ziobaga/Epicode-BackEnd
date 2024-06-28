using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esercizio2
{
    internal class Cv
    {
        public InformazioniPersonali PersonlInfo { get; set; }
        public List<Studi> Studi = new List<Studi>();
        public List<Esperienze> Impiego = new List<Esperienze>();
    }
}
