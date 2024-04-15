using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Enums;
using Assets.Scripts;
using Assets.Scripts.Bullets;


namespace Assets.Scripts.Old
{

    public class FirstTowerScript : MonoBehaviour
    {
        // Bullet prefab
        public GameObject bulletPrefab;
        // Damage amount to apply to monsters
        public int damageAmount = 10;
        // Collider of the tower
        private CircleCollider2D towerCollider;
        // Fire rate and variable to count
        public float fireRate = 1.0f;
        private float nextFireTime = 0f;

        CircleCollider2D circleCollider;

        float ArmorPenetration = 1;
        float MagicPenetration = 1;

        // Start is called before the first frame update
        void Start()
        {
            towerCollider = GetComponent<CircleCollider2D>();
            towerCollider.radius = 2;
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// On trigger and stay event to always detect the monster
        /// </summary>
        /// <param name="collision"></param>
        void OnTriggerStay2D(Collider2D collision)
        {
            // Check if the collision is with a monster and enough time has passed since the last shot
            if (collision.gameObject.CompareTag("Monster") && Time.time >= nextFireTime)
            {
                // Spawn bullet
                Transform targetTransform = collision.transform;
                SpawnBullet(targetTransform);

                // Update next fire time based on fire rate
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        /// <summary>
        /// Spawn bullet
        /// </summary>
        /// <param name="target"></param>
        void SpawnBullet(Transform target)
        {
            if (bulletPrefab != null)
            {
                GameObject bulletInstantioation = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                FirstBullet bullet = bulletInstantioation.GetComponent<FirstBullet>();

                // Pass damage amount, tower name, and target transform to the bullet
                if (bullet != null)
                {
                    BulletInfo bulletInfo = new BulletInfo
                    {
                        Damage = damageAmount,
                        DamageType = DamageType.Physical,
                        ArmorPenetration = ArmorPenetration,
                        MagicPenetration = MagicPenetration,
                        TowerName = this.name,
                        TargetTranform = target
                    };

                    // Initialize the bullet with the BulletInfo object
                    bullet.InitializeBullet(bulletInfo);
                }
            }
            else
            {
                Debug.LogError("Error creating the bullet.");
            }
        }
    }
}