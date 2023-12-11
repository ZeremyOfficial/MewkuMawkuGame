using UnityEngine;

public class DontDestroyTMP : MonoBehaviour
{
    private static DontDestroyTMP instance;

    private void Awake()
    {
        if (instance == null)
        {
            // If no instance exists, make this instance the singleton
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }
}