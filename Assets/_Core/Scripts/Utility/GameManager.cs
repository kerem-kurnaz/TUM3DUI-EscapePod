using System;
using _Core.Scripts.SelectionAndManipulation;
using UnityEngine;

namespace _Core.Scripts.Utility
{
    public class GameManager : MonoBehaviour
    {
        #region SingletonStructure
        // Static instance of GameManager which allows it to be accessed by any other script
        private static GameManager _instance;

        // Public property to access the instance
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // If instance is null, look for an existing GameManager in the scene
                    _instance = FindObjectOfType<GameManager>();

                    if (_instance == null)
                    {
                        // If no existing instance is found, create a new GameObject and attach the GameManager component
                        GameObject singletonObject = new GameObject(typeof(GameManager).Name);
                        _instance = singletonObject.AddComponent<GameManager>();

                        // Make the GameManager persistent across scenes
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            // Ensure that there is only one instance of GameManager
            if (_instance == null)
            {
                // Set the instance to this instance if it hasn't been set yet
                _instance = this;
                // Make this instance persistent across scenes
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                // If there is already an instance, destroy this one to enforce the singleton pattern
                Destroy(gameObject);
            }
        }
    
        #endregion

        public Action OnArControllerAvailable;
        public Action OnArControllerUnavailable;
        
        [SerializeField] private Transform temporaryObjects;
        public Transform TemporaryObjects => temporaryObjects;

        public Selector Selector { get; private set; }

        [SerializeField] private Transform arCamera;
        public Transform ArCamera => arCamera;
        
        [SerializeField] private Transform mainCameraTransform;
        public Transform MainCameraTransform => mainCameraTransform;
        
        private void Start()
        {
            if (!temporaryObjects)
            {
                Debug.LogError("Temporary objects' transform is not set!");
            }
            if (!arCamera)
            {
                Debug.LogError("AR Camera's transform is not set!");
            }
            if (!mainCameraTransform)
            {
                Debug.LogError("Main camera's transform is not set!");
            }
        }

        public void InvokeArControllerAvailable()
        {
            OnArControllerAvailable?.Invoke();
        }
        
        public void InvokeArControllerUnavailable()
        {
            OnArControllerUnavailable?.Invoke();
        }
        
        public void SetSelector(Selector newSelector)
        {
            Selector = newSelector;
        }
    }
}
