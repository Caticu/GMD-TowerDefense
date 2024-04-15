using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Monsters;
namespace Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats
{
    public class MonsterStats : IMonsterStats
    {
        private float hp;
        public float Hp
        {
            get => hp;
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    // Invoke the event when HP reaches zero or below
                    OnHpZeroOrBelow?.Invoke();
                }
            }
        }
        public float Armor { get ; set ; }
        public float MagicResist { get ; set; }
        public float MovementSpeed { get ; set ; }
        public bool IsAerial { get ; set; }

        public string Name { get; set; }

        public event Action OnHpZeroOrBelow;

        public MonsterStats()
        {
            
        }
    }
}
