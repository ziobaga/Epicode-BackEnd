using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esercizio2.Service
{
    public interface iCv
    {
        void AggiungiTitoloStudio(Studi TitoloStudio);

        void AggiungiEsperienza(Esperienze Esperienza);

        void AggiungiInformazionePersonali(InformazioniPersonali InformazioniPersonali);

        Cv CreaCv();

    }
}
