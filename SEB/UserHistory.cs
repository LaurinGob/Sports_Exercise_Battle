using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class UserHistory
    {
        public string Name { get; set; } = "";
        public string ExerciseType { get; set; } = "";
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(1);
        public int Count { get; set; } = 0;
    }
}
