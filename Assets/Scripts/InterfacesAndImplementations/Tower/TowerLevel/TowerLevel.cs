using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.InterfacesAndImplementations.Tower
{
    public class TowerLevel : ITowerLevel
    {
        public int Level { get; set; }
        

        public void LevelUpTower(int gold, ITowerStats towerStats)
        {
            int nextLevel = Level + 1;
            int requiredGold = 0; 
            switch (nextLevel)
            {
                case 1:
                    requiredGold = 50;
                    break;
                case 2:
                    requiredGold = 100;
                    break;
                case 3:
                    requiredGold = 150;
                    break;
                case 4:
                    requiredGold = 200;
                    break;
                default:
                    
                    return;
            }
            if (gold >= requiredGold)
            {
                Level = nextLevel;
                // double the tower's stats
                towerStats.Range *= 2f;
                towerStats.Damage *= 2f;
                towerStats.ArmorPenetration *= 2f;
                towerStats.MagicPenetration *= 2f;

                FireRates fireRates = new FireRates();
                var nextFireRateAndAttackTimer = fireRates.GetNextFireRateAndAttackTimer(towerStats.FireRate, towerStats.AttackTimer);
                towerStats.FireRate = nextFireRateAndAttackTimer.Item1;
                towerStats.AttackTimer = nextFireRateAndAttackTimer.Item2;
            }
            else return;
        }
    }
}
