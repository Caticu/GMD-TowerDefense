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
                GameObject bulletGameObject = GameObject.Instantiate(BulletPrefab, spawnTransform.position, Quaternion.identity);

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
                        TargetTranform = target
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
