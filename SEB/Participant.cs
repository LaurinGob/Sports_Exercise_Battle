using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class Participant
    {
        public int UserID { get; set; }
        public string ProfileName { get; set; }
        public int Sum { get; set; }
        public int Elo {  get; set; }

        public Participant(string profileName, int sum, int elo, int userID)
        {
            UserID = userID;
            ProfileName = profileName;
            Sum = sum;
            Elo = elo;
        }

    }
}
