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
  
        void LevelUpTower(int gold, ITowerStats towerStats);
    }
}
