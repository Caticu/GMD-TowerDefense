using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Monsters;
using UnityEngine;
namespace Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats
{
    public class MonsterStats : IMonsterStats
    {
        private FloatingHealthBar healthBar;
        private float hp;
        private bool hasDied = false;
        public float Hp
        {
            get => hp;
            set
            {
                hp = Mathf.Max(0, value);
                Debug.Log("Setting HP: " + hp);
                if (healthBar != null)
                {
                    healthBar.SetHealth(hp, Name);
                }
                if (hp < 0 || hp == 0 )
                {
                    hasDied = true;
                    OnHpZeroOrBelow?.Invoke();
                    
                }
            }
        }
        public int GoldValue { get; set; }
        public float Armor { get ; set ; }
        public float MagicResist { get ; set; }
        public float MovementSpeed { get ; set ; }
        public bool IsAerial { get ; set; }

        public string Name { get; set; }

        public event Action OnHpZeroOrBelow;

        public event Action<int> OnDeath;
        public MonsterStats()
        {
            
        }

        private struct InitialStats
        {
            public float InitialHp;
            public float InitialArmor;
            public float InitialMagicResist;
            public float InitialMovementSpeed;
            public bool InitialIsAerial;
            public int InitialGoldValue;
        }

        private InitialStats initialStats;

        public void Initialize(float hp, float armor, float magicResist, float movementSpeed, bool isAerial, string name, int goldValue)
        {
            Hp = hp;
            Armor = armor;
            MagicResist = magicResist;
            MovementSpeed = movementSpeed;
            IsAerial = isAerial;
            Name = name;
            GoldValue = goldValue;
            // save initial values
            initialStats = new InitialStats
            {
                InitialHp = hp,
                InitialArmor = armor,
                InitialMagicResist = magicResist,
                InitialMovementSpeed = movementSpeed,
                InitialIsAerial = isAerial,
                InitialGoldValue = goldValue
            };
        }

        public void SetHealthBar(FloatingHealthBar healthBar)
        {
            this.healthBar = healthBar;
            if (this.healthBar != null)
            {
                this.healthBar.SetMaxHealth(hp);
            }
        }

        public void Reset()
        {
            Hp = initialStats.InitialHp;
            Armor = initialStats.InitialArmor;
            MagicResist = initialStats.InitialMagicResist;
            MovementSpeed = initialStats.InitialMovementSpeed;
            IsAerial = initialStats.InitialIsAerial;

            // unsubscribe to event
            OnHpZeroOrBelow = null;
            OnDeath = null;
        }
    }
}
