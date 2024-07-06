using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.Utility
{
    public class AudioClipPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PlayAudioClip()
        {
            if (_audioSource)
            {
                _audioSource.Play();
            }
        }
        
        public void StopAudioClip()
        {
            if (_audioSource)
                _audioSource.Stop();
        }
    }
}
