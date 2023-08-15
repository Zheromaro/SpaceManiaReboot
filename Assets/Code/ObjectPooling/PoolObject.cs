using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.ObjectPooling
{
    public class PoolObject : MonoBehaviour, IPoolable<PoolObject>
    {
        private Action<PoolObject> returnToPool;

        private void OnDisable()
        {
            ReturnToPool();
        }

        public void Initialize(Action<PoolObject> returnAction)
        {
            //cache referance to return action
            this.returnToPool = returnAction;
        }

        public void ReturnToPool()
        {
            //invoke and return this object to pool
            returnToPool?.Invoke(this);
        }
    }
}
