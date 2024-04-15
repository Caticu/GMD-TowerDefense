using Assets.Scripts.Bullets;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterCombat;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using Assets.Scripts.InterfacesAndImplementations.Bullet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Monsters
{
    public class Troll : MonoBehaviour
    {
        public IMonsterCombat MonsterCombat { get; private set; }
        public ICheckPointMonsterMovement MonsterMovement { get; private set; }
        public IMonsterStats MonsterStats { get; private set; }

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

                // Destroy the bullet after hitting the monster
                Destroy(collision.gameObject);
            }
        }
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {

        }
        public void InitializeTroll(IMonsterCombat monsterCombat, ICheckPointMonsterMovement monsterMovement, IMonsterStats monsterStats, int PathId)
        {
            MonsterCombat = monsterCombat;
            MonsterMovement = monsterMovement;
            MonsterStats = monsterStats;

            MonsterStats.Hp = 150;
            MonsterStats.Armor = 50;
            MonsterStats.MagicResist = 10;
            MonsterStats.MovementSpeed = 30;
            MonsterStats.IsAerial = false;
            MonsterStats.Name = this.name.ToUpper();

            CheckPoints = MonsterMovement.FindCheckPoints(PathId);

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
