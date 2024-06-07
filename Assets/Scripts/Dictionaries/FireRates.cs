using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public  class FireRates
    {
        public Dictionary<string, (float, float)> FireRate = new Dictionary<string, (float, float)>
        {
            {"0.33bullets/second", (0.33f, 3f)},
            {"0.5bullets/second", (0.5f, 2f)},
            {"1bullet/second", (1f, 1f)},
            {"2bullets/second", (2f, 0.5f)},
            {"3bullets/second", (3f, 1f/3f)},
            {"4bullets/second", (4f, 0.25f)},
            {"5bullets/second", (5f, 0.2f)},
            {"6bullets/second", (6f, 1f/6f)},
            {"7bullets/second", (7f, 1f/7f)},
            {"8bullets/second", (8f, 1f/8f)},
            {"9bullets/second", (9f, 1f/9f)},
            {"10bullets/second", (10f, 0.1f)},
            {"11bullets/second", (11f, 1f/11f)},
            {"12bullets/second", (12f, 1f/12f)},
            {"13bullets/second", (13f, 1f/13f)},
            {"14bullets/second", (14f, 1f/14f)},
            {"15bullets/second", (15f, 1f/15f)},
        };

        public (float, float) GetFireRateAndAttackTimer(string description)
        {
            if (FireRate.TryGetValue(description, out var values))
            {
                return values;
            }
            else
            {
                return (0f, 0f);
            }
        }

        /// <summary>
        /// get the next fire rate when lvling up the tower
        /// </summary>
        /// <param name="currentFireRate"></param>
        /// <param name="currentAttackTimer"></param>
        /// <returns></returns>
        public (float, float) GetNextFireRateAndAttackTimer(float currentFireRate, float currentAttackTimer)
        {
            float nextFireRate = float.MaxValue;
            float nextAttackTimer = float.MaxValue;

            foreach (var entry in FireRate)
            {
                if (entry.Value.Item1 > currentFireRate)
                {
                    if (entry.Value.Item1 < nextFireRate)
                    {
                        nextFireRate = entry.Value.Item1;
                        nextAttackTimer = entry.Value.Item2;
                    }
                }
            }

            // If nextFireRate and nextAttackTimer didn t change their values, it means they are at max level
            
            if (nextFireRate == float.MaxValue && nextAttackTimer == float.MaxValue)
            {
                return (currentFireRate, currentAttackTimer);
            }
            else
            {
                return (nextFireRate, nextAttackTimer);
            }
        }
    }
}
