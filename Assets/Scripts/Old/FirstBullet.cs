using Assets.Scripts.Bullets;
using Assets.Scripts.Dictionaries;
using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Old
{
    public class FirstBullet : MonoBehaviour
    {


        public static BulletSpeed bulletSpeed = new BulletSpeed();


        public BulletInfo BulletInfo = new BulletInfo();
        public void InitializeBullet(BulletInfo bulletInfo)
        {
            BulletInfo = bulletInfo;
            BulletInfo.Speed = bulletSpeed.GetBulletSpeed(5);
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            if (BulletInfo.TargetTranform != null)
            {
                // Calculate direction towards the target
                Vector3 direction = (BulletInfo.TargetTranform.position - transform.position).normalized;
                // Shoot
                transform.Translate(direction * BulletInfo.Speed * Time.deltaTime);
            }
        }
    }

}