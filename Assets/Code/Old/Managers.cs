using UnityEngine;
using SpaceGame.Stats.Units;

namespace SpaceGame.Old
{
    public class Managers : MonoBehaviour
    {
        public static Managers managers { get; private set; }

        private void Awake()
        {
            if (managers != null && managers != this)
            {
                Destroy(gameObject);
            }
            else
            {
                managers = this;
            }

            DontDestroyOnLoad(gameObject);
        }

    }
}
