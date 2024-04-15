using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InterfacesAndImplementations.Tower
{
    public interface ITowerShoot
    {
        

        /// <summary>
        /// Method to spawn a bullet
        /// </summary>
        /// <param name="spawnTransform">The position where the bullet will be instantiated</param>
        /// <param name="target">The position towards which the bullet will move</param>
        /// <param name="bulletPrefab">Bullet prefab</param>
        /// <param name="towerStats">Tower stats</param>
        public void SpawnBullet(Transform spawnTransform, Transform target, GameObject BulletPrefab, ITowerStats towerStats);
    }
}

