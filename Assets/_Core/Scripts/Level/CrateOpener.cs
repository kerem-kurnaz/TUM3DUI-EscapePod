using System;
using _Core.Scripts.SelectionAndManipulation;
using DG.Tweening;
using UnityEngine;

namespace _Core.Scripts.Level
{
    public class CrateOpener : MonoBehaviour
    {
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;
        
        [SerializeField] private Transform crateHead;
        
        [SerializeField] private float duration = 1.0f; 
        [SerializeField] private Ease rotationEase = Ease.OutQuad;
        
        private Pressable _pressable;
        private AudioSource _audioSource;

        private void Awake()
        {
            _pressable = GetComponentInChildren<Pressable>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _pressable.OnPress += OpenCloseCrate;
        }

        private void OnDisable()
        {
            _pressable.OnPress -= OpenCloseCrate;
        }

        private void OpenCloseCrate(bool isActive)
        {
            if (isActive)
            {
                OpenCrate();
            }
            else
            {
                CloseCrate();
            }
        }

        private void OpenCrate()
        {
            crateHead.DOLocalRotate(new Vector3(-85, 0, 180), duration).SetEase(rotationEase);
            _audioSource.Stop();
            _audioSource.PlayOneShot(openSound);
        }
        
        private void CloseCrate()
        {
            crateHead.DOLocalRotate(new Vector3(0, 0, 180), duration).SetEase(rotationEase);
            _audioSource.Stop();
            _audioSource.PlayOneShot(closeSound);
        }
    }
}
