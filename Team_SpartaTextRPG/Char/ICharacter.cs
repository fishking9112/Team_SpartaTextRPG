using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal interface ICharacter
    {
        string Name { get; }
        int Level { get; set; }
        int HP { get; set; }
        int MaxHP { get; set; }
        float AttDamage { get; set; }
        int Defense { get; set; }
        bool IsDead { get; }
    }
}
