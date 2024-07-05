using DG.Tweening;
using UnityEngine;

namespace _Core.Scripts.Level
{
    public class CrateOpener : MonoBehaviour
    {
        [SerializeField] private Transform crateHead;
        
        [SerializeField] private float duration = 1.0f; 
        [SerializeField] private Ease rotationEase = Ease.OutQuad;

        private void OpenCrate()
        {
            crateHead.DOLocalRotate(new Vector3(-85, 0, 180), duration).SetEase(rotationEase);
        }
        
        private void CloseCrate()
        {
            crateHead.DOLocalRotate(new Vector3(0, 0, 180), duration).SetEase(rotationEase);
        }
    }
}
