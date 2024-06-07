using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ObjectPooling
{
    [CreateAssetMenu(fileName = "NewObjectPool", menuName = "Pooling/Object Pool")]
    public class ObjectPool : ScriptableObject
    {
        public GameObject prefab;
        private Queue<GameObject> objects = new Queue<GameObject>();

        public void Initialize(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objects.Enqueue(obj);
            }
        }

        public GameObject Get()
        {
            if (objects.Count > 0)
            {
                GameObject obj = objects.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                // create new objects if needed
                GameObject obj = Instantiate(prefab);
                obj.SetActive(true);
                return obj;
            }
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            objects.Enqueue(obj);
        }

        public int AvailableCount
        {
            get { return objects.Count; }
        }
    }

}
