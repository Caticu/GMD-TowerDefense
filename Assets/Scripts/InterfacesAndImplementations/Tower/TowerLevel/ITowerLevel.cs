using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.InterfacesAndImplementations.Tower
{
    public interface ITowerLevel
    {
        int Level { get; set; }
        int Level2Cost { get; set; }
        int Level3Cost { get; set; }
        int Level4Cost { get; set; }
        int Level5Cost { get; set; }
        void LevelUpTower(int gold, ITowerStats towerStats);
    }
}
