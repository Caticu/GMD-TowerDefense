using Assets.Scripts.Bullets;
using Assets.Scripts.InterfacesAndImplementations.Bullet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.InterfacesAndImplementations.Tower
{
    
    public class TowerShoot : ITowerShoot
    {
        public void SpawnBullet(UnityEngine.Transform spawnTransform, UnityEngine.Transform target, GameObject BulletPrefab, ITowerStats towerStats)
        {
            if (BulletPrefab != null)
            {
                // Calculate the initial rotation to face the target
                Vector3 directionToTarget = target.position - spawnTransform.position;
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                // adjust with -90 = i noticed this is best optionb right now
                Quaternion initialRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); 

                // Instantiate the bullet
                GameObject bulletGameObject = GameObject.Instantiate(BulletPrefab, spawnTransform.position, initialRotation);

                // Check if the instantiated bullet has IBullet component
                IBullet bullet = bulletGameObject.GetComponent<IBullet>();
                if (bullet != null)
                {
                    // Initialize bullet with tower stats and target
                    bullet.InitializeBullet(new BulletInfo
                    {
                        Damage = towerStats.Damage,
                        DamageType = towerStats.DamageType,
                        ArmorPenetration = towerStats.ArmorPenetration,
                        MagicPenetration = towerStats.MagicPenetration,
                        TowerName = towerStats.TowerName,
                        TargetTranform = target,
                        
                    });
                }
                else
                {
                    Debug.LogError("Bullet prefab does not implement IBullet interface.");
                }
            }
            else
            {
                Debug.LogError("Bullet prefab is null.");
            }
        }

    }
}
