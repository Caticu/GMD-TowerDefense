using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.InterfacesAndImplementations.Tower
{
    public class TowerStats : ITowerStats
    {
        public float Range { get; set; }
        public float Damage { get; set; }
        public float FireRate { get; set; }
        public float AttackTimer { get; set; }
        public DamageType DamageType { get; set; }
        public float ArmorPenetration { get; set; }
        public float MagicPenetration { get; set; }

        public string TowerName { get; set; }
    }
}
