using Assets.Scripts.Bullets;
using Assets.Scripts.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.InterfacesAndImplementations.Bullet
{
    /// <summary>
    /// Interface to define basic elements of a bullet
    /// </summary>
    public interface IBullet
    {
        #region Properties
        #endregion Properties
        #region Methods
        /// <summary>
        /// Create the bullet
        /// </summary>
        /// <param name="bulletInfo">Bullet Information</param>
        public void InitializeBullet(BulletInfo bulletInfo);
        /// <summary>
        /// Move towards the target
        /// </summary>
        public void MoveTowardsTarget();

        public BulletInfo GetBulletInfo();
        void ReturnToPool();
        #endregion Methods
    }
}
