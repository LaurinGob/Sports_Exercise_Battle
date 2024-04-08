using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class BLL_TournamentManager
    {
        private static readonly Lazy<BLL_TournamentManager> lazyInstance = new Lazy<BLL_TournamentManager>(() => new BLL_TournamentManager());

        // Private constructor to prevent instantiation
        private BLL_TournamentManager() { }

        public static BLL_TournamentManager Instance => lazyInstance.Value;
    }
}
