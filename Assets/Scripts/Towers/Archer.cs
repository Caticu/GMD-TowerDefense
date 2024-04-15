using Assets.Scripts.Bullets;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.InterfacesAndImplementations.Tower;
namespace Assets.Scripts.Towers
{
    public class Archer : MonoBehaviour
    {
        #region Properties
        public ITowerStats TowerStats { get; private set; }
        public ITowerLevel TowerLevel { get; private set; }
        public ITowerShoot TowerShoot { get; private set; }

        public GameObject Arrow;
        private CircleCollider2D TowerCollider;
        #endregion Properties


        #region Methods
        private void Start()
        {
            
        }

        public void InitializeTower(ITowerStats towerStats, ITowerLevel towerLevel, ITowerShoot towerShoot)
        {

            TowerStats = towerStats;
            TowerLevel = towerLevel;
            TowerShoot = towerShoot;


            FireRates fireRates = new FireRates();
            // Set the stats
            TowerStats.Range = 5;
            TowerStats.Damage = 20;
            TowerStats.FireRate = fireRates.GetFireRateAndAttackTimer("2bullets/second").Item1;
            TowerStats.AttackTimer = fireRates.GetFireRateAndAttackTimer("2bullets/second").Item2;
            TowerStats.DamageType = DamageType.Physical;
            TowerStats.ArmorPenetration = 20 / 100;
            TowerStats.MagicPenetration = 0;
            TowerLevel.Level = 1;

            // Get tower collider and set the range
            TowerCollider = GetComponent<CircleCollider2D>();
            TowerCollider.radius = TowerStats.Range;

        }

        private void Update()
        {

        }

        void OnTriggerStay2D(Collider2D collision)
        {
            // Check if the collision is with a monster and enough time has passed since the last shot
            if (collision.gameObject.CompareTag("Monster") && Time.time >= TowerStats.AttackTimer)
            {
                // Spawn bullet
                Transform targetTransform = collision.transform;
                TowerShoot.SpawnBullet(this.transform, targetTransform, Arrow, this.TowerStats);

                // Update next fire time based on fire rate
                TowerStats.AttackTimer = Time.time + 1f / TowerStats.FireRate;
            }
        }
        #endregion Methods



    }
}
