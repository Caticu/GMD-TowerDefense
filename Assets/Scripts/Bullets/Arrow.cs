using Assets.Scripts.Dictionaries;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.InterfacesAndImplementations.Bullet;

namespace Assets.Scripts.Bullets
{
    public class Arrow : MonoBehaviour, IBullet
    {
        #region Properties

        public BulletSpeed BulletSpeed = BulletSpeed.Instance;
        public BulletInfo BulletInfo = new BulletInfo();


        #endregion Properties

        #region Methods
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            MoveTowardsTarget();
        }

        public void InitializeBullet(BulletInfo bulletInfo)
        {
            BulletInfo = bulletInfo;
            if(BulletSpeed != null && BulletInfo != null)
            {
                float speed = BulletSpeed.GetBulletSpeed(10);
                // Set the bullet speed in the BulletInfo
                BulletInfo.Speed = speed;
            }
            else
            {
                Debug.LogError("BulletSpeed or BulletInfo is null.");
            }
        }

        public void MoveTowardsTarget()
        {
            if (BulletInfo.TargetTranform != null)
            {
                // Calculate direction towards the target
                Vector3 targetPosition = BulletInfo.TargetTranform.position;

                // Calculate the distance between current position and target position
                float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

                // Calculate the movement speed based on distance and a maximum speed
                float movementSpeed = Mathf.Min(distanceToTarget, BulletInfo.Speed * Time.deltaTime);

                // Lerping (interpolating) towards the target position
                transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed / distanceToTarget);
            }
        }

        public BulletInfo GetBulletInfo()
        {
            return BulletInfo;
        }


        #endregion Methods
    }
}
