using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.InterfacesAndImplementations.Monster.Factories
{
    public  class MonsterStatsFactory
    {

        public static IMonsterStats CreateMonsterStats()
        {
            var stats = new Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats.MonsterStats();

            // Subscribe to the event
            stats.OnHpZeroOrBelow += () =>
            {
                // Disable object and add it into the pool
                // Play death animation
                Debug.Log($"{stats.Name} has reached zero HP!");
            };

            return stats;
        }
    }
}
