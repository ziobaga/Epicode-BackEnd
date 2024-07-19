using venerdì.Models;

namespace venerdì.Services
{
    public interface IVerbaleService
    {

        Verbale Create(Verbale verbale);
        List<VerbaliTrasgressori> GetAllVerbaliByTrasgressore();
    }
}
