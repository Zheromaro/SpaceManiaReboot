using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.ObjectPooling
{
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private System.Action<T> pullObject;
        private System.Action<T> puchObject;
        private Stack<T> pooledObjects = new Stack<T>();
        private GameObject prefab;
        public int pooledCount 
        {
            get { return pooledObjects.Count; }
        }

        public ObjectPool(GameObject pooledObject, Action<T> pullObject, Action<T> puchObject, int numToSpawn = 0)
        {
            prefab = pooledObject;
            this.puchObject = puchObject;
            this.pullObject = pullObject;
            Spawn(numToSpawn);
        }

        public ObjectPool(GameObject pooledObject, int numToSpawn = 0)
        {
            prefab = pooledObject;
            Spawn(numToSpawn);
        }

        private void Spawn(int number)
        {
            T t;

            for (int i = 0; i < number; i++)
            {
                t = GameObject.Instantiate(prefab).GetComponent<T>();
                pooledObjects.Push(t);
                t.gameObject.SetActive(false);
            }
        }

        public T Pull()
        {
            T t;
            if (pooledCount > 0)
                t = pooledObjects.Pop();
            else
                t = GameObject.Instantiate(prefab).GetComponent<T>();

            t.gameObject.SetActive(true); // ensure the object is on
            t.Initialize(Puch);

            //allow default behavior and turning object back on
            pullObject?.Invoke(t);

            return t;
        }

        public T Pull(Vector3 position)
        {
            T t = Pull();
            t.transform.position = position;
            return t;
        }

        public T Pull(Vector3 position, Quaternion rotation)
        {
            T t = Pull();
            t.transform.position = position;
            t.transform.rotation = rotation;
            return t;
        }

        public void Puch(T t)
        {
            pooledObjects.Push(t);

            //create default behavior to turn off objects
            puchObject?.Invoke(t);

            t.gameObject.SetActive(false);
        }

    }
}
