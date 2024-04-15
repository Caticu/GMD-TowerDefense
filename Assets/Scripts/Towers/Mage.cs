using Assets.Scripts.Bullets;
using Assets.Scripts.Enums;
using Assets.Scripts.InterfacesAndImplementations.Tower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class Mage : MonoBehaviour
    {
        public ITowerStats TowerStats { get; private set;}
        public ITowerLevel TowerLevel { get; private set; }
        public ITowerShoot TowerShoot { get; private set; }

        public GameObject MagicBall;

        private CircleCollider2D TowerCollider;


        public void InitializeMageTower(ITowerStats towerStats, ITowerLevel towerLevel, ITowerShoot towerShoot)
        {
            TowerStats = towerStats;
            TowerLevel = towerLevel;
            TowerShoot = towerShoot;

            FireRates fireRates = new FireRates();

            TowerStats.Range = 3;
            TowerStats.Damage = 30;
            TowerStats.FireRate = fireRates.GetFireRateAndAttackTimer("1bullets/second").Item1;
            TowerStats.AttackTimer = fireRates.GetFireRateAndAttackTimer("1bullets/second").Item2;
            TowerStats.DamageType = DamageType.Magic;
            TowerStats.ArmorPenetration = 0;
            TowerStats.MagicPenetration = 30/100;
            TowerLevel.Level = 1;

            // Get tower collider and set the range
            TowerCollider = GetComponent<CircleCollider2D>();
            TowerCollider.radius = TowerStats.Range;
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            // Check if the collision is with a monster and enough time has passed since the last shot
            if (collision.gameObject.CompareTag("Monster") && Time.time >= TowerStats.AttackTimer)
            {
                // Spawn bullet
                Transform targetTransform = collision.transform;
                TowerShoot.SpawnBullet(this.transform, targetTransform, MagicBall, this.TowerStats);

                // Update next fire time based on fire rate
                TowerStats.AttackTimer = Time.time + 1f / TowerStats.FireRate;
            }
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }


    }
}
