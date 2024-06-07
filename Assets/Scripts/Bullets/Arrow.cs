using Assets.Scripts.Dictionaries;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.InterfacesAndImplementations.Bullet;
using Assets.ObjectPooling;

namespace Assets.Scripts.Bullets
{
    public class Arrow : MonoBehaviour, IBullet
    {
        #region Properties

        public BulletSpeed BulletSpeed = BulletSpeed.Instance;
        public BulletInfo BulletInfo = new BulletInfo();
        private ObjectPool arrowPool;

        #endregion Properties

        #region Methods
        void Start()
        {

        }

        public void SetPool(ObjectPool pool)
        {
            arrowPool = pool;  
        }

        // Update is called once per frame
        void Update()
        {
            if (BulletInfo.TargetTranform != null && BulletInfo.TargetTranform.gameObject.activeInHierarchy)
            {
                MoveTowardsTarget();
            }
            else
            {
                HandleMissingTarget();
            }
        }

        public void InitializeBullet(BulletInfo bulletInfo)
        {
            BulletInfo = bulletInfo;
            if (BulletSpeed != null && BulletInfo != null)
            {
                float speed = BulletSpeed.GetBulletSpeed(10);
                BulletInfo.Speed = speed;
            }
            else
            {
                Debug.LogError("BulletSpeed or BulletInfo is null.");
            }
        }

        public void MoveTowardsTarget()
        {
            if (BulletInfo.TargetTranform != null && BulletInfo.TargetTranform.gameObject.activeInHierarchy)
            {
                Vector3 targetPosition = BulletInfo.TargetTranform.position;
                Vector3 direction = targetPosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);

                // Update position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, BulletInfo.Speed * Time.deltaTime);

                //  rotate to the target
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }
        }

        public void ReturnToPool()
        {
            if (arrowPool != null)
            {
                gameObject.SetActive(false);
                arrowPool.Return(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void HandleMissingTarget()
        {
            if (arrowPool != null)
            {
                gameObject.SetActive(false);
                arrowPool.Return(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public BulletInfo GetBulletInfo()
        {
            return BulletInfo;
        }
        public ObjectPool GetPool()
        {
            return arrowPool;
        }


        #endregion Methods
    }
}
