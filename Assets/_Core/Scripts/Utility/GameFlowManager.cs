using System;
using System.Collections;
using _Core.Scripts.Level;
using NavKeypad;
using UnityEngine;

namespace _Core.Scripts.Utility
{
    public class GameFlowManager : MonoBehaviour
    {
        #region SingletonStructure
        // Static instance of GameManager which allows it to be accessed by any other script
        private static GameFlowManager _instance;
    
        // Public property to access the instance
        public static GameFlowManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // If instance is null, look for an existing GameManager in the scene
                    _instance = FindObjectOfType<GameFlowManager>();
    
                    if (_instance == null)
                    {
                        // If no existing instance is found, create a new GameObject and attach the GameManager component
                        GameObject singletonObject = new GameObject(typeof(GameFlowManager).Name);
                        _instance = singletonObject.AddComponent<GameFlowManager>();
    
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

        public static Action OnBeforeStartGame;
        public static Action OnStartGame;
        public static Action OnOxygenGameEnd;
        public static Action OnKeypadGameEnd;
        
        [SerializeField] private Keypad _keypad;

        private int _oxygenTankCount = 0;
        private void Start()
        {
            _keypad.enabled = false;
            StartCoroutine(StartGame());
        }

        private void OnDisable()
        {
            OnBeforeStartGame = null;
            OnStartGame = null;
            OnOxygenGameEnd = null;
        }

        private IEnumerator StartGame()
        {
            OnBeforeStartGame?.Invoke();
            
            yield return new WaitForSeconds(3f);
            
            OnStartGame?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OxygenGameEnd();
            }
        }

        public void InsertOxygenTank()
        {
            _oxygenTankCount++;
            if (_oxygenTankCount >= 2)
            {
                OxygenGameEnd();
            }
        }

        private void OxygenGameEnd()
        {
            OnOxygenGameEnd?.Invoke();
            _keypad.enabled = true;
        }
    }
}
