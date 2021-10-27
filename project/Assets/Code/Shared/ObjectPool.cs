using System.Collections.Generic;
using UnityEngine;

namespace MAG.Utils
{
    public class ObjectPool
    {
        private readonly Queue<GameObject> pool;

        public ObjectPool(GameObject prefab, int poolSize, Transform container)
        {
            pool = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject go = MonoBehaviour.Instantiate(prefab, container);
                pool.Enqueue(go);
                go.SetActive(false);
            }
        }

        public GameObject SpawnObject(Vector3 position, Quaternion rotation)
        {
            GameObject go = pool.Dequeue();
            go.SetActive(true);
            go.transform.position = position;
            go.transform.rotation = rotation;

            pool.Enqueue(go);

            return go;
        }

        public GameObject SpawnObject(Vector3 position)
        {
            return SpawnObject(position, Quaternion.identity);
        }
    }
}
