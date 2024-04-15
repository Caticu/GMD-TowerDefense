using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Interface to define  a tower
    /// </summary>
    public interface ITower
    {
        #region Properties

        /// <summary>
        /// Range of the tower
        /// </summary>
        public float Range { get; set; }
        /// <summary>
        /// Damage the tower can deal
        /// </summary>
        public float Damage { get; set; }

        /// <summary>
        /// Fire rate of the tower
        /// </summary>
        public float FireRate { get; set; }

        public float AttackTimer { get; set; }
        /// <summary>
        /// Damage type 
        /// Physical or Magic
        /// </summary>
        public DamageType DamageType { get; set; }
        
        /// <summary>
        /// Armor penetration of the tower
        /// </summary>
        public float ArmorPenetration { get; set; }
        /// <summary>
        /// Magic penetration of the tower
        /// </summary>
        public float MagicPenetration { get; set; }
        /// <summary>
        /// A tower has 3 levels and a final upgrade
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Cost to level up a tower to level 1
        /// </summary>
        public int Level1Cost { get; set; }
        /// <summary>
        /// Cost to level up a tower to level 2
        /// </summary>
        public int Level2Cost { get; set; }
        /// <summary>
        /// Cost to level up a tower to level 3
        /// </summary>
        public int Level3Cost { get; set; }
        /// <summary>
        /// Cost to level up a tower to level 4
        /// </summary>
        public int Level4Cost { get; set; }


        #endregion Properties

        #region Methods

        /// <summary>
        /// Method to lvl up a tower
        /// </summary>
        /// <param name="gold"></param>
        public void LevelUpTower(int gold);
        /// <summary>
        /// On trigger stay for towers to detect monsters
        /// This method also deals with fire rate;
        /// </summary>
        /// <param name="collision"></param>
        void OnTriggerStay2D(Collider2D collision);
        /// <summary>
        /// Spawn bullet method
        /// </summary>
        /// <param name="target"></param>
        void SpawnBullet(Transform target);

        #endregion Methods
    }
}
