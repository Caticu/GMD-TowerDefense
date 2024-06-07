using Assets.Scripts.Bullets;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Old;
using UnityEngine.Splines;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterCombat;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using Unity.VisualScripting;
using Assets.Scripts.InterfacesAndImplementations.Bullet;
using UnityEngine.UI;
using Assembly_CSharp;


namespace Assets.Scripts.Monsters
{
    public class Troll : MonoBehaviour
    {
        public FloatingHealthBar healthBar;
        public IMonsterCombat MonsterCombat { get; private set; } = new MonsterCombat();
        public ICheckPointMonsterMovement MonsterMovement { get; private set; } = new CheckPointMonsterMovement();
        public IMonsterStats MonsterStats { get; private set; } = new MonsterStats();
      
        private Transform[] CheckPoints { get;set; }
        

        void OnTriggerEnter2D(Collider2D collision)
        {
            IBullet bullet = collision.GetComponent<IBullet>();
            if (bullet != null)
            {
                // Get the BulletInfo from the bullet
                BulletInfo bulletInfo = bullet.GetBulletInfo();

                // Deal damage to the monster using the BulletInfo
                MonsterCombat.TakeDamage(bulletInfo, MonsterStats);

                // Return the bullet to the pool 
                bullet.ReturnToPool();
            }
        }
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            
        }
        public Troll InitializeWolf(IMonsterCombat monsterCombat, ICheckPointMonsterMovement monsterMovement, IMonsterStats monsterStats, int PathId)
        {
            MonsterCombat = monsterCombat;
            MonsterMovement = monsterMovement;
            MonsterStats = monsterStats;
            MonsterStats.Initialize(100, 30, 20, 50, false, this.name.ToUpper(), 10);
            MonsterStats.SetHealthBar(healthBar);
            CheckPoints = MonsterMovement.FindCheckPoints(PathId);

            return this;
        }


        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            MonsterMovement.MoveTowardsCheckPoint(this.gameObject, MonsterStats.MovementSpeed, CheckPoints);
        }
    }
}
