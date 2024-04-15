using Assets.Scripts.Bullets;
using Assets.Scripts.Interfaces;
using System;
using Unity.VisualScripting;
using UnityEngine;


namespace Assets.Scripts.Old
{
    public class FirstMonsterScript : MonoBehaviour
    {
        public Transform[] checkPoints;
        public float movementSpeed = 5f;
        private int currentCheckPointIndex = 0;

        // Monster stats properties
        public float Hp { get; set; }
        public float Armor { get; set; }




        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            MoveTowardsCheckPoint();
        }

        private void MoveTowardsCheckPoint()
        {
            if (currentCheckPointIndex < checkPoints.Length)
            {
                Vector3 targetPosition = checkPoints[currentCheckPointIndex].position;
                // Move object
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                // Check if the monster has reached close enough to the target position
                if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
                {
                    //Debug.Log($"Hit checkPoint {checkPoints[currentCheckPointIndex]} + {targetPosition.ToString()}");
                    currentCheckPointIndex = (currentCheckPointIndex + 1) % checkPoints.Length;

                    // Check if it got to the last checkpoint
                    if (currentCheckPointIndex + checkPoints.Length == checkPoints.Length)
                    {
                        enabled = false;
                    }
                }

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                // Get the BulletInfo from the bullet
                BulletInfo bulletInfo = collision.GetComponent<FirstBullet>().BulletInfo;

                // Deal damage to the monster using the BulletInfo
                TakeDamage(bulletInfo);

                // Destroy the bullet after hitting the monster
                Destroy(collision.gameObject);
            }
        }



        public void TakeDamage(BulletInfo bulletInfo)
        {
            float damageTaken = bulletInfo.Damage - Armor; // Apply armor reduction
            Hp -= damageTaken;

            // Log the damage
            Debug.Log($"{this.name.ToUpper()} took {damageTaken} damage from {bulletInfo.TowerName.ToUpper()}"
                + '\n' + $"Hp left = {Hp}");
        }


        public string PrintHp()
        {
            return Hp.ToString();
        }
    }
}