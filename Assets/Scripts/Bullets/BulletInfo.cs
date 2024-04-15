using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Bullets
{
    /// <summary>
    /// Bullet information
    /// </summary>
    public class BulletInfo
    {
        /// <summary>
        /// Pure damage
        /// </summary>
        public float Damage { get; set; }
        /// <summary>
        /// Damage type
        /// Physical / Magic
        /// </summary>
        public DamageType DamageType { get; set; }
        /// <summary>
        /// Armor penetration of the bullet
        /// </summary>
        public float ArmorPenetration { get; set; }
        /// <summary>
        /// Magic penetration of the bullet
        /// </summary>
        public float MagicPenetration { get; set; }
        /// <summary>
        /// Tower name that started the bullet
        /// </summary>
        public string TowerName { get; set; }
        /// <summary>
        /// Target of the bullet
        /// </summary>
        public Transform TargetTranform { get; set; }
        /// <summary>
        /// Speed of the bullet
        /// </summary>
        public float Speed { get; set; }

        public  string MyToString()
        {
            return $" '\n' + Bullet: " + '\n' +
                $"Damage : {Damage}" + '\n' +
                $"DamageType : {DamageType}" + '\n' +
                $"ArmorPenetration : {ArmorPenetration}" + '\n' +
                $"MagicPenetration : {MagicPenetration}" + '\n' +
                $"TowerName : {TowerName}" + '\n' +
                $"TargetTranform : {TargetTranform.name}" + '\n' ;
        }
    }
}
