using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SingletonStructure
    // Static instance of GameManager which allows it to be accessed by any other script
    private static GameManager instance;

    // Public property to access the instance
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // If instance is null, look for an existing GameManager in the scene
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    // If no existing instance is found, create a new GameObject and attach the GameManager component
                    GameObject singletonObject = new GameObject(typeof(GameManager).Name);
                    instance = singletonObject.AddComponent<GameManager>();

                    // Make the GameManager persistent across scenes
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (instance == null)
        {
            // Set the instance to this instance if it hasn't been set yet
            instance = this;
            // Make this instance persistent across scenes
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // If there is already an instance, destroy this one to enforce the singleton pattern
            Destroy(gameObject);
        }
    }
    
    #endregion

    [SerializeField] private Transform temporaryObjects;
    public Transform TemporaryObjects => temporaryObjects;

    private void Start()
    {
        if (!temporaryObjects)
        {
            Debug.LogError("Temporary objects' transform is not set!");
        }
    }
}
