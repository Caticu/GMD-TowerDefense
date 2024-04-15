using Assets.Scripts.Bullets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Interface to define a monster
    /// This contains hp, armor, mr, and methods for taking dmg
    /// </summary>
    public interface IMonster
    {
        #region Properties
        /// <summary>
        /// Hp of the monster
        /// </summary>
        public float Hp { get; set; }
        /// <summary>
        /// Armor of the monster
        /// </summary>
        public float Armor { get; set; }
        /// <summary>
        /// Magic resist of the monster
        /// </summary>
        public float MagicResist { get; set; }
        /// <summary>
        /// Damage the monster can deal
        /// </summary>
        public float MovementSpeed { get; set; }
        /// <summary>
        /// Is the monster aerial?
        /// </summary>
        public bool IsAerial { get; set; }
       
        /// <summary>
        /// Path id
        /// </summary>
        public int PathId { get; set; }

        /// <summary>
        /// Checkpoint counter
        /// </summary>
        public int CurrentCheckPointIndex { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Method to find checkpoints
        /// </summary>
        /// <returns></returns>
        public Transform[] FindCheckPoints();

        /// <summary>
        /// Method to move towards a checkpoint;
        /// </summary>
        /// <param name="pathId"> id of the path : 1 / 2 / 3</param>
        public void MoveTowardsCheckPoint();

        /// <summary>
        /// Method to take damage
        /// </summary>
        /// <param name="bulletInfo"> info from the bullet</param>
        public void TakeDamage(BulletInfo bulletInfo);

        /// <summary>
        /// Trigger when something enters the collider
        /// </summary>
        /// <param name="collision"></param>
        void OnTriggerEnter2D(Collider2D collision);

        #endregion Methods
    }
}
