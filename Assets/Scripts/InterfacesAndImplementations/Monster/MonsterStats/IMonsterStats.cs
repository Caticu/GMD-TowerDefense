using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats
{
    public interface IMonsterStats
    {
        public float Hp { get; set; }
        public float Armor { get; set; }
        public float MagicResist { get; set; }
        public float MovementSpeed { get; set; }
        public bool IsAerial { get; set; }

        public string Name { get; set; }

        event Action OnHpZeroOrBelow;
    }
}
