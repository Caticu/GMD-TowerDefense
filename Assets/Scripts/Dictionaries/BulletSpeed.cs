using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dictionaries
{
    public class BulletSpeed
    {
        private static BulletSpeed instance;
        /// <summary>
        /// Dictionary for bullet speed
        /// </summary>
        public Dictionary<int, float> BulletSpeeds = new Dictionary<int, float>
        {
            {1, 10},
            {2, 20},
            {3, 30},
            {4, 40},
            {5, 50},
            {6, 60},
            {7, 70},
            {8, 80},
            {9, 90},
            {10, 100},
            {11, 150 },
            {12, 200 },
            {13,250 }
        };

        public float GetBulletSpeed(int bulletId)
        {
            if (BulletSpeeds.TryGetValue(bulletId, out var speed))
            {
                return speed;
            }
            else
            {
                return 0f;
            }
        }

        public static BulletSpeed Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BulletSpeed();
                }
                return instance;
            }
        }

    }
}
