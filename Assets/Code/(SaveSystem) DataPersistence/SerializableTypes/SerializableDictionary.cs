using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.SaveSystem
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValus> : Dictionary<TKey, TValus>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValus> valus = new List<TValus>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            valus.Clear();
            foreach(KeyValuePair<TKey, TValus> pair in this)
            {
                keys.Add(pair.Key);
                valus.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();

            if (keys.Count != valus.Count)
            {
                Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys ("
                    + keys.Count + ") does not match the number of values(" + valus.Count
                    + ") which indicates that something went wrong");

            }

            for(int i = 0; i < keys.Count; i++)
            {
                Add(keys[i], valus[i]);
            }
        }

    }
}
