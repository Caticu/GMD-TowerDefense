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
        public int Level2Cost { get; set; }
        public int Level3Cost { get; set; }
        public int Level4Cost { get; set; }
        public int Level5Cost { get; set; }

        public void LevelUpTower(int gold, ITowerStats towerStats)
        {
            int nextLevel = Level + 1;
            int requiredGold = 0;
            // Determine the required gold and the level up conditions for the next level
            switch (nextLevel)
            {
                case 1:
                    requiredGold = 100;
                    break;
                case 2:
                    requiredGold = 150;
                    break;
                case 3:
                    requiredGold = 200;
                    break;
                case 4:
                    requiredGold = 250;
                    break;
                default:
                    
                    return;
            }
            // Check if the tower has enough gold to level up
            if (gold >= requiredGold)
            {
                // Deduct the required gold
                gold -= requiredGold;
                // Increase the tower's level
                Level = nextLevel;
                // Increase the tower's stats by 15%
                towerStats.Range *= 1.15f;
                towerStats.Damage *= 1.15f;
                towerStats.ArmorPenetration *= 1.15f;
                towerStats.MagicPenetration *= 1.15f;

                FireRates fireRates = new FireRates();
                var nextFireRateAndAttackTimer = fireRates.GetNextFireRateAndAttackTimer(towerStats.FireRate, towerStats.AttackTimer);
                towerStats.FireRate = nextFireRateAndAttackTimer.Item1;
                towerStats.AttackTimer = nextFireRateAndAttackTimer.Item2;
            }
        }
    }
}
