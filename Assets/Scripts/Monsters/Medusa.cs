using Assembly_CSharp;
using Assets.Scripts.Bullets;
using Assets.Scripts.InterfacesAndImplementations.Bullet;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterCombat;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Monsters
{
    public class Medusa : MonoBehaviour
    {
        public FloatingHealthBar healthBar;
        public IMonsterCombat MonsterCombat { get; private set; } = new MonsterCombat();
        public ICheckPointMonsterMovement MonsterMovement { get; private set; } = new CheckPointMonsterMovement();
        public IMonsterStats MonsterStats { get; private set; } = new MonsterStats();

        private Transform[] CheckPoints;


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

        void Start()
        {
            if (healthBar == null)
            {
                Debug.LogError("Health slider not assigned.");
            }
        }
        public Medusa InitializeSnake(IMonsterCombat monsterCombat, ICheckPointMonsterMovement monsterMovement, IMonsterStats monsterStats, int PathId)
        {
            MonsterCombat = monsterCombat;
            MonsterMovement = monsterMovement;
            MonsterStats = monsterStats;
            MonsterStats.Initialize(120, 10, 10, 30, false, this.name.ToUpper(), 10);
            CheckPoints = MonsterMovement.FindCheckPoints(PathId);
            MonsterStats.SetHealthBar(healthBar);

            return this;
        }

        void Update()
        {
            MonsterMovement.MoveTowardsCheckPoint(this.gameObject, MonsterStats.MovementSpeed, CheckPoints);
        }  
    }
}
