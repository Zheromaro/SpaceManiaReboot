using UnityEngine;
using SpaceGame.ObjectPooling;

namespace SpaceGame.SoundEffectMangement
{
    [CreateAssetMenu(fileName = "NewSoundEffect",menuName = "Audio/New Sound Effect")]
    public class SoundEffectSO : ScriptableObject
    {
        public static ObjectPool<PoolObject> objectPool;
        [SerializeField] private GameObject Prefab;
        [SerializeField] private int preSpawn;

        #region config

        public AudioClip[] clips;
        public Vector2 volume = new Vector2(0.5f, 0.5f);
        public Vector2 pitch = new Vector2(1, 1);

        [SerializeField] private int playIndex;
        [SerializeField] private SoundClipPlayDrder playOrder;
        #endregion

        public void Prepare()
        {
            objectPool = new ObjectPool<PoolObject>(Prefab, preSpawn);
        }

        public AudioSource Play(AudioSource audioSourceParam = null)
        {
            if(clips.Length == 0)
            {
                Debug.LogWarning("Missing sound clips for" + this.name);
                return null;
            }

            var source = audioSourceParam;
            if(source == null)
            {
                var _obj = objectPool.Pull();
                source = _obj.GetComponent<AudioSource>();
            }

            // set source config
            source.clip = GetAudioClip();
            source.volume = Random.Range(volume.x, volume.y);
            source.pitch = Random.Range(pitch.x, pitch.y);

            source.Play();

            return source;
        }

        private AudioClip GetAudioClip()
        {
            // get current clip
            var clip = clips[playIndex >= clips.Length ? 0 : playIndex];

            // find next clip 
            switch (playOrder)
            {
                case SoundClipPlayDrder.in_order:
                    playIndex = (playIndex + 1) % clips.Length;
                    break;
                case SoundClipPlayDrder.random:
                    playIndex = Random.Range(0, clips.Length);
                    break;
                case SoundClipPlayDrder.reverse:
                    playIndex = (playIndex + clips.Length - 1) % clips.Length;
                    break;

            }

            // return clip
            return clip;
        }

        enum SoundClipPlayDrder
        {
            random,
            in_order,
            reverse
        }

    }
}
