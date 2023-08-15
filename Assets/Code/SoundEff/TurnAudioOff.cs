using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.SoundEffectMangement
{
    public class TurnAudioOff : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (audioSource.isPlaying == false)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
